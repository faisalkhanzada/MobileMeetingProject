using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TempAttendees
/// </summary>
public class TempAttendees
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyId { get; set; }
    public string JobTitle { get; set; }
    public string CompanyWebsite { get; set; }
    public string AllocatedTable { get; set; }
    public string Role { get; set; }
    public bool Day1 { get; set; }
    public bool Day2 { get; set; }
    public bool Day3 { get; set; }


    public TempAttendees(string Id, string Name, string CompanyName, string JobTitle, string CompanyWebsite, string AllocatedTable, string Role, bool Day1, bool Day2, bool Day3, string CompanyId)
    {
        this.Id = Id;
        this.Name = Name;
        this.CompanyName = CompanyName;
        this.JobTitle = JobTitle;
        this.CompanyWebsite = CompanyWebsite;
        this.AllocatedTable = AllocatedTable;
        this.Role = Role;
        this.Day1 = Day1;
        this.Day2 = Day2;
        this.Day3 = Day3;
        this.CompanyId = CompanyId;
    
    }
}