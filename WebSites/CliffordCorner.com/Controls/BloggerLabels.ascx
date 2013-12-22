<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BloggerLabels.ascx.cs" Inherits="Controls_BloggerLabels" %>
<%@ Register TagPrefix="blog" TagName="Label" Src="~/Controls/BloggerLabel.ascx" %>
<div class="box">
<h2>Top Labels</h2>
<ul class="menublock">
<asp:Repeater ID="rptLabels" Runat="server"><ItemTemplate>
<blog:Label runat="server" DataSource="<%# Container.DataItem %>" />
</ItemTemplate></asp:Repeater>
	</ul>
</div>
