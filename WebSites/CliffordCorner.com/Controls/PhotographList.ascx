<%@ Control Language="c#" Inherits="Controls_PhotographList" CodeFile="PhotographList.ascx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="content" TagName="Tags" Src="~/controls/PhotographTags.ascx" %>
<asp:Repeater ID="mImages" Runat="server" OnItemCreated="Images_ItemCreated">
<HeaderTemplate><div><strong><asp:Literal ID="mBreadCrumb" runat="server" /></strong></div></HeaderTemplate>
<ItemTemplate><div class="thumbnail"><a id="mLink" runat="server" href="~/photograph.aspx"><img id="mImage" runat="server" width="75" height="75" /></a></div></ItemTemplate>
</asp:Repeater>

<asp:Panel ID="mNoImagesText" Runat="server" Visible="False">
<div class="important">I'm sorry, I couldn't find any photographs with that tag.</div>
<p>Please try searching for another tag.</p>
</asp:Panel>
