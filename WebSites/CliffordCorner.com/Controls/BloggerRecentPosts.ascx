<%@ Control Language="c#" Inherits="Controls_BloggerRecentPosts" CodeFile="BloggerRecentPosts.ascx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="blog" TagName="Post" Src="~/Controls/BloggerPostSummary.ascx" %>
<div class="box">
<h2>Recent Posts</h2>
<ul class="menublock">
<asp:Repeater ID="rptPosts" Runat="server"><ItemTemplate>
<blog:Post runat="server" DataSource="<%# Container.DataItem %>" /></ItemTemplate>
</asp:Repeater>
</ul>
</div>