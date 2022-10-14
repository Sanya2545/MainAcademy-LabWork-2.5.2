<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRM_v2._Default"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <table class="nav-justified">
            <tr>
                <td style="width: 399px">
       <img src="Images/coffee.bmp" class="my_logo" /></td>
                <td>
        <h1 style="padding-left:40px;">Let&#39;s go...</h1>
                    <p class="lead" style="padding-left:40px;">About: ...</p>
                </td>
            </tr>
        </table>
<%--&nbsp;<p class="lead">&nbsp;Маленька CRM програма (керування взаємодією з клієнтом).</p>--%>
       <%-- <p><a href="http://www.asp.net" class="btn btn-primary btn-large">Далі &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Почнемо</h2>
            <p>
                Галерея.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Далі &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Працюємо</h2>
            <p>
                Авторизованим користувачам.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Далі &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Як ?</h2>
            <p>
               Довідка.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Далі &raquo;</a>
            </p>

        </div>

    </div>

<div class="row">
        <div class="col-md-4">
           <asp:Panel runat="server" ID="admpanel"> 
              <h2>Адміністрування</h2>
            <p>               
              <asp:Button runat="server" OnClick="LogIn" ID= "admctrl" Text="Увійти" CssClass="btn btn-default" />
            </p>
            </asp:Panel> 
        </div>
            <div class="col-md-4">
           <asp:Panel runat="server" ID="chatpanel"> 
              <h2>Чат</h2>
            <p>     
                 <a Class="btn btn-default"  href="/Chat/Index" target="_blank" B>Чат</a>                                                  
                <%--<a Class="btn btn-default"  href="/Chat/Index" ">Чат</a> --%>               
            </p>              
            </asp:Panel> 
        </div>
</div>

</asp:Content>
