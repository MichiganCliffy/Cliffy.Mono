<%@ Control Language="c#" Inherits="Controls_BlogTags" CodeFile="BlogTags.ascx.cs" %>
<div class="box">
	<h2 id="hTitle" runat="server"><asp:Literal ID="txtTitle" Runat="server" Text="Labels" /></h2>
	<ul class="menublock">
<asp:Repeater ID="rptTags" Runat="server" OnItemCreated="rptTags_ItemCreated"><ItemTemplate>
		<li><a id="lnkTag" runat="server"/></li>
</ItemTemplate></asp:Repeater>
	</ul>
</div>
