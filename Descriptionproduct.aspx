<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Descriptionproduct.aspx.cs" Inherits="database.Descriptionproduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Описание блюд меню
          </h1>
    </div>
    <asp:label runat="server"><b>Имя продукта или блюда: </b></asp:label>
    <asp:label id="Label1" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Описание: </b></asp:label>
    <asp:label id="Label2" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Вес: </b></asp:label>
    <asp:label id="Label3" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Время готовки: </b></asp:label>
    <asp:label id="Label4" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Сложность приготовления(от 0 до 5): </b></asp:label>
    <asp:label id="Label5" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Калорийность: </b></asp:label>
    <asp:label id="Label6" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Белки(г): </b></asp:label>
    <asp:label id="Label7" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Жиры(г): </b></asp:label>
    <asp:label id="Label8" runat="server"></asp:label>
    <br>
     <asp:label runat="server"><b>Углеводы(г): </b></asp:label>
    <asp:label id="Label9" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Алергены: </b></asp:label>
    <asp:Repeater ID="rptTable" runat="server">
    <HeaderTemplate>
        <table class="table">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td width="50%"><%# Eval("allergenname") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
    <asp:button id="b1" type="submit" runat="server" OnClick="Button1_Click" Text ="Удалить"></asp:button>
</asp:Content>
