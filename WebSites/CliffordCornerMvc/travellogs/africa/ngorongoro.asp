<%@ Language="VBScript" %>
<% Option Explicit %>
<% 'On Error Resume Next %>
<!-- #include file="../common/Paging.asp" -->
<!-- #include file="../common/Album.asp" -->
<!-- #include file="../common/ErrorMgr.asp" -->
<%
	' Declare Variables
	Dim objPaging
	Dim objAlbum
	
	' Instantiate Objects
	Set objPaging = New clsPaging
	Set objAlbum = New clsAlbum
	
	' Load Images & Thumbnails
	Call objAlbum.Load("ngorongoro", Request.ServerVariables("SCRIPT_NAME"), "Ngorongoro")
	
	' Load Paging
	Call objPaging.LoadOverride(objAlbum, 24, 6)
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
<head>
	<title>African Safari - 2003 : Ngorongoro</title>
	<link rel="stylesheet" type="text/css" href="includes/styles.css" />
	<script src="../common/javascripts.js"></script>
	<script src="includes/javascripts.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body>

<!-- #include file="includes/defaultnav.asp" -->

<div id="container">
	<div id="content"><%= objPaging.RenderPaging() %></div>
	<div id="content"><%= objPaging.RenderThumbnails() %></div>
</div>

<!-- #include file="../common/footer.htm" -->

</body>
</html>
