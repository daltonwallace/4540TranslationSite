<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="_Upload" MasterPageFile="MasterPage.master"%>

<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">

    <!-- Docs page layout -->
    <div class="bs-docs-header" id="content">
      <div class="container">
        <h1>Translate your mod</h1>
        <p>Each moodle mod contains a language file. Obtain that language file and upload it here. Then you will choose what language to tranlate it into to, then you will be able to download it. Place that modified file in the 'lang' folder, and now your mod will be translated once you change the language.</p>
        </div>
    </div>

    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
    
    <div class="row">
        <div class="col-lg-6">
             <div class="well bs-component">
                <asp:FileUpload ID="FileUploadControl" runat="server" type="button" class="btn btn-default"/>
                <asp:Button ID="uploadButton" runat="server" Text="Upload File" OnClick ="uploadFile" type="button" class="btn btn-primary"/>
                <a href="Upload.aspx"></a>
            </div>
        </div>
       
    </div>
    </form>

    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>

</asp:Content>