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
    public partial class Insertmenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Insertbutton_Click(object sender, EventArgs e)
        {
            helper.idmenu = -1;
            helper.iddish = -1;
            helper.menucalories = 0;
            helper.menup = 0;
            helper.menuf = 0;
            helper.menuc = 0;
            if (name.Text.Trim() == "")
            {
                Label3.Text = "Введите название.";
            }
            else
            {
                List<int> al = new List<int>();
                foreach (ListItem li in CheckboxList1.Items)
                {
                    if (li.Selected)
                        al.Add(Convert.ToInt32(li.Value));
                }
                if (al.Count == 0)
                {
                    using (NpgsqlConnection conndel = new NpgsqlConnection("Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;"))
                    {
                        string sqldel = "Delete from food.menus where menusid = " + Convert.ToString(helper.idmenu);

                        using (NpgsqlCommand commdel = new NpgsqlCommand(sqldel, conndel))
                        {
                            conndel.Open();
                            commdel.ExecuteNonQuery();
                            conndel.Close();
                        }
                    }
                    Label3.Text = "Выберете блюда.";
                    
                }
                else if (al.Count > 9)
                {
                    Label3.Text = "Слишком много блюд. Максимальное колличество блюд в меню: 9";
                }
                else
                {
                    using (NpgsqlConnection connect = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;"))
                    {
                        string sql = "INSERT INTO food.menus (menuname, menudescription, numberofmeals, caloriccontentofmenu, proteinofmenus, fatofmenus, carbohydratesofmenus) VALUES " + "(@menuname, @menudescription, 0, 0, 0, 0, 0) returning menusid";

                        using (NpgsqlCommand comm = new NpgsqlCommand(sql, connect))
                        {
                            NpgsqlParameter[] prms = new NpgsqlParameter[2];
                            prms[0] = new NpgsqlParameter("@menuname", NpgsqlTypes.NpgsqlDbType.Varchar, 100);
                            prms[0].Value = name.Text.Trim();
                            prms[1] = new NpgsqlParameter("@menudescription", NpgsqlTypes.NpgsqlDbType.Varchar, 1000);
                            prms[1].Value = TextBox1.Text.Trim();
                            comm.Parameters.AddRange(prms);
                            connect.Open();
                            comm.ExecuteNonQuery();
                            helper.idmenu = Int32.Parse(Convert.ToString(comm.ExecuteScalar())) - 1;
                            connect.Close();
                            Label8.Text = Convert.ToString(helper.idmenu);
                        }
                    }
                    for (int i = 0; i < al.Count; i++)
                    {
                        using (NpgsqlConnection connect3 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan;"))
                        {
                            string sqlin = "INSERT INTO food.outlinesmenus (idmenus, iddish) VALUES " + "(@idmenus, @iddish)";

                            using (NpgsqlCommand comm = new NpgsqlCommand(sqlin, connect3))
                            {
                                NpgsqlParameter[] pr = new NpgsqlParameter[2];
                                pr[0] = new NpgsqlParameter("@idmenus", NpgsqlTypes.NpgsqlDbType.Integer);
                                pr[0].Value = helper.idmenu;
                                pr[1] = new NpgsqlParameter("@iddish", NpgsqlTypes.NpgsqlDbType.Integer);
                                pr[1].Value = al[i];
                                comm.Parameters.AddRange(pr);
                                connect3.Open();
                                comm.ExecuteNonQuery();
                                connect3.Close();
                            }
                        }

                        helper.iddish = al[i];
                        string s = "SELECT caloriccontentofdish, proteinofdish, fatofdish, carbohydratesofdish FROM food.dish where dishid = " + helper.iddish;
                        NpgsqlConnection connect2 = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                        connect2.Open();
                        NpgsqlCommand comman2 = new NpgsqlCommand();
                        comman2.Connection = connect2;
                        comman2.CommandType = CommandType.Text;
                        comman2.CommandText = s;
                        NpgsqlDataReader dr2 = comman2.ExecuteReader();
                        while (dr2.Read())
                        {
                            helper.menucalories += Convert.ToInt32(dr2[0]);
                            helper.menup += Convert.ToInt32(dr2[1]);
                            helper.menuf += Convert.ToInt32(dr2[2]);
                            helper.menuc += Convert.ToInt32(dr2[3]);
                        }
                        connect2.Close();
                    }
                    if (helper.menucalories < 10000 && helper.menup < 200 && helper.menuf < 200 && helper.menuc < 200)
                    {
                        NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = 5827; Port = 5432; Database = MealPlan; ");
                        connection.Open();
                        NpgsqlCommand command = new NpgsqlCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "UPDATE food.menus SET numberofmeals = " + Convert.ToString(al.Count()) + ", caloriccontentofmenu  = " + Convert.ToString(helper.menucalories)
                            + ", proteinofmenus = " + Convert.ToString(helper.menup) + ", fatofmenus =  " + Convert.ToString(helper.menuf) + ", carbohydratesofmenus = "
                            + Convert.ToString(helper.menuc) + " WHERE menusid = " + Convert.ToString(helper.idmenu);
                        command.ExecuteNonQuery();
                        connection.Close();
                        Response.Redirect("~/Default");
                    }
                    else
                    {
                        Label3.Text = "Меню не соответствует требованиям";
                        using (NpgsqlConnection conndel = new NpgsqlConnection("Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;"))
                        {
                            string sqldel = "Delete from food.outlinesmenus where idmenus = " + Convert.ToString(helper.idmenu);

                            using (NpgsqlCommand commdel = new NpgsqlCommand(sqldel, conndel))
                            {
                                conndel.Open();
                                commdel.ExecuteNonQuery();
                                conndel.Close();
                            }
                        }
                        using (NpgsqlConnection conndel2 = new NpgsqlConnection("Server=localhost;Port=5432;Database=MealPlan;User Id=postgres;Password=5827;"))
                        {
                            string sqldel2 = "Delete from food.menus where menusid = " + Convert.ToString(helper.idmenu);

                            using (NpgsqlCommand commdel2 = new NpgsqlCommand(sqldel2, conndel2))
                            {
                                conndel2.Open();
                                commdel2.ExecuteNonQuery();
                                conndel2.Close();
                            }
                        }
                    }
                }
            }
            helper.menucalories = 0;
            helper.menup = 0;
            helper.menuf = 0;
            helper.menuc = 0;
        }
    }
}