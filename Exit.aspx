<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exit.aspx.cs" Inherits="database.Exit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Аккаунт
          </h1>
        </div>
        <div class="container">
            <hr>
            <asp:label id="Label1" runat="server"><b>Логин: </b></asp:label>
            <asp:Label ID="Label2" runat="server"></asp:Label>   
            <br>
            <hr>
            <asp:button type="submit" runat="server" OnClick="Button_Click" Text ="Выход"></asp:button>
        </div>
</asp:Content>
