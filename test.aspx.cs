using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;
using System.Net;
using System.Text;
using System.IO;

public partial class test : System.Web.UI.Page
{
    EngineClient EngClient;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        EngClient = Authentication.GetEngineClient();

        // GridView1.DataSource = EngClient.GetAttendees("2ea2e9ea-06a6-4362-97c8-8635c0cbb9ae");
        // GridView1.DataBind();
        string EventId = "2d5d649d-902f-4f02-ada4-b0a6c44d1293";
        string uid = "a0c078ae-56dc-e411-b23b-6c3be5a872c4";

        List<MobileMeetingProject.EsEngine.MeetingParticipant> MeetingParticipants = this.Session["MeetingParticipants" + Request.QueryString["UId"] + HttpContext.Current.User.Identity.Name] as List<MobileMeetingProject.EsEngine.MeetingParticipant>;
        //GridView2.DataSource 
        List<MobileMeetingProject.EsEngine.Meeting> Meetings = EngClient.GetAttendeeMeetings(uid, EventId.ToString()).ToList();


        List<MobileMeetingProject.EsEngine.MeetingParticipant> ParticipantAll = (from a in GetAttendeeMeetings1(uid, EventId.ToString()) orderby a.Name select a).ToList();


        MeetingParticipants = (from a in ParticipantAll orderby a.CompanyName select a).ToList();

