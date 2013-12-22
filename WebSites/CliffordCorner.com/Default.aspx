<%@ Page language="c#" Inherits="Default" CodeFile="Default.aspx.cs" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Rotator" Src="~/controls/ImageRotator.ascx" %>
<%@ Register TagPrefix="flickr" TagName="Photographs" Src="~/controls/RecentPhotographs.ascx" %>
<%@ Register TagPrefix="blog" TagName="Archives" Src="~/controls/BloggerArchives.ascx" %>
<%@ Register TagPrefix="blog" TagName="Labels" Src="~/controls/BloggerLabels.ascx" %>
<%@ Register TagPrefix="blog" TagName="Posts" Src="~/Controls/BloggerPosts.ascx" %>
<%@ Register TagPrefix="content" TagName="Image" Src="~/controls/SimpleImage.ascx" %>

<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
<div id="content">
<blog:Posts runat="server" />
</div>

<div id="subcontent">
<flickr:Photographs id="mPhotographs" runat="server" />
<blog:Labels runat="server" />
<blog:Archives runat="server" />

<div class="small box">
	<a href="http://blog.cliffordcorner.com/feeds/posts/default?alt=rss" title="CliffordCorner Blog RSS Feed" target="_blank"><content:Image runat="server" src="~/images/feed.gif" width="16" height="16" alt="CliffordCorner Blog RSS Feed" border="0" align="absmiddle" /></a>
	<a href="http://blog.cliffordcorner.com/feeds/posts/default?alt=rss" title="CliffordCorner Blog RSS Feed" target="_blank">CliffordCorner Blog RSS Feed</a>
</div>
</div>
</asp:Content>