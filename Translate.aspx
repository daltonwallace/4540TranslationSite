<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Translate.aspx.cs" Inherits="Translate" MasterPageFile="MasterPage.master"%>


<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">
    <div>
        <h1>You uploaded: </h1>
        <h2><%:Session["filename"] %></h2>
    </div>

    <form id="form1" runat="server">

        <div>

            <h2>Select translation language: </h2>
            <asp:DropDownList ID="languagesDDL" runat="server"></asp:DropDownList>

        </div>

        <div>
            <asp:Button ID="translateButton" runat="server" Text="Translate" OnClick="TranslateClick" />
        </div>

        <div>
            <asp:Button ID="downloadButton" runat="server" Text="Download" OnClick="DownloadFile" Visible="False" />
        </div>
        
    </form> 
</asp:Content>

       




