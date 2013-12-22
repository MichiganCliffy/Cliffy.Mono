<%@ Page language="c#" Inherits="ContentEngine_ContentPageFlickrTag" CodeFile="ContentPageFlickrTag.aspx.cs" AutoEventWireup="true" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Menu" Src="~/controls/ContentEngineMenu.ascx" %>
<%@ Register TagPrefix="content" TagName="Photographs" Src="~/controls/PhotographList.ascx" %>
<%@ Register TagPrefix="error" TagName="Flickr" Src="~/controls/ErrorFlickr.ascx" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="contentPlaceHolder">
<div id="content">
	<div id="splitcontentleft">
<h2 ID="mTitle" Runat="server" />

<a id="slideShowLink" href="javascript: void(viewer.show(0))" title="slide show">slide show</a>
<h3 ID="mBreadCrumb" runat="server" />

<asp:Panel ID="pnlMessage" runat="server" style="margin-bottom: 10px;"><asp:Literal ID="txtMessage" runat="server" /></asp:Panel>
<content:Photographs id="mPhotographs" runat="server" />
<error:Flickr id="mErrorFlickr" runat="server" visible="false" />
	</div>
</div>

<div id="subcontent">
<content:Menu id="mMenu" runat="server"/>
</div>
</asp:Content>