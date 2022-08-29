using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC = BCrypt.Net.BCrypt;
using Npgsql;
using System.Data;
using Devart.Data.PostgreSql;
using System.Configuration;

namespace database
{
    public partial class Select : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int c = Convert.ToInt32(helper.calories);
            string str = "(SELECT distinct menusid, menuname, caloriccontentofmenu, menudescription, proteinofmenus, fatofmenus, carbohydratesofmenus FROM food.menus where ((0 < caloriccontentofmenu - " + Convert.ToString(c)
                + " and caloriccontentofmenu - " + Convert.ToString(c) + " < 100) or (0 <  " + Convert.ToString(c)
                + " - caloriccontentofmenu and  " + Convert.ToString(c) + " - caloriccontentofmenu < 100)))"
                + " EXCEPT SELECT distinct  m.menusid, m.menuname, caloriccontentofmenu, menudescription, proteinofmenus, fatofmenus, carbohydratesofmenus FROM food.menus m JOIN food.menuswithdishandallergen ma ON ma.menusid = m.menusid"
                + " join userdata.allergeninthehuman ah on ah.idallergen = ma.allergenid and idhuman = " + helper.iduser;
            NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect.Open();
            NpgsqlCommand comman = new NpgsqlCommand();
            comman.Connection = connect;
            comman.CommandType = CommandType.Text;
            comman.CommandText = str;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(str, connect);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "food.menus");
            GridView1.DataSource = dataset.Tables["food.menus"];
            GridView1.DataBind();
            NpgsqlDataReader dr = comman.ExecuteReader();
            while (dr.Read())
            {
                helper.idmenu = Convert.ToInt32(dr[0]);
            }           
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;

            //  Ключевое поле можно извлечь из свойства SelectedDataKey
            Response.Redirect("~/Description");

        }
    }
}