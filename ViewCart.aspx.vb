Imports System.Data
Imports System.Data.SqlClient
Public Class ViewCart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strCartNo As String
        Dim CookieBack As HttpCookie = HttpContext.Current.Request.Cookies("CartNo")
        If CookieBack IsNot Nothing Then
            strCartNo = CookieBack.Value
            SqlDSCart.SelectCommand = "SELECT * FROM Cart WHERE CartNo = '" & strCartNo & "'"
        End If

        Response.Write(SqlDSCart.SelectCommand)

        'Experimantal ----don't judge me
        'If Request.QueryString("CartNo") <> "" Then
        '    Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
        '    Dim connCart As SqlConnection
        '    Dim cmdCart As SqlCommand
        '    Dim drCart As SqlDataReader
        '    Dim strSQL As String = "Select * from Cart Where CartNo = '" & strCartNo & "'"
        '    connCart = New SqlConnection(strConn)
        '    cmdCart = New SqlCommand(strSQL, connCart)
        '    connCart.Open()
        '    drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
        '    If drCart.Read() Then
        '        lblProductName.Text = drCart.Item("ProductName")
        '        lblProductID.Text = drCart.Item("ProductID")
        '        lblDescription.Text = drCart.Item("ProductDescription")
        '        lblPrice.Text = drCart.Item("Price")
        '        lblDiscountPrice.Text = GetDiscountPrice(drCart.Item("Price"))
        '        imgCart.ImageUrl = "images/shop/" + Trim(drCart.Item("ProductID")) + ".jpg"

        '        Dim strRating As String = ""
        '        Dim intRating As Integer
        '        If intRating = Nothing Then
        '            lblRating.Text = "Not rated"
        '        Else
        '            For intRating = 1 To drCart.Item("Rating")
        '                strRating = strRating + "<img src='images/product-details/1star.png' width='15px' />"
        '            Next
        '            lblRating.Text = strRating
        '        End If
        '    End If
        'End If
    End Sub

End Class