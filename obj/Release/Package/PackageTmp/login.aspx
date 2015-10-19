<%@ Page Language="C#" AutoEventWireup="true" Inherits="login" Codebehind="login.aspx.cs" %>

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
        <h1><strong>Login</strong></h1>
        <form runat="server">
        <div class="login-input">
            <asp:TextBox ID="username" runat="server" value="User Name" name="username"  
 onblur="if (this.value == '') {this.value = 'User Name';}"
 onfocus="if (this.value == 'User Name') {this.value = '';}" required></asp:TextBox>
        </div>
         <div class="login-input">
               <asp:TextBox ID="pass" runat="server" TextMode="Password" value="Password" name="pass"  
 onblur="if (this.value == '') {this.value = 'Password';}"
 onfocus="if (this.value == 'Password') {this.value = '';}" required></asp:TextBox>
        </div>
            <asp:Button ID="btnsubmit" runat="server" Text="Login" OnClick="btnsubmit_Click" />

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Username." ControlToValidate="username" CssClass="dayfalse"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pass" CssClass="dayfalse" ErrorMessage="Please enter password."></asp:RequiredFieldValidator>
            </form>  
      </div>
    </div>
  </div>
</div>
</body>
</html>

