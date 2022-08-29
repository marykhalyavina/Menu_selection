<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="database.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Регистрация
          </h1>
        </div>
    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
    <div class="container">           
            <asp:label id="Label1" runat="server" AssociatedControlID="login"><b>Логин</b></asp:label>
            <asp:TextBox ID="login" runat="server" placeholder="Введи логин"/>
            <br>
            <label id="Label2" runat="server" Text ="Пароль" AssociatedControlID="psw"><b>Пароль</b></label>
            <asp:TextBox ID="psw" runat="server" placeholder="Введи пароль"/>      
            <br>
        <asp:button type="submit" runat="server" OnClick="Button1_Click" Text ="Зарегистрироваться"></asp:button>
            <asp:SqlDataSource runat="server" ID="SqlDataSource1"  ConnectionString="Server=127.0.0.1;User Id=postgres;Password=12345;Port=5432;Database=MealPlan;" ProviderName="Npgsql"  SelectCommand="INSERT INTO users.registereduser(login,password_hash, role) values(login.text,psw.text, 'admin')"></asp:SqlDataSource>
            </div>
</asp:Content>