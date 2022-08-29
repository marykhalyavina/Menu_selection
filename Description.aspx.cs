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
    public partial class Description : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (helper.role == "admin")
            {
                b1.Visible = true;
                b2.Visible = false;
            }
            else
            {
                b2.Visible = true;
                b1.Visible = false;
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
            //////////////////переделать в таблицу
            string str = "SELECT DISTINCT nameofdish, dishid FROM food.menuswithdishs where menusid = " + Convert.ToString(helper.idmenu);
            NpgsqlConnection connec = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connec.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connec;
            command.CommandType = CommandType.Text;
            command.CommandText = str;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(str, connec);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "food.menuswithdishs");
            GridView1.DataSource = dataset.Tables["food.menuswithdishs"];
            GridView1.DataBind();
            connec.Close();
            string str1 = "SELECT DISTINCT allergenname FROM food.menuswithdishandallergen where menusid = " + Convert.ToString(helper.idmenu);
            NpgsqlConnection connect1 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect1.Open();
            NpgsqlDataAdapter ad1 = new NpgsqlDataAdapter(str1, connect1);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            rptTable1.DataSource = ds1;
            rptTable1.DataBind();
            connect1.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string sql = "Delete from food.menus where menusid =" + Convert.ToString(helper.idmenu);

                using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            Response.Redirect("~/Default");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (helper.role == "unreg")
            {
                Response.Redirect("~/Contact");
            }
            else
            {
                string strin = "SELECT ma.allergenid from userdata.allergeninthehuman ah  join food.menuswithdishandallergen ma on ma.allergenid = ah.idallergen and ah.idhuman= " + Convert.ToString(helper.iduser) + " and ma.menusid = "+ Convert.ToString(helper.idmenu);
                NpgsqlConnection conn1 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                conn1.Open();
                NpgsqlCommand comm1 = new NpgsqlCommand();
                comm1.Connection = conn1;
                comm1.CommandType = CommandType.Text;
                comm1.CommandText = strin;
                NpgsqlDataReader dr = comm1.ExecuteReader();
                if (dr.HasRows )
                {
                    Label8.Text = "Данное меню содержит ваши алергены.";
                }
                else
                {
                    NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                    connection.Open();
                    NpgsqlCommand comman = new NpgsqlCommand();
                    comman.Connection = connection;
                    comman.CommandType = CommandType.Text;
                    comman.CommandText = "UPDATE users.registereduser SET usermenu = true WHERE registereduserid = " + Convert.ToString(helper.iduser);
                    comman.ExecuteNonQuery();
                    connection.Close();
                    string connectionString = "Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;";
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        string sql = "INSERT INTO userdata.humanmenu (idhuman,idmenu) VALUES " + "(@idhuman,@idmenu)";
                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                        {
                            NpgsqlParameter[] prms = new NpgsqlParameter[2];
                            prms[0] = new NpgsqlParameter("@idhuman", NpgsqlTypes.NpgsqlDbType.Integer);
                            prms[0].Value = helper.iduser;
                            prms[1] = new NpgsqlParameter("@idmenu", NpgsqlTypes.NpgsqlDbType.Integer);
                            prms[1].Value = helper.idmenu;
                            comm.Parameters.AddRange(prms);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    helper.mymenu = helper.idmenu;
                    Response.Redirect("~/mymenu");
                }
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            helper.iddish = (int)GridView1.SelectedDataKey.Values["dishid"];
            Response.Redirect("~/Descriptionproduct");
        }
    }
}