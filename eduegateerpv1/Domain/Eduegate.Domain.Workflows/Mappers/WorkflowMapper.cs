using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
                foreach (var conditions in rule.WorkflowRuleConditions)
                {
                    var approvers = new List<KeyValueDTO>();

                    foreach (var approver in conditions.WorkflowRuleApprovers)
                    {
                        approvers.Add(new KeyValueDTO()
                        {
                            Key = approver.EmployeeID.ToString(),
                            Value = approver.Employee.EmployeeCode + "-" + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName
                        });
                    }

                    approvalConditions.Add(new ApprovalConditionDTO()
                    {
                        Condition = conditions.ConditionID.HasValue ? new KeyValueDTO()
                        {
                            Key = conditions.ConditionID.ToString(),
                            Value = conditions.Condition.ConditionName
                        } : new KeyValueDTO(),
                        Approvers = approvers,
                        WorkflowRuleConditionIID = conditions.WorkflowRuleConditionIID
                    });
                }

                workflowRules.Add(new WorkflowRulesDTO()
                {
                    WorkflowRuleIID = rule.WorkflowRuleIID,
                    Condition = rule.Condition != null ? new KeyValueDTO()
                    {
                        Key = rule.Condition?.WorkflowConditionID.ToString(),
                        Value = rule.Condition?.ConditionName
                    } : new KeyValueDTO(),
                    Value1 = rule.Value1,
                    Value2 = rule.Value2,
                    Value3 = rule.Value3,
                    ApprovalConditions = approvalConditions
                });
            }

            //var workflowField = dbContext.WorkflowFileds
            //    .Where(a => a.WorkflowFieldID == entity.WorkflowApplyFieldID).FirstOrDefault();

            //dto.WorkflowField = new KeyValueDTO()
            //{
            //    Key = entity.WorkflowApplyFieldID.ToString(),
            //    Value = workflowField.ColumnName
            //};

            var workflowDTO = new WorkflowDTO()
            {
                WorkflowDescription = entity.WokflowName,
                WorkflowTypeID = entity.WorkflowTypeID,
                WorkflowIID = entity.WorkflowIID,
                WorkflowType = entity.WorkflowTypeID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.WorkflowTypeID.ToString(),
                    Value = entity.WorkflowType.WorkflowTypeName
                } : new KeyValueDTO(),
                WorkflowField = entity.WorkflowApplyFieldID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.WorkflowApplyFieldID.ToString(),
                    Value = entity.WorkflowFiled.ColumnName
                } : new KeyValueDTO(),
                WorkflowRules = workflowRules,
            };

            return workflowDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as WorkflowDTO;

            if (toDto.WorkflowDescription == null)
            {
                throw new Exception("Select Description Properlly!");
            }
            if (toDto.WorkflowType.Key == null)
            {
                throw new Exception("Select Type Properlly!");
            }
            if (toDto.WorkflowField.Key == null)
            {
                throw new Exception("Select Apply Field Properlly!");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Eduegate.Domain.Entity.Models.Workflows.Workflow()
            {
                WokflowName = toDto.WorkflowDescription,
                WorkflowTypeID = int.Parse(toDto.WorkflowType.Key),
                WorkflowIID = toDto.WorkflowIID,
                WorkflowApplyFieldID = int.Parse(toDto.WorkflowField.Key),
            };

            foreach (var rule in toDto.WorkflowRules)
            {
                var conditionID = string.IsNullOrEmpty(rule.Condition.Key) ? (byte?)null : byte.Parse(rule.Condition.Key);

                if (!conditionID.HasValue) continue;

                var ruleEntity = (new WorkflowRule()
                {
                    WorkflowRuleIID = rule.WorkflowRuleIID,
                    ConditionID = conditionID,
                    Value1 = rule.Value1,
                    Value2 = rule.Value2,
                    Value3 = rule.Value3,
                    WorkflowID = toDto.WorkflowIID,
                });

                ruleEntity.WorkflowRuleConditions = new List<WorkflowRuleCondition>();

                if (rule.ApprovalConditions != null)
                {
                    foreach (var conditions in rule.ApprovalConditions)
                    {
                        if (conditions.Condition.Key.IsNullOrEmpty()) continue;

                        var app = new List<WorkflowRuleApprover>();

                        foreach (var approver in conditions.Approvers)
                        {
                            app.Add(new WorkflowRuleApprover()
                            {
                                EmployeeID = long.Parse(approver.Key),
                            });
                        }

                        ruleEntity.WorkflowRuleConditions.Add(new WorkflowRuleCondition()
                        {
                            WorkflowRuleConditionIID = conditions.WorkflowRuleConditionIID,
                            WorkflowRuleID = rule.WorkflowRuleIID,
                            ConditionID = int.Parse(conditions.Condition.Key),
                            WorkflowRuleApprovers = app,
                        });
                    }
                }

                entity.WorkflowRules.Add(ruleEntity);
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var existingIIDs = entity.WorkflowRules.Select(a => a.WorkflowRuleIID).ToList();

                //Delete contacts
                var rules = dbContext.WorkflowRules
                    .Include(i => i.WorkflowRuleConditions)
                    .ThenInclude(i => i.WorkflowRuleApprovers)
                    .Where(x => x.WorkflowID == entity.WorkflowIID && !existingIIDs.Contains(x.WorkflowRuleIID))
                    .AsNoTracking()
                    .ToList();

                if (rules.IsNotNull())
                {
                    dbContext.WorkflowRules.RemoveRange(rules);
                }

                dbContext.Workflows.Add(entity);

                if (entity.WorkflowIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var ruleEntity in entity.WorkflowRules)
                    {
                        if (ruleEntity.WorkflowRuleIID == 0)
                        {
                            dbContext.Entry(ruleEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(ruleEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        foreach (var approver in ruleEntity.WorkflowRuleConditions)
                        {
                            if (approver.WorkflowRuleConditionIID == 0)
                            {
                                dbContext.Entry(approver).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(approver).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            var apprvs = dbContext.WorkflowRuleApprovers
                                .AsNoTracking()
                                .Where(x => x.WorkflowRuleConditionID == approver.WorkflowRuleConditionIID).ToList();

                            if (apprvs.IsNotNull())
                                dbContext.WorkflowRuleApprovers.RemoveRange(apprvs);

                            foreach (var app in approver.WorkflowRuleApprovers)
                            {
                                if (app.WorkflowRuleApproverIID == 0)
                                {
                                    dbContext.Entry(app).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }
                        }

                        //first deleted child approvers and then rule conditions
                        var existingRuleConditionIIDs = ruleEntity.WorkflowRuleConditions.Select(a => a.WorkflowRuleConditionIID).ToList();

                        var ruleArppovers = dbContext.WorkflowRuleApprovers
                           .Include(i => i.WorkflowRuleCondition)
                           .AsNoTracking()
                           .Where(x => !existingRuleConditionIIDs.Contains(x.WorkflowRuleConditionID) &&
                           x.WorkflowRuleCondition != null && x.WorkflowRuleCondition.WorkflowRuleID == ruleEntity.WorkflowRuleIID)
                           .ToList();

                        if (ruleArppovers.Count > 0)
                        {
                            dbContext.WorkflowRuleApprovers.RemoveRange(ruleArppovers);
                        }

                        var ruleConditions = dbContext.WorkflowRuleConditions
                            //.Include(b=> b.WorkflowRuleApprovers)
                            .Where(x => x.WorkflowRuleID == ruleEntity.WorkflowRuleIID
                                && !existingRuleConditionIIDs.Contains(x.WorkflowRuleConditionIID))
                            .AsNoTracking()
                            .ToList();

                        if (ruleConditions.Count > 0)
                        {
                            dbContext.WorkflowRuleConditions.RemoveRange(ruleConditions);
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.WorkflowIID));
        }

    }
}