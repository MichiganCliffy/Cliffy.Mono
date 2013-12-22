<%@ Language="VBScript" %>
<% Option Explicit %>
<% 'On Error Resume Next %>
<!-- #include file="../common/Paging.asp" -->
<!-- #include file="../common/Album.asp" -->
<!-- #include file="../common/ErrorMgr.asp" -->
<%
	' Constants
	Const DISPLAY_COUNT = 16
	
	' Declare Variables
	Dim strImgFile
	Dim strAlbumName
	Dim strAlbumTitle
	Dim objAlbum
	Dim aryImgData
	
	' Instantiate Objects
	Set objAlbum = New clsAlbum
	
	' Set Variables
	strImgFile = Request.QueryString("img")
	strAlbumName = Request.QueryString("album")
	strAlbumTitle = Request.QueryString("title")
	
	' Load Images & Thumbnails
	Call objAlbum.Load(strAlbumName, strAlbumName & ".asp", strAlbumTitle)
	
	' Get Next/Prev Link
	aryImgData = objAlbum.GetPictureData(strImgFile)
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
<head>
	<title>African Safari - 2003 : <%= strAlbumTitle %></title>
	<link rel="stylesheet" type="text/css" href="includes/styles.css" />
	<script src="../common/javascripts.js"></script>
	<script src="includes/javascripts.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body>

<!-- #include file="includes/defaultnav.asp" -->

<div id="container">
	<div id="content">
		<div id="paging">
			<div id="pagingLeft">
				&lt; <a href="<%= objAlbum.Landing %>">Back to Album</a> |
				(Taken by <%= aryImgData(3) %>)
			</div>
			<div id="pagingRight">
<% If (Len(aryImgData(1)) > 0) Then %>
			&lt; <a href="picture.asp?img=<%= aryImgData(1) %>&album=<%= strAlbumName %>&title=<%= strAlbumTitle %>">Prev</a>
<% End If %>
			[ Picture <%= aryImgData(0) %> of <%= uBound(objAlbum.Thumbnails) %> ]
<% If (Len(aryImgData(2)) > 0) Then %>
			<a href="picture.asp?img=<%= aryImgData(2) %>&album=<%= strAlbumName %>&title=<%= strAlbumTitle %>">Next</a> &gt;
<% End If %>
			</div>
		</div>
	</div>

	<div id="image"><img class="border" src="<%= objAlbum.Folder & "/images/" & strImgFile %>" border="0" alt=""></div>

	<% If (Len(aryImgData(5)) > 0) Then %><div id="content"><%= aryImgData(5) %></div><% End If %>
</div>

<!-- #include file="../common/footer.htm" -->

</body>
</html>
