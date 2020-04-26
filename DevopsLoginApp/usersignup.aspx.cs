using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevOpsLoginApp
{
    public partial class usersignup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
                string username = TextBox8.Text;
                string password = TextBox9.Text;

                Response.Redirect("userlogin.aspx");
        }
    }
}