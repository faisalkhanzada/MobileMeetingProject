using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;
using System.Web.Services;
using System.Threading;
using System.Globalization;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;

public partial class Meeeting_details : System.Web.UI.Page
{
    string EventId = string.Empty;
    string userName = string.Empty;
    string ID = string.Empty;
    public string personName = string.Empty;
    string role = string.Empty;

    
    public List<TempAttendees> AllAttendees;
    public string statusofday1 = "", statusofday2 = "", persontype = "", statusDelofday1 = "", statusDelofday2="";
    public String Html = "";
    public string slothtml = "";
    EngineClient EngClient;
    public string delhtml="";
    public static string GlobalEventid = "";
    public static string Globalvenid = ""; 
    
    


    protected void Page_Load(object sender, EventArgs e)
    {

        EngClient = Authentication.GetEngineClient();
        
        if (Request.QueryString["EvtId"] != null)
            EventId = Request.QueryString["EvtId"];

        if (Request.Cookies["EventID"] != null)
            EventId = Request.Cookies["EventID"].Value;

        if (Request.QueryString["attID"] != null)
            ID = Request.QueryString["attID"];

        GlobalEventid = EventId;
        Globalvenid = ID;
        

       // ID = "39642dd6-97e2-e411-8740-6c3be5a8017c";
        
        //for temporary
      //  EventId = "2d5d649d-902f-4f02-ada4-b0a6c44d1293";

        if (!IsPostBack)
        {
            if (ID.Length > 0)
            {
               // string uid = "39642dd6-97e2-e411-8740-6c3be5a8017c";

                List<MobileMeetingProject.EsEngine.MeetingParticipant> MeetingParticipants = this.Session["MeetingParticipants" + Request.QueryString["UId"] + HttpContext.Current.User.Identity.Name] as List<MobileMeetingProject.EsEngine.MeetingParticipant>;
                //GridView2.DataSource 
                List<MobileMeetingProject.EsEngine.Meeting> Meetings = EngClient.GetAttendeeMeetings(ID, EventId.ToString()).ToList();


                List<MobileMeetingProject.EsEngine.MeetingParticipant> ParticipantAll = (from a in GetAttendeeMeetings1(ID, EventId.ToString()) orderby a.Name select a).ToList();


                MeetingParticipants = (from a in ParticipantAll orderby a.MeetingId select a).ToList();

                List<MobileMeetingProject.EsEngine.MeetingParticipant> distinctPeople = MeetingParticipants.GroupBy(p => p.MeetingSlotId).Select(g => g.First()).ToList();

                //List<MobileMeetingProject.EsEngine.MeetingParticipant> distinctPeople = MeetingParticipants.GroupBy(p => Convert.ToInt64(p.MeetingSlotId)).Select(g => g.First());

                //fetching delegates
                List<TempAttendees> AllAttendees = this.Session["AllAttendees" + EventId + HttpContext.Current.User.Identity.Name] as List<TempAttendees>;
                List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(EventId) orderby a.Name select a).ToList();

                List<MobileMeetingProject.EsEngine.Attendee> delegatesAttendees = EngClient.GetAttendees(EventId).ToList();
                delegatesAttendees = (from a in delegatesAttendees orderby a.Name select a).ToList();
                AllAttendees = (from a in AttendeesAll join AA in delegatesAttendees on a.Id equals AA.Id where AA.Role.StartsWith("Del") orderby AA.Name ascending select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList();
              //  AllAttendees.AddRange((from a in AttendeesAll where a.Role.StartsWith("Del") orderby a.Name select new TempAttendees(a.Id, a.Name, a.CompanyName, a.JobTitle, a.CompanyWebsite, a.AllocatedTable, a.Role, a.Day1, a.Day2, a.Day3, a.CompanyId)).ToList<TempAttendees>());
                this.Session["AllAttendees" + EventId + HttpContext.Current.User.Identity.Name] = AllAttendees;
                
                foreach (var item in AttendeesAll)
                {
                    
                    if (item.Day1 == true)
                    {
                        statusDelofday1 = "daytrue";
                    }
                    else
                    {
                        statusDelofday1 = "dayfalse";
                    }

                    if (item.Day2 == true)
                    {
                        statusDelofday2 = "daytrue";
                    }
                    else
                    {
                        statusDelofday2 = "dayfalse";
                    }
                    delhtml += "<li class='row delclass' id="+item.Id+"  data-day1="+statusDelofday1+" data-day2="+statusDelofday2+"><p class='am-poplist-p'><span>"+item.Name+"</span>"+item.CompanyName+"</p><p class='date'><span class=" + statusDelofday1 + ">Day 1</span><span class=" + statusDelofday2 + ">Day2</span></p><p class='am-poplist-righticon'><img src='images/right-icon.png' alt='' /></p><div class='clear'></div></li>";
                }
                
                
                GridView1.DataSource = MeetingParticipants;
                GridView1.DataBind();

                GridView2.DataSource = distinctPeople;
                GridView2.DataBind();
            }
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

    public bool day1 = false;
    public bool day2 = false;
    public bool isprimary = false;
    
    public string vencompanyname = "";
    public string roletype = "";
    public string venmobile = "";
    public string ventitle = "";
    public string allocatedtableid = "";
    public string venroletype = "";
    public string venemail = "";
    public int countisprimary=0;
    public int countbackup = 0;
    public string timingslothtml="";
    public int batchnumber = 0;

    protected void GridView1_Load(object sender, EventArgs e)
    {
        GridView1.DataBind();

       //EngClient = Authentication.GetEngineClient();
        
        List<MobileMeetingProject.EsEngine.Attendee> Attendees = EngClient.GetAttendees(EventId).ToList();
        List<MobileMeetingProject.EsEngine.Attendee> AttendeesAll = (from a in EngClient.GetAttendees(EventId) orderby a.Name select a).ToList();
        
        Attendees = (from a in Attendees where a.Id == ID orderby a.Name select a).ToList();

        //For Pop up

        //
        if (GridView2.Rows.Count > 0)
        {
            personName = Attendees[0].Name;
            role = Attendees[0].Role;
            lblpersonname.Text = Attendees[0].CompanyName;
            vencompanyname = Attendees[0].CompanyName;
            venroletype = Attendees[0].Role;
            ventitle = Attendees[0].Title;
            venemail = Attendees[0].Email;
            allocatedtableid = Attendees[0].AllocatedTable;
            batchnumber = Attendees[0].BadgeNumber;
            int timeslotid =0;

            if (role.StartsWith("Ven"))
            {
                roletype = "V";
            }

            else if (role.StartsWith("Del"))
            {
                roletype = "D";
            }

            else if (role.StartsWith("Spe"))
            {
                roletype = "S";
            }
            else if (role.StartsWith("Par"))
            {
                roletype = "P";
            }

            List<MobileMeetingProject.EsEngine.MeetingSlot> meet = EngClient.GetMeetingSlots(EventId, allocatedtableid).ToList();

            //meeting slots
            int rowiter=0;
            foreach (var item in meet)
            {
                DateTimeFormatInfo dti = new DateTimeFormatInfo();
                DateTime dateOnly = meet[rowiter].MeetingSlotStartTime;

                timingslothtml += "<li id=" + item.MeetingSlotId + " class='row t-act'><p  class='am-timelist-p'><span>" + dti.GetAbbreviatedMonthName(dateOnly.Month) + " " + dateOnly.Day + "  | </span>" + item.MeetingSlotStartTime.TimeOfDay + " - " + item.MeetingSlotEndTime.TimeOfDay + "</p><p class='am-poplist-righticon'><img src='images/right-icon.png' alt='' /></p><div class='clear'></div></li>";
                rowiter++;
            }
            int count = 0;
            string statustype = "";
            string statustypename = "";
            int counter = 0;
            int countertd = 0;
            
            
            
            foreach (GridViewRow row in GridView1.Rows)
            {
              //  day1 = Convert.ToBoolean(row.Cells[1].Text);
              //  day2 = Convert.ToBoolean(row.Cells[2].Text); 
                 //= Convert.ToBoolean(row.Cells[4].Text);
                if (count == meet.Count)
                {
                    break;
                }
                timeslotid = meet[count].MeetingSlotId;
               

                List<MobileMeetingProject.EsEngine.MeetingParticipant> ParticipantAll = (from a in GetAttendeeMeetings1(ID, EventId.ToString()) orderby a.Name select a).ToList();
                countisprimary = (from a in ParticipantAll  where a.IsPrimary == true  select a).ToList().Count;
                countbackup = (from a in ParticipantAll where a.IsPrimary == false select a).ToList().Count;
                     
                List<MobileMeetingProject.EsEngine.MeetingParticipant> AttendmeetSlot = (from a in ParticipantAll where a.MeetingSlotId == timeslotid select a).ToList();

                if (AttendmeetSlot.Count > 0)
                {
                    
                    DateTimeFormatInfo dti = new DateTimeFormatInfo();
                    DateTime dateOnly = AttendmeetSlot[0].MeetingSlotStartTime;

                    slothtml += "<div class='rightmeetingbox_date'> <img src='images/time-icon.jpg' alt='' /><p>" + AttendmeetSlot[0].MeetingSlotStartTime.TimeOfDay + "</p><span>&nbsp;</span><img src='images/date-icon.jpg' alt='' /><p>" + dti.GetAbbreviatedMonthName(dateOnly.Month) + " " + dateOnly.Day + "</p></div> <table cellpadding='0' cellspacing='0' width='100%'><tr>";

                    foreach (var slots in AttendmeetSlot)
                    {
                       
                        counter++;
                        if (slots.MeetingStatusId == 1)
                        {
                            statustype = "didnottake";
                            statustypename = "did not take Place";
                        }
                        else if (slots.MeetingStatusId == 2)
                        {
                            statustype = "tookplace";
                            statustypename = "Took Place";
                        }
                        else if (slots.MeetingStatusId == 3)
                        {
                            statustype = "scheduled";
                            statustypename = "Scheduled";
                        }
                        else if (slots.MeetingStatusId == 4)
                        {
                            statustype = "replaced";
                            statustypename = "Replaced";
                        }


                        if (slots.Day1 == true)
                        {
                            statusofday1 = "daytrue";
                        }
                        else
                        {
                            statusofday1 = "dayfalse";
                        }

                        if (slots.Day2 == true)
                        {
                            statusofday2 = "daytrue";
                        }
                        else
                        {
                            statusofday2 = "dayfalse";
                        }
                        if (isprimary == true)
                        {
                            
                            countertd++;
                            slothtml += "<td width='50%'><div width='97%' id=" + counter + " data-meetingstatustype=" + statustype + "   data-day1main=" + statusofday1 + " data-day2main=" + statusofday2 + " class='rightmeetingbox_box meetrow ' ><div class='mboxtop'><div class='mboxtop_left'><p>" + slots.BadgeNumber + "</p></div><div class='mboxtop_right'><h2>" + slots.Name + "</h2><p>" + slots.JobTitle + "," + slots.CompanyName + " </p><p class='number'>" + slots.Mobile + "</p><p class='date'><span class=" + statusofday1 + ">Day 1</span><span class=" + statusofday2 + ">Day2</span></p></div></div><div class='clear'></div><div style='cursor:pointer' class='mboxbottom " + statustype + "'><p class='show2' id=" + slots.MeetingId + " data-batchnumber=" + slots.BadgeNumber + " data-name='" + slots.Name + "' data-jobtitle='" + slots.JobTitle + "' data-company='" + slots.CompanyName + "'>" + statustypename + "</p></div></div></td>";
                        }
                        else
                        {
                            countertd++;
                            
                            slothtml += "<td width='50%'><div width='97%' id=" + counter + " data-meetingstatustype="+statustype+"  data-day1main=" + statusDelofday1 + " data-day2main=" + statusDelofday2 + " class='rightmeetingbox_box mro meetrow'><div class='mboxtop'><div class='mboxtop_left'><p>" + slots.BadgeNumber + "</p></div><div class='mboxtop_right'><h2>" + slots.Name + "</h2><p>" + slots.JobTitle + "," + slots.CompanyName + " </p><p class='number'>" + slots.Mobile + "</p><p class='date'><span class=" + statusofday1 + ">Day 1</span><span class=" + statusofday2 + ">Day2</span></p></div></div><div class='clear'></div><div style='cursor:pointer' class='mboxbottom " + statustype + "'><p class='show2' id=" + slots.MeetingId + " data-batchnumber=" + slots.BadgeNumber + " data-name='" + slots.Name + "' data-jobtitle='" + slots.JobTitle + "' data-company='" + slots.CompanyName + "'>" + statustypename + "</p></div></div></td>";
                        }
                        if (counter % 2 == 0)
                        {
                            if (countertd == 2)
                            {

                                slothtml += "</tr>";

                            }
                            else
                            {
                                slothtml += "<td width='50%'></td>";
                                slothtml += "</tr>";
                            }
                            countertd = 0;

                           // slothtml += "</tr>";
                        }
                    }
                    if (counter % 2 != 0)
                    {
                        if (countertd == 2)
                        {
                          slothtml += "</tr>";

                        }
                        else
                        {
                            slothtml += "<td width='50%'></td>";
                            slothtml += "</tr>";
                        }
                        countertd = 0;
                    }
                    slothtml += "</table>";



                }
                count++;
                
            }
            


        }
        
     }

   
  

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static object GetCurrentStatus(int meetid,int statusid)
    {
       string apiUrl = "http://crm.naseba.com/enginerest.svc/web/UpdateMeetingStatus?MeetingId="+meetid+"&MeetingStatusId="+statusid+"&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";


       Uri address = new Uri(apiUrl);

       // Create the web request 
       HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

       // Set type to POST 
       request.Method = "GET";
       request.ContentType = "text/xml";
       //request.ContentLength = 0;


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

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string addmeeting(string meetingslotid, string delid)
    {


        // string apiUrl = "http://crm.naseba.com/enginerest.svc/web/AddAdditionalMeeting?AttendeeWantId=a0c078ae-56dc-e411-b23b-6c3be5a872c4&AttendeeToId=ab0329c9-dce8-e411-b23b-6c3be5a88b80&EventId=2d5d649d-902f-4f02-ada4-b0a6c44d1293&MeetingSlotId=83056&MeetingBy=7Vendor%27&IsPrimary=false&MeetingStatusId=5&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";
        string apiUrl = "http://crm.naseba.com/enginerest.svc/web/AddAdditionalMeeting?AttendeeWantId="+Globalvenid+"&AttendeeToId="+delid+"&EventId="+GlobalEventid+"&MeetingSlotId="+Convert.ToInt32(meetingslotid.ToString())+"&MeetingBy=Vendor&IsPrimary=false&MeetingStatusId=5&apikey=bda11d91-7ade-4da1-5555-24adfe39d177";

        Uri address = new Uri(apiUrl);

        // Create the web request 
        HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

        // Set type to POST 
        request.Method = "POST";
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
  
}