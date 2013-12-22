<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotographsCooliris.aspx.cs" Inherits="PhotographsCooliris" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Tags" Src="~/controls/Tags.ascx" %>
<%@ Register TagPrefix="content" TagName="TagSearch" Src="~/controls/TagSearch.ascx" %>
<%@ Register TagPrefix="content" TagName="Link" Src="~/controls/SimpleLink.ascx" %>
<%@ Register TagPrefix="content" TagName="Image" Src="~/controls/SimpleImage.ascx" %>
<%@ Register TagPrefix="blog" TagName="Posts" Src="~/controls/BloggerRecentPosts.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="content">
	<div id="splitcontentleft">
<div class="spotlight">
<object id="o" 
  classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" 
  width="600" 
  height="450">
    <param name="movie"
      value="http://apps.cooliris.com/embed/cooliris.swf" />
    <param name="allowFullScreen" value="true" />
    <param name="allowScriptAccess" value="always" />
    <param name="flashvars" value="feed=api://www.flickr.com/" />
    <embed type="application/x-shockwave-flash"
      src="http://apps.cooliris.com/embed/cooliris.swf"
      flashvars="feed=api://www.flickr.com/?group=31386902@N00"
      width="600" 
      height="450" 
      allowFullScreen="true"
      allowScriptAccess="always">
    </embed>
</object>
</div>
	</div>
</div>

<div id="subcontent">

<div class="small box">
	<a href="http://www.flickr.com/groups/cliffordcorner/pool/" title="CliffordCorner Flickr photographs" target="_blank">CliffordCorner Photographs</a><br /><br />
	<a href="http://api.flickr.com/services/feeds/groups_pool.gne?id=31386902@N00&format=rss_200" title="CliffordCorner Flickr RSS Feed" target="_blank"><content:Image runat="server" src="~/images/feed.gif" width="16" height="16" alt="CliffordCorner Flickr RSS Feed" border="0" align="absmiddle" /></a>
	<a href="http://api.flickr.com/services/feeds/groups_pool.gne?id=31386902@N00&format=rss_200" title="CliffordCorner Flickr RSS Feed" target="_blank">CliffordCorner RSS Feed</a>
</div>

<div class="box">
<h2>Quicklinks</h2>
<ul class="menublock">
	<li><content:Link runat="server" href="~/photographs/family" title="Family Photographs" Text="Family Photographs" /></li>
	<li><content:Link runat="server" href="~/photographs/2009" title="2009 Photographs" Text="2009 Photographs" /></li>
	<li><content:Link runat="server" href="~/photographs/2008" title="2008 Photographs" Text="2008 Photographs" /></li>
	<li><content:Link runat="server" href="~/photographs/2007" title="2007 Photographs" Text="2007 Photographs" /></li>
	<li><content:Link runat="server" href="~/photographs/2006" title="2006 Photographs" Text="2006 Photographs" /></li>
</ul>
</div>
	
<content:Tags id="mPopularTags" runat="server" Title="Popular Tags" Count="5" />
<content:TagSearch id="mTagSearch" runat="server" />
<blog:Posts runat="server" />

<div class="small box">
	<a href="http://www.flickr.com/photos/michigancliffy/" title="My personal Flickr photographs" target="_blank">My Flickr Photographs</a><br /><br />
	<a href="http://www.flickr.com/services/feeds/photos_public.gne?id=22671681@N00&format=rss_200" title="My personal Flickr RSS Feed" target="_blank"><content:Image runat="server" src="~/images/feed.gif" width="16" height="16" alt="My personal Flickr RSS Feed" border="0" align="absmiddle" /></a>
	<a href="http://www.flickr.com/services/feeds/photos_public.gne?id=22671681@N00&format=rss_200" title="My personal Flickr RSS Feed" target="_blank">My Flickr RSS Feed</a>
</div>
</div>
</asp:Content>
