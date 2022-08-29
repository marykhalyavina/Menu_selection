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
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Insertbutton_Click(object sender, EventArgs e)
        {
            double act = 1.0;
            string activ = "";
            if (radio11.Selected) { act = 1.2; activ = "Extremely_inactive"; }
            else if (radio12.Selected) { act = 1.375; activ = "Sedentary"; }
            else if (radio13.Selected) { act = 1.55; activ = "Moderately_active"; }
            else if (radio14.Selected) { act = 1.725; activ = "Vigorously_active"; }
            else if (radio15.Selected) { act = 1.9; activ = "Extremely_active"; }
            double goal = 1.0;
            string goals = "";
            if (radio21.Selected) { goal = 1.0; goals = "Maintain_weight"; }
            else if (radio22.Selected) { goal = 0.9; goals = "Mild_weight_loss"; }
            else if (radio23.Selected) { goal = 0.8; goals = "Weight_loss"; }
            else if (radio24.Selected) { goal = 1.1; goals = "Mild_weight_gain"; }
            else if (radio25.Selected) { goal = 1.2; goals = "Weight_gain"; }
            string sex;
            if (radiow.Selected)
            {//женщина
                helper.calories = helper.Getcalories("female", Convert.ToDateTime(date.Text), Convert.ToDouble(weight.Text), Convert.ToDouble(height.Text), act, goal);
                sex = "female";
            }
            else
            {
                helper.calories = helper.Getcalories("male", Convert.ToDateTime(date.Text), Convert.ToDouble(weight.Text), Convert.ToDouble(height.Text), act, goal);
                sex = "male";
            }
            string s1 = "SELECT dailycalories FROM users.registereduser where registereduserid = " + helper.iduser;
            NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect.Open();
            NpgsqlCommand comman = new NpgsqlCommand();
            comman.Connection = connect;
            comman.CommandType = CommandType.Text;
            comman.CommandText = s1;
            NpgsqlDataReader dr = comman.ExecuteReader();
            bool b = true;
            while (b && dr.Read())
            {
                if (Convert.ToBoolean(dr[0]))
                {
                    Label3.Text = "Ошибка. Данные уже существуют";
                    connect.Close();
                    b = false;
                }
                else
                {
                    connect.Close();
                    string sql2 = "Update users.registereduser set dailycalories=true where registereduserid = " + helper.iduser;
                    string connectionString = "Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ";
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        string sql = "INSERT INTO userdata.dailycalorierequirement (username, sex, dateofbirth, weight, height, physicalactivity, goal, idhuman) VALUES " + "(@username, @sex, @dateofbirth, @weight, @height, @physicalactivity, @goal, @idhuman)";

                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                        {
                            NpgsqlParameter[] prms = new NpgsqlParameter[8];
                            prms[0] = new NpgsqlParameter("@username", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                            prms[0].Value = name.Text.Trim();
                            prms[1] = new NpgsqlParameter("@sex", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[1].Value = sex;
                            prms[2] = new NpgsqlParameter("@dateofbirth", NpgsqlTypes.NpgsqlDbType.Date);
                            prms[2].Value = Convert.ToDateTime(date.Text);
                            prms[3] = new NpgsqlParameter("@weight", NpgsqlTypes.NpgsqlDbType.Numeric);
                            prms[3].Value = Math.Round(Convert.ToDouble(weight.Text), 1);
                            prms[4] = new NpgsqlParameter("@height", NpgsqlTypes.NpgsqlDbType.Numeric);
                            prms[4].Value = Math.Round(Convert.ToDouble(height.Text), 1); ;
                            prms[5] = new NpgsqlParameter("@physicalactivity", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[5].Value = activ;
                            prms[6] = new NpgsqlParameter("@goal", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[6].Value = goals;
                            prms[7] = new NpgsqlParameter("@idhuman", NpgsqlTypes.NpgsqlDbType.Integer);
                            prms[7].Value = helper.iduser;
                            comm.Parameters.AddRange(prms);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();
                        }
                        
                    }
                    NpgsqlConnection connect2 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;");
                    NpgsqlCommand comman2 = new NpgsqlCommand();
                    comman2.Connection = connect2;
                    comman2.CommandType = CommandType.Text;
                    comman2.CommandText = sql2;
                    connect2.Open();
                    comman2.ExecuteNonQuery();
                    connect2.Close();
                    //аллергии
                    List<int> al = new List<int>();
                    foreach (ListItem li in CheckboxList1.Items)
                    {
                        if (li.Selected)
                            al.Add(Convert.ToInt32(li.Value));
                    }
                    for (int i = 0; i < al.Count; i++)
                    {
                        using (NpgsqlConnection connect3 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;"))
                        {
                            string sql3 = "INSERT INTO userdata.allergeninthehuman (idhuman, idallergen) VALUES " + "(@idhuman, @idallergen)";

                            using (NpgsqlCommand comm = new NpgsqlCommand(sql3, connect3))
                            {
                                NpgsqlParameter[] prms1 = new NpgsqlParameter[2];
                                prms1[0] = new NpgsqlParameter("@idhuman", NpgsqlTypes.NpgsqlDbType.Integer);
                                prms1[0].Value = helper.iduser;
                                prms1[1] = new NpgsqlParameter("@idallergen", NpgsqlTypes.NpgsqlDbType.Integer);
                                prms1[1].Value = al[i];
                                comm.Parameters.AddRange(prms1);
                                connect3.Open();
                                comm.ExecuteNonQuery();
                                connect3.Close();
                            }

                        }
                    }
                    Response.Redirect("~/Select");
                }
            }

        }
        protected void updatebutton_Click(object sender, EventArgs e)
        {
            //не забыть удалить выбранное меню
            NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE users.registereduser SET usermenu = false WHERE registereduserid = " + Convert.ToString(helper.iduser);
            cmd.ExecuteNonQuery();
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
            double act = 1.0;
            string activ = "";
            if (radio11.Selected) { act = 1.2; activ = "Extremely_inactive"; }
            else if (radio12.Selected) { act = 1.375; activ = "Sedentary"; }
            else if (radio13.Selected) { act = 1.55; activ = "Moderately_active"; }
            else if (radio14.Selected) { act = 1.725; activ = "Vigorously_active"; }
            else if (radio15.Selected) { act = 1.9; activ = "Extremely_active"; }
            double goal = 1.0;
            string goals = "";
            if (radio21.Selected) { goal = 1.0; goals = "Maintain_weight"; }
            else if (radio22.Selected) { goal = 0.9; goals = "Mild_weight_loss"; }
            else if (radio23.Selected) { goal = 0.8; goals = "Weight_loss"; }
            else if (radio24.Selected) { goal = 1.1; goals = "Mild_weight_gain"; }
            else if (radio25.Selected) { goal = 1.2; goals = "Weight_gain"; }
            string sex;
            if (radiow.Selected)
            {//женщина
                helper.calories = helper.Getcalories("female", Convert.ToDateTime(date.Text), Convert.ToDouble(weight.Text), Convert.ToDouble(height.Text), act, goal);
                sex = "female";
            }
            else
            {
                helper.calories = helper.Getcalories("male", Convert.ToDateTime(date.Text), Convert.ToDouble(weight.Text), Convert.ToDouble(height.Text), act, goal);
                sex = "male";
            }
            string s1 = "SELECT dailycalories FROM users.registereduser where registereduserid = " + helper.iduser;
            NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
            connect.Open();
            NpgsqlCommand comman = new NpgsqlCommand();
            comman.Connection = connect;
            comman.CommandType = CommandType.Text;
            comman.CommandText = s1;
            NpgsqlDataReader dr = comman.ExecuteReader();
            bool b = true;
            while (b && dr.Read())
            {
                if (!Convert.ToBoolean(dr[0]))
                {
                    Label3.Text = "Ошибка. Данные не найлены.";
                    connect.Close();
                    b = false;
                }
                else
                {
                    connect.Close();
                    string sql2 = "Update users.registereduser set dailycalories=true where registereduserid = " + helper.iduser;
                    string connectionString1 = "Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ";
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString1))
                    {
                        string sql = "UPDATE userdata.dailycalorierequirement set username = @username, sex = @sex, dateofbirth = @dateofbirth, weight = @weight, height = @height, physicalactivity =  @physicalactivity, goal = @goal  where idhuman = " + helper.iduser;

                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                        {
                            NpgsqlParameter[] prms = new NpgsqlParameter[7];
                            prms[0] = new NpgsqlParameter("@username", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                            prms[0].Value = name.Text.Trim();
                            prms[1] = new NpgsqlParameter("@sex", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[1].Value = sex;
                            prms[2] = new NpgsqlParameter("@dateofbirth", NpgsqlTypes.NpgsqlDbType.Date);
                            prms[2].Value = Convert.ToDateTime(date.Text);
                            prms[3] = new NpgsqlParameter("@weight", NpgsqlTypes.NpgsqlDbType.Numeric);
                            prms[3].Value = Math.Round(Convert.ToDouble(weight.Text), 1);
                            prms[4] = new NpgsqlParameter("@height", NpgsqlTypes.NpgsqlDbType.Numeric);
                            prms[4].Value = Math.Round(Convert.ToDouble(height.Text), 1); ;
                            prms[5] = new NpgsqlParameter("@physicalactivity", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[5].Value = activ;
                            prms[6] = new NpgsqlParameter("@goal", NpgsqlTypes.NpgsqlDbType.Unknown);
                            prms[6].Value = goals;
                            comm.Parameters.AddRange(prms);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    NpgsqlConnection connect2 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;");
                    NpgsqlCommand comman2 = new NpgsqlCommand();
                    comman2.Connection = connect2;
                    comman2.CommandType = CommandType.Text;
                    comman2.CommandText = sql2;
                    connect2.Open();
                    comman2.ExecuteNonQuery();
                    connect2.Close();
                    //аллергии
                    List<int> al = new List<int>();
                    foreach (ListItem li in CheckboxList1.Items)
                    {
                        if (li.Selected)
                            al.Add(Convert.ToInt32(li.Value));
                    }
                    using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;"))
                    {
                        string sql = "Delete from userdata.allergeninthehuman where idhuman = " + Convert.ToString(helper.iduser);

                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, conn))
                        {
                            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                            conn.Open();
                            command.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    for (int i = 0; i < al.Count; i++)
                    {
                        using (NpgsqlConnection connect3 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;"))
                        {
                            string sql3 = "INSERT INTO userdata.allergeninthehuman (idhuman, idallergen) VALUES " + "(@idhuman, @idallergen)";

                            using (NpgsqlCommand comm = new NpgsqlCommand(sql3, connect3))
                            {
                                NpgsqlParameter[] prms1 = new NpgsqlParameter[2];
                                prms1[0] = new NpgsqlParameter("@idhuman", NpgsqlTypes.NpgsqlDbType.Integer);
                                prms1[0].Value = helper.iduser;
                                prms1[1] = new NpgsqlParameter("@idallergen", NpgsqlTypes.NpgsqlDbType.Integer);
                                prms1[1].Value = al[i];
                                comm.Parameters.AddRange(prms1);
                                connect3.Open();
                                comm.ExecuteNonQuery();
                                connect3.Close();
                            }

                        }
                    }
                    Response.Redirect("~/Select");
                }
            }
        }
    }
}