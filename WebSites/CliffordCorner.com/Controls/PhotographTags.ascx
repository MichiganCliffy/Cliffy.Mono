<%@ Control Language="c#" Inherits="Controls_PhotographTags" CodeFile="PhotographTags.ascx.cs" AutoEventWireup="true" %>
<div id="PhotoTags" class="isHidden">
	<asp:Repeater ID="mTags" Runat="server" OnItemCreated="Tags_ItemCreated">
	<HeaderTemplate><div>View all pictures about:</div></HeaderTemplate>
	<ItemTemplate><div><a id="mTagUrl" runat="server" /></div></ItemTemplate>
	</asp:Repeater>
</div>