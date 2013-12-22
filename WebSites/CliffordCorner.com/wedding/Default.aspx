<%@ Page Language="C#" MasterPageFile="~/CliffyMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="wedding_Default" Title="The Clifford Corner => Wedding Info => Ceremony &amp; Reception" %>
<%@ Register TagPrefix="wedding" TagName="Details" Src="~/controls/WeddingDetails.ascx" %>
<%@ Register TagPrefix="wedding" TagName="Menu" Src="~/controls/WeddingMenu.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div id="content">

<asp:Panel ID="pnlBackground" runat="server" style="background-position: right; background-repeat: no-repeat; height: 180px;">

<h2>The Ceremony &amp; Reception</h2>

<div style="margin-bottom: 5px;">
<div style="float: left; width: 50px; margin-right: 5px;"><strong>When:</strong></div>
<div>May 22, 2009, 5:00 PM</div>
</div>

<div style="margin-bottom: 5px; clear: both;">
<div style="float: left; width: 50px; margin-right: 5px;"><strong>Where:</strong></div>
<div style="float: left;">
    <div><a href="http://www.1fortmason.com/" target="_blank" title="One Fort Mason">One Fort Mason</a></div>
    <div>San Francisco, CA, 94123</div>
    <div><a href="http://maps.google.com/maps?cid=0,0,17933146269487798097&fb=1&split=1&gl=us&dq=1+fort+mason,+san+francisco,c+a&daddr=1+Fort+Mason,+San+Francisco,+CA+94123&geocode=3543102527534366071,37.805579,-122.426340&ei=zJmuSdbbKZGksQPhjOWXDg&sa=X&oi=local_result&resnum=1&ct=directions-to" target="_blank" title="directions to 1 Fort Mason">directions</a></div>
</div>
</div>

<div style="margin-bottom: 5px; clear: both;">
<div style="float: left; width: 50px; margin-right: 5px;"><strong>Dress:</strong></div>
<div>Semi-formal</div>
</div>

<div style="margin-bottom: 5px; clear: both;">Reception to follow the ceremony.</div>
</asp:Panel>

<h3>Open House</h3>
<p style="margin-bottom: 5px; padding: 0px;">For out of towners and locals alike, we will be hosting an open house at our flat on Saturday, May 23rd, 2009,
from 5:00 PM onward. Come by anytime to say hi, see our place (if you haven't already) and help us drink the inevitable leftover wine and champagne.</p>
<div style="float: left; width: 50px; margin-right: 5px;"><strong>Where:</strong></div>
<div style="float: left;">
    <div>4214 24th Street (at Diamond)</div>
    <div>San Francisco, CA, 94114</div>
    <div><a href="http://maps.google.com/maps?hl=en&rls=com.microsoft:*&q=4214+24th+Street,+San+Francisco,+CA&um=1&ie=UTF-8&split=0&gl=us&ei=8J6uSZLtOZGYsAOThviqDg&sa=X&oi=geocode_result&resnum=1&ct=title" target="_blank" title="directions to our place">directions</a></div>
</div>

</div>

<div id="subcontent">
<wedding:Details runat="server" />
<wedding:Menu runat="server"/>
</div>
</asp:Content>
