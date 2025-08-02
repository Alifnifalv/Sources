<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eduegate.Application.Reports._Default" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/font-awesome.min.css") %>"/> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap.min.css") %>"/> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/datetimepicker.css") %>"/> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/report.css") %>"/> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/select2V4.min.css") %>"/> 
<link rel="stylesheet" href="<%=ResolveUrl("~/Content/jquery-ui.css") %>"/>

<script src="<%=ResolveUrl("~/scripts/jquery-3.1.1.min.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/scripts/jquery-ui.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/scripts/moment.min.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/scripts/angular.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/scripts/select2.min.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/app/Common/utility.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/app/Controller/Reports/ReportController.js") %>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/scripts/print.min.js") %>" type="text/javascript"></script>

    <form id="form1" runat="server">
        <%--btnPrint</form>--%>
        <div  class="panel panel-default" style="margin:5px 5px 5px 5px">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" href="#collapse1">
                        <span class="glyphicon glyphicon-collapse-down"> </span>
                        <asp:Label ID="lblParameterHeading" runat="server" Text="Parameters"></asp:Label>
                     </a>
                </h4>
            </div>
            <div id="collapse1" class="panel-collapse in">
              <table class="table table-striped">
                    <asp:Repeater ID="reportParameterPanel" runat="server" OnItemDataBound="reportParameterPanel_ItemDataBound">       
                        <HeaderTemplate>
        
                        </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                            <td>
                            <asp:HiddenField ID="HiddenField" runat="server" value="<%# ((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).Name %>"></asp:HiddenField>
                            <%# ((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).Prompt %>
                            <td>
                            <div runat="server" visible='<%# (((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).DataType == "String" 
                            && (((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).ValidValues == null))%>'>
                            <asp:TextBox ID="FreeTextField" runat="server" />
                            </div>
                            <div runat="server" style="position: relative" visible='<%# (((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).DataType == "DateTime") %>'>
                            <asp:TextBox ID="DateField" autocomplete="off" CssClass = 'date-picker' runat="server"></asp:TextBox>
                            </div>
                            <div runat="server" visible='<%# (((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).ValidValues != null) && !(((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).DataType == "select2") %>'>
                            <asp:DropDownList ID="DropDownList" runat="server"></asp:DropDownList>
                            </div>
                            <div runat="server" visible='<%# (((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).DataType == "select2") %>'>
                            <select ID="Select2" class="select2" datafield='<%# ((Eduegate.Utilities.SSRSHelper.ReportParameter)Container.DataItem).Name %>' runat="server" multiple="true" style="width:300px"></select>
                            <asp:HiddenField runat="server" id = "Select2Hidden" ></asp:HiddenField>
                            </div>
                            </td>
                            </tr>
                            </ItemTemplate>
                        <FooterTemplate>
                    </table>
                        </FooterTemplate>
                        </asp:Repeater>
                         
       </div>
    </div>
       <asp:button style="width: 80px;height: 35px;margin-left: 7px;font-weight: bolder;" 
          runat="server" ID="btnIssue" text="Issue" OnClick="btnIssue_Click"/>
       <asp:button style="width: 80px;height: 35px;margin-left: 7px;font-weight: bolder;" 
          runat="server" ID="btnView" text="View" OnClick="btnView_Click"/>
        <asp:button style="width: 80px;height: 35px;margin-left: 7px;font-weight: bolder;" 
          runat="server" ID="btnPrint" text="Print" OnClick="btnPrint_Click" />

        <%--<asp:button style="width: 80px;height: 35px;margin-left: 7px;font-weight: bolder;" 
          runat="server" ID="btnPrint" text="Print" OnClientClick="printDiv('viewer_ctl13')"/>--%>
        <%--<asp:button runat="server" ID="btnPrint" text="Print" OnClick="Export" class="print"/>--%>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <rsweb:ReportViewer ID="viewer" runat="server" ProcessingMode="Local" SizeToReportContent="true"
                              AsyncRendering="true" PageCountMode="Actual" 
            ShowPageNavigationControls="true" KeepSessionAlive="true">
        </rsweb:ReportViewer>

        <iframe id="frmPrint" name="frmPrint" width="500" height="200" runat="server" style="display: none" ></iframe>

        <%--<iframe onload="isLoaded()" id="pdf" name="pdf" src=""></iframe>--%>
        <%--<asp:button ID="ImageButton1" runat="server" OnClick="btnPrint_Click"  />--%>


    <script type="text/javascript">
        utility.myHost = "<%=Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("RootUrl")%>";
        var _dateTimeFormat = "<%=Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateTimeFormat")%>";
        new reportController().Initialize('<%=viewer.LocalReport.ReportPath%>');
        function printDiv(divName)
        {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
</form>

