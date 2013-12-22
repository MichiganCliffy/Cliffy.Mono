<%@ Control Language="c#" Inherits="Controls_RecentPhotographs" CodeFile="RecentPhotographs.ascx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="flickr" TagName="Thumbnail" Src="~/Controls/RecentPhotograph.ascx" %>
<div class="box">
<h2>Recent Photographs</h2>
<asp:Repeater ID="mLinks" Runat="server"><HeaderTemplate>
<div style="padding-left: 10px;"></HeaderTemplate><ItemTemplate>
<flickr:Thumbnail runat="server" DataSource="<%# Container.DataItem %>" /></ItemTemplate><FooterTemplate>
<div style="clear:both;"></div></div></FooterTemplate>
</asp:Repeater>
</div>