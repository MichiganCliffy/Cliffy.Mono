<%@ Control Language="c#" Inherits="Controls_ErrorFlickr" CodeFile="ErrorFlickr.ascx.cs" AutoEventWireup="true" %>
<div class="box">
	<div class="important">We are having problems communicating with Flickr.Com</div>
	<p>Give it a bit of time, I'm sure things will be working shortly.</p>
	
	<hr />
	<p>The following is for debugging purposes.</p>
	<div class="important" id="mMessage" runat="server" />
	<p id="mStackTrace" runat="server" />
</div>