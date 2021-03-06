<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Template.Master" CodeBehind="ViewCart2.aspx.vb" Inherits="OnlineStoreSummer2021.ViewCart2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: right; margin: 0 30px 5px 0;">
        <asp:Button ID="btnEmptyCart" runat="server" Text="Empty the Cart" />
    </div>   
    <asp:SqlDataSource ID="sqlDSCart2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionStringOnlineStore %>"> 
    </asp:SqlDataSource>
    <asp:ListView ID="lvCart" runat="server" DataSourceID="sqlDSCart2"
            OnItemCommand="lvCart_OnItemCommand" CellPadding="3" DataKeyField="CartNo"
            CellSpacing="0" RepeatColumns="1" DataKeyNames="ID">
        <LayoutTemplate>              
            <div style="float: right; margin: 0 30px 5px 0;">
                <asp:Label ID="lblPage" runat="server" Text="" Font-Size="14px"></asp:Label>
            </div><br />
            <div class="table-responsive cart_info">
            <table class="table table-condensed">
                <thead>
                    <tr class="cart_menu" style="background-color: darkorange">
					    <td class="image">Item</td>
					    <td class="description"></td>
					    <td class="price">Price</td>
					    <td class="quantity">Quantity</td>
					    <td class="total">Subtotal</td>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" id="itemPlaceholder"></asp:PlaceHolder>
                </tbody>
            </table> 
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
				<td class="cart_product">
					<a href="#"><img style="height: 100px; width: 100px;" src="images/shop/<%# Trim(Eval("ProductID")) %>.jpg" alt=""></a>
				</td>
				<td class="cart_description">
					<h4><a href="#"><%# Trim(Eval("ProductName")) %></a></h4>
					<p>Product ID: <%# Eval("ProductID") %></p>
				</td>
				<td class="cart_price">
					<p>$<%# Eval("Price") %></p>
				</td>
				<td class="cart_quantity">
                    <asp:TextBox ID="tbQuantity" Text='<%# Eval("Quantity")%>' Width="50px" CssClass="" runat="server"></asp:TextBox>
                    <asp:LinkButton runat="server" ID="lbUpdate" Text='Update'
                        CommandName="cmdUpdate" CommandArgument='<%# Eval("ProductID")%>' />
                    <asp:LinkButton runat="server" ID="lbDelete" Text='Delete'
                        CommandName="cmdDelete" CommandArgument='<%# Eval("ProductID")%>' />
				</td>
				<td class="cart_total">
					<p class="cart_total_price">$<%# Eval("Price") * Eval("Quantity")%></p>
				</td>
            </tr> 
            
        </ItemTemplate>
    </asp:ListView>

                <p class="total"> <strong>Total Price: 
                    <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label></strong>
                </p>
    <div style="padding: 8px;width: 100%;text-align: center;">
        <div style="display: inline-block; margin-top: 5px">
            <asp:Button runat="server" Text="&laquo;" id="show_prev" CssClass="show_prevx"></asp:Button>
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCart" PageSize="3">
                <Fields>        
                <asp:NumericPagerField NextPageText='&raquo;' PreviousPageText='&laquo;' ButtonCount="5" 
                    ButtonType="Button" NextPreviousButtonCssClass="next_prevx" NumericButtonCssClass="numericx" 
                    CurrentPageLabelCssClass="currentx" RenderNonBreakingSpacesBetweenControls="False" />
                </Fields>
            </asp:DataPager>
            <asp:Button runat="server" Text="&raquo;" id="show_next" CssClass="show_nextx"></asp:Button>
        </div>
    </div>
</asp:Content>
