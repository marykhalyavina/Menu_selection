using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace database
{
    public partial class Exit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label2.Text = helper.login;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            helper.iduser = -1;
            helper.login = "";
            helper.role = "unreg";
            Response.Redirect("~/Default");
        }
    }
}