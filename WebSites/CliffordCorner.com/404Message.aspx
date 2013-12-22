<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404Message.aspx.cs" Inherits="_404Message" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Reference Control="~/controls/imagerotator.ascx" %>
<%@ Register TagPrefix="content" TagName="Header" Src="~/controls/Header.ascx" %>
<%@ Register TagPrefix="content" TagName="TopNav" Src="~/controls/NavTop.ascx" %>
<%@ Register TagPrefix="content" TagName="Rotator" Src="~/controls/ImageRotator.ascx" %>
<%@ Register TagPrefix="content" TagName="Photographs" Src="~/controls/RecentPhotographs.ascx" %>
<%@ Register TagPrefix="content" TagName="Articles" Src="~/controls/BloggerRecentPosts.ascx" %>
<%@ Register TagPrefix="content" TagName="Footer" Src="~/controls/Footer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="content">
	<h2>Requested Page Not Found</h2>
	<p class="important">I'm sorry, but I cannot seem to find the page you are looking for. Please select an item
	from the top navigation bar.</p>
</div>
	
<div id="subcontent">
<content:Photographs id="mPhotographs" runat="server" />
<content:Articles id="mArticles" runat="server" Count="2" />
</div>
</asp:Content>
