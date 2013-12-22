<%@ Page language="c#" Inherits="Photograph" CodeFile="Photograph.aspx.cs" AutoEventWireup="true" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Header" Src="~/controls/Header.ascx" %>
<%@ Register TagPrefix="content" TagName="TopNav" Src="controls/NavTop.ascx" %>
<%@ Register TagPrefix="content" TagName="Tags" Src="~/controls/Tags.ascx" %>
<%@ Register TagPrefix="content" TagName="TagSearch" Src="~/controls/TagSearch.ascx" %>
<%@ Register TagPrefix="content" TagName="Footer" Src="controls/Footer.ascx" %>
<%@ Register TagPrefix="blog" TagName="Posts" Src="~/controls/BloggerRecentPosts.ascx" %>
<%@ Register TagPrefix="error" TagName="Flickr" Src="~/controls/ErrorFlickr.ascx" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder" runat="server">
<div id="content">
	<div id="splitcontentleft">
		<h2 ID="mTitle" Runat="server" />
		<a id="mImageLink" runat="server" title="View {0} on flickr.com" class="spotlight"><img id="mImage" runat="server" /></a>
		<div class="small">
<!--
			<div id="mOwnerName" runat="server">taken by {0}</div>
			<div id="mDateTaken" runat="server">on {0}</div>
-->
			<div id="mDateUploaded" runat="server">uploaded on {0}</div>
		</div>
		<p id="mDescription" runat="server" />
		<error:Flickr id="mErrorFlickr" runat="server" visible="false" />
	</div>
</div>

<div id="subcontent">
<content:Tags id="mImageTags" runat="server" Title="Image Tags" Count="5" />
<content:Tags id="mPopularTags" runat="server" Title="Popular Tags" Count="5" />
<content:TagSearch id="mTagSearch" runat="server" />
<blog:Posts runat="server" />
</div>
</asp:Content>