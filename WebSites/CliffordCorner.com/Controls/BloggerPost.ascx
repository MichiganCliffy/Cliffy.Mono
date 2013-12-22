<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BloggerPost.ascx.cs" Inherits="Controls_BloggerPost" %>
<%@ Register TagPrefix="post" TagName="Label" Src="~/Controls/BloggerPostLabel.ascx" %>
<div style="clear: both;">
<h2><asp:Literal ID="txtPublishDate" runat="server" /></h2>
<h3><asp:Literal ID="txtTitle" runat="server" /></h3>
<asp:Literal ID="txtContent" runat="server" />
<asp:Panel ID="pnlAuthor" runat="server" style="clear: both;">
<asp:Literal ID="txtAuthor" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlLabels" runat="server" style="clear: both;">
Labels:
<asp:Repeater ID="rptLabels" runat="server"><ItemTemplate><post:Label runat="server" DataSource="<%# Container.DataItem %>" /></ItemTemplate><SeparatorTemplate>,
</SeparatorTemplate></asp:Repeater>
</asp:Panel>
<asp:Panel ID="pnlReadMore" runat="server" style="clear: both; margin-bottom: 10px;">
<asp:HyperLink ID="lnkReadMore" runat="server" ToolTip="read and write comments and more">Read / Write comments and more</asp:HyperLink>
</asp:Panel>
</div>