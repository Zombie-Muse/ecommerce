<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Template.Master" CodeBehind="Logout.aspx.vb" Inherits="OnlineStoreSummer2021.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-4 col-sm-offset-1">
        <div class="login-form"><!--logout form-->
            
            <h2>Are you sure you want to logout?</h2>
            <asp:Button ID="btnLogout" runat="server" Text="Yes" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div><!--logout form-->
    </div>
        
    
</asp:Content>
