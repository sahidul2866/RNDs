<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="BackOfficeUI.Default" %>

<%@ MasterType VirtualPath="Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="errPanel" runat="server" Visible="false">
        <asp:Label runat="server" ID="lblErr" Style="color: red; align-content: center" Text="YOU ARE NOT AUTHORISED TO VIEW THIS PAGE."> </asp:Label>
    </asp:Panel>
    <asp:Panel ID="mainPanel" runat="server" Visible="false" ForeColor="darkcyan">
        <script>

            $(document).ready(function () {

                // var app_id = getUrlVars()["AppID"];

                $('#tblMarkAttachment').DataTable({
                    "iDisplayLength": 5,
                    "autoWidth": true,
                    "oLanguage": {
                        "sEmptyTable": "No Attachment Rows Found"
                    }
                    //"aoColumnDefs": [
                    //    { "bSortable": false, "aTargets": [7] }
                    //]
                });

            });

            function markAttachment(dID) {
                if (confirm('Are you sure you want to mark the attachment ' + dID + ' as Unrecoverable? Press OK to Delete.')) {
                    var deltrid = "#TR" + dID;
                    var table = $('#tblMarkAttachment').DataTable();
                    var removingRow = $(deltrid).closest('tr');
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "default.aspx/MarkRow",
                        data: JSON.stringify({ dID: dID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (obj) {
                            if (obj.d.issuccess) {
                                //window.location.reload(true);
                                table.row(removingRow).remove();
                                $(deltrid).remove();
                            }
                            else if (!obj.d.issuccess)
                                alert(obj.d.message);
                        }
                    });
                }
            }

            function unmarkAttachment(dID) {
                if (confirm('Are you sure you want to unmark the attachment ' + dID + ' as Unrecoverable? Press OK to Delete.')) {
                    var deltrid = "#TR" + dID;
                    var table = $('#tblMarkAttachment').DataTable();
                    var removingRow = $(deltrid).closest('tr');
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "default.aspx/UnmarkRow",
                        data: JSON.stringify({ dID: dID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (obj) {
                            if (obj.d.issuccess) {
                                //window.location.reload(true);
                                table.row(removingRow).remove();
                                $(deltrid).remove();
                            }
                            else if (!obj.d.issuccess)
                                alert(obj.d.message);
                        }
                    });
                }
            }

        </script>
        <div class="row">
            <div class="col-3 mt-2"></div>
            <div class="col-2 mt-2">
                <h5>Search Criteria:  </h5>
            </div>
            <div class="col-3">
                <asp:TextBox runat="server" ID="txtSearch" Style="background-image: url('/css/searchicon.png'); background-position: 10px 12px; /* position the search icon */
                    background-repeat: no-repeat; /* do not repeat the icon image */
                    width: 100%; /* full-width */
                    font-size: 12px; /* increase font-size */
                    padding: 12px 20px 12px 40px; /* add some padding */
                    border: 1px solid #ddd; /* add a grey border */
                    margin-bottom: 12px;"></asp:TextBox>
            </div>
            <div class="col-1 mt-2">
                <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" />
            </div>
        </div>
        <table id="tblMarkAttachment" class="table table-responsive table-striped table-bordered text-left dataTable no-footer">

            <asp:Repeater ID="rptMarkAttachmentRows" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th>LOG ID</th>
                            <th>ATTACHMENT RSN</th>
                           <%-- <th>RUN DATE</th>
                            <th>RECOVERY STATUS</th>
                            <th>DOSPATH</th>
                            <th>TABLENAME</th>
                            <th>STAMPDATE</th>
                            <th>STAMPUSER</th>--%>
                        </tr>
                    </thead>
                    <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr id="TR<%#Eval("attachmentrsn")%>">
                        <td><%#Eval("OC_ATTACHMENTS_MISSING_LOG_ID")%></td>
                        <td><%#Eval("attachmentrsn")%><br />
                            <a href='javascript:markAttachment(<%#Eval("attachmentrsn")%>)'>Mark</a>&nbsp;<a href="javascript:unmarkAttachment(<%#Eval("attachmentrsn")%>)">Unmark</a>
                        </td>
                        <%--<td><%#Eval("rundate")%></td>
                        <td><%#Eval("RECOVERYSTATUS")%></td>
                        <td><%#Eval("DOSPATH")%></td>
                        <td><%#Eval("TABLENAME")%></td>
                        <td><%#Eval("STAMPDATE")%></td>
                        <td><%#Eval("STAMPUSER")%></td>--%>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</asp:Content>
