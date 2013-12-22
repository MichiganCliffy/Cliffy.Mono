<%@ Page language="c#" Inherits="ContentEngine_ContentPageFlickrImage" CodeFile="ContentPageFlickrImage.aspx.cs" AutoEventWireup="true" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Menu" Src="~/controls/ContentEngineMenu.ascx" %>
<%@ Register TagPrefix="error" TagName="Flickr" Src="~/controls/ErrorFlickr.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentPlaceHolder">
<div id="content">
	<div id="splitcontentleft">
		<h2 ID="mTitle" Runat="server" />
		<h3 ID="mBreadCrumb" Runat="server" />
		<a id="mImageLink" runat="server" title="View {0} on flickr.com" class="spotlight"><img id="mImage" runat="server" /></a>
		<div style="float: left"><p id="mDescription" runat="server" /></div>
        <error:Flickr id="mErrorFlickr" runat="server" visible="false" />
	</div>
</div>

<div id="subcontent">
<content:Menu id="mMenu" runat="server" />
</div>
</asp:Content>
