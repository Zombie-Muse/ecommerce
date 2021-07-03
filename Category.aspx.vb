Public Class Category
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("SearchString") <> "" Then
            Response.Write(Request.QueryString("SearchString"))
            ' Search for products which has a value match in ProductDescription (LIKE) that has the searchstring in any position 
            ' Title Label control shows "Search result for " + the searchstring if found any; otherwise shows "Not Found".
            SqlDSProduct.SelectCommand = "Select * From Product Where ProductDescription Like '%" & Request.QueryString("SearchString") & "%'"
            Response.Write(SqlDSProduct.SelectCommand)
            SqlDSProduct.DataBind()
            If rpProduct.DataSource Then
                lblSubCategoryName.Text = "Search Result For: " + Request.QueryString("SearchString")
            Else
                lblSubCategoryName.Text = "Not found"
            End If
        Else
            If Request.QueryString("MainCategoryID") <> "" Then
                sqlDSSubCategory.SelectCommand = "Select * From CatName Where CatParent = " & CInt(Request.QueryString("MainCategoryID"))
                sqlDSSubCategory.DataBind()
                lblCatName.Text = Request.QueryString("MainCategoryName")
                SqlDSProduct.SelectCommand = "Select * From Product Where Feature = 'y' And MainCategoryID = " & CInt(Request.QueryString("MainCategoryID"))
                SqlDSProduct.DataBind()
                lblSubCategoryName.Text = "Featured " + Request.QueryString("MainCategoryName")
            End If
            If Request.QueryString("SubCategoryID") <> "" Then
                SqlDSProduct.SelectCommand = "Select * From Product Where SubCategoryID = " & Request.QueryString("SubCategoryID")
                SqlDSProduct.DataBind()
                lblSubCategoryName.Text = Request.QueryString("SubCategoryName")
            End If


        End If

    End Sub

    Public Function GetDiscountPrice(ByVal Price As Double) As String
        Dim OriginalPrice As Double = Price
        Dim Discount As Double = 0.2
        Dim Result As Double = OriginalPrice - (OriginalPrice * Discount)
        Dim FormattedResult As String = FormatCurrency(Result,,, TriState.True, TriState.True)
        Return FormattedResult
    End Function

End Class