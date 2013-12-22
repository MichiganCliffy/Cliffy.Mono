<%@ Page language="c#" Inherits="ContentEngine_ContentChapter" CodeFile="ContentChapter.aspx.cs" AutoEventWireup="true" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Menu" Src="~/controls/ContentEngineMenu.ascx" %>
<%@ Register TagPrefix="error" TagName="Flickr" Src="~/controls/ErrorFlickr.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="contentPlaceHolder">
<div id="content">
<asp:Panel ID="pnlContent" runat="server">
<img ID="imgImage" runat="server" class="spotlight" />
<h2><asp:Literal ID="txtChapterTitle" runat="server" /></h2>
<p><asp:Literal ID="txtSummary" runat="server" /></p>
</asp:Panel>
<error:Flickr id="mErrorFlickr" runat="server" visible="false" />
</div>

<div id="subcontent">
<content:Menu id="ctrMenu" runat="server"/>
</div>
</asp:Content>