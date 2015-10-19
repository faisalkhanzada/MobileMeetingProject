<%@ Page Language="C#" AutoEventWireup="true" Inherits="Selection" Codebehind="Selection.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Naseba Meeting Updater</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="mainboby">
  <div class="mainbox">
    <div class="nasebalogo"><img src="images/naseba-logo.png" alt="" /></div>
    <div class="landingpage">
      <div class="landingbox">
        <h1>Meeting <strong>Updater</strong></h1>
          <form runat="server" >
        <div class="styled-select">
            <asp:DropDownList ID="year"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="year_SelectedIndexChanged" Height="32px" Width="530px"></asp:DropDownList>
          <%--<select id="year">
            <option>Select Year</option>
            <option value="2015">2015</option>
            <option value="2014">2014</option>
            <option value="2013">2013</option>
            <option value="2012">2012</option>
            <option value="2011">2011</option>
            <option value="2010">2010</option>
            <option value="2009">2009</option>
            <option value="2008">2008</option>
            <option value="2007">2007</option>
            <option value="2006">2006</option>
            <option value="2005">2005</option>
            <option value="2004">2004</option>
            <option value="2003">2003</option>
            <option value="2002">2002</option>
            <option value="2001">2001</option>
            <option value="2000">2000</option>
          </select>--%>
      
          
        </div>
              
        <div class="styled-select">
            <asp:DropDownList ID="eventsddl" runat="server" Height="31px" Width="526px"></asp:DropDownList>
          <%--<select id="series">
            <option>Select Series</option>
            <option>Leadership</option>
            <option>Hospitality</option>
            <option>Construction &amp; Design</option>
            <option>Healthcare</option>
            <option>Infrastructure</option>
            <option>Training</option>
            <option>Food and beverage</option>
            <option>IT &amp; Telecom</option>
          </select>--%>
        </div>
        <%--<div class="styled-select">
          <select id="event">
            <option>Select Event</option>
            <option value="10th Annual Human Assets Expansion Summit MENA 2015">10th Annual Human Assets Expansion Summit MENA 2015 </option>
            <option value="9th Annual CFO Strategies Forum MENA 2015">9th Annual CFO Strategies Forum MENA 2015 </option>
            <option value="Global Women In Leadership Forum 2015">Global Women In Leadership Forum 2015</option>
          </select>
        </div>--%>
              <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click1" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="year" runat="server" ForeColor="White" Font-Size="Medium" ErrorMessage="Please select Year &"></asp:RequiredFieldValidator>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="eventsddl" runat="server" ForeColor="White" Font-Size="Medium" ErrorMessage="Event."></asp:RequiredFieldValidator>
        <%--<input name="Submit" type="button" value="Submit" />--%>
              </form>
      </div>
    </div>
  </div>
</div>
</body>
</html>

