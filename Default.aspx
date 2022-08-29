<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="database._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <div class="jumbotron">
        <h1>Меню</h1>      
    </div>
    <asp:button id="b1" type="submit" runat="server" OnClick="Button1_Click" Text ="Добавить"></asp:button>
        <article>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="menusid" DataSourceID="menus1" Alignment = DataGridViewContentAlignment.MiddleCenter OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                 <Columns>
                     <asp:BoundField DataField="menusid" HeaderText="Id" ReadOnly="True" InsertVisible="False"/>
                     <asp:BoundField DataField="menuname" HeaderText="Название" SortExpression="menuname" />
                     <asp:BoundField DataField="caloriccontentofmenu" HeaderText="Кол-во ккал" SortExpression="caloriccontentofmenu" />
                     <asp:BoundField DataField="ProteinOfMenus" HeaderText="Белки" SortExpression="ProteinOfMenus"/>
                     <asp:BoundField DataField="FatOfMenus" HeaderText="Жиры" SortExpression="FatOfMenus"/>
                     <asp:BoundField DataField="CarbohydratesOfMenus" HeaderText="Углеводы" SortExpression="CarbohydratesOfMenus"/>
                     <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                 </Columns>
             </asp:GridView>
             <asp:SqlDataSource ID="menus1" runat="server" ConnectionString="Server=127.0.0.1;User Id=postgres;Password=5827;Port=5432;Database=MealPlan;" 
                 ProviderName="Npgsql" SelectCommand="SELECT distinct * FROM food.menus where caloriccontentofmenu > 0;">
             </asp:SqlDataSource>
             <br />
        </article>
</asp:Content>
