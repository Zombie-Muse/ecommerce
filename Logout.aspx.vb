Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub BtnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If Session("Email") <> "" Then

            'Session("Email") = ""
            Session.Abandon()
            'Redirect to the default page after 3 seconds
            Dim meta As New HtmlMeta With {
                .HttpEquiv = "Refresh",
                .Content = "3;url=Default.aspx"
            }
            Me.Page.Controls.Add(meta)

            lblMessage.Text = "Logout Successful! You will now be redirected in 3 seconds"

        Else
            lblMessage.Text = "Logout failed"
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Cancels logout and takes user back to the default page
        Response.Redirect("Default.aspx")
    End Sub
End Class

