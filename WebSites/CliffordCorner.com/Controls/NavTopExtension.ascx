<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavTopExtension.ascx.cs" Inherits="Controls_NavTopExtension" %>
<%@ Register TagPrefix="nav" TagName="Item" Src="~/Controls/NavTopExtensionItem.ascx" %>
<li><a href="javascript: void(0);" onclick="moreClicked(this, 'more');">More</a>
<div id="more" style="display: none;">
<ul><asp:Repeater ID="rptMoreTravelLogs" runat="server"><ItemTemplate>
<nav:Item runat="server" Chapter="<%# Container.DataItem %>" /></ItemTemplate></asp:Repeater>
</ul></div></li>