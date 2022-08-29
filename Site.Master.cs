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
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (helper.role == "admin")
            {
                hrefmymenu.Visible = false;
                hrefselect.Visible = false;
                hrefabout.Visible = false;
                hrefcontact.Visible = false;
            }
            else if (helper.role == "unreg")
            {
                hrefmymenu.Visible = false;
                hrefselect.Visible = false;
                hrefabout.Visible = false;
                hrefexit.Visible = false;
            }
            else
            {
                hrefcontact.Visible = false;
                if (!helper.dailycalories)
                {
                    hrefmymenu.Visible = false;
                    hrefselect.Visible = false;
                }
                if (helper.mymenu == -1)
                {
                    hrefmymenu.Visible = false;
                }
            }
        }
    }
}