<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Description.aspx.cs" Inherits="database.Description" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Описание меню</h1>      
    </div>
    <asp:Label ID="Label8" runat="server" ForeColor="Red"></asp:Label>
    <br>
    <asp:label runat="server"><b>Имя меню: </b></asp:label>
    <asp:label id="Label1" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Описание: </b></asp:label>
    <asp:label id="Label2" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Количество приемов пищи: </b></asp:label>
    <asp:label id="Label3" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Калорийность: </b></asp:label>
    <asp:label id="Label4" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Белки(г): </b></asp:label>
    <asp:label id="Label5" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Жиры(г): </b></asp:label>
    <asp:label id="Label6" runat="server"></asp:label>
    <br>
     <asp:label runat="server"><b>Углеводы(г): </b></asp:label>
    <asp:label id="Label7" runat="server"></asp:label>
    <br>
    <asp:label runat="server"><b>Блюда или продукты: </b></asp:label>
    <asp:GridView ID="GridView1" runat="server" Alignment = DataGridViewContentAlignment.MiddleCenter DataKeyNames="dishid" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" >
                    <Columns>
                     <asp:BoundField DataField="nameofdish"  HeaderText="Название" SortExpression="nameofdish"/>
                     <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                 </Columns>
             </asp:GridView>
    <asp:label runat="server"><b>Алергены: </b></asp:label>
    <asp:Repeater ID="rptTable1" runat="server">
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
    <asp:button id="b2" type="submit" runat="server" OnClick="Button2_Click" Text ="Выбрать"></asp:button>
</asp:Content>
