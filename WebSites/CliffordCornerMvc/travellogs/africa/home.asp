<%@ Language="VBScript" %>
<% Option Explicit %>
<% 'On Error Resume Next %>
<%
	' Declare Variables
	Dim aryImages(3)
	Dim intImage
	
	' Image Randomizer
	Randomize
	aryImages(0) = "home_01.jpg"
	aryImages(1) = "home_02.jpg"
	aryImages(2) = "home_03.jpg"
	aryImages(3) = "home_04.jpg"
	intImage = Int((uBound(aryImages) - lBound(aryImages) + 1)*Rnd() + lBound(aryImages))
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
<head>
	<title>African Safari - 2003 : Homepage</title>
	<link rel="stylesheet" type="text/css" href="includes/styles.css" />
	<script src="../common/javascripts.js"></script>
	<script src="includes/javascripts.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<style>
		#paragraph1 { float: left; width: 440px; margin: 0px; padding: 0px; }
		#resources { position: relative; float: left; width: 180px; border: 1px solid #993300; margin: 10px 10px 10px 0px; }
		#resources h3 { margin: 2px 2px 4px 2px; padding: 0px; }
		#resources p { margin: 2px 5px 2px 5px; }
		#paragraph2 { position: relative; float: left; width: 400px; margin: 0px; padding: 0px; }
	</style>
</head>

<body>

<!-- #include file="includes/defaultnav.asp" -->

<div id="container">
	<div id="content">
		<div id="paragraph1">
			<p>On September 3rd, 2003, nine intrepid travelers set forth to an unknown (well, to them anyway) continent for two
and a half weeks of adventure. This is their story (my version of it).</p>

			<p>Big props to Mungi Rodi (Chief Rod, aka Rod Schultz) for spending countless hours planning the trip, haggling with
the safari tour operators, and basically getting all of us there and back in one piece. Simply put, without Rod, this trip
would never have happened.</p>
		</div>
	<!-- #include file="includes/subnav.asp" -->
	</div>
	
	<div id="content">
		<div id="resources">
			<h3>Other Links:</h3>
			<p><a href="http://www.employees.org/~rschultz/Safari/intro.htm" target="_blank">Rod & Jen's Site</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.15oplgj9&x=0&y=oml2ms" target="ofoto">Amsterdam (Pierce/Bres)</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.10mjrqel&x=0&y=-2gx3h1" target="ofoto">Tarangire (Pierce/Bres)</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.13v9e3bx&x=1&y=nxhj4p" target="ofoto">Serengeti (Pierce/Bres)</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.7qqc7jx&x=1&y=-2dfh7k" target="ofoto">Hiking (Pierce/Bres)</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.11vr41wd&x=1&y=7lw744" target="ofoto">Ngorongoro (Pierce/Bres)</a></p>
			<p><a href="http://www.ofoto.com/I.jsp?c=x7a9bqt.18wmuyot&x=0&y=3b560z" target="ofoto">Masai Mara (Pierce/Bres)</a></p>
			<p><a href="http://www.glcom.com/hassan/swahili.html"" target="_blank">Useful Swahili</a></p>
		</div>
		<div id="paragraph2">
			<p>Special thanks to Jen for: 1) keeping Rod sane and motivated while planning the trip and 2) putting up with all of 
us interlopers.</p>

			<p>Also thanks to our videographers Rich (Coppola) Mosko and Jed (Spielberg) Lau. As well as photographers Don Breslin
and Laura Pierce. And lastly, but definitely not lease, videographer assistant Ana Lui and the "entertainment" Keith
Holleman.</p>

			<p>Thanks guys, for letting me be a part of such an amazing trip.<br />Bill</p>
		</div>
	</div>
	
	<div id="content">
		<div id="border"><img src="images/<%= aryImages(intImage) %>" width="600" height="200" alt="" border="0" /></div>
	</div>
</div>

<!-- #include file="../common/footer.htm" -->

</body>
</html>
