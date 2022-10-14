<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRM_v2._Default"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <div class="jumbotron">
        <table class="nav-justified">
            <tr>
                <td style="width: 638px">


       <img src="Images/coffee.bmp" class="my_logo" style="height:160px;  width: 320px; background-color:gray; border:thick;  " /></td>
                <td>
        <h1 style="padding-left:50px;">Let&#39;s go...</h1>
                    
                </td>
            </tr>
        </table>

    </div>

    <div class="row">
        <div class="col-md-4">
            <h2 >Begin</h2>
            <p style="font-size:larger">
                Gallery
            </p>
            <p>          
                <asp:Button runat="server" OnClick="GalIn" ID= "Button3" Text="Next &raquo;" CssClass="btn btn-default" />
                 </p>
        </div>
        <div class="col-md-4">
            <h2 >For clients</h2>
            <p style="font-size:larger">
                For authorized clients
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Next &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2 >How ?</h2>
            <p style="font-size:larger">
                Help
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Next &raquo;</a>
            </p>

        </div>

    </div>

<div class="row">
        <div class="col-md-4">
           <asp:Panel runat="server" ID="admpanel"> 
              <h2 >Administration</h2>
            <p>               
              <asp:Button runat="server" OnClick="LogIn" ID= "admctrl" Text="Enter" CssClass="btn btn-default" />
            </p>
            </asp:Panel> 
        </div>
            <div class="col-md-4">
           <asp:Panel runat="server" ID="chatpanel"> 
              <h2 >Chat</h2>
            <p>     
                 <a Class="btn btn-default"  href="/Chat/Index" target="_blank" >Chat</a>                                                  
            
            </p>              
            </asp:Panel> 
        </div>
</div>

</asp:Content>
