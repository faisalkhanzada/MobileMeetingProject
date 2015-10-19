<%@ Page Language="C#" AutoEventWireup="true" Inherits="test" Codebehind="test.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="addmeeting" name="addmeeting" type="button" value="add meeting" />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    </form>
</body>
    <script type="text/javascript" >
        alert();
        //data: '{meetingid:' + meetingid + ',delid:' + delid + '}',
        $("#addmeeting").click(function () {
           
            var meetingid = '83036';
            //var meetingid = "ab0329c9";
            var delid = 'ab0329c9-dce8-e411-b23b-6c3be5a88b80';
           
            if (meetingid == "" && delid == "") {
                alert("Please fill the form correctly");

            }
            else {

                $.ajax({
                    type: "POST",
                    url: "test.aspx/addmeeting",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ meetingslotid: meetingid, delid: delid }),
                    dataType: "json",
                    success: function (result) {
                     ///   alert(result);
                        console.log(result);
                        //  window.location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }

                });
                return false;
            }



        });

    </script>
   
</html>
