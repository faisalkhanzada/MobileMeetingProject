<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Meeeting_details" Codebehind="Meeeting_details.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="maintextright">
      <div class="righttopmain">
        <div class="righttopmain_list"><% Response.Write(roletype); %></div>
        <div class="righttopmain_number"><% Response.Write(batchnumber.ToString()); %></div>
        <div class="righttopmain_name">
            <asp:Label ID="lblpersonname" runat="server" Text="Label"></asp:Label></div>
        <div class="righttopmain_but"><a href="#" class="show1"><% Response.Write(personName); %></a></div>
        
        <div class="righttopmain_addmeeting"><a href="#" class="show3">Add Meeting</a></div>
      </div>
      <div class="rightflitermain">
        <div class="rightflitermainleft"> <strong>Meetings on</strong>
          <input type="checkbox" name="checkboxG1" id="checkboxG1" class="css-checkbox" />
          <label for="checkboxG1" class="css-label">All</label>
          <input type="checkbox" name="checkboxG2" id="checkboxG2" class="css-checkbox" />
          <label for="checkboxG2" class="css-label">Day1</label>
          <input type="checkbox" name="checkboxG3" id="checkboxG3" class="css-checkbox" />
          <label for="checkboxG3" class="css-label">Day2</label>
        </div>
        <div class="rightflitermainleft"> <strong>Attendance on</strong>
          <input type="checkbox" name="checkboxG4" id="checkboxG4" class="css-checkbox" />
          <label for="checkboxG4" class="css-label">All</label>
          <input type="checkbox" name="checkboxG5" id="checkboxG5" class="css-checkbox" />
          <label for="checkboxG5" class="css-label">Day1</label>
          <input type="checkbox" name="checkboxG6" id="checkboxG6" class="css-checkbox" />
          <label for="checkboxG6" class="css-label">Day2</label>
        </div>
      </div>
      <div class="meetinglabelbox">
        <div class="meetinglabelbox_left">
          <p>primary meetings <% Response.Write(countisprimary); %></p>
        </div>
        <div class="meetinglabelbox_right">
          <p>backup meetings <% Response.Write(countbackup); %></p>
        </div>
      </div>
      <div class="rightmeetingbox">
     
    <div class='rightmeetingbox_mainbox'>
           <% Response.Write(slothtml); %>
      </div>      
          
        
        
      </div>
    </div>
    <div id="pop1" class="simplePopup vpbox">
  <div class="vpboxheadding"><% Response.Write(personName); %></div>
  <div id="tabs">
    <ul>
      <li><a href="#tabs-1" title="">Profile</a></li>
      <li><a href="#tabs-2" title="">Meeting Choices</a></li>
    </ul>
    <div id="tabs_container" class="vpboxpop">
      <div id="tabs-1">
        <label class="vpbox_pro">
        <p><span>Full Name</span><% Response.Write(personName); %></p>
        <p><span>Company Name</span><% Response.Write(vencompanyname); %></p>
        <p><span>Designation</span><% Response.Write(ventitle); %></p>
        <p><span>Email</span><a href="mailto:<% Response.Write(venemail); %>"><% Response.Write(venemail); %></a></p>
        <p><span>Mobile</span><% Response.Write(venmobile); %></p>
        </label>
      </div>
      <div id="tabs-2">
        <ul class="vpbox_mc">
          <li>
            <label class="mboxtop_right">
            <h2>Bander Saad Al Samari</h2>
            <p class="vpbox_mcpos">General Manager, Nexus Secuity Commision</p>
            <p class="number">96650168556</p>
            <p class="date"><span class="daytrue">Day 1</span><span class="dayfalse">Day2</span></p>
            <p class="popmc-but replacedbut">replaced</p>
            </label>
          </li>
          <li>
            <label class="mboxtop_right">
            <h2>Bander Saad Al Samari</h2>
            <p class="vpbox_mcpos">General Manager, Nexus Secuity Commision</p>
            <p class="number">+966 5016 8556</p>
            <p class="date"><span class="daytrue">Day 1</span><span class="dayfalse">Day2</span></p>
            <p class="popmc-but tookplacebut">Took Place</p>
            </label>
          </li>
          <li>
            <label class="mboxtop_right">
            <h2>Bander Saad Al Samari</h2>
            <p class="vpbox_mcpos">General Manager, Nexus Secuity Commision</p>
            <p class="number">+966 5016 8556</p>
            <p class="date"><span class="daytrue">Day 1</span><span class="dayfalse">Day2</span></p>
            <p class="popmc-but arrangmeetingbut">
              <input name="arrange meeting" type="button" value="arrange meeting" />
            </p>
            </label>
          </li>
          <li>
            <label class="mboxtop_right">
            <h2>Bander Saad Al Samari</h2>
            <p class="vpbox_mcpos">General Manager, Nexus Secuity Commision</p>
            <p class="number">+966 5016 8556</p>
            <p class="date"><span class="daytrue">Day 1</span><span class="dayfalse">Day2</span></p>
            <p class="popmc-but didnottakebut">did not take Place</p>
            </label>
          </li>
          <li>
            <label class="mboxtop_right">
            <h2>Bander Saad Al Samari</h2>
            <p class="vpbox_mcpos" >General Manager, Nexus Secuity Commision</p>
            <p class="number">+966 5016 8556</p>
            <p class="date"><span class="daytrue">Day 1</span><span class="dayfalse">Day2</span></p>
            <p class="popmc-but arrangmeetingbut">
              <input name="arrange meeting" type="button" value="arrange meeting" />
            </p>
            </label>
          </li>
        </ul>
      </div>
    </div>
    <!--End tabs container-->
  </div>
