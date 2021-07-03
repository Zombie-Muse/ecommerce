<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Template.Master" CodeBehind="Login.aspx.vb" Inherits="OnlineStoreSummer2021.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-4 col-sm-offset-1">
        <div class="login-form"><!--login form-->
            <h2>Login to your account</h2>
            Email:<br />
            <asp:TextBox ID="tbEmail" Width="200" runat="server"></asp:TextBox><br />
            Password:<br />
            <asp:TextBox ID="tbPassword" Width="200" runat="server"></asp:TextBox><br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" cssclass="btn btn-default" /><br />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div><!--login form-->
    </div>
    <div class="col-sm-1">
        <h2 class="or">OR</h2>
    </div>
    <div class="col-sm-4">
        <div class="signup-form"><!--signup form-->
            <h2>New User Signup!</h2>
        </div><!--signup form-->
    </div>
</asp:Content>
