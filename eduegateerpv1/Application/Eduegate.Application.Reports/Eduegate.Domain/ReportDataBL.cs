using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
//using Eduegate.Domain.ReportExecutionProxy;
using Eduegate.Domain.ReportExecutionWebProxy;
using System.Web.Services.Protocols;
using Eduegate.Services.Contracts.Commons;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain
{
    public class ReportDataBL
    {
        /// <summary>
        /// Exports a Reporting Service Report to the specified format using Windows Communication Foundation (WCF) endpoint configuration specified.
        /// </summary>
        public byte[] Export(ReportDTO reportContract)
        {
            #region Report Generation
            ReportExecutionService rs = new ReportExecutionService();

            var systemParameters = reportContract.SystemParameters;
            var reportParameters = reportContract.ReportParameters;

            rs.Credentials = new System.Net.NetworkCredential(systemParameters.Find(p => p.ParameterName == "ReportServerDomainUser").ParameterValue, systemParameters.Find(p => p.ParameterName == "ReportServerDomainPassword").ParameterValue);
            rs.Url = systemParameters.Find(p => p.ParameterName == "ReportExecutionServiceURL").ParameterValue;
            //"http://13.80.70.106:8085/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = systemParameters.Find(p => p.ParameterName == "ReportPath").ParameterValue;// "/WB.Inventory.Reports/TransactionPreview";
            string format = systemParameters.Find(p => p.ParameterName == "FileFormat").ParameterValue;// "PDF";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";


            // Create report parameters if dto has it
            ParameterValue[] ssrsParameters = null;
            if (reportParameters.IsNotNull() && reportParameters.Count > 0)
            {
                ssrsParameters = new ParameterValue[reportParameters.Count];
                for (int reportParamCounter = 0; reportParamCounter < reportParameters.Count; reportParamCounter++)
                {
                    ssrsParameters[reportParamCounter] = new ParameterValue();
                    ssrsParameters[reportParamCounter].Name = reportParameters[reportParamCounter].ParameterName;
                    ssrsParameters[reportParamCounter].Value= reportParameters[reportParamCounter].ParameterValue;
                }
            }

            DataSourceCredentials[] credentials = null;
            string showHideToggle = null;
            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            ParameterValue[] reportHistoryParameters = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            // Set report parameters
            if (ssrsParameters.IsNotNull() && ssrsParameters.Length > 0)
            {
                rs.SetExecutionParameters(ssrsParameters, "en-us");
            }
            //string SessionId = rs.ExecutionHeaderValue.ExecutionID;
            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException e)
            {
                throw e;
            }
           
            #endregion
            return result; 
        }

        /// <summary>
        /// Returns the binding to use, eliminates the app.config
        /// </summary>
        /// <returns></returns>
        internal static Binding GetBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = 0;
            binding.MaxReceivedMessageSize = 5242880;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.TextEncoding = System.Text.Encoding.UTF8;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TransferMode = TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;
            binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
            {
                MaxArrayLength = 2147483647,
                MaxBytesPerRead = 2147483647,
                MaxDepth = 2147483647,
                MaxNameTableCharCount = 2147483647,
                MaxStringContentLength = 2147483647
            };
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            return binding;
        }

        /// <summary>
        /// Returns the binding to use, eliminates the app.config
        /// </summary>
        /// <returns></returns>
        internal static Binding GetDefaultBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = 0;
            binding.MaxReceivedMessageSize = 5242880;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.TextEncoding = System.Text.Encoding.UTF8;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TransferMode = TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;
            binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
            {
                MaxArrayLength = 2147483647,
                MaxBytesPerRead = 2147483647,
                MaxDepth = 2147483647,
                MaxNameTableCharCount = 2147483647,
                MaxStringContentLength = 2147483647
            };
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            return binding;
        }



    }
}
