<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CacheMgr.aspx.cs" Inherits="CacheMgr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:Repeater ID="rptItems" runat="server">
    <ItemTemplate>
    <div><asp:LinkButton runat="server" CommandName="DeleteItem" CommandArgument="<%# Container.DataItem %>" Text="<%# Container.DataItem %>" OnCommand="lnkDelete_Command" /></div></ItemTemplate><FooterTemplate>
    <div><asp:LinkButton runat="server" CommandName="DeleteAll" Text="Delete All" OnCommand="lnkDelete_Command" /></div>
    </FooterTemplate>
    </asp:Repeater>
    
    </form>
</body>
</html>
