using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;

public partial class login : System.Web.UI.Page
{
    private string SFUsername = string.Empty;
    private string SFPassword = string.Empty;
    private string Domain = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["status"] != null)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
        }
          
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            SFUsername = string.Empty;
            SFPassword = string.Empty;
            Domain = string.Empty;
            try
            {
                EngineClient EngClient = Authentication.GetEngineClient();
                SFUsername = username.Text.Trim();
                SFPassword = pass.Text.Trim();
                //Domain = SFUsername.Split(new char[] { '@' })[1].ToString();
                //SFUsername = SFUsername.Split(new char[] { '@' })[0].ToString();
                //Domain = Domain.Split(new char[] { '.' })[0].ToString();
                EngClient = new EngineClient();
                MobileMeetingProject.EsEngine.MyLoginResult CurrentUser = EngClient.Login(username.Text.Trim(), pass.Text.Trim());
                // CurrentUser = EngClient.Login(SFUsername, SFPassword);

                if (CurrentUser.OpResult.Success)
                {
                    HttpCookie ContactCookie = new HttpCookie("ContactInfo");
                    ContactCookie["ContactTitle"] = CurrentUser.UserId.ToString();
                    ContactCookie["ContactName"] = CurrentUser.UserName.ToString();
                    ContactCookie.Expires = DateTime.Now.AddMonths(1);

                    Response.Cookies.Add(ContactCookie);

                    //FormsAuthentication.RedirectFromLoginPage(CurrentUser.UserId.ToString(), false);
                    //FormsAuthentication.RedirectFromLoginPage("bc705759-0855-e411-a06d-6c3be5a82838", false);
                    Response.Redirect("Selection.aspx");
                    /* if (chkbox_remeberme.Checked)
                     {
                         HttpCookie SchedulerLoginCookie = new HttpCookie("SchedulerLoginCookie");
                         SchedulerLoginCookie["Username"] = txt_username.Text.Trim();
                         SchedulerLoginCookie["Password"] = Params.Encrypt(txt_password.Text.Trim());
                         SchedulerLoginCookie.Expires = DateTime.Now.AddDays(7);

                         Response.Cookies.Add(SchedulerLoginCookie);
                     }
                     else
                     {
                         HttpCookie SchedulerLoginCookie = Request.Cookies.Get("SchedulerLoginCookie");
                         if (SchedulerLoginCookie != null)
                         {
                             SchedulerLoginCookie.Expires = DateTime.Now.AddYears(-1);
                             Response.Cookies.Add(SchedulerLoginCookie);
                         }
                     }

                     /*if (Request.QueryString["returnurl"] != null && Request.QueryString["returnurl"].Trim() != string.Empty)
                     {
                         Response.Redirect(Request.QueryString["returnurl"]);
                     }
                     else
                     {
                         Response.Redirect("~/Pages/Welcome.aspx");
                     }*/
                }
                else
                {
                    //lbl_msg.Text = "Authentication failed: invalid username or password";

                }
            }
            catch (Exception ex)
            {
                //lbl_msg.Text = "Authentication failed: " + ex.Message;
            }
        }
    }

  
}