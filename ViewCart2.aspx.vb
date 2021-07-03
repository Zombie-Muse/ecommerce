Imports System.Data
Imports System.Data.SqlClient
Public Class ViewCart2
    Inherits System.Web.UI.Page
    Public strCartNo As String = " "
    Dim CookieBack As HttpCookie = HttpContext.Current.Request.Cookies("CartNo")
    Public strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' get CartNo
        If CookieBack IsNot Nothing Then
            strCartNo = CookieBack.Value
        End If
        sqlDSCart2.SelectCommand = "Select * From Cart Where CartNo = '" & strCartNo & "'"
        ' get cart total

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString)
        Dim cmd As New SqlCommand
        cmd.CommandType = Data.CommandType.Text
        cmd.CommandText = "SELECT * FROM Cart Where CartNo = '" & strCartNo & "'"
        cmd.Connection = sqlConnection1

        Dim DataAdapter As New SqlDataAdapter
        DataAdapter.SelectCommand = cmd

        Dim TotalPrice As New DataTable()
        DataAdapter.Fill(TotalPrice)

        Dim totalCost As Double = 0
        For Each row As DataRow In TotalPrice.Rows
            totalCost = totalCost + (row.Item("Quantity") * row.Item("Price"))
        Next
        lblTotal.Text = totalCost

    End Sub

    Protected Sub LvCart_OnItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs)
        If e.CommandName = "cmdUpdate" Then
            ' get ProductID and quantity
            Dim strProductID As String = e.CommandArgument
            Dim tbQuantity As TextBox = CType(e.Item.FindControl("tbQuantity"), TextBox)
            Dim strSQL As String = "Update Cart set Quantity = '" & CInt(tbQuantity.Text) & "' where ProductID = '" & strProductID & "' and CartNo = '" & strCartNo & "'"
            ' update
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            sqlDSCart2.DataBind()
            Response.Redirect("ViewCart2.aspx")
        ElseIf e.CommandName = "cmdDelete" Then
            ' get productid and quantity
            Dim strProductID As String = e.CommandArgument
            Dim strSQL As String = "Delete From Cart Where ProductID = '" & strProductID & "' AND cartNo = '" & strCartNo & "'"
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            sqlDSCart2.DataBind()
            Response.Redirect("ViewCart2.aspx")
        End If
    End Sub

    Protected Sub DataPager1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataPager1.PreRender
        Dim total_pages As Integer
        Dim current_page As Integer
        lvCart.DataBind()
        total_pages = DataPager1.TotalRowCount / DataPager1.PageSize
        current_page = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        If DataPager1.TotalRowCount Mod DataPager1.PageSize <> 0 Then
            total_pages += 1
        End If
        If CInt(lvCart.Items.Count) <> 0 Then
            Dim lbl As Label = lvCart.FindControl("lblPage")
            lbl.Text = "Page " + CStr(current_page) + " of " + CStr(total_pages) + " (Total items: " + CStr(DataPager1.TotalRowCount) + ")"
        End If
        If CInt(lvCart.Items.Count) = 0 Then
            DataPager1.Visible = False
            show_next.Visible = False
            show_prev.Visible = False
        End If
    End Sub

    Protected Sub Show_prev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_prev.Click
        Dim pagesize As Integer = DataPager1.PageSize
        Dim current_page As Integer = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        Dim total_pages As Integer = DataPager1.TotalRowCount / DataPager1.PageSize
        Dim last As Integer = total_pages \ 3
        last *= 3
        Do While current_page < last
            last -= 3
        Loop
        If last < 3 Then
            last = 0
        Else
            last -= 3
        End If
        DataPager1.SetPageProperties(last * pagesize, pagesize, True)
    End Sub

    Protected Sub Show_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_next.Click
        Dim last As Integer = 3
        Dim pagesize As Integer = DataPager1.PageSize
        Dim current_page As Integer = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        Dim total_pages As Integer = DataPager1.TotalRowCount / DataPager1.PageSize
        Do While current_page > last
            last += 3
        Loop
        If last > total_pages Then
            last = total_pages
        End If
        DataPager1.SetPageProperties(last * pagesize, pagesize, True)
    End Sub

    Private Sub BtnEmptyCart_Click(sender As Object, e As EventArgs) Handles btnEmptyCart.Click
        If strCartNo IsNot Nothing Then
            Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            Dim strSQL As String = "DELETE FROM Cart WHERE CartNo = '" & strCartNo & "'"
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            Response.Redirect("ViewCart2.aspx")
        End If
    End Sub


End Class