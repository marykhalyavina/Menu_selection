<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insertmenu.aspx.cs" Inherits="database.Insertmenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Добавление меню
          </h1>
    </div>
    <asp:Label ID="Label8" runat="server" ForeColor="Red"></asp:Label>
    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><br>
    <label for="name"><b>Название</b></label> 
    <asp:TextBox runat="server" required="required" ID="name" placeholder="Введи Название"></asp:TextBox>
    <br>
    <label for="name"><b>Описание</b></label>     
    <br>
    <asp:TextBox ID="TextBox1" runat="server" Height="89px" Width="628px"></asp:TextBox>
    <br>
    <asp:CheckBoxList runat="server" ID="CheckboxList1"  DataSourceID="SqlDataSource2" DataTextField="nameofdish" DataValueField="dishid" >
        <asp:ListItem>&#39;&lt;%# Eval(&quot;nameofdish&quot;) %&gt;&#39;</asp:ListItem>
            </asp:CheckBoxList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MealPlanConnectionString %>" ProviderName="<%$ ConnectionStrings:MealPlanConnectionString.ProviderName %>" SelectCommand="SELECT distinct * FROM food.dish;"></asp:SqlDataSource>
        <br>
    <asp:button id="Insertbutton" type="submit" runat="server" OnClick="Insertbutton_Click" Text ="Добавить"></asp:button>
</asp:Content>
