<% Dim strPageName %>
<% strPageName = lCase(Right(Request.ServerVariables("SCRIPT_NAME"), Len(Request.ServerVariables("SCRIPT_NAME")) -  InStrRev(Request.ServerVariables("SCRIPT_NAME"), "/"))) %>

<div id="header"><h1>East Africa - September 2003</h1></div>

<div id="navigation">
	<a href="default.htm" onMouseOver="imgOn('tn1')" onMouseOut="imgOff('tn1')" onMouseDown="imgDown('tn1')" onMouseUp="imgOn('tn1')"><img name="tn1" src="images/tn_intro_off.gif" width="60" height="40" border="0" alt=""></a>

<% If (lCase(strPageName) = "home.asp") Then %>
	<a href="home.asp" onMouseDown="imgDown('tn2')" onMouseUp="imgOn('tn2')"><img name="tn2" src="images/tn_home_on.gif" width="60" height="40" border="0" alt=""></a>
<% Else %>
	<a href="home.asp" onMouseOver="imgOn('tn2')" onMouseOut="imgOff('tn2')" onMouseDown="imgDown('tn2')" onMouseUp="imgOn('tn2')"><img name="tn2" src="images/tn_home_off.gif" width="60" height="40" border="0" alt=""></a>
<% End If %>

<% If (lCase(strPageName) = "tarangire.asp") OR (lCase(Request.QueryString("title")) = "tarangire") Then %>
		<a href="tarangire.asp" onMouseDown="imgDown('tn3')" onMouseUp="imgOn('tn3')"><img name="tn3" src="images/tn_tarangire_on.gif" width="100" height="40" border="0" alt=""></a>
<% Else %>
		<a href="tarangire.asp" onMouseOver="imgOn('tn3')" onMouseOut="imgOff('tn3')" onMouseDown="imgDown('tn3')" onMouseUp="imgOn('tn3')"><img name="tn3" src="images/tn_tarangire_off.gif" width="100" height="40" border="0" alt=""></a>
<% End If %>

<% If (lCase(strPageName) = "ngorongoro.asp") OR (lCase(Request.QueryString("title")) = "ngorongoro") Then %>
		<a href="ngorongoro.asp" onMouseDown="imgDown('tn4')" onMouseUp="imgOn('tn4')"><img name="tn4" src="images/tn_ngorongoro_on.gif" width="120" height="40" border="0" alt=""></a>
<% Else %>
		<a href="ngorongoro.asp" onMouseOver="imgOn('tn4')" onMouseOut="imgOff('tn4')" onMouseDown="imgDown('tn4')" onMouseUp="imgOn('tn4')"><img name="tn4" src="images/tn_ngorongoro_off.gif" width="120" height="40" border="0" alt=""></a>
<% End If %>

<% If (lCase(strPageName) = "serengeti.asp") OR (lCase(Request.QueryString("title")) = "serengeti") Then %>
		<a href="serengeti.asp" onMouseDown="imgDown('tn5')" onMouseUp="imgOn('tn5')"><img name="tn5" src="images/tn_serengeti_on.gif" width="100" height="40" border="0" alt=""></a>
<% Else %>
		<a href="serengeti.asp" onMouseOver="imgOn('tn5')" onMouseOut="imgOff('tn5')" onMouseDown="imgDown('tn5')" onMouseUp="imgOn('tn5')"><img name="tn5" src="images/tn_serengeti_off.gif" width="100" height="40" border="0" alt=""></a>
<% End If %>

<% If (lCase(strPageName) = "mara.asp") OR (lCase(Request.QueryString("title")) = "massi mara") Then %>
		<a href="mara.asp" onMouseDown="imgDown('tn6')" onMouseUp="imgOn('tn6')"><img name="tn6" src="images/tn_mara_on.gif" width="100" height="40" border="0" alt="Checkin out the Masai Mara"></a>
<% Else %>
		<a href="mara.asp" onMouseOver="imgOn('tn6')" onMouseOut="imgOff('tn6')" onMouseDown="imgDown('tn6')" onMouseUp="imgOn('tn6')"><img name="tn6" src="images/tn_mara_off.gif" width="100" height="40" border="0" alt="Checkin out the Masai Mara"></a>
<% End If %>

</div>
