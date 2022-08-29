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
    public partial class Descriptionproduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (helper.role == "admin")
                b1.Visible = true;
            else
                b1.Visible = false;
            NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT nameofdish, description, weightofdish, cookingtime, difficultyincooking, caloriccontentofdish, proteinofdish, fatofdish, carbohydratesofdish FROM food.dish where dishid=" + Convert.ToString(helper.iddish);
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
                Label8.Text = Convert.ToString(dr[7]);
                Label9.Text = Convert.ToString(dr[8]);
            }
            //Label3.Text = Convert.ToString(helper.iddish);
            //    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><br>
            string str = "SELECT DISTINCT a.allergenname FROM food.allergeninthedish AS al JOIN food.allergen AS a ON a.allergenid=al.idallergen AND iddish = " + Convert.ToString(helper.iddish);
            NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect.Open();
            NpgsqlDataAdapter ad = new NpgsqlDataAdapter(str, connect);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            rptTable.DataSource = ds;
            rptTable.DataBind();
            //MyDataList.DataSource = ds;
            //MyDataList.DataBind();
            connect.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string sql = "Delete from food.dish where dishid =" + Convert.ToString(helper.iddish);

                using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            Response.Redirect("~/Product");
        }
    }
}