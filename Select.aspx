<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="database.Select" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Подходящие меню
          </h1>
        </div>
    <article>
                <asp:GridView ID="GridView1" runat="server" Alignment = DataGridViewContentAlignment.MiddleCenter OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" >
                    <Columns>
                     <asp:BoundField DataField="menuname"  HeaderText="Название" SortExpression="menuname"/>
                     <asp:BoundField DataField="menudescription" HeaderText="Описание" SortExpression="menudescription" />
                     <asp:BoundField DataField="proteinofmenus" HeaderText="Белки" SortExpression="proteinofmenus"/>
                     <asp:BoundField DataField="fatofmenus" HeaderText="Жиры" SortExpression="fatofmenus"/>
                     <asp:BoundField DataField="carbohydratesofmenus" HeaderText="Углеводы" SortExpression="carbohydratesofmenus"/>
                     <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                 </Columns>
             </asp:GridView>
             <br />
        </article>
</asp:Content>
