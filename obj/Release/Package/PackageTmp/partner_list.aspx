<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="partner_list" Codebehind="partner_list.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       
    <div class="maintextright" id="users">
      <div class="maintextright_left">
        <div class="mtrl_search">
          <input name="company_search" type="text" placeholder="Search company or individual" id="company_search" class="searchbox search" />
          <div class="close_serch"></div>
        </div>
        <div class="mtrl_attendance">
          <div class="toggle">
            <!-- Toggle Link -->
            <a href="#" title="Title of Toggle" class="toggle-trigger">Attendance</a>
            <!-- Toggle Content to display -->
            <div class="toggle-content">
              <input type="checkbox" name="checkboxG21" id="checkboxG21" class="css-checkbox" />
              <label for="checkboxG21" class="css-label">Day 1</label>
              <input type="checkbox" name="checkboxG22" id="checkboxG22" class="css-checkbox" />
              <label for="checkboxG22" class="css-label">Day 2</label>
            </div>
            <!-- .toggle-content (end) -->
          </div>
          <div class="toggle">
            <!-- Toggle Link -->
            <a href="#" title="Title of Toggle" class="toggle-trigger">Meetings</a>
            <!-- Toggle Content to display -->
            <div class="toggle-content">
              <input type="checkbox" name="checkboxG23" id="checkboxG23" class="css-checkbox" />
              <label for="checkboxG23" class="css-label">Day 1</label>
              <input type="checkbox" name="checkboxG24" id="checkboxG24" class="css-checkbox" />
              <label for="checkboxG24" class="css-label">Day 2</label>
              <input type="checkbox" name="checkboxG25" id="checkboxG25" class="css-checkbox" />
              <label for="checkboxG25" class="css-label">All Day</label>
            </div>
            <!-- .toggle-content (end) -->
          </div>
          <div class="toggle">
            <!-- Toggle Link -->
            <a href="#" title="Title of Toggle" class="toggle-trigger">Meetings Attended</a>
            <!-- Toggle Content to display -->
            <div class="toggle-content">
              <input class="range-slider" type="hidden" value="25,75"/>
            </div>
            <!-- .toggle-content (end) -->
          </div>
        </div>
      </div>
      <div class="maintextright_right alllisttable">
        <div class="listtable listtabletop ">
          <div class="ltbox6"><a href="#"></a></div>
          <div class="ltbox1"><a href="#">table#</a></div>
          <div class="ltbox2"><a href="#">Company</a></div>
          <div class="ltbox3"><a href="#">representative</a></div>
          <div class="ltbox4">
            <div class="ltbox4top">scheduled<br />
              meetings</div>
            <div class="ltbox4bottom">
              <div class="ltbox4left"><a href="#">day1</a></div>
              <div class="ltbox4right"><a href="#">day2</a></div>
            </div>
          </div>
          <div class="ltbox5">
            <div class="ltbox4top">meetings<br />
              attended</div>
            <div class="ltbox4bottom">
              <div class="ltbox4left"><a href="#">day1</a></div>
              <div class="ltbox4right"><a href="#">day2</a></div>
            </div>
          </div>
        </div>
          
          
        <div class="vtablelist list">
           <% Response.Write(list); %>
             
          </div>
        </div>
          
          

      </div>




              <asp:GridView ID="GridView1" Visible="false" runat="server" CssClass="vtablelist" AutoGenerateColumns="False" OnLoad="GridView1_Load" AllowSorting="True">
                  <Columns>
                      <asp:BoundField HeaderText="Role" DataField="Role" />
                      <asp:BoundField  DataField="AllocatedTable" />
                      <asp:BoundField HeaderText="Name" DataField="Name" />
                      <asp:BoundField HeaderText="Company" DataField="CompanyName" />
                      <asp:BoundField HeaderText="Job Title" DataField="JobTitle" />
                      <asp:BoundField HeaderText="Day1" DataField="Day1" />
                      <asp:BoundField HeaderText="Day2" DataField="Day2" />
                      <asp:BoundField HeaderText="Day3" DataField="Day3" />
                      <asp:BoundField HeaderText="CompanyId" DataField="CompanyId" />
                      <asp:BoundField HeaderText="Id" DataField="Id" />
                      <asp:BoundField />
                  </Columns>


              </asp:GridView>
                  
</asp:Content>

