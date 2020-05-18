using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblErrorMessage.Text = "Userid or password could not be Empty.";

            }
            else
            {
                if (username == "admin" & password == "admin")
                {
                    lblErrorMessage.Text = "successfull login redirect to home page";
                    Response.Redirect("Devops.aspx");
                }
                else
                {
                    lblErrorMessage.Text = "wrong credentials";

                }
            }

        }
    }
}