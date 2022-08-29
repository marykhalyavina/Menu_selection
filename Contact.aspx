<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="database.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Пожалуйста войдите в свой аккаунт
          </h1>
        </div>
    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
        <div class="container">
            <hr>
            <asp:label id="Label4" runat="server" AssociatedControlID="login1"><b>Логин</b></asp:label>
            <asp:TextBox ID="login1" runat="server" placeholder="Введи логин"/>
            <br>
            <label id="Label5" runat="server" Text ="Пароль" AssociatedControlID="psw1"><b>Пароль</b></label>
            <asp:TextBox ID="psw1" runat="server" placeholder="Введи пароль"/>      
            <br>
            <hr>
            <asp:button type="submit" runat="server" OnClick="Button2_Click" Text ="Войти"></asp:button>
        </div>
        <div class="container signin">
            <p>Нет аккаунта?  <a runat="server" href="~/Registration">Зарегистрируйся</a>
        </div>
</asp:Content>
