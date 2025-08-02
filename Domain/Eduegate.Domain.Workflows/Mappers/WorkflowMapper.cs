using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Eduegate.Domain.Workflows.Mappers
{
    public class WorkflowMapper : DTOEntityDynamicMapper
    {
        public static WorkflowMapper Mapper(CallContext context)
        {
            var mapper = new WorkflowMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<WorkflowDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private WorkflowDTO ToDTO(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Workflows.Where(a => a.WorkflowIID == IID)
                  .Include(i => i.WorkflowType)
                  .Include(i => i.WorkflowFiled)

                  .Include(i => i.WorkflowRules)
                  .ThenInclude(i => i.Condition)

                  .Include(i => i.WorkflowRules)
                  .ThenInclude(d => d.WorkflowRuleConditions)
                  .ThenInclude(i => i.Condition)

                  .Include(i => i.WorkflowRules)
                  .ThenInclude(d => d.WorkflowRuleConditions)
                  .ThenInclude(i => i.WorkflowRuleApprovers)
                  .ThenInclude(i => i.Employee)

                  .AsNoTracking()
                  .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private WorkflowDTO ToDTO(Workflow entity)
        {
            var workflowRules = new List<WorkflowRulesDTO>();
            foreach (var rule in entity.WorkflowRules)
            {
                var approvalConditions = new List<ApprovalConditionDTO>();
                foreach (var condition in rule.WorkflowRuleConditions)
                {
                    var approvers = new List<KeyValueDTO>();
                    foreach (var approver in condition.WorkflowRuleApprovers)
                    {
                        approvers.Add(new KeyValueDTO()
                        {
                            Key = approver.EmployeeID.ToString(),
                            Value = approver.Employee.EmployeeCode + " - " + approver.Employee.FirstName + " " + (string.IsNullOrEmpty(approver.Employee.MiddleName) ? "" : approver.Employee.MiddleName + " ") + approver.Employee.LastName
                        });
                    }

                    approvalConditions.Add(new ApprovalConditionDTO()
                    {
                        WorkflowRuleConditionIID = condition.WorkflowRuleConditionIID,
                        WorkflowRuleID = condition.WorkflowRuleID,
                        ConditionID = condition.ConditionID,
                        Condition = condition.ConditionID.HasValue ? new KeyValueDTO()
                        {
                            Key = condition.ConditionID?.ToString(),
                            Value = condition.Condition?.ConditionName
                        } : new KeyValueDTO(),
                        Approvers = approvers,
                    });
                }

                workflowRules.Add(new WorkflowRulesDTO()
                {
                    WorkflowRuleIID = rule.WorkflowRuleIID,
                    WorkflowID = rule.WorkflowID,
                    ConditionID = rule.ConditionID,
                    Condition = rule.ConditionID.HasValue ? new KeyValueDTO()
                    {
                        Key = rule.ConditionID?.ToString(),
                        Value = rule.Condition?.ConditionName
                    } : new KeyValueDTO(),
                    Value1 = rule.Value1,
                    Value2 = rule.Value2,
                    Value3 = rule.Value3,
                    ApprovalConditions = approvalConditions
                });
            }

            var workflowDTO = new WorkflowDTO()
            {
                WorkflowIID = entity.WorkflowIID,
                WorkflowDescription = entity.WokflowName,
                WorkflowTypeID = entity.WorkflowTypeID,
                WorkflowApplyFieldID = entity.WorkflowApplyFieldID,
                LinkedEntityTypeID = entity.LinkedEntityTypeID,
                WorkflowType = entity.WorkflowTypeID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.WorkflowTypeID?.ToString(),
                    Value = entity.WorkflowType?.WorkflowTypeName
                } : new KeyValueDTO(),
                WorkflowField = entity.WorkflowApplyFieldID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.WorkflowApplyFieldID?.ToString(),
                    Value = entity.WorkflowFiled?.ColumnName
                } : new KeyValueDTO(),
                WorkflowRules = workflowRules,
            };

            return workflowDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as WorkflowDTO;

            if (string.IsNullOrEmpty(toDto.WorkflowDescription))
            {
                throw new Exception("Select Description Properlly!");
            }
            if (!toDto.WorkflowTypeID.HasValue)
            {
                throw new Exception("Select Type Properlly!");
            }
            if (!toDto.WorkflowApplyFieldID.HasValue)
            {
                throw new Exception("Select Apply Field Properlly!");
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //convert the dto to entity and pass to the repository.
                var entity = new Workflow()
                {
                    WorkflowIID = toDto.WorkflowIID,
                    WokflowName = toDto.WorkflowDescription,
                    WorkflowTypeID = toDto.WorkflowTypeID,
                    LinkedEntityTypeID = toDto.LinkedEntityTypeID,
                    WorkflowApplyFieldID = toDto.WorkflowApplyFieldID,
                };

                //Fetch all rule IDs from DTO.
                var ruleIIDs = toDto.WorkflowRules
                    .Select(a => a.WorkflowRuleIID).ToList();

                //Retrieve all rules if they are not available in the DTO, including conditions and approvers.
                var ruleEntities = dbContext.WorkflowRules.Where(x =>
                    x.WorkflowID == entity.WorkflowIID &&
                    !ruleIIDs.Contains(x.WorkflowRuleIID))
                    .Include(i => i.WorkflowRuleConditions).ThenInclude(i => i.WorkflowRuleApprovers)
                    .AsNoTracking().ToList();

                //Check the count of ruleEntities to be deleted
                if (ruleEntities.Any())
                {
                    //Fetch all conditions under each rule.
                    var allConditions = ruleEntities
                        .SelectMany(r => r.WorkflowRuleConditions)
                        .ToList();

                    //Fetch all approvers from all conditions.
                    var allApprovers = allConditions
                        .SelectMany(c => c.WorkflowRuleApprovers)
                        .ToList();

                    //Check the availability of the approvers list and remove it.
                    if (allApprovers.Any())
                    {
                        dbContext.WorkflowRuleApprovers.RemoveRange(allApprovers);
                    }

                    //Then check the availability of the condition list and remove it.
                    if (allConditions.Any())
                    {
                        dbContext.WorkflowRuleConditions.RemoveRange(allConditions);
                    }

                    //Finally, remove the rules.
                    dbContext.WorkflowRules.RemoveRange(ruleEntities);
                }

                entity.WorkflowRules = new List<WorkflowRule>();
                foreach (var rule in toDto.WorkflowRules)
                {
                    //If the rules are not removed by the user, then execute the following code.
                    if (!ruleEntities.Any())
                    {
                        //Fetch all condition IDs from DTO.
                        var conditionIIDs = rule.ApprovalConditions
                            .Select(a => a.WorkflowRuleConditionIID).ToList();

                        //Fetch all rule conditions if they are not present in the DTO, including approvers.
                        var conditionEntities = dbContext.WorkflowRuleConditions.Where(x =>
                            x.WorkflowRuleID == rule.WorkflowRuleIID &&
                            !conditionIIDs.Contains(x.WorkflowRuleConditionIID))
                            .Include(i => i.WorkflowRuleApprovers)
                            .AsNoTracking().ToList();

                        //Check the count of conditionEntities to be deleted
                        if (conditionEntities.Any())
                        {
                            //Fetch all approvers under each conditon.
                            var approverEntities = conditionEntities
                            .SelectMany(c => c.WorkflowRuleApprovers)
                            .ToList();

                            //Then check the availability of the approvers list and remove it.
                            if (approverEntities.Any())
                            {
                                dbContext.WorkflowRuleApprovers.RemoveRange(approverEntities);
                            }

                            //Finally, remove the rule conditons.
                            dbContext.WorkflowRuleConditions.RemoveRange(conditionEntities);
                        }
                    }

                    var ruleConditions = new List<WorkflowRuleCondition>();
                    foreach (var condition in rule.ApprovalConditions)
                    {
                        var ruleApprovers = new List<WorkflowRuleApprover>();
                        foreach (var approver in condition.Approvers)
                        {
                            ruleApprovers.Add(new WorkflowRuleApprover()
                            {
                                WorkflowRuleApproverIID = 0,
                                EmployeeID = long.Parse(approver.Key),
                                CreatedBy = (int)_context.LoginID,
                                UpdatedBy = dto.UpdatedBy,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = dto.UpdatedDate,
                            });
                        }

                        ruleConditions.Add(new WorkflowRuleCondition()
                        {
                            WorkflowRuleConditionIID = condition.WorkflowRuleConditionIID,
                            WorkflowRuleID = condition.WorkflowRuleID,
                            ConditionID = condition.ConditionID,
                            WorkflowRuleApprovers = ruleApprovers,
                        });
                    }

                    entity.WorkflowRules.Add(new WorkflowRule()
                    {
                        WorkflowRuleIID = rule.WorkflowRuleIID,
                        WorkflowID = rule.WorkflowID,
                        ConditionID = rule.ConditionID,
                        Value1 = rule.Value1,
                        Value2 = rule.Value2,
                        Value3 = rule.Value3,
                        WorkflowRuleConditions = ruleConditions,
                    });
                }

                dbContext.Workflows.Add(entity);

                if (entity.WorkflowIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    if (entity.WorkflowRules.Count > 0)
                    {
                        foreach (var rule in entity.WorkflowRules)
                        {
                            foreach (var condition in rule.WorkflowRuleConditions)
                            {
                                //Fetch all rule approvers to be deleted.
                                var apprvrs = dbContext.WorkflowRuleApprovers
                                    .Where(x => x.WorkflowRuleConditionID == condition.WorkflowRuleConditionIID)
                                    .AsNoTracking()
                                    .ToList();

                                //Then check the availability of the rule approvers list and remove it.
                                if (apprvrs.Any())
                                {
                                    dbContext.WorkflowRuleApprovers.RemoveRange(apprvrs);
                                }

                                foreach (var app in condition.WorkflowRuleApprovers)
                                {
                                    if (app.WorkflowRuleApproverIID == 0)
                                    {
                                        dbContext.Entry(app).State = EntityState.Added;
                                    }
                                }

                                if (condition.WorkflowRuleConditionIID == 0)
                                {
                                    dbContext.Entry(condition).State = EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(condition).State = EntityState.Modified;
                                }
                            }

                            if (rule.WorkflowRuleIID == 0)
                            {
                                dbContext.Entry(rule).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(rule).State = EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.WorkflowIID));
            }
        }

    }
}