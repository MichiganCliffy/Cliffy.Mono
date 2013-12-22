<%@ Control Language="c#" Inherits="Controls_Tags" CodeFile="Tags.ascx.cs" AutoEventWireup="true" %>
<div class="box">
<asp:Repeater ID="mLinks" Runat="server" OnItemCreated="Links_ItemCreated">
<HeaderTemplate>
<h2 id="mTitle" runat="server" />
<ul class="menublock">
</HeaderTemplate>
<ItemTemplate>
<li><a id="mLink" runat="server" href="~/photographs" /></li>
</ItemTemplate>
<FooterTemplate>
</ul>
</FooterTemplate>
</asp:Repeater>
</div>