</div>

    <div id="pop2" class="simplePopup vabox">
  <div class="vpboxheadding">Change Meeting Status for</div>
  <div class="vaboxname">
    <div class="vaboxname-left">
      <p id="batch"></p>
    </div>
    <div class="vaboxname-right" id="n"></div>
    <div class="clear"></div>
  </div>
  <ul class="meeting-status">
      <input type="hidden" id="meetingid">
    <li class="didnot-radio active_status">
      <input type="radio" name="meeting-status" id="meetingstatus1" value="1" class="css-radio"/>
      <label class="css-label" for="meetingstatus1">Did not take Place</label>
    </li>
    <li class="took-radio">
      <input type="radio" name="meeting-status" id="meetingstatus2" value="2" class="css-radio" />
      <label class="css-label" for="meetingstatus2">Took Place</label>
    </li>
    <li class="scheduled-radio">
      <input type="radio" name="meeting-status" id="meetingstatus3" value="3" class="css-radio" />
      <label class="css-label" for="meetingstatus3">Scheduled</label>
    </li>
    <%--<li class="replaced-radio">
      <input type="radio" name="meeting-status" id="meetingstatus4" value="4" class="css-radio"  />
      <label class="css-label" for="meetingstatus4">Replaced by</label>
      <input type="text" placeholder="Enter the name of individual" name="individual-name" id="individual-name" class="searchbox"  />
       <div class="close_serch"></div>
    </li>--%>
    <li class="vaboxdone"><a id="done" href="#">Done</a></li>
  </ul>
</div>
    <div id="pop3" class="simplePopup ambox">
  <div class="vpboxheadding">Add Meeting</div>
  <div class="amtextbox" id="users">
    <div class="amtextbox-left">
      <div class="amtextbox-left-sd">
        <h2>Select Delegate </h2>
        <div class="mtrl_search">
          <input type="text" placeholder="Search company or individual" name="" class="searchbox search">
           <div class="close_serch"></div>
        </div>
      </div>
      <div class="amtextbox-left-ao">
        <div class="rightflitermainleft"> <strong>Attendance on</strong>
          <input type="checkbox" class="css-checkbox" id="checkboxG44" name="checkboxG4">
          <label class="css-label" for="checkboxG44">All</label>
          <input type="checkbox" class="css-checkbox" id="checkboxG45" name="checkboxG5">
          <label class="css-label" for="checkboxG45">Day1</label>
          <input type="checkbox" class="css-checkbox" id="checkboxG46" name="checkboxG6">
          <label class="css-label" for="checkboxG46">Day2</label>
        </div>
      </div>
      <div class="am-poplist">
        <ul class="delegates-list list">
            <% Response.Write(delhtml); %>
       
        </ul>
      </div>
    </div>
    <div class="amtextbox-right">
      <h3>Select Time Slot</h3>
      <div class="am-poplist">
        <ul class="timeshot-list">
          <% Response.Write(timingslothtml); %>
        </ul>
      </div>
      <div class="amtextbox-right-am">
          
        <input id="addmeeting" name="addmeeting" type="button" value="add meeting" />
      </div>
    </div>
    <div class="clear"></div>
  </div>
