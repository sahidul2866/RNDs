<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="BackOfficeUI.Home" %>

<%@ MasterType VirtualPath="Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="errPanel" runat="server" Visible="false">
        <asp:Label runat="server" ID="lblErr" Style="color: red; align-content: center" Text="YOU ARE NOT AUTHORISED TO VIEW THIS PAGE."> </asp:Label>
    </asp:Panel>
    <asp:Panel ID="mainPanel" runat="server" Visible="false" ForeColor="darkcyan">
        <center>
            <div class="row">
                <div class="col-6">
                    Put in the attachmentrsn you wish to mark as unrecoverable.
                    <div class="row mt-2">
                        <div class="col-2"></div>
                        <div class="col-3">
                            AttachmentRSN: 
                        </div>
                        <div class="col-3">
                            <asp:TextBox runat="server" OnTextChanged="txtAttachmentRSN_TextChanged" ID="txtAttachmentRSN"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mt-2">
                        <asp:Button OnClick="btnMarkAttachment_Click" OnClientClick='confirm("Do you confirm?")' ID="btnMarkAttachment" ForeColor="White" BackColor="darkcyan" runat="server" Text="Mark Attachment" />
                    </div>
                </div>
                <div class="col-6">
                    Put in the attachmentrsn you wish to Unmark as unrecoverable.
                    <div class="row mt-2">
                        <div class="col-2"></div>
                        <div class="col-3">
                            AttachmentRSN: 
                        </div>
                        <div class="col-3">
                            <asp:TextBox runat="server" OnTextChanged="txtAttachmentRSN_TextChanged" ID="txtUnmark"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mt-2">
                        <asp:Button OnClick="btnUnmark_Click" OnClientClick='confirm("Do you confirm?")' ID="btnUnmark" ForeColor="White" BackColor="darkcyan" runat="server" Text="Unmark Attachment" />
                    </div>
                </div>
            </div>

            <br />
            <h2>Missing Attachments Log </h2>
        </center>
        <div class="row">
            <div class="col-3">
                <h2>Search Criteria:  </h2>
            </div>
            <div class="col-9">
                <asp:TextBox runat="server" ID="txtSearch" OnTextChanged="txtSearch_TextChanged" style="background-image: url('/css/searchicon.png'); background-position: 10px 12px; /* position the search icon */
        background-repeat: no-repeat; /* do not repeat the icon image */
        width: 100%; /* full-width */
        font-size: 16px; /* increase font-size */
        padding: 12px 20px 12px 40px; /* add some padding */
        border: 1px solid #ddd; /* add a grey border */
        margin-bottom: 12px;"></asp:TextBox>
            </div>
        </div>
        
        <input type="text" id="myInput" style="background-image: url('/css/searchicon.png'); background-position: 10px 12px; /* position the search icon */
        background-repeat: no-repeat; /* do not repeat the icon image */
        width: 100%; /* full-width */
        font-size: 16px; /* increase font-size */
        padding: 12px 20px 12px 40px; /* add some padding */
        border: 1px solid #ddd; /* add a grey border */
        margin-bottom: 12px; /* add some space below the input */"
            onkeyup="myFunction()" placeholder="Frontend search for Attachment RSN..">
        <asp:PlaceHolder ID="ListDetails" runat="server" />
    </asp:Panel>
    <script>
        function myFunction() {
            // Declare variables
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("myTable");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>
