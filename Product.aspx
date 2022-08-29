<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="database.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Блюда
          </h1>
        </div>
            <article>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="dishid" DataSourceID="product1" Alignment = DataGridViewContentAlignment.MiddleCenter OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                 <Columns>
                     <asp:BoundField DataField="nameofdish" HeaderText="Название" SortExpression="nameofdish" />
                     <asp:BoundField DataField="WeightofDish" HeaderText="Вес(г)" SortExpression="WeightofDish" />
                     <asp:BoundField DataField="CookingTime" HeaderText="Время готовки" SortExpression="CookingTime" />
                     <asp:BoundField DataField="DifficultyInCooking" HeaderText="Сложность приготовления" SortExpression="DifficultyInCooking" />
                     <asp:BoundField DataField="caloriccontentofdish" HeaderText="Кол-во ккал" SortExpression="caloriccontentofdish" />
                     <asp:BoundField DataField="proteinofdish" HeaderText="Белки" SortExpression="proteinofdish"/>
                     <asp:BoundField DataField="fatofdish" HeaderText="Жиры" SortExpression="fatofdish"/>
                     <asp:BoundField DataField="carbohydratesofdish" HeaderText="Углеводы" SortExpression="carbohydratesofdish"/>
                     <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description" />
                     <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                 </Columns>
             </asp:GridView>
             <asp:SqlDataSource ID="product1" runat="server" ConnectionString="Server=127.0.0.1;User Id=postgres;Password=5827;Port=5432;Database=MealPlan;" 
                 ProviderName="Npgsql" SelectCommand="SELECT distinct * FROM food.dish;"></asp:SqlDataSource>
             <br />
        </article>
</asp:Content>
