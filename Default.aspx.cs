using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileMeetingProject.com.naseba.crmstaging;
using System.ServiceModel.Description;
using MobileMeetingProject.EsEngine;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ClientCredentials cc = new ClientCredentials();        
        //Engine obj = new Engine();

        EngineClient objEngineClient = new EngineClient();
        MobileMeetingProject.EsEngine.MyLoginResult lr = objEngineClient.Login("paallavim@naseba.com", "Jhansibk5");
       
        GridView1.DataSource = objEngineClient.GetYears();
        GridView1.DataBind();


        //userid
        GridView2.DataSource = objEngineClient.GetEvents("bc705759-0855-e411-a06d-6c3be5a82838","2014");
        GridView2.DataBind();

        //eventid
        GridView3.DataSource = objEngineClient.GetAttendees("2ea2e9ea-06a6-4362-97c8-8635c0cbb9ae");
        GridView3.DataBind();


        //for (int i = 0; i <= GridView1.Rows.Count - 1; i++) 
        //{
        //    if (GridView2.Rows.Count == 0)
        //    {
        //        GridView2.DataSource = objEngineClient.GetEvents("bc705759-0855-e411-a06d-6c3be5a82838", GridView1.Rows[i].Cells[0].Text);
        //        GridView2.DataBind();
        //    }
        //    else 
        //    { 
        //        break;             
        //    }
        //}


        //GridView2.DataSource = objEngineClient.GetEvents(lr.UserId.ToString() , "2013");
        //GridView2.DataBind();
        

      
    }
}
