<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BloggerPosts.ascx.cs" Inherits="Controls_BloggerPosts" %>
<%@ Register TagPrefix="blog" TagName="Post" Src="~/Controls/BloggerPost.ascx" %>
<asp:Repeater ID="rptPosts" Runat="server"><ItemTemplate>
<blog:Post runat="server" DataSource="<%# Container.DataItem %>" />
</ItemTemplate><SeparatorTemplate>
<hr style="height: 1px; clear: both; margin-bottom: 20px; border-width: 0px; color: #c0c0c0; background-color: #c0c0c0;" />
</SeparatorTemplate></asp:Repeater>
