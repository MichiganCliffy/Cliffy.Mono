<%@ Control Language="c#" Inherits="Controls_NavTop" CodeFile="NavTop.ascx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="nav" TagName="Link" Src="~/Controls/NavTopItem.ascx" %>
<%@ Register TagPrefix="nav" TagName="Extension" Src="~/Controls/NavTopExtension.ascx" %>
<div id="navigation">
<ul>
<li id="mHome" runat="server"><a id="mHomeLink" runat="server">Home</a></li>
<li id="mPhotographs" runat="server"><a href="~/photographs" id="mPhotographsLink" runat="server">Photographs</a></li>
<asp:Repeater ID="rptTravelLogs" Runat="server"><ItemTemplate>
<nav:Link runat="server" Chapter="<%# Container.DataItem %>" /></ItemTemplate></asp:Repeater>
<nav:Extension ID="ctrExtension" runat="server" />
</ul>
</div>
