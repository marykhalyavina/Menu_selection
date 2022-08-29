<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="database.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Style.css">
    <div class="jumbotron">
       <h1>
         Данные для составления меню
          </h1>
    </div>
    <h3> <b> Для формирования подходящего меню необходимо ввести данныне: </b></h3>
    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><br>
     <label for="name"><b>Имя</b></label> 
    <asp:TextBox runat="server" required="required" ID="name" placeholder="Введи имя"></asp:TextBox>
    <br>
    <b>Ваш пол:</b><Br>
    <asp:RadioButtonList ID="RadioButtonList1" required="required" runat="server">
        <asp:ListItem id="radiom" name="sex" value="male">Мужской</asp:ListItem>
        <asp:ListItem id="radiow" name="sex" value="female">Женский</asp:ListItem>
    </asp:RadioButtonList>
    <label for="date">Дата рождения: </label>
    <asp:TextBox runat="server" required="required" ID="date" type="date"></asp:TextBox>
    <br>
    <label for="weight"><b>Вeс</b></label>
    <asp:TextBox runat="server" required="required" ID="weight" placeholder="Введи вес в кг" type="number"></asp:TextBox>
    <br>
    <label for="height"><b>Рост</b></label>
    <asp:TextBox runat="server" required="required" ID="height" placeholder="Введи рост в см" type="number"></asp:TextBox>
    <br>
    <b>Выбери степень физической активности:</b><Br>
        <asp:RadioButtonList ID="RadioButtonList2" required="required" runat="server">
        <asp:ListItem id="radio11" name="PhysicalActivity" value="Extremely_inactive"> Отсутствие/минимум физической автивности</asp:ListItem>
        <asp:ListItem id="radio12" name="PhysicalActivity" value="Sedentary"> Легкая активность(1-3 раза в неделю)</asp:ListItem>
        <asp:ListItem id="radio13" name="PhysicalActivity" value="Moderately_active"> Средняя активность(5 раз в неделю)</asp:ListItem>
        <asp:ListItem id="radio14" name="PhysicalActivity" value="Vigorously_active"> Высокая активность(Высокие нагрузки ежедневно)</asp:ListItem>
        <asp:ListItem id="radio15" name="PhysicalActivity" value="Extremely_active"> Экстремально-высокая активность</asp:ListItem>
    </asp:RadioButtonList>
    <br>
    <b>Выбери цель:</b><Br>
    <asp:RadioButtonList ID="RadioButtonList3" runat="server">
        <asp:ListItem id="radio21" name="DifferentGoals" value="Maintain_weight"> Сохранение веса</asp:ListItem>
        <asp:ListItem id="radio22" name="DifferentGoals" value="Mild_weight_loss"> Постепенное снижение веса</asp:ListItem>
        <asp:ListItem id="radio23" name="DifferentGoals" value="Weight_loss"> Снижение веса</asp:ListItem>
        <asp:ListItem id="radio24" name="DifferentGoals" value="Mild_weight_gain"> Постепенное увеличение веса</asp:ListItem>
        <asp:ListItem id="radio25" name="DifferentGoals" value="Weight_gain"> Увеличение веса</asp:ListItem>
    </asp:RadioButtonList>
    <br>
    <b>Выберете аллергии из списка:</b><Br>
    <asp:CheckBoxList runat="server" ID="CheckboxList1"  DataSourceID="SqlDataSource2" DataTextField="allergenname" DataValueField="allergenid" >
        <asp:ListItem>&#39;&lt;%# Eval(&quot;allergenname&quot;) %&gt;&#39;</asp:ListItem>
            </asp:CheckBoxList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MealPlanConnectionString %>" ProviderName="<%$ ConnectionStrings:MealPlanConnectionString.ProviderName %>" SelectCommand="SELECT allergenid, allergenname FROM food.allergen"></asp:SqlDataSource>
        <br>
    <asp:button id="Insertbutton" type="submit" runat="server" OnClick="Insertbutton_Click" Text ="Подобрать"></asp:button>
    <asp:button id="updatebutton" type="submit" runat="server" OnClick="updatebutton_Click" Text ="Изменить"></asp:button>
</asp:Content>