        GridView1.DataSource = MeetingParticipants;
        GridView1.DataBind();

        

        
          
         

    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string addmeeting(string meetingslotid, string delid)
    {
     

        string apiUrl = "http://crm.naseba.com/enginerest.svc/web/AddAdditionalMeeting?AttendeeWantId=a0c078ae-56dc-e411-b23b-6c3be5a872c4&AttendeeToId=ab0329c9-dce8-e411-b23b-6c3be5a88b80&EventId=2d5d649d-902f-4f02-ada4-b0a6c44d1293&MeetingSlotId=83056&MeetingBy=7Vendor%27&IsPrimary=false&MeetingStatusId=5&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";
       // string apiUrl = "http://crm.naseba.com/enginerest.svc/web/AddAdditionalMeeting?AttendeeWantId =" + Globalvenid + "&AttendeeToId =" + delid + "&EventId =" + GlobalEventid + "&MeetingSlotId =" + Convert.ToInt32(meetingslotid.ToString()) + "&MeetingBy ='Vendor'&IsPrimary =false&MeetingStatusId =5&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";

        Uri address = new Uri(apiUrl);

        // Create the web request 
        HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

        // Set type to POST 
        request.Method = "GET";
        request.ContentType = "text/xml";

        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            // Get the response stream 
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Console application output 
            string strOutputXml = reader.ReadToEnd();
            //Response.Write(strOutputXml);
            return strOutputXml;
        }
   }
  

    public static List<MobileMeetingProject.EsEngine.MeetingParticipant> GetAttendeeMeetings1(string AttendeeId, string EventId)
    {
        //List<MeetingParticipant> MeetingsParticipants = HttpContext.Current.Cache["Meeting" + AttendeeId + EventId] as List<MeetingParticipant>;
        List<MobileMeetingProject.EsEngine.MeetingParticipant> MeetingsParticipants = HttpContext.Current.Session["Meeting" + AttendeeId + EventId] as List<MobileMeetingProject.EsEngine.MeetingParticipant>;


        if (MeetingsParticipants == null)
        {
            EngineClient EngClient = Authentication.GetEngineClient();
            List<MobileMeetingProject.EsEngine.Meeting> MeetingsList = EngClient.GetAttendeeMeetings(AttendeeId, EventId).ToList();

            if (MeetingsList != null && MeetingsList.Count > 0)
            {
                List<MobileMeetingProject.EsEngine.Attendee> AttendeesList = GetAttendees(EventId);
                MeetingsParticipants = new List<MobileMeetingProject.EsEngine.MeetingParticipant>();

                foreach (MobileMeetingProject.EsEngine.Meeting TempMeeting in MeetingsList)
                {
                    if (TempMeeting.AttendeeWantId == AttendeeId)
                    {
                        MeetingsParticipants.Add(GetMeetingParticipant((from a in AttendeesList where a.Id == TempMeeting.AttendeeToId select a).FirstOrDefault(), TempMeeting));
                    }
                    else
                    {
                        MeetingsParticipants.Add(GetMeetingParticipant((from a in AttendeesList where a.Id == TempMeeting.AttendeeWantId select a).FirstOrDefault(), TempMeeting));
                    }
                }

                if (MeetingsParticipants != null && MeetingsParticipants.Count > 0)
                {
                    //HttpContext.Current.Cache.Insert("Meeting" + AttendeeId + EventId, MeetingsParticipants, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                    HttpContext.Current.Session["Meeting" + AttendeeId + EventId] = MeetingsParticipants;
                }
            }
        }

        return MeetingsParticipants == null ? new List<MobileMeetingProject.EsEngine.MeetingParticipant>() : BinaryClone<List<MobileMeetingProject.EsEngine.MeetingParticipant>>(MeetingsParticipants);
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

    private static MobileMeetingProject.EsEngine.MeetingParticipant GetMeetingParticipant(MobileMeetingProject.EsEngine.Attendee ThisAttendee, MobileMeetingProject.EsEngine.Meeting ThisMeeting)
    {
        MobileMeetingProject.EsEngine.MeetingParticipant TempMeetingParticipant = new MobileMeetingProject.EsEngine.MeetingParticipant();

        if (ThisAttendee != null)
        {
            TempMeetingParticipant.Id = ThisAttendee.Id;
            TempMeetingParticipant.ContactId = ThisAttendee.ContactId;
            TempMeetingParticipant.Title = ThisAttendee.Title;
            TempMeetingParticipant.Name = ThisAttendee.Name;
            TempMeetingParticipant.CompanyId = ThisAttendee.CompanyId;
            TempMeetingParticipant.CompanyName = ThisAttendee.CompanyName;
            TempMeetingParticipant.CompanyWebsite = ThisAttendee.CompanyWebsite;
            TempMeetingParticipant.CompanyProfile = ThisAttendee.CompanyProfile;
            TempMeetingParticipant.Industry = ThisAttendee.Industry;
            TempMeetingParticipant.Region = ThisAttendee.Region;
            TempMeetingParticipant.CaseStudyHTML = ThisAttendee.CaseStudyHTML;
            TempMeetingParticipant.Gender = ThisAttendee.Gender;
            TempMeetingParticipant.HasMeetings = ThisAttendee.HasMeetings;
            TempMeetingParticipant.Weight = ThisAttendee.Weight;
            TempMeetingParticipant.BadgeNumber = ThisAttendee.BadgeNumber;
            TempMeetingParticipant.Role = ThisAttendee.Role;
            TempMeetingParticipant.Status = ThisAttendee.Status;
            TempMeetingParticipant.Email = ThisAttendee.Email;
            TempMeetingParticipant.Phone = ThisAttendee.Phone;
            TempMeetingParticipant.Fax = ThisAttendee.Fax;
            TempMeetingParticipant.Mobile = ThisAttendee.Mobile;
            TempMeetingParticipant.Password = ThisAttendee.Password;
            TempMeetingParticipant.AvailableFrom = ThisAttendee.AvailableFrom;
            TempMeetingParticipant.AvailableTo = ThisAttendee.AvailableTo;
            TempMeetingParticipant.LastModifiedDate = ThisAttendee.LastModifiedDate;
            TempMeetingParticipant.EmailAlertType = ThisAttendee.EmailAlertType;
            TempMeetingParticipant.SmsBeforeMeeting = ThisAttendee.SmsBeforeMeeting;
            TempMeetingParticipant.AllocatedTable = ThisAttendee.AllocatedTable;
            TempMeetingParticipant.JobTitle = ThisAttendee.JobTitle;
            TempMeetingParticipant.AttendeeNote = ThisAttendee.AttendeeNote;
            TempMeetingParticipant.PassedWizard = ThisAttendee.PassedWizard;
            TempMeetingParticipant.Day1 = ThisAttendee.Day1;
            TempMeetingParticipant.Day2 = ThisAttendee.Day2;
            TempMeetingParticipant.Day3 = ThisAttendee.Day3;
            TempMeetingParticipant.AccommodationIncluded = ThisAttendee.AccommodationIncluded;
            TempMeetingParticipant.IsImportant = ThisAttendee.IsImportant;
            TempMeetingParticipant.IsWalkin = ThisAttendee.IsWalkin;
        }

        if (ThisMeeting != null)
        {
            TempMeetingParticipant.MeetingId = ThisMeeting.MeetingId;
            TempMeetingParticipant.AttendeeWantId = ThisMeeting.AttendeeWantId;
            TempMeetingParticipant.AttendeeToId = ThisMeeting.AttendeeToId;
            TempMeetingParticipant.EventId = ThisMeeting.EventId;
            TempMeetingParticipant.MeetingSlotId = ThisMeeting.MeetingSlotId;
            TempMeetingParticipant.MeetingSlotStartTime = ThisMeeting.MeetingSlotStartTime;
            TempMeetingParticipant.MeetingSlotEndTime = ThisMeeting.MeetingSlotEndTime;
            TempMeetingParticipant.TableName = ThisMeeting.TableName;
            TempMeetingParticipant.MeetingBy = ThisMeeting.MeetingBy;
            TempMeetingParticipant.SessionId = ThisMeeting.SessionId;
            TempMeetingParticipant.AttendeeWantSmsSent = ThisMeeting.AttendeeWantSmsSent;
            TempMeetingParticipant.AttendeeToSmsSent = ThisMeeting.AttendeeToSmsSent;
            TempMeetingParticipant.IsPrimary = ThisMeeting.IsPrimary;
            TempMeetingParticipant.MeetingStatusId = ThisMeeting.MeetingStatusId;
            TempMeetingParticipant.CreatedDate = ThisMeeting.CreatedDate;
        }

        return TempMeetingParticipant;
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


    public MobileMeetingProject.EsEngine.Attendee GetAttndeeByAttendeeId(string AttendeeId, string EventId)
    {
        EngClient = Authentication.GetEngineClient();
        return (from a in EngClient.GetAttendees(EventId) where a.Id == AttendeeId select a).FirstOrDefault();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        EngineClient EngClient1 = Authentication.GetEngineClient();
       

        
        string apiUrl = "http://crm.naseba.com/enginerest.svc/web/UpdateMeetingStatus?MeetingId=41266&MeetingStatusId=4&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";

        Uri address = new Uri(apiUrl);

        // Create the web request 
        HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

        // Set type to POST 
        request.Method = "GET";
        request.ContentType = "text/xml";

        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            // Get the response stream 
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Console application output 
            string strOutputXml = reader.ReadToEnd();
            Response.Write(strOutputXml);
        }

       // EngClient1.UpdateMeetingStatus(41266, 4);
    }
}