using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BC = BCrypt.Net.BCrypt;
using Npgsql;
using System.Data;
using Devart.Data.PostgreSql;
using System.Configuration;

namespace database
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            bool b = false;
            string login11;
            if ((login.Text == "") || (psw.Text == ""))
                Label3.Text = "Ошибка регистрации.Данные не заплнены";
            else
            {
                NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                connect.Open();
                NpgsqlCommand comman = new NpgsqlCommand();
                comman.Connection = connect;
                comman.CommandType = CommandType.Text;
                comman.CommandText = "SELECT  registereduserid, login, role FROM users.registereduser";
                NpgsqlDataReader dr = comman.ExecuteReader();
                while (dr.Read())
                {
                    login11 = dr[1].ToString();

                    if (login.Text == login11)
                    {
                        Label3.Text = "Ошибка регистрации.Данный логин уже существует";
                        b = true;
                        
                    }
                    login11 = "";
                }
                connect.Close();
                if (!b)
                {
                    string connectionString = "Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;";
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        string sql = "INSERT INTO users.registereduser (login,password_hash, role) VALUES " + "(@login,@password_hash, 'user')";
                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                        {
                            NpgsqlParameter[] prms = new NpgsqlParameter[2];
                            prms[0] = new NpgsqlParameter("@login", NpgsqlTypes.NpgsqlDbType.Varchar, 200);
                            prms[0].Value = login.Text.Trim();
                            prms[1] = new NpgsqlParameter("@password_hash", NpgsqlTypes.NpgsqlDbType.Varchar, 200);
                            prms[1].Value = BCrypt.Net.BCrypt.HashPassword(psw.Text.Trim(), 14);
                            helper.login = login.Text.Trim();
                            helper.role = "user";
                            comm.Parameters.AddRange(prms);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    NpgsqlConnection conn1 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                    conn1.Open();
                    NpgsqlCommand comm1 = new NpgsqlCommand();
                    comm1.Connection = conn1;
                    comm1.CommandType = CommandType.Text;
                    comm1.CommandText = "SELECT registereduserid, login FROM users.registereduser where login = '" + helper.login + "';";
                    NpgsqlDataReader dr1 = comm1.ExecuteReader();
                    while (dr1.Read())
                    {
                        helper.iduser = Convert.ToInt32(dr1[0]);
                    }
                    conn1.Close();
                    Response.Redirect("~/About");

                }
            }
            

        }
    }
}