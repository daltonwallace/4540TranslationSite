<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Translate.aspx.cs" Inherits="Translate" MasterPageFile="MasterPage.master"%>


<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">
    <div class="bs-docs-header" id="content">
       <div class="container">
          <h1>You uploaded: </h1>
          <h4><font color='red'><%:Session["filename"] %></font></h4>
       </div>
    </div>

    <form id="form1" runat="server">

        <div class="row">
            <div class="col-lg-6">
                <div class="well bs-component">
            <h2>Select translation language: </h2>
            <asp:DropDownList ID="languagesDDL" runat="server">
                <asp:ListItem Text="English" Value="en" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Afrikaans" Value="af"></asp:ListItem>
                <asp:ListItem Text="Albanian" Value="sq"></asp:ListItem>
                <asp:ListItem Text="Armenian" Value="hy"></asp:ListItem>
                <asp:ListItem Text="Azerbaijani " Value="az"></asp:ListItem>
                <asp:ListItem Text="Belarusian" Value="be"></asp:ListItem>
                <asp:ListItem Text="Bengali" Value="bn"></asp:ListItem>
                <asp:ListItem Text="Bosnian" Value="bs"></asp:ListItem>
                <asp:ListItem Text="Bulgarian" Value="bg"></asp:ListItem>
                <asp:ListItem Text="Catalan" Value="ca"></asp:ListItem>
                <asp:ListItem Text="Cebuano" Value="ceb"></asp:ListItem>
                <asp:ListItem Text="Croatian" Value="hr"></asp:ListItem>
                <asp:ListItem Text="Czech" Value="cs"></asp:ListItem>
                <asp:ListItem Text="Danish" Value="da"></asp:ListItem>
                <asp:ListItem Text="Dutch" Value="nl"></asp:ListItem>
                <asp:ListItem Text="Esperanto" Value="eo"></asp:ListItem>
                <asp:ListItem Text="Estonian" Value="et"></asp:ListItem>
                <asp:ListItem Text="Filipino" Value="tl"></asp:ListItem>
                <asp:ListItem Text="Finnish" Value="fi"></asp:ListItem>
                <asp:ListItem Text="French" Value="fr"></asp:ListItem>
                <asp:ListItem Text="Gallcian" Value="gl"></asp:ListItem>
                <asp:ListItem Text="Georgian" Value="ka"></asp:ListItem>
                <asp:ListItem Text="German" Value="de"></asp:ListItem>
                <asp:ListItem Text="Greek" Value="el"></asp:ListItem>
                <asp:ListItem Text="Gujarati" Value="gu"></asp:ListItem>
                <asp:ListItem Text="Haitian Creole" Value="ht"></asp:ListItem>
                <asp:ListItem Text="Hausa" Value="ha"></asp:ListItem>
                <asp:ListItem Text="Hebrew" Value="iw"></asp:ListItem>
                <asp:ListItem Text="Hindi" Value="hi"></asp:ListItem>
                <asp:ListItem Text="Hmong" Value="hmn"></asp:ListItem>
                <asp:ListItem Text="Hungarian" Value="hu"></asp:ListItem>
                <asp:ListItem Text="Icelandic" Value="is"></asp:ListItem>
                <asp:ListItem Text="Igbo" Value="ig"></asp:ListItem>
                <asp:ListItem Text="Indonesian" Value="id"></asp:ListItem>
                <asp:ListItem Text="Irish" Value="ga"></asp:ListItem>
                <asp:ListItem Text="Italian" Value="it"></asp:ListItem>
                <asp:ListItem Text="Kannada" Value="kn"></asp:ListItem>
                <asp:ListItem Text="Khmer" Value="km"></asp:ListItem>
                <asp:ListItem Text="Lao" Value="lo"></asp:ListItem>
                <asp:ListItem Text="Latin" Value="la"></asp:ListItem>
                <asp:ListItem Text="Latvian" Value="lv"></asp:ListItem>
                <asp:ListItem Text="Lithuanian" Value="lt"></asp:ListItem>
                <asp:ListItem Text="Macedonian" Value="mk"></asp:ListItem>
                <asp:ListItem Text="Malay" Value="ms"></asp:ListItem>
                <asp:ListItem Text="Maltese" Value="mt"></asp:ListItem>
                <asp:ListItem Text="Maori" Value="mi"></asp:ListItem>
                <asp:ListItem Text="Marathi" Value="mr"></asp:ListItem>
                <asp:ListItem Text="Mongolian" Value="mn"></asp:ListItem>
                <asp:ListItem Text="Nepali" Value="ne"></asp:ListItem>
                <asp:ListItem Text="Norwegian" Value="no"></asp:ListItem>
                <asp:ListItem Text="Persian" Value="fa"></asp:ListItem>
                <asp:ListItem Text="Polish" Value="pl"></asp:ListItem>
                <asp:ListItem Text="Portuguese" Value="pt"></asp:ListItem>
                <asp:ListItem Text="Punjabi" Value="pa"></asp:ListItem>
                <asp:ListItem Text="Romanian" Value="ro"></asp:ListItem>
                <asp:ListItem Text="Russian" Value="ru"></asp:ListItem>
                <asp:ListItem Text="Serbian" Value="sr"></asp:ListItem>
                <asp:ListItem Text="Slovak" Value="sl"></asp:ListItem>
                <asp:ListItem Text="Slovenian" Value="sk"></asp:ListItem>
                <asp:ListItem Text="Somali" Value="so"></asp:ListItem>
                <asp:ListItem Text="Spanish" Value="es"></asp:ListItem>
                <asp:ListItem Text="Swahili" Value="sw"></asp:ListItem>
                <asp:ListItem Text="Swedish" Value="sv"></asp:ListItem>
                <asp:ListItem Text="Tamil" Value="ta"></asp:ListItem>
                <asp:ListItem Text="Telugu" Value="te"></asp:ListItem>
                <asp:ListItem Text="Thai" Value="th"></asp:ListItem>
                <asp:ListItem Text="Turkish" Value="tr"></asp:ListItem>
                <asp:ListItem Text="Ukranian" Value="uk"></asp:ListItem>
                <asp:ListItem Text="Urdu" Value="ur"></asp:ListItem>
                <asp:ListItem Text="Vietnamese" Value="vi"></asp:ListItem>
                <asp:ListItem Text="Welsh" Value="cy"></asp:ListItem>
                <asp:ListItem Text="Yiddish" Value="yi"></asp:ListItem>
                <asp:ListItem Text="Yoruba" Value="yo"></asp:ListItem>
                <asp:ListItem Text="Zulu" Value="zu"></asp:ListItem>               
            </asp:DropDownList>
              
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

             </div>
          </div>
        </div>
        
    </form> 
</asp:Content>

       




