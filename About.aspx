<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="_About" MasterPageFile="MasterPage.master"%>

<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">


    <!-- Docs page layout -->
    <div class="bs-docs-header" id="content">
      <div class="container">
        <h1>About us</h1>
        <p>This page describes who we are and why did this.</p>
        </div>
    </div>

     <div class="row">
        <div class="col-lg-6">
             <div class="well bs-component">
                <p>This was a project done for a Web Architecture Computer Science Project for the University of Utah. This allows a user to automatically translate their Moodle mods into many different languages. This is done with a series of parsing and Selenium scripts.</p>
            </div>
        </div>
    </div>
    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>

</asp:Content>