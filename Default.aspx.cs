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

namespace database
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (helper.role != "user") b1.Visible = false;
            else b1.Visible = true;
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper.idmenu = (int)GridView1.SelectedDataKey.Values["menusid"];
            Response.Redirect("~/Description");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Insertmenu");
        }
    }
}