using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string evtid = "";
    public int countall = 0;
    public int countdelegates = 0;
    public int countvendors = 0;
    public int countspeakers = 0;
    public int countpartners = 0;
    

    EngineClient EngClient;



    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.Cookies["EventYear"] != null)
            lblyear.Text = Request.Cookies["EventYear"].Value;
        if (Request.Cookies["EventName"] != null)
            lbleventname.Text = Request.Cookies["EventName"].Value;

        if (Request.Cookies["EventID"] != null)
            evtid = Request.Cookies["EventID"].Value;

        
        ////if (HttpContext.Current.Session["alldelegatescount"] != null)
        ////{ }
        ////else
        ////{
        ////    Session["alldelegatescount"] = getdelegatescount(evtid);
        ////}

        ////if (HttpContext.Current.Session["allvendorscount"] != null)
        ////{ }
        ////else
        ////{
        ////    Session["allvendorscount"] = getvendorscount(evtid);
        ////}


        ////if (HttpContext.Current.Session["allspeakerscount"] != null)
        ////{ }
        ////else
        ////{
        ////    Session["allspeakerscount"] = getspeakerscount(evtid);
        ////}

        ////if (HttpContext.Current.Session["allpartnerscount"] != null)
        ////{ }
        ////else
        ////{
        ////    Session["allpartnerscount"] = getpartnerscount(evtid);
        ////}

        if (HttpContext.Current.Session["allcountcount"] != null)
        { }
        else
        {
            Session["allcountcount"] = getallcount();
            Session["alldelegatescount"] = countdelegates;
            Session["allvendorscount"] = countvendors;
            Session["allspeakerscount"] = countspeakers;
            Session["allpartnerscount"] = countpartners;
            
        }
    }

    public int getallcount()
    {
        //EngClient = Authentication.GetEngineClient();

        //List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(evtid) orderby a.Name select a).ToList();

        //List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();
        //Attendees = (from a in Attendees orderby a.Name select a).ToList();
        //countall = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList().Count;


        EngClient = Authentication.GetEngineClient();

        List<TempAttendees> AllAttendees = this.Session["AllAttendees" + evtid + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;
        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(evtid) orderby a.Name select a).ToList();

        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();
        
        //For Delegates
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Del") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        //AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Del") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countdelegates = AllAttendees.Count;

        //For Vendors
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Ven") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        //AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Ven") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countvendors = AllAttendees.Count;

        //For Speakers
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Spea") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        //AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Spea") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countspeakers = AllAttendees.Count;
        
        //For Partner
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Partner") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        //AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Partner") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countpartners = AllAttendees.Count;
        
        //Total Count
        countall = countdelegates + countpartners + countspeakers + countvendors;


        return countall;
    }

    public int getdelegatescount(string evtid)
    {
        EngClient = Authentication.GetEngineClient();

        List<TempAttendees> AllAttendees = this.Session["AllAttendees" + evtid + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;
        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(evtid) orderby a.Name select a).ToList();

        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Del") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Del") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countdelegates = AllAttendees.Count;
        
        return countdelegates;
    }

    public int getvendorscount(string evtid)
    {
        EngClient = Authentication.GetEngineClient();

        List<TempAttendees> AllAttendees = this.Session["AllAttendees" + evtid + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;

        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(evtid) orderby a.Name select a).ToList();

        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();

        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Ven") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Ven") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());

        countvendors = AllAttendees.Count;

        return countvendors;

    }


    private int getspeakerscount(string evtid)
    {
        EngClient = Authentication.GetEngineClient();

        List<TempAttendees> AllAttendees = this.Session["AllAttendees" + evtid + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;

        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(evtid) orderby a.Name select a).ToList();

        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();

        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Spea") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Spea") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());

        countspeakers = AllAttendees.Count;
        return countspeakers;
    }

    private int getpartnerscount(string evtid)
    {
        List<TempAttendees> AllAttendees = this.Session["AllAttendees" + evtid + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;
        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in  GetAttendees(evtid) orderby a.Name select a).ToList();

        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(evtid).ToList();
        Attendees = (from a in Attendees orderby a.Name select a).ToList();
        AllAttendees = (from a in AttendeesAll join AA in Attendees on a.Id equals AA.Id where AA.Role.StartsWith("Partner") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
        AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Partner") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
        countpartners = AllAttendees.Count;
        return countpartners;
    }

    public static List<MobileMeetingProject.EsEngine.Attendee> GetAttendees(string EventId)
    {
        //List<Attendee> AttendeesList = HttpContext.Current.Cache["EventAttendees" + EventId] as List<Attendee>;
        List<MobileMeetingProject.EsEngine.Attendee> AttendeesList = HttpContext.Current.Session["EventAttendees" + EventId] as List<MobileMeetingProject.EsEngine.Attendee>;

        if (AttendeesList == null)
        {
            // HttpContext.Current.Response.Redirect("~/pages/LoadData.aspx?EvtId=" + EventId);

            EngineClient EngClient = Authentication.GetEngineClient();
            AttendeesList = EngClient.GetAttendees(EventId).ToList();

            /*if (AttendeesList != null && AttendeesList.Count > 0)
            {
                //HttpContext.Current.Cache.Insert("EventAttendees" + EventId, AttendeesList, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                HttpContext.Current.Session["EventAttendees" + EventId] = AttendeesList;
            }*/
        }

        return AttendeesList == null ? new List<MobileMeetingProject.EsEngine.Attendee>() : BinaryClone<List<MobileMeetingProject.EsEngine.Attendee>>(AttendeesList);
    }


    public static T BinaryClone<T>(T originalObject)
    {
        using (var stream = new System.IO.MemoryStream())
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, originalObject);
            stream.Position = 0;

            return (T)binaryFormatter.Deserialize(stream);
        }
    }


    protected void LinkButtonlogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx?status=logout");
        //FormsAuthentication.SignOut();
        //Session.Abandon();

        //// clear authentication cookie
        //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        //cookie1.Expires = DateTime.Now.AddYears(-1);
        //Response.Cookies.Add(cookie1);

        //// clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
        //HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
        //cookie2.Expires = DateTime.Now.AddYears(-1);
        //Response.Cookies.Add(cookie2);

        //FormsAuthentication.RedirectToLoginPage();
    }
}
