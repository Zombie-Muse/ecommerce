Imports System.Data
Imports System.Data.SqlClient
Public Class ProductDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("ProductID") <> "" Then
            Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
            Dim connProduct As SqlConnection
            Dim cmdProduct As SqlCommand
            Dim drProduct As SqlDataReader
            Dim strSQL As String = "Select * from Product Where ProductID = " & Trim(Request.QueryString("ProductID"))
            connProduct = New SqlConnection(strConn)
            cmdProduct = New SqlCommand(strSQL, connProduct)
            connProduct.Open()
            drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)
            If drProduct.Read() Then
                lblProductName.Text = drProduct.Item("ProductName")
                lblProductID.Text = drProduct.Item("ProductID")
                lblProductDescription.Text = drProduct.Item("ProductDescription")
                lblPrice.Text = drProduct.Item("Price")
                lblDiscountPrice.Text = GetDiscountPrice(drProduct.Item("Price"))
                imgProduct.ImageUrl = "images/shop/" + Trim(drProduct.Item("ProductID")) + ".jpg"

                Dim strRating As String = ""
                Dim intRating As Integer
                If intRating = Nothing Then
                    lblRating.Text = "Not rated"
                Else
                    For intRating = 1 To drProduct.Item("Rating")
                        strRating += "<img src='images/product-details/1star.png' width='15px' />"
                    Next
                    lblRating.Text = strRating
                End If
            End If
        End If
    End Sub

    Private Sub BtnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
        ' validate the tbQuantity.text value
        '*** get CartNo
        Dim strCartNo As String
        If HttpContext.Current.Request.Cookies("CartNo") Is Nothing Then
            strCartNo = GetRandomCartNoUsingGUID(10)
            Dim CookieTo As New HttpCookie("CartNo", strCartNo)
            HttpContext.Current.Response.AppendCookie(CookieTo)
        Else
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartNo")
            CookieBack.Expires = DateTime.Now.AddMinutes(3)
            strCartNo = CookieBack.Value
        End If
        ' set up ado. objects and variables
        Dim strConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
        Dim conn As New SqlConnection(strConnectionString)
        Dim drCheck As SqlDataReader
        Dim strSQLStatement As String
        Dim cmdSQL As SqlCommand
        ' get product price
        strSQLStatement = "Select * From Product Where ProductID = '" & lblProductID.Text & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        drCheck = cmdSQL.ExecuteReader()
        Dim decPrice As Decimal
        If drCheck.Read() Then
            If Session("Email") <> "" Then
                decPrice = GetDiscountPrice(drCheck.Item("Price"))
            Else
                decPrice = drCheck.Item("Price")
            End If
        End If
        drCheck.Close()
        ' check if this product already exits in the cart
        strSQLStatement = "SELECT * FROM Cart WHERE CartNo = '" & strCartNo & "' and ProductID= '" & Trim(lblProductID.Text) & "'"
        cmdSQL.CommandText = strSQLStatement
        drCheck = cmdSQL.ExecuteReader()
        If drCheck.Read() Then
            ' update the quantity
            strSQLStatement = "Update Cart Set Quantity = " & CInt(tbQuantity.Text) & " Where ProductID = '" & Trim(lblProductID.Text) & "' And CartNo = '" & strCartNo & "'"
            ' get the value from the Quantity data field in this datareader and add it to the tbquantity.text
            ' write the sql statement

        Else ' insert the product  
            strSQLStatement = "INSERT INTO Cart (CartNo, ProductID, ProductName, Quantity, Price) values('" & strCartNo & "', '" & lblProductID.Text & "', '" & lblProductName.Text & "', " & CInt(tbQuantity.Text) & ", " & decPrice & ")"
        End If
        drCheck.Close() ' When a DataReader is open, its Connection is dedicated to the its associated SQLcommand.
        cmdSQL.CommandText = strSQLStatement
        Dim drCart As SqlDataReader
        drCart = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Response.Redirect("ViewCart2.aspx")
    End Sub

    Public Function GetRandomCartNoUsingGUID(ByVal length As Integer) As String
        'Get the GUID
        Dim guidResult As String = Guid.NewGuid().ToString()
        'Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty)
        'Make sure length is valid
        If length <= 0 OrElse length > guidResult.Length Then
            Throw New ArgumentException("Length must be between 1 and " & guidResult.Length)
        End If
        'Return the first length bytes
        Return guidResult.Substring(0, length)
    End Function

    Public Function GetDiscountPrice(ByVal Price As Double) As Double
        Dim OriginalPrice As Double = Price
        Dim Discount As Double = 0.2
        Dim Result As Double = OriginalPrice - (OriginalPrice * Discount)
        'formats the number as currency and rounds to 2 decimal places
        Dim FormattedResult As String = FormatCurrency(Result,,, TriState.True, TriState.True)
        Return FormattedResult
    End Function
End Class