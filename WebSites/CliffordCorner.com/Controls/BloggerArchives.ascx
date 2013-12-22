<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BloggerArchives.ascx.cs" Inherits="Controls_BloggerArchives" %>
<%@ Register TagPrefix="blog" TagName="Archive" Src="~/Controls/BloggerArchive.ascx" %>
<div class="box">
<h2>Archives</h2>
<ul class="menublock">
<asp:Repeater ID="rptArchives" Runat="server"><ItemTemplate>
<blog:Archive runat="server" DataSource="<%# Container.DataItem %>" />
</ItemTemplate></asp:Repeater>
	</ul>
</div>
