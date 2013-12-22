<%@ Page language="c#" Inherits="ContentEngine_ContentPageHtml" CodeFile="ContentPageHtml.aspx.cs" AutoEventWireup="true" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Menu" Src="~/controls/ContentEngineMenu.ascx" %>
<%@ Register TagPrefix="error" TagName="Flickr" Src="~/controls/ErrorFlickr.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentPlaceHolder">
<div id="content">
<h2 ID="mTitle" Runat="server" />
<h3 ID="mBreadCrumb" runat="server" />
<asp:Panel id="pnlContent" runat="server" />
<error:Flickr id="mErrorFlickr" runat="server" visible="false" />
</div>

<div id="subcontent"><content:Menu id="mMenu" runat="server"/></div>
</asp:Content>
