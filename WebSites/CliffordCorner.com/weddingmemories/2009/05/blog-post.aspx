<%@ Page language="c#" MasterPageFile="~/CliffyMasterPage.master" %>
<%@ Register TagPrefix="content" TagName="Photographs" Src="~/controls/RecentPhotographs.ascx" %>
<%@ Register TagPrefix="content" TagName="Rotator" Src="~/controls/ImageRotator.ascx" %>
<%@ Register TagPrefix="content" TagName="Tags" Src="~/controls/BlogTags.ascx" %>
<%@ Register TagPrefix="content" TagName="CountDown" Src="~/Controls/CountDown.ascx" %>
<%@ Register TagPrefix="wedding" TagName="Details" Src="~/controls/WeddingDetails.ascx" %>

<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
<div id="content">
	<a name="2713656161710606543"></a>
	<h3>Our First Batch of Pictures!!!</h3>
	<div style="margin-bottom: 5px;"><div>Courtesy of Ray and Carmel Rogers.</div><br /><a target="_Blank" style="float: left; margin-right: 5px; margin-bottom: 5px;" onblur="try {parent.deselectBloggerImageGracefully();} catch(e) {}" href="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-006-791234.jpg"><img style="cursor:pointer; cursor:hand;width: 320px; height: 240px;" src="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-006-791231.jpg" border="0" alt="" /></a><br /><a target="_Blank" style="float: left; margin-right: 5px; margin-bottom: 5px;" onblur="try {parent.deselectBloggerImageGracefully();} catch(e) {}" href="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-005-748375.jpg"><img style="cursor:pointer; cursor:hand;width: 240px; height: 320px;" src="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-005-748372.jpg" border="0" alt="" /></a><br /><a target="_Blank" style="float: left; margin-right: 5px; margin-bottom: 5px;" onblur="try {parent.deselectBloggerImageGracefully();} catch(e) {}" href="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-004-748352.jpg"><img style="cursor:pointer; cursor:hand;width: 240px; height: 320px;" src="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-004-748350.jpg" border="0" alt="" /></a><br /><a target="_Blank" style="float: left; margin-right: 5px; margin-bottom: 5px;" onblur="try {parent.deselectBloggerImageGracefully();} catch(e) {}" href="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-002-719021.jpg"><img style="cursor:pointer; cursor:hand;width: 240px; height: 320px;" src="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-002-719018.jpg" border="0" alt="" /></a><br /><a target="_Blank" style="float: left; margin-right: 5px; margin-bottom: 5px;" onblur="try {parent.deselectBloggerImageGracefully();} catch(e) {}" href="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-001-719000.jpg"><img style="cursor:pointer; cursor:hand;width: 240px; height: 320px;" src="http://www.cliffordcorner.com/weddingmemories/uploaded_images/Bill-&-Debbie's-Wedding-May-2009-001-718997.jpg" border="0" alt="" /></a><br /><div style="clear: both;"></div><p class="blogger-labels">Labels: <a rel='tag' href="http://www.cliffordcorner.com/weddingmemories/labels/Ceremony.aspx">Ceremony</a></p></div>
	<p class="entry-footer">
		Posted by MichiganCliffy on 

Thursday, May 28, 2009 at 5:07 PM </span>
	</p>

</div>

<div id="subcontent">
<wedding:Details runat="server" />
<content:Tags runat="server" Path="/weddingmemories/labels" />
</div>
</asp:Content>