﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="MasterPage.master"%>

<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">

    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
    <div>

        <asp:FileUpload ID="FileUploadControl" runat="server" type="button" class="btn btn-default"/>
        <asp:Button ID="uploadButton" runat="server" Text="Upload File" OnClick ="uploadFile" type="button" class="btn btn-primary"/>

    </div>
    </form>

    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>

</asp:Content>