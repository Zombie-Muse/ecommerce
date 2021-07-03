 <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Template.Master" CodeBehind="Category.aspx.vb" Inherits="OnlineStoreSummer2021.Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="col-sm-3">
		<div class="left-sidebar">
			<h2><asp:Label ID="lblCatName" runat="server" Text=""></asp:Label></h2>
			<div class="panel-group category-products" id="accordian"><!--category-productsr-->
                <asp:SqlDataSource ID="sqlDSSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringOnlineStore %>" SelectCommand=""></asp:SqlDataSource>
	            <asp:Repeater ID="rpSubCategory" runat="server" DataSourceID="SqlDSSubCategory">
		            <ItemTemplate>
 				        <div class="panel panel-default">
					        <div class="panel-heading">
						        <h4 class="panel-title"><a href="Category.aspx?MainCategoryID=<%# Request.QueryString("MainCategoryID") %>&MainCategoryName=<%# Trim(Request.QueryString("MainCategoryName")) %>&SubCategoryID=<%# CStr(Eval("ID")) %>&SubCategoryName=<%# Trim(Eval("CatName")) %>"><%#Eval("CatName") %></a></h4>
					        </div>
				        </div>
					</ItemTemplate>
	            </asp:Repeater> 
			</div><!--/category-productsr-->					
		</div>
	</div>			
	<div class="col-sm-9 padding-right">
		<div class="features_items"><!--features_items-->
			<h2 class="title text-center">
                <asp:Label ID="lblSubCategoryName" runat="server" Text=""></asp:Label></h2>

            <asp:SqlDataSource ID="SqlDSProduct" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringOnlineStore %>" SelectCommand=""></asp:SqlDataSource>
            <asp:Repeater ID="rpProduct" runat="server" DataSourceID="SqlDSProduct">
				<ItemTemplate>
					<div class="col-sm-4">
							<div class="product-image-wrapper">
								<div class="single-products">
									<div class="productinfo text-center">
										<img src="images/shop/<%# Trim(Eval("ProductID")) %>.jpg" alt="" />
										<h2>
											<a href="ProductDetail.aspx?ProductID='<%# Trim(Eval("ProductID")) %>'" class=""><%# Eval("ProductName") %></a>
											<br />
                                            
												<div><%If Session("Email") <> "" Then %>
													<%# GetDiscountPrice(Eval("Price")) %>
													<%Else %>
													$<%# Eval("Price") %>
													<%End If %>
												</div>
										</h2>
										<p><%# Trim(Eval("ProductID")) %><br /><%# Trim(Eval("ProductDescription")) %></p>
										<a href="ProductDetail.aspx?ProductID='<%# Trim(Eval("ProductID")) %>'" class="">View Product</a>
									</div>
								</div>
							</div>
						</div>
				</ItemTemplate>
            </asp:Repeater>

		</div><!--features_items-->
	</div>
</asp:Content>
