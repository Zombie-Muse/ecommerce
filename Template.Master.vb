Imports System.Data
Imports System.Data.SqlClient

Public Class Template
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' get all MainCategoryName
        Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
        Dim connMainCategory As SqlConnection
        Dim cmdMainCategory As SqlCommand
        Dim drMainCategory As SqlDataReader
        Dim strSQL As String = "Select * from Category Where parent = 0"
        connMainCategory = New SqlConnection(strConn)
        cmdMainCategory = New SqlCommand(strSQL, connMainCategory)
        connMainCategory.Open()
        drMainCategory = cmdMainCategory.ExecuteReader(CommandBehavior.CloseConnection)
        'Dim strMainCategory As String = ""
        'Do While drMainCategory.Read()
        'strMainCategory = strMainCategory + "<li><a href=''>" + Trim(drMainCategory("CategoryName")) + "</a></'li>"
        'Loop
        'rpMainCategory.Text = strMainCategory
        If Session("Email") <> "" Then
            'isLoggedIn = True
            hlLogin.Visible = False
            hlLogout.Visible = True
            hrefEmail.InnerText = Session("Email")

        End If
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If tbSearchString.Text <> "" Then
            Dim strCheck As String = " "
            Dim strURL As String
            If Not Trim(tbSearchString.Text).Contains(strCheck) Then ' there is only one word
                Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
                Dim connProduct As SqlConnection
                Dim cmdProduct As SqlCommand
                Dim drProduct As SqlDataReader
                'Dim strSQL As String = "Select * from Product Where ID = " & CInt(tbSearchString.Text)
                Dim strSQL As String = "Select * from Product Where ProductID = '" & Trim(tbSearchString.Text) & "'"
                connProduct = New SqlConnection(strConn)
                cmdProduct = New SqlCommand(strSQL, connProduct)
                connProduct.Open()
                drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)

                If drProduct.Read() Then
                    strURL = "ProductDetail.aspx?ProductID='" & Trim(tbSearchString.Text) & "'"
                    Response.Redirect(strURL)

                Else
                    strURL = "Category.aspx?SearchString=" & Trim(tbSearchString.Text)
                    Response.Redirect(strURL)
                End If
            End If

        End If
    End Sub
End Class