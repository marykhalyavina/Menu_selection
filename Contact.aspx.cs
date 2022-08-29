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
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            bool b = false;
            string login, password_hash;
            NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT registereduserid, login, password_hash, role, dailycalories, usermenu FROM users.registereduser";
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                login = dr[1].ToString();
                password_hash = dr[2].ToString();
                if ((login1.Text == login) && (BCrypt.Net.BCrypt.Verify(psw1.Text, password_hash)))
                {
                    helper.iduser = Convert.ToInt32(dr[0]);
                    helper.role = Convert.ToString(dr[3]);
                    helper.login = Convert.ToString(dr[1]);
                    helper.dailycalories = Convert.ToBoolean(dr[4]);
                    helper.usermenu = Convert.ToBoolean(dr[5]);
                    if (helper.role == "admin")
                        Response.Redirect("~/Default");
                    else if (Convert.ToBoolean(dr[4]) && Convert.ToBoolean(dr[5]))
                    {
                        helper.mymenu = 0;
                        Response.Redirect("~/mymenu");
                    }
                    else if (Convert.ToBoolean(dr[4]) && !Convert.ToBoolean(dr[5]))
                    {
                        conn.Close();
                        NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                        connect.Open();
                        NpgsqlCommand comman = new NpgsqlCommand();
                        comman.Connection = connect;
                        comman.CommandType = CommandType.Text;
                        comman.CommandText = "SELECT sex, dateofbirth, weight, height, physicalactivity, goal FROM  userdata.dailycalorierequirement where idhuman = " + helper.iduser;
                        NpgsqlDataReader dr1 = comman.ExecuteReader();
                        double act = 1.0;
                        while (dr1.Read())
                        {
                            string activ = Convert.ToString(dr1[4]);
                            if (activ == "Extremely_inactive") act = 1.2;
                            else if (activ == "Sedentary") act = 1.375;
                            else if (activ == "Moderately_active") act = 1.55;
                            else if (activ == "Vigorously_active") act = 1.725;
                            else if (activ == "Extremely_active") act = 1.9;
                            double goal = 1.0;
                            string goals = Convert.ToString(dr1[5]);
                            if (goals == "Maintain_weight") goal = 1.0;
                            else if (goals == "Mild_weight_loss") goal = 0.9;
                            else if (goals == "Weight_loss") goal = 0.8;
                            else if (goals == "Mild_weight_gain") goal = 1.1;
                            else if (goals == "Weight_gain") goal = 1.2;
                            helper.calories = helper.Getcalories(Convert.ToString(dr1[0]), Convert.ToDateTime(dr1[1]), Convert.ToDouble(dr1[2]), Convert.ToDouble(dr1[3]), act, goal);
                        }
                        Response.Redirect("~/Select");
                    }

                    else
                        Response.Redirect("~/About");
                    b = true;
                }
                login = "";
                password_hash = "";
            }
            if(!b)
                {
                    Label3.Text = "Ошибка авторизации";
                }
        }
    }
}