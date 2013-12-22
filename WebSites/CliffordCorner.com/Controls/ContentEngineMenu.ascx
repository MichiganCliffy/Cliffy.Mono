<%@ Control Language="c#" Inherits="Controls_ContentEngineMenu" CodeFile="ContentEngineMenu.ascx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="menu" TagName="Tag" Src="~/Controls/ContentEngineMenuItem.ascx" %>
<div class="box">
<h2><asp:Literal ID="txtMenuTitle" runat="server" Text="Destinations" /></h2>
<ul class="menublock">
<li id="ctrMenuHome" runat="server"><asp:HyperLink ID="lnkMenuHome" runat="server" Text="Overview" /></li>
<asp:Repeater ID="mLinks" Runat="server">
<ItemTemplate><menu:Tag runat="server" BasePath="<%# this.BasePath %>" SelectedTag="<%# this.SelectedItem %>" ThisTag="<%# Container.DataItem %>" /></ItemTemplate>
</asp:Repeater>
</ul>
</div>