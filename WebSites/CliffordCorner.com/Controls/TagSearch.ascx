<%@ Control Language="c#" Inherits="Controls_TagSearch" CodeFile="TagSearch.ascx.cs" AutoEventWireup="true" %>
<asp:Panel ID="pnlSearchBox" runat="server" CssClass="box" DefaultButton="btnSearch">
<div id="searchbar"><h2>Tag Search</h2>
<fieldset>
<input type="text" id="t" name="t" class="input" value="<%= this.SearchText %>" />
<asp:Button ID="btnSearch" Runat="server" CssClass="button" Text="Go!" UseSubmitBehavior="false" />
</fieldset>
</div>
</asp:Panel>