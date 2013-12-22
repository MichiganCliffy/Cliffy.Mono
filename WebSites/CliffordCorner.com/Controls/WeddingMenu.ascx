<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeddingMenu.ascx.cs" Inherits="Controls_WeddingMenu" %>
<div class="box">
<ul class="menublock">
<li><a href="~/wedding/" runat="server" ID="lnkHome">Ceremony &amp; Reception</a></li>
<li><a href="~/wedding/todo.aspx" runat="server" id="lnkToDo">Around San Francisco</a></li>
<ul class="menublock">
    <li><a href="~/wedding/todo.aspx#alcatraz" runat="server">Alcatraz</a></li>
    <li><a href="~/wedding/todo.aspx#fishermanswharf" runat="server">Fishermans Wharf</a></li>
    <li><a href="~/wedding/todo.aspx#goldengatepark" runat="server">Golden Gate Park</a></li>
    <li><a href="~/wedding/todo.aspx#neighborhoods" runat="server">The Neighborhoods</a></li>
    <li><a href="~/wedding/todo.aspx#presidio" runat="server">The Presidio</a></li>
    <li><a href="~/wedding/todo.aspx#civiccenter" runat="server">The Civic Center</a></li>
    <li><a href="~/wedding/todo.aspx#winecountry" runat="server">Wine Country</a></li>
</ul>
</ul>
</div>