<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="MasterPage.master"%>

<asp:Content ContentPlaceHolderId="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderId="body" runat="server">


    <!-- Docs page layout -->
    <div class="bs-docs-header" id="content">
      <div class="container">
        <h1>Getting started</h1>
        <p>Translate your Moodle mods with our automated translator! Click on the image below to get started.</p>
        </div>
    </div>

    <div class="row">

    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#carousel-example-generic" data-slide-to="0" ></li>
    <li data-target="#carousel-example-generic" data-slide-to="1" class="active"></li>
    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
    <li data-target="#carousel-example-generic" data-slide-to="3"></li>
  </ol>


      <!-- Wrapper for slides -->
      <div class="carousel-inner">
        <div class="item active">
          <a href="Upload.aspx"><img src="img/English.png" alt="English" > </a>
          <div class="carousel-caption">
            <div style="background-color:black; overflow:hidden;" onmouseover="this.bgColor='white'"><font size="16" color="orange">Translate Today!</font></div>
          </div>
        </div>
        <div class="item">
            <a href="Upload.aspx"><img src="img/German.png" alt="German"></a>
          <div class="carousel-caption">
            <div style="background-color:black; overflow:hidden;" onmouseover="this.bgColor='white'"><font size="16" color="orange">Translate Today!</font></div>
          </div>
        </div>
        <div class="item">
            <a href="Upload.aspx"><img src="img/Russian.png" alt="Russian"></a>
          <div class="carousel-caption">
             <div style="background-color:black; overflow:hidden;" onmouseover="this.bgColor='white'"><font size="16" color="orange">Translate Today!</font></div>
          </div>
        </div>
        <div class="item">
            <a href="Upload.aspx"><img src="img/Spanish2.png" alt="Spanish"></a>
          <div class="carousel-caption">
            <div style="background-color:black; overflow:hidden;" onmouseover="this.bgColor='white'"><font size="16" color="orange">Translate Today!</font></div>
          </div>
        </div>
      </div>

      <!-- Controls -->
      <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
        <%--<span class="glyphicon glyphicon-chevron-left"></span>--%>
      </a>
      <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
       <%-- <span class="glyphicon glyphicon-chevron-right"></span>--%>
      </a>
    </div>
</div>
    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>

</asp:Content>