</div>
    <input type="hidden" id="hiddenslotid" name="hiddenslotid" />
    <input type="hidden" id="hiddendelid" name="hiddendelid" />

    
    <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="False" OnLoad="GridView1_Load">
        <Columns>
            <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" /> <%--1--%>
            <asp:BoundField HeaderText="Day1" DataField="Day1" /><%--2--%>
            <asp:BoundField HeaderText="Day2" DataField="Day2" /><%--3--%>
            <asp:BoundField HeaderText="Role" DataField="Role" /><%--4--%>
            <asp:BoundField HeaderText="IsPrimary" DataField="IsPrimary" /><%--5--%>
            <asp:BoundField HeaderText="JobTitle" DataField="JobTitle" /><%--6--%>
            <asp:BoundField HeaderText="MeetingBy" DataField="MeetingBy" /><%--7--%>
            <asp:BoundField HeaderText="MeetingSlotStartTime" DataField="MeetingSlotStartTime" /><%--8--%>
            <asp:BoundField HeaderText="MeetingSlotEndTime" DataField="MeetingSlotEndTime" /><%--9--%>
            <asp:BoundField HeaderText="MeetingSlotId" DataField="MeetingSlotId" /><%--10--%>
            <asp:BoundField HeaderText="Mobile" DataField="Mobile" /><%--11--%>
            <asp:BoundField HeaderText="Name" DataField="Name" /><%--12--%>
            

        </Columns>
        </asp:GridView>

        <asp:GridView ID="GridView2" Visible="false" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="MeetingSlotId" DataField="MeetingSlotId" /> <%--1--%>
            </Columns>
            </asp:GridView>
    

