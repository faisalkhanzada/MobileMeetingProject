using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileMeetingProject.com.naseba.crmstaging;
using MobileMeetingProject.EsEngine;

/// <summary>
/// Summary description for Authentication
/// </summary>
public class Authentication
{
	public Authentication()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static EngineClient GetEngineClient()
    {
        if (HttpContext.Current.Session["EngClient"] == null)
        {
            EngineClient EngClient = new EngineClient();

            EngClient.ClientCredentials.UserName.UserName = "pallavim@naseba.com";
            EngClient.ClientCredentials.UserName.Password = "Jhansibk5";

            //EngClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            //EngClient.ClientCredentials.Windows.ClientCredential = new NetworkCredential(GetWindowsUsername(), GetWindowsPassword(), "naseba");
            //EngClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Identification;
            //EngClient.ClientCredentials.Windows.AllowNtlm = true;


            /*EngClient.ClientCredentials.UserName.UserName = GetWindowsUsername();
            EngClient.ClientCredentials.UserName.Password = GetWindowsPassword();
            EngClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;*/
            
            MobileMeetingProject.EsEngine.MyLoginResult LR =  EngClient.WebLogin();

            if (LR != null && LR.OpResult != null && LR.OpResult.Success)
            {
                HttpContext.Current.Session["EngClient"] = EngClient;
                return EngClient;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return (EngineClient)HttpContext.Current.Session["EngClient"];
        }
    }

}