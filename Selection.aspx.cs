using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;

public partial class Selection : System.Web.UI.Page
{
    public string EventId { get; set; }
    string user = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie ContactCookie = Request.Cookies.Get("ContactInfo");
        if (ContactCookie != null)
        {
            user = ContactCookie["ContactTitle"];
        }
        if (!Page.IsPostBack)
        {
            BindYears();
        }    
    }

    private void BindYears()
    {
        EngineClient EngClient = Authentication.GetEngineClient();
        year.DataSource = EngClient.GetYears();
        year.DataTextField = "display";
        year.DataValueField = "display";
        year.DataBind();
        year.Items.Insert(0, new ListItem("Select Year", ""));
    }

    protected void year_SelectedIndexChanged(object sender, EventArgs e)
    {
        EngineClient EngClient = Authentication.GetEngineClient();
        eventsddl.DataSource = EngClient.GetEvents(user, year.SelectedItem.Value);
        eventsddl.DataTextField = "display";
        eventsddl.DataValueField = "value";
        eventsddl.DataBind();
        eventsddl.Items.Insert(0, new ListItem("Select Event", ""));
    }
    

    protected void Submit_Click1(object sender, EventArgs e)
    {
        try
        {
            //this.EventId =((AddValue)eventsddl.SelectedItem.Value);

            HttpCookie eventYear = new HttpCookie("EventYear");
            eventYear.Value = year.SelectedItem.Value;
            Response.Cookies.Add(eventYear);

            HttpCookie eventsName = new HttpCookie("EventName");
            eventsName.Value = eventsddl.SelectedItem.Text;
            Response.Cookies.Add(eventsName);

            HttpCookie eventsID = new HttpCookie("EventID");
            eventsID.Value = eventsddl.SelectedItem.Value;
            Response.Cookies.Add(eventsID);

            Session["alldelegatescount"] = null;
            Session["allvendorscount"] = null;
            Session["allspeakerscount"] = null; 
            Session["allpartnerscount"] = null;
            Session["allcountcount"] = null;

            //Response.Redirect("~/Pages/MeetingListAll.aspx?yearddl=" + yearddl.SelectedItem.Value + "-" +eventsddl.SelectedItem.Value);   
            Response.Redirect("all_list.aspx?EvtId=" + eventsddl.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            Exception E = ex;
        }
    }
}