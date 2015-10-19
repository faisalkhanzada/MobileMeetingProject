using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;

public partial class speaker_list : System.Web.UI.Page
{
    string EventId = string.Empty;
    string userName = string.Empty;
    string ID = string.Empty;

    List<string> lst = new List<string>();
  public String list = "";
    String role = "";
    String tableno = "";
    String company = "";
    String name = "";
    String jobtitle = "";
    String day1 = "";
    String day2 = "";
    String day3 = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        EngineClient EngClient = Authentication.GetEngineClient();

        if (Request.QueryString["EvtId"] != null)
            EventId = Request.QueryString["EvtId"];

        if (Request.Cookies["EventID"] != null)
            EventId = Request.Cookies["EventID"].Value;

        //for temporary
     //   EventId = "2d5d649d-902f-4f02-ada4-b0a6c44d1293";

        if (!IsPostBack)
        {
            //List<MobileMeetingProject.EsEngine.Attendee> att = (from a in EngClient.GetAttendees(EventId) orderby a.CompanyName select a).ToList();
            //GridView1.DataSource = att;
            //GridView1.DataBind();
            ////  string a =GridView1.Rows[0].Cells[0].ToString();


            List<TempAttendees> AllAttendees = this.Session["AllAttendees" + EventId + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;
            
            List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(EventId) orderby a.Name select a).ToList();

            List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(EventId).ToList();

            Attendees = (from a in Attendees orderby a.Name select a).ToList();
            AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Spea") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3,a.CompanyId)).ToList();
            //AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Spea") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3,a.CompanyId)).ToList<TempAttendees>());
            this.Session["AllAttendees" + EventId + HttpContext.Current.User.Identity.Name] = AllAttendees;

            GridView1.DataSource = AllAttendees;
            GridView1.DataBind();
        }
    }




    protected void GridView1_Load(object sender, EventArgs e)
    {
        EngineClient EngClient = Authentication.GetEngineClient();
        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(EventId).ToList();
        string companyid = "";
        int countday1 = 0;
        int countday2 = 0;
        int hasmeetingday1 = 0;
        int hasmeetingday2 = 0;
        int counter = 0;

        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                counter++;
                companyid = row.Cells[8].Text.ToString();

                if (companyid != row.Cells[8].Text.ToString())
                {
                    countday1 = 0;
                    countday2 = 0;
                }

                if (row.Cells[0].Text.StartsWith("Spea"))
                {
                    for (int i = 0; i < GridView1.Columns.Count; i++)
                    {
                        countday1 = (from a in Attendees orderby a.CompanyName where a.Day1 == true && a.CompanyId == companyid select a).ToList().Count;
                        countday2 = (from a in Attendees orderby a.CompanyName where a.Day2 == true && a.CompanyId == companyid select a).ToList().Count;

                        hasmeetingday1 = (from a in Attendees orderby a.CompanyName where a.Day1 == true && a.HasMeetings == true && a.CompanyId == companyid select a).ToList().Count;
                        hasmeetingday2 = (from a in Attendees orderby a.CompanyName where a.Day2 == true && a.HasMeetings == true && a.CompanyId == companyid select a).ToList().Count;

                        String cellText = row.Cells[i].Text;
                        lst.Add(cellText);
                    }
                    String html = "<a id=" + counter + " class='venrow' data-attcountday1=" + countday1 + " data-attcountday2=" + countday2 + " data-meetingscountday1=" + hasmeetingday1 + " data-meetingscountday2=" + hasmeetingday2 + "  href='#' style='color:black'><div class='listtable'><div class='ltbox6'><p><img src='images/s-icon.jpg' alt='' /></p></p></div> <div class='ltbox1'> <p>" + lst[1] + "</p> </div> <div class='ltbox2'> <p>" + lst[3] + "</p> </div> <div class='ltbox3'> <p><strong>" + lst[2] + "</strong></p> </div> <div class='ltbox4'> <div class='ltbox4bottom'> <div class='ltbox4left'> <p>" + countday1 + "</p> </div> <div class='ltbox4right'> <p>" + countday2 + "</p> </div> </div> </div> <div class='ltbox5'> <div class='ltbox4bottom'> <div class='ltbox4left'> <p>" + hasmeetingday1 + "</p> </div> <div class='ltbox4right'> <p>" + hasmeetingday2 + "</p> </div> </div> </div> </div></a>";
                    list += html;
                    lst.Clear();
                }

                
            }
        }

        // Response.Write(list); %>




    }
}