<%@ Control Language="c#" Inherits="Controls_Paging" CodeFile="Paging.ascx.cs" AutoEventWireup="true" %>
<div id="paging">
<ul>Pages:
<asp:PlaceHolder ID="phPrevPage" Runat="server"><li><a id="lnkPrevPage" runat="server">&lt;</a></li></asp:PlaceHolder>
<asp:Repeater ID="mPages" Runat="server" OnItemCreated="Pages_ItemCreated">
<ItemTemplate><li><a id="mPage" runat="server" /></li></ItemTemplate>
</asp:Repeater>
<asp:PlaceHolder ID="phNextPage" Runat="server"><li><a id="lnkNextPage" runat="server">&gt;</a></li></asp:PlaceHolder>
</ul>
</div>