<script src="js/jquery.simplePopup.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tabulous.js"></script>
<script type="text/javascript" src="js/tabulousjs.js"></script> 
    <script type="text/javascript">

        var options = {
            valueNames: ['am-poplist-p']
        };

        var userList = new List('users', options);




        //day1 pop up add meeting
        $('#checkboxG45').change(function () {

            
            if ($(this).is(':checked') == true) {
                $(".row").each(function (index) {
                    console.log(this.id);
                    var id = this.id;
                    var statusday1 = $('#' + id).data('day1');
                    var statusday2 = $('#' + id).data('day2');
                    console.log(statusday1 + " day2");
                    console.log(statusday2);


                    if (statusday1 == "dayfalse") {


                        $("#" + id).hide();
                    }
                });
            }
            else {
                $(".row").show();
            }

            
            //hide other div u wish
        });

        //day2 pop up add meeting
        $('#checkboxG46').change(function () {

            if ($(this).is(':checked') == true) {
                $(".row").each(function (index) {
                    console.log(this.id);
                    var id = this.id;
                    var statusday1 = $('#' + id).data('day1');
                    var statusday2 = $('#' + id).data('day2');

                    if (statusday2 == "dayfalse") {
                        $("#" + id).hide();
                    }
                });
            }
            else {
                $(".row").show();
            }


          

        });

        //day2 pop up add meeting
        $('#checkboxG44').click(function () {
            $(".row").show();
        });

        //getmeetingslotid
        $('.t-act').click(function () {
            var meetingslotid = this.id;
            $("#hiddenslotid").val(meetingslotid);
        });

        //getdelid
        $('.delclass').click(function () {
            var delid = this.id;
            $("#hiddendelid").val(delid);
        });

        //addmeeting
        $("#addmeeting").click(function () {

            var meetingid = $("#hiddenslotid").val();
            //var meetingid = "ab0329c9";
            var delid = $("#hiddendelid").val();
          //  alert(meetingid);
            if (meetingid == "" && delid == "") {
                alert("Please fill the form correctly");

            }
            else {
                
                $.ajax({
                    type: "POST",
                    url: "Meeeting_details.aspx/addmeeting",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ meetingslotid: meetingid, delid: delid }),
                    dataType: "json",
                    success: function (result) {
                        alert("Successfully Added.");
                        window.location.reload();
                        
                        //  window.location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }

                });
                return false;
            }
         
       

        });

        //

        $("#done").click(function () {
            var meetid = $("#meetingid").val();
            var chk_value = $('input:radio[name=meeting-status]:checked').val();
            //alert(meetid);
            //alert(chk_value);

            $.ajax({
                type: "POST",
                url: "Meeeting_details.aspx/GetCurrentStatus",
                data: '{meetid: ' + meetid + ',statusid:'+chk_value+' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(result) {
                    alert("Successfully Updated.");
                    window.location.reload();
                },
                failure: function (response) {
                    alert(response.d);
                }

            });
            return false;

        });

        //meeting details filteration checks
        //day1 checkbox
        $('#checkboxG2').change(function () {

            if ($(this).is(':checked') == true) {
                $(".meetrow").each(function (index) {
                    console.log(this.id);
                    var id = this.id;
                    var statusday1 = $('#' + id).data('day1main');
                    var statusday2 = $('#' + id).data('day2main');

                    console.log(statusday1+"day1");
                    console.log(statusday2);
                    if (statusday1 == "dayfalse") {
                        
                        $("#" + id).hide();
                    }
                });
            }
            else {
                $(".meetrow").show();
            }




        });
        //day2 checkbox
        $('#checkboxG3').change(function () {

            if ($(this).is(':checked') == true) {
                $(".meetrow").each(function (index) {
                   // console.log(this.id);
                    var id = this.id;
                    var statusday1 = $('#' + id).data('day1main');
                    var statusday2 = $('#' + id).data('day2main');


                    console.log(statusday1 + "day1");
                    console.log(statusday2);

                    if (statusday2 == "dayfalse") {
                        $("#" + id).hide();
                    }
                });
            }
            else {
                $(".meetrow").show();
            }




        });
        //all check box
        $('#checkboxG1').change(function () {

        
                $(".meetrow").show();
           
   });

        function showdata(data) {
            alert();
             $('#pop2').simplePopup();
             alert(data);
            //   $('.vaboxname-right').append('<span>' + data + '</span>General Manager, Nexus Secuity Commision');
        }

        

        $(document).ready(function () {

           
            $('.show1').click(function () {
                $('#pop1').simplePopup();
            });
            
            

            $('.show2').click(function () {
                $('#pop2').simplePopup();

                var meetid = this.id;
                $("#meetingid").val(meetid);
                var name = $('#'+meetid).data('name');
                var company = $('#' + meetid).data('company');
                var title = $('#' + meetid).data('jobtitle'); 
                var batchnumber = $('#' + meetid).data('batchnumber');
               // alert(batchnumber);

                $("#n").html("<span>" + title + "</span>," + company);
                $("#batch").html(batchnumber);


            });



            $('.show3').click(function () {
                $('#pop3').simplePopup();
            });
            $('.meeting-status li').click(function () {
                $('#individual-name').hide();
                $('.meeting-status li').removeClass('active_status');
                $(this).addClass('active_status');
                $(this).find('input').prop("checked", true);

                if ($(this).find('input').attr('id') == 'meetingstatus4') {
                    $('#individual-name').show();
                }

            });

            $('.delegates-list li').click(function () {
                $('.delegates-list li').removeClass('active');
                $(this).addClass('active');
            });

            $('.timeshot-list li').click(function () {
                $('.timeshot-list li').removeClass('active');
                $(this).addClass('active');
            });

            $(".searchbox").keypress(function () {
                $('.close_serch').show();
            });

            $('.close_serch').click(function () {
                $(".searchbox").val('');
                $('.close_serch').hide();
            });


        });

</script>
   
</asp:Content>

