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
            <asp:DropDownList ID="languagesDDL" runat="server">
                <asp:ListItem Text="English" Value="en" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Because the translation takes so long, call AJAX so that the user knows to wait-->
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="downloadButton" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <asp:Button ID="translateButton" runat="server" Text="Translate" OnClick="TranslateClick" type="button" class="btn btn-primary" />
                </div>
                <div>
                    <asp:Button ID="downloadButton" runat="server" Text="Download" OnClick="DownloadFile" Visible="False" type="button" class="btn btn-primary"/>
                </div>
                <asp:Label ID="TranslateStatusLabel" runat="server" Text=""></asp:Label>
            </ContentTemplate>         
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="img/processing.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>

        
        
    </form> 
</asp:Content>

       




