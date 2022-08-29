using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using Devart.Data.PostgreSql;
using System.Configuration;

namespace database
{
    public partial class mymenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connection.Open();
            NpgsqlCommand comman = new NpgsqlCommand();
            comman.Connection = connection;
            comman.CommandType = CommandType.Text;
            comman.CommandText = "SELECT idhuman,idmenu FROM userdata.humanmenu where idhuman=" + Convert.ToString(helper.iduser);
            NpgsqlDataReader dread = comman.ExecuteReader();
            while (dread.Read())
            {
                helper.idmenu = Convert.ToInt32(dread[1]);
            }
            NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT menuname, menudescription, numberofmeals, caloriccontentofmenu, proteinofmenus, fatofmenus, carbohydratesofmenus FROM food.menus where menusid=" + Convert.ToString(helper.idmenu);
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                Label1.Text = Convert.ToString(dr[0]);
                Label2.Text = Convert.ToString(dr[1]);
                Label3.Text = Convert.ToString(dr[2]);
                Label4.Text = Convert.ToString(dr[3]);
                Label5.Text = Convert.ToString(dr[4]);
                Label6.Text = Convert.ToString(dr[5]);
                Label7.Text = Convert.ToString(dr[6]);
            }
            string str = "SELECT DISTINCT dishid, nameofdish FROM food.menuswithdishs where menusid = " + Convert.ToString(helper.idmenu);
            NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connect;
            command.CommandType = CommandType.Text;
            command.CommandText = str;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(str, connect);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "food.menuswithdishs");
            GridView1.DataSource = dataset.Tables["food.menuswithdishs"];
            GridView1.DataBind();
            //MyDataList.DataSource = ds;
            //MyDataList.DataBind();
            connect.Close();
            string str1 = "SELECT DISTINCT allergenname FROM food.menuswithdishandallergen where menusid = " + Convert.ToString(helper.idmenu);
            NpgsqlConnection connect1 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect1.Open();
            NpgsqlDataAdapter ad1 = new NpgsqlDataAdapter(str1, connect);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            rptTable1.DataSource = ds1;
            rptTable1.DataBind();
            connect1.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connection.Open();
            NpgsqlCommand comman = new NpgsqlCommand();
            comman.Connection = connection;
            comman.CommandType = CommandType.Text;
            comman.CommandText = "UPDATE users.registereduser SET usermenu = false WHERE registereduserid = " + Convert.ToString(helper.iduser);
            comman.ExecuteNonQuery();
            connection.Close();
            string connectionString = "Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string sql = "Delete from userdata.humanmenu where idmenu = " + Convert.ToString(helper.idmenu);

                using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            helper.mymenu = -1;
            Response.Redirect("~/Default");
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            helper.iddish = (int)GridView1.SelectedDataKey.Values["dishid"];
            //  Ключевое поле можно извлечь из свойства SelectedDataKey
            Response.Redirect("~/Descriptionproduct");
        }
    }
}