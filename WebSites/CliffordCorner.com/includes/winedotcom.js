<!--
// Common Properties
var keyTimer = null; // timer used for footnote pop ups
var requests = null; // add to shopping cart requests
var cartOmnitureData = null; // shopping cart data to send to omniture
var cartAjaxUrl = "/checkout/cartsummary.aspx"; // default page for ajax cart requests
var cartDefaultUrl = "/checkout/default.aspx"; // default display page for the shopping cart
var addthis_pub='wine.com'; // account information for the addthis sharing page
var cart = null; // Cart Widget, for sending products to the shopping cart

function HideInfoPop()
{
    var div = document.getElementById("divInfoPop");
    div.style.visibility = "hidden";
    var subDiv;
    
    for(i = 1; i< 15; i++)
    {
        subDiv = document.getElementById("divInfoSub" + i);
        subDiv.style.display = "none";
    }
  
}

// for about pop up
function LoadInfoPop(id) 
{
    var div = document.getElementById("divInfoPop");
    var subDiv;
    var subDivImage;
    
    if (div) 
	{
	    subDiv = document.getElementById("divInfoSub" + id);       
	    subDivImage = document.getElementById("imgInfoPop" + id);  

	    subDiv.style.display = "block";
	    //subDivImage.src = "http://" + document.domain + "/images/aboutwine/aboutPop/imgPop" + id + ".jpg"; 
	    div.style.visibility = "visible";
	}
}



//  for promo DHTML

    function RePositionPromoPopup()
        {
               var height = 300;
               var leftOffSet =180;             
               var  topOffSet = 110;
               var positionTop =  findPosition(document.getElementById("lowerTopNav")); 
               var positionBottom =  findPosition(document.getElementById("lowerBottomNav")); 
               var positionCurrent = findPosition(document.getElementById("divPromo")); 
               
               //alert(positionTop[1] + " - " + positionTop[0]);
               //alert(positionCurrent[1] + " - " + positionCurrent[0]);
               if(positionTop  && positionBottom)
               {
                        height = positionBottom[1] -  positionTop[1];
                        leftOffSet= 100+positionTop[0];
                        topOffSet=110+positionTop[1];
                        //document.getElementById("divPromo").style.position="absolute";
                        //document.getElementById("divPromo").style.top=positionTop[1]+"px";
                        if (positionCurrent[0] < 150)
                        {
                           //document.getElementById("divPromo").style.left= 350;
                        }
                        //document.getElementById("divPromo").style.height=height+"px";
                     
                        //document.getElementById("StateSelectPanel").style.top=topOffSet+"px";
                        //document.getElementById("StateSelectPanel").style.left=leftOffSet+"px";
                        //document.getElementById("StateSelectPanel").style.position="absolute";
               }
              
          }     
          
    function HideDiv2(divId)
    {
        var item = document.getElementById(divId);
        if (item) {
            item.style.visibility = "hidden";
		    item.style.display = "block";
	    }
    }

    function LoadPromo(redirectId) 
    {
		var url = "/v6/services/promocodeservice.aspx?id=" + redirectId;
	    // branch for native XMLHttpRequest object
	    if (window.XMLHttpRequest) {
	        req = new XMLHttpRequest();
	        req.onreadystatechange = ShowPromo;
	        req.open("GET", url, true);
	        req.send(null);
	    // branch for IE/Windows ActiveX version
	    } else if (window.ActiveXObject) {
	        req = new ActiveXObject("Microsoft.XMLHTTP");
	        if (req) {
	            req.onreadystatechange = ShowPromo;
	            req.open("GET", url, true);
	            req.send();
	        }
	    }
	}
	
	function ShowPromo() 
	{
		if (req.readyState == 4) 
		{
		    
		    var divPromo = document.getElementById("divPromo");
			var divPromoCode = document.getElementById("divPromoCode");
			var divLongDesc = document.getElementById("divLongDesc");
			var divShortDesc = document.getElementById("divShortDesc");
			
			if (divPromo) 
			{
			    
			    // process the request
			    if (req.responseXML.getElementsByTagName('Error')[0].firstChild.nodeValue == "True")
			    {
                    divLongDesc.innerHTML = "Sorry, we had a problem getting that promo code";
				    divPromo.style.visibility = "visible";
			    }
			    else
			    {
				    // we are good to go
                    // eval the XML
               	    divLongDesc.innerHTML 		= req.responseXML.getElementsByTagName('LongDesc')[0].firstChild.nodeValue;
				    divShortDesc.innerHTML 	    = req.responseXML.getElementsByTagName('ShortDesc')[0].firstChild.nodeValue;
				    divPromoCode.innerHTML 	    = req.responseXML.getElementsByTagName('PromoCode')[0].firstChild.nodeValue;
    				
				    divPromo.style.visibility = "visible";
			    }
			}
			//setTimeout("showSuggestionsFinished()", 1000);
			//RePositionPromoPopup();
		}
	}
   


// state.js
function StateAbbrev(thisValue) { 
	var ReturnValue;
	var thisNewValue = thisValue.toLowerCase();
	switch (thisNewValue) {
		case 'alaska':
			ReturnValue = 'AK'
		break
		case 'alabama':
			ReturnValue = 'AL'
		break
		case 'arkansas':
			ReturnValue = 'AR'
		break
		case 'arizona':
			ReturnValue = 'AZ'
		break
		case 'california':
			ReturnValue = 'CA'
		break
		case 'colorado':
			ReturnValue = 'CO'
		break
		case 'connecticut':
			ReturnValue = 'CT'
		break
		case 'the district of columbia':
			ReturnValue = 'DC'
		break
		case 'delaware':
			ReturnValue = 'DE'
		break
		case 'florida':
			ReturnValue = 'FL'
		break
		case 'georgia':
			ReturnValue = 'GA'
		break
		case 'hawaii':
			ReturnValue = 'HI'
		break
		case 'iowa':
			ReturnValue = 'IA'
		break
		case 'illinois':
			ReturnValue = 'IL'
		break
		case 'indiana':
		 ReturnValue = 'IN'
		break
		case 'japan':
			ReturnValue = 'JP'
		break
		case 'kansas':
			ReturnValue = 'KS'
		break
		case 'kentucky':
			ReturnValue = 'KY'
		break
		case 'louisiana':
			ReturnValue = 'LA'
		break
		case 'massachusetts':
			ReturnValue = 'MA'
		break
		case 'maryland':
			ReturnValue = 'MD'
		break
		case 'maine':
			ReturnValue = 'ME'
		break
		case 'michigan':
			ReturnValue = 'MI'
		break
		case 'minnesota':
			ReturnValue = 'MN'
		break
		case 'missouri':
			ReturnValue = 'MO'
		break
		case 'mississippo':
			ReturnValue = 'MS'
		break
		case 'montana':
			ReturnValue = 'MT'
		break
		case 'north carolina':
			ReturnValue = 'NC'
		break
		case 'north dakota':
			ReturnValue = 'ND'
		break
		case 'nebraska':
			ReturnValue = 'NE'
		break
		case 'new hampshire':
			ReturnValue = 'NH'
		break
		case 'new jersey':
			ReturnValue = 'NJ'
		break
		case 'new mexico':
			ReturnValue = 'NM'
		break
		case 'nevada':
			ReturnValue = 'NV'
		break
		case 'new york':
			ReturnValue = 'NY'
		break
		case 'ohio':
			ReturnValue = 'OH'
		break
		case 'oklahoma':
			ReturnValue = 'OK'
		break
		case 'oregon':
			ReturnValue = 'OR'
		break
		case 'pennsylvania':
			ReturnValue = 'PA'
		break
		case 'rhode island':
			ReturnValue = 'RI'
		break
		case 'south carolina':
			ReturnValue = 'SC'
		break
		case 'south dakota':
			ReturnValue = 'SD'
		break
		case 'tennessee':
			ReturnValue = 'TN'
		break
		case 'texas':
			ReturnValue = 'TX'
		break
		case 'utah':
			ReturnValue = 'UT'
		break
		case 'virginia':
			ReturnValue = 'VA'
		break
		case 'vermont':
			ReturnValue = 'VT'
		break
		case 'washington':
			ReturnValue = 'WA'
		break
		case 'wisconsin':
			ReturnValue = 'WI'
		break 
		case 'west virginia':
			ReturnValue = 'WV'
		break
		case 'wyoming':
			ReturnValue = 'WY'
		break
		default:
			ReturnValue = thisValue;
		}
	
	return ReturnValue

}


// global.js

function keyShowPositional(icon, legend, top, left) {
	keyMouseOver();
	keyHideAll();
	keyTimeout(4000);
	
	// Get key and icon
	var key = document.getElementById(legend);
	
	// Set up positioning
	var position = findPosition(icon);
	var intOffsetTop = 15 + position[1] + top;
	var intOffsetLeft = 15 + position[0] + left;
	
	key.style.top = intOffsetTop + "px";
	key.style.left = intOffsetLeft + "px";
	
	// Make visibile
	key.className = "isVisible";
	
	if (key.style.display == 'none') {
		key.style.display = '';
	}
	
	if (key.style.visibility == 'hidden') {
		key.style.visibility = "";
	}
}
	
function sharePage(service, url) {
	if (url.length == 0)
		url = location.href;
	url = url.replace(/^http:\/\/[a-z]+\.u\./i,'http://www.');
	
	//if (url.indexOf("?") > 0) {
	//	url = url + '&cid=share:' + service;
	//} else {
	//	url = url + '?cid=share:' + service;
	//}

	var title = document.title
	if ((title.indexOf("wine.com") <= 0) && (title.indexOf("Wine.com") <= 0)) {
		title = title + ' from wine.com'
	}
    encodedurl = encodeURIComponent(url);
    var encodedtitle = encodeURIComponent(title);

    var serviceUrl = null;
	
	switch(service) {
		case 'delicious':
			serviceURL	= 'http://del.icio.us/post?v=4&noui&jump=close'
						+ '&url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'digg':
			serviceURL	= 'http://digg.com/submit?phase=2'
						+ '&url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'fark':
			serviceURL	= 'http://www.fark.com/cgi/fark/submit.pl'
						+ '?new_url=' + encodedurl
						+ '&new_comment=' + encodedtitle;
			break;
			
		case 'google':
			serviceURL	= 'http://www.google.com/bookmarks/mark?op=add'
						+ '&bkmk=' + encodedurl
						+ '&title=' + encodedtitle
						+ '&labels=' + ''
						+ '&annotation=' + '';
			break;
			
		case 'newsvine':
			serviceURL	= 'http://www.newsvine.com/_tools/seed&save'
						+ '?u=' + encodedurl;
			break;
			
		case 'reddit':
			serviceURL	= 'http://reddit.com/submit'
						+ '?url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'slashdot':
			serviceURL	= 'http://slashdot.org/bookmark.pl'
						+ '?url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'technorati':
			serviceURL	= 'http://technorati.com/faves?sub=favthis'
						+ '&add=' + encodedurl;
			break;
			
		case 'blink':
			serviceURL	= 'http://www.blinklist.com/index.php?Action=Blink/addblink.php&Quick=true&Pop=yes'
						+ '&url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'yahoo':
			serviceURL	= 'http://myweb2.search.yahoo.com/myresults/bookmarklet?d=&ei=UTF-8'
						+ '&u=' + encodedurl
						+ '&t=' + encodedtitle;
			break;
			
		case 'furl':
			serviceURL	= 'http://www.furl.net/storeIt.jsp'
						+ '?u=' + encodedurl
						+ '&t=' + encodedtitle;
			break;
		
		case 'simpy':
			serviceURL	= 'http://simpy.com/simpy/LinkAdd.do?v=6&src=bookmarklet'
						+ '&href=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'spurl':
			serviceURL	= 'http://www.spurl.net/spurl.php?v=3';
						+ '&url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'wink':
			serviceURL	= 'http://www.wink.com/_/tag'
						+ '?url=' + encodedurl
						+ '&doctitle=' + encodedtitle;
			break;
			
		case 'live':
			serviceURL	= 'https://favorites.live.com/quickadd.aspx?marklet=1&mkt=en-us&top=1'
						+ '&url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'diigo':
			serviceURL	= 'http://www.diigo.com/post'
						+ '?url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
			
		case 'stumble':
			serviceURL	= 'http://www.stumbleupon.com/submit'
						+ '?url=' + encodedurl
						+ '&title=' + encodedtitle;
			break;
    }
	
    if ( serviceURL != null ) {
        var theNewWin = window.open(serviceURL,'share','width=900,height=640,resizable=yes,toolbar=no,location=no,scrollbars=yes');
        if ( typeof theNewWin != "undefined" &&
             theNewWin != null ) {
            theNewWin.focus();
        }
    }
	
    // remove the layer.
    //document.getElementById('shareKey').className = "isHidden";
	// reload page for omniture tracking
	reloadWithShare(service);
}
	
// utilities.v2.js

function open_window(url, scrollbars, width, height, name, toolbar)	{
	m_lscroll_top = document.body.scrollTop;

	var aChildWin = new Array();
	var iLeft = ((screen.availWidth - parseInt(width)) / 2);
	var iTop = ((screen.availHeight - parseInt(height)) /2);
	
	if (!toolbar) { toolbar = "no"; }
	var sProperties = "width=" + width 
					+ ", height=" + height 
					+ ", resizable=no, scrollbars=" + scrollbars 
					+ ", left=" + iLeft + "px, top= " + iTop + "px" 
					+ ", toolbar=" + toolbar 
					+ ", center=yes, status=no";
	
	var iWindex = aChildWin.length;
	var sWinName;
	
	if (name) {
		sWinName = name;
	} else {
		sWinName = "win" + iWindex.toString();
	}
	
	aChildWin[iWindex] = window.open(url, sWinName, sProperties);
	aChildWin[iWindex].opener = window.self;
	
	return aChildWin[iWindex];
}

function open_new_window(url, scrollbars, width, height, name, toolbar)	{
	m_lscroll_top = document.body.scrollTop;
	
	var aChildWin = new Array();
	var iLeft = ((screen.availWidth - parseInt(width)) / 2);
	var iTop = ((screen.availHeight - parseInt(height)) /2);
	
	if (!toolbar) { toolbar = "no"; }
	var sProperties = "width=" + width
					+ ", height=" + height 
					+ ", resizable=yes, scrollbars=" + scrollbars 
					+ ", left=" + iLeft + "px, top= " + iTop + "px" 
					+ ", toolbar=" + toolbar 
					+ ", center=yes, status=no";

	var iWindex = aChildWin.length;
	var sWinName;
	if (name) {
		sWinName = name;
	}
	else {
		sWinName = "win" + iWindex.toString();
	}
	
	aChildWin[iWindex] = window.open(url, sWinName, sProperties);
	aChildWin[iWindex].opener = window.self;
}

function getJsDate(USDateString) {
	var arrDtParts = USDateString.split("/");
	var dt = null;
	
	try {
		dt =  new Date(arrDtParts[2], (arrDtParts[0] - 1), arrDtParts[1]);
	} catch(e) {
		return null;
	}
	
	return dt;
}

function validateDate(dateString) {
	var oRE = /^\d\d\/\d\d\/\d\d\d\d$/;
	
	if (oRE.test(dateString)) {
		var dt = getJsDate(dateString);
		return (dt != null);
	}
	
	return false;
}

function calculateAge(Birthday) {
	var dtNow = new Date();
	var iYears = dtNow.getFullYear() - Birthday.getFullYear();
	var iBirthMonth = Birthday.getMonth();
	var iBirthDay = Birthday.getDay();
	var iNowMonth = dtNow.getMonth();
	var iNowDay = dtNow.getDay();
	
	if (iNowMonth > iBirthMonth) {
		return iYears;
	} else {
		if (iNowDay > iBirthDay) {
			return iYears;
		} else {
			return iYears - 1;
		}
	}
}

function IsNumeric(expression) {
	var validChars = "0123456789";
	var validChar;
	var output = true;
	
	for (i = 0; i < expression.length && output == true; i++) { 
		validChar = expression.charAt(i);
		
		if (validChars.indexOf(validChar) == -1) {
			output = false;
		}
	}
	
	return output;
}

function IsOrderNumber(expression) {
	var validChars = "0123456789-";
	var validChar;
	var output = true;
	
	for (i = 0; i < expression.length && output == true; i++) { 
		validChar = expression.charAt(i);
		
		if (validChars.indexOf(validChar) == -1) {
			output = false;
		}
	}
	
	return output;
}
	
function quantityCheck(input, limit, stock) {
	if (!IsNumeric(input.value)) {
		alert("Please enter a valid, numeric, quantity.");
		input.focus();
		return;
	}
	
	var quantity = parseInt(input.value);
	if (quantity > limit) {
		alert("This item is limited to " + limit.toString() + " per customer.");
		input.value = limit;
		input.focus();
		return;
	}
	
	return;
}
   
function findPosition(item) {
	if (item.offsetParent) {
		
		for( var posX = 0, posY = 0; item.offsetParent; item = item.offsetParent ) {
			posX += item.offsetLeft;
			posY += item.offsetTop;
		}
		
		return [ posX, posY ];
		
	} else {
		
		return [ item.x, item.y ];
		
	}
}

function findBrowserDimensions() {
	var width = 0;
	var height = 0;
	
	if (typeof(window.innerWidth) == 'number') {
		//Non-IE
		width = window.innerWidth;
		height = window.innerHeight;
	} else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
		//IE 6+ in 'standards compliant mode'
		width = document.documentElement.clientWidth;
		height = document.documentElement.clientHeight;
	} else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
		//IE 4 compatible
		width = document.body.clientWidth;
		height = document.body.clientHeight;
	}
	
	return [ width, height ];
}

function findBrowserOffset() {
	var width = 0;
	var height = 0;
	
	if (typeof(window.pageYOffset) == 'number') {
		//Netscape compliant
		width = window.pageXOffset;
		height = window.pageYOffset;
	} else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
		//DOM compliant
		width = document.body.scrollLeft;
		height = document.body.scrollTop;
	} else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
		//IE6 standards compliant mode
		width = document.documentElement.scrollLeft;
		height = document.documentElement.scrollTop;
	}
	
	return [ width, height ];
}

function closeAndRefreshParent() {
	window.opener.page_form.mode.value="refresh";
	window.opener.page_form.onsubmit();
	window.opener.page_form.submit();
	window.opener.focus();
	window.close();
}

function parentPageJump(path) {
	window.opener.location=path;
	window.opener.focus();
	window.close();
}

function replaceIt(sString, sReplaceThis, sWithThis) { 
	if (sReplaceThis != "" && sReplaceThis != sWithThis) { 
		var counter = 0; 
		var start = 0; 
		var before = ""; 
		var after = ""; 
		while (counter<sString.length) { 
			start = sString.indexOf(sReplaceThis, counter); 
			if (start == -1) { 
				break; 
			} else { 
				before = sString.substr(0, start); 
				after = sString.substr(start + sReplaceThis.length, sString.length); 
				sString = before + sWithThis + after; 
				counter = before.length + sWithThis.length; 
			} 
		} 
	} 
	return sString; 
} 

function closeBoxOver(item) {
	item.className = "closeBoxHover";
}

function closeBoxOut(item) {
	item.className = "closeBox";
}


// shoppingcart.v2.js

function cartOmniture(e) {
	this.PageName = SetPageName(e);
	this.PageType = SetPageType(e);
	this.Refinements = SetRefinements(e);
	this.RefinementsOrdered = SetRefinementsOrdered(e);
	
	function SetPageName(e) {
		if (e) {
			return e.pageName;
		}
	}
	
	function SetPageType(e) {
		if (e) {
			return e.prop10;
		}
	}
	
	function SetRefinements(e) {
		if (e) {
			return e.prop17;
		}
	}
	
	function SetRefinementsOrdered(e) {
		if (e) {
			return e.prop18;
		}
	}
}

function cartWidget() {
	this.mCartLink 		= "<a href='/checkout/default.aspx' class='widgetlink'  title='Proceed to Checkout'>cart</a>";
	this.mCheckoutLink	= "<span class=\"tnavshipto\"> | </span><a href=\"/checkout/default.aspx\" title=\"Proceed to Checkout\" class=\"widgetlink\">checkout</a></span>";
	this.mLoading 		= "<div style='display: inline;' align='center'><i>loading cart</i> <img src=\"/images/img_loading.gif\" width=\"10\" height=\"10\" /></div>";
	this.mWidget 		= null;

	// Private method that generates an Id to use for the request
	function timeStamp() {
		var date = new Date();
		return date.getTime();
	}
	
	// Initializes the widget
	this.Initialize = function() {
		this.mWidget = document.getElementById("cartWidget");
		if (this.mWidget) {
			this.mWidget.style.visibility = "visible";
		}
		
		var request = new cartRequest();
		cartMakeRequest(request);
	}
	
	// Flip the cart to the loading state
	this.SetLoading = function() {
		if (this.mWidget) {
			this.mWidget.innerHTML = this.mLoading;
		}
	}
	
	// Flip the cart to the summary state
	this.SetSummary = function(qty) {
		if (this.mWidget) {
			this.mWidget.innerHTML = "<b>" + qty + "</b> items in your " + this.mCartLink + this.mCheckoutLink;
		}
	}
}

// Simple object that defines a product in the cart
function cartProduct() {
	this.Id = 0; // product id
	this.Sku = ""; // pproduct id
	this.Qty = 0; // quantity added
	this.Description = ""; // description of the product
	this.Action = ""; // action (added)
	this.Price = 0.0; // price of the product
	this.Type = -1; // product type (category)
	this.Stock = 9999; // current stock levels of the product
	this.NeverShow = false; // never show flag
	
	// write to Omniture string format (type;pproduct_id;qty;price)
	this.ToOmniture = function() {
		return this.Type + ";" + this.Sku + ";" + this.Qty + ";" + this.Price
	}
}

// Object that defines a request to the cart service
function cartRequest() {
	this.PproductId = ""; // The pproduct/sku that is being added to the cart
	this.Qty = 0; // The quantity that is being added to the cart
	
	this.Ct = ""; // A ct tracking code
	this.Iid = ""; // A iid tracking code (is this needed?)
	this.Id = timeStamp(); // The id to use for the request
	this.Button = null; // The button that triggered the request
	this.SrcOn = ""; // The image to use when adding the product to the cart
	this.SrcOff = ""; // The image to use when finished
	
	// Private method that generates an Id to use for the request
	function timeStamp() {
		var date = new Date();
		return date.getTime();
	}
	
	// A public method that generates the URL to use when adding a product
	this.GetUrl = function() {
		var output = "?";
		if (this.PproductId.length > 0) { output += "mode=add&pproduct_id=" + this.PproductId; }
		if (this.Qty > 1) { output += "&qty=" + this.Qty }
		if (this.Ct.length > 0) { output += "&ct=" + this.Ct; }
		if (this.Iid.length > 0) { output += "&iid=" + this.Iid; }
		if (output.length > 1) { output += "&"; }
		output += "counter=" + this.Id;
		return output;
	}
	
	// A public method for flipping the button into the loading state
	this.ButtonOn = function() {
		if (this.Button) {
			if (this.SrcOn) {
				if (this.SrcOn.length > 0) {
					this.Button.src = this.SrcOn;
				}
			}
		}
	}
	
	// A public method for flipping the button into the finished state
	this.ButtonOff = function() {
		if (this.Button) {
			if (this.SrcOff) {
				if (this.SrcOff.length > 0) {
					this.Button.src = this.SrcOff;
				}
			}
		}
	}
}

// Parses out the response xml into useful data objects
function cartResponse(data) {
	this.HasError = parseError(data);
	this.ErrorMsg = parseMessage(data);
	this.CartQty = parseCartQty(data);
	this.CartTotal = parseCartTotal(data);
	this.Id = parseId(data);
	this.Products = parseProducts(data);
	
	// retrieves the root data node, in this case it's WINEDOTCOMCARTSUM
	function parseRootNode(data) {
		if (data) {
			var nodes = data.getElementsByTagName("WINEDOTCOMCARTSUM");
			if (nodes) { if (nodes.length > 0) { return nodes[0]; } }
		}
		return null;
	}
	
	// utility method for returning the data for a specific node
	function parseSimpleNode(data, node) {
		var nodes = data.getElementsByTagName(node);
		if (nodes) {
			for (var j=0; j<nodes.length; j++) {
				var value = parseNodeValue(nodes[j]);
				if (value) { return value; }
			}
		}
		return null;
	}
	
	// utility method for returning the data for a specific attribute
	function parseSimpleAttribute(data, attribute) {
		if (data) {
			var attrib = data.attributes.getNamedItem(attribute);
			if (attrib) { return attrib.value; }
		}
		return null;
	}
	
	// utility method for returning the value of a specific node
	function parseNodeValue(data) {
		if (data) {
			if (data.childNodes.length > 0) { return data.firstChild.nodeValue; }
			return data.nodeValue;
		}
		return null;
	}
	
	// returns the error flag
	function parseError(data) {
		var root = parseRootNode(data);
		if (root) {
			var value = parseSimpleNode(root, "IsError");
			if (value) { if (value == "True") { return true; } }
		} else {
			return true;
		}
		return false;
	}
	
	// returns the error message
	function parseMessage(data) {
		var root = parseRootNode(data);
		if (root) {
			var message = parseSimpleNode(root, "ErrorMsg");
			if (message) { return message; }
		}
		return "";
	}
	
	// returns the number of products in the cart
	function parseCartQty(data) {
		var root = parseRootNode(data);
		if (root) {
			var count = parseSimpleNode(root, "TotalItemCount");
			if (count) { return count; }
		}
		return 0;
	}
	
	// returns the subtotal amount in the cart (excludes tax, shipping, discounts)
	function parseCartTotal(data) {
		var root = parseRootNode(data);
		if (root) {
			var total = parseSimpleNode(root, "Total");
			if (total) { return total; }
		}
		return "$0.00";
	}
	
	// returns the id for the request/response
	function parseId(data) {
		var root = parseRootNode(data);
		if (root) {
			var id = parseSimpleAttribute(root, "id");
			if (id) { return id; }
		}
		return "";
	}
	
	// returns all of the actions that happened during the request
	function parseProducts(data) {
		var output = new Array();
		var root = parseRootNode(data);
		if (root) {
			var top = root.getElementsByTagName("products");
			if (top) {
				if (top.length > 0) {
					var products = top[0].getElementsByTagName("product");
					for (var i=0; i<products.length; i++) {
						var product = parseProduct(products[i]);
						if (product) { output.push(product); }
					}
				}
			}
		}
		
		return output;
	}
	
	// parses out a single action
	function parseProduct(data) {
		if (data) {
			var product = new cartProduct();
			
			var description = parseNodeValue(data);
			if (description) { product.Description = description; }
			
			var id = parseSimpleAttribute(data, "id");
			if (id) { product.Id = id; }
			
			var sku = parseSimpleAttribute(data, "sku");
			if (sku) { product.Sku = sku; }
			
			var qty = parseSimpleAttribute(data, "qty");
			if (qty) { product.Qty = qty; }
			
			var mode = parseSimpleAttribute(data, "mode");
			if (mode) { product.Action = mode; }
			
			var price = parseSimpleAttribute(data, "price");
			if (price) { product.Price = price; }
			
			var type = parseSimpleAttribute(data, "type");
			if (type) { product.Type = type; }
			
			var stock = parseSimpleAttribute(data, "stock");
			if (stock) { product.Stock = stock; }
			
			var nevershow = parseSimpleAttribute(data, "nevershow");
			if (nevershow) { product.NeverShow = nevershow; }
			
			return product;
		}
		
		return null;
	}
}

// method for making a ajax request to the cart
function cartMakeRequest(request) {
	if (request) {
		if (requests) {
			// add request to the requests array - this array contains all active requests
			requests.push(request);
		}
		
		// get the url and flip the button on
		var url = cartAjaxUrl + request.GetUrl();
		request.ButtonOn();
		
		// show loading state
		if (cart) {
			cart.SetLoading();
		}
		
		// make the request
		if (window.XMLHttpRequest) {
			req = new XMLHttpRequest();
			req.onreadystatechange = cartProcessRequest;
			req.open("GET", url, true);
			req.send(null);
		} else if (window.ActiveXObject) {
			req = new ActiveXObject("Microsoft.XMLHTTP");
			if (req) {
				req.onreadystatechange = cartProcessRequest;
				req.open("GET", url, true);
				req.send();
			}
		}
	}
}

// prass the response from the request
function cartProcessRequest() {
	switch(req.readyState) {
		case 200: // waiting
			break;
		case 4: // got it, process
			var response = new cartResponse(req.responseXML); // renders the response
			cartProcessResults(response); // calls method for processing the response
			break;
	}
}

// method for processing the response
function cartProcessResults(response) {
	var request = null;
	var requestPosition = -1;
	
	// get the matching request
	if (requests && response) {
		for (var i=0; i<requests.length; i++) {
			if (requests[i].Id = response.Id) {
				request = requests[i];
				requestPosition = i;
				break;
			}
		}
	}
	
	// found the matching request
	if (request) {
		// flip off the button
		request.ButtonOff();
		
		// remove the request from the active request array
		if ((requests) && (requestPosition >= 0)) {
			requests.splice(requestPosition, 1);
		}
	}
	
	// show summary in the cart widget
	if ((cart) && (response)) {
		if (response.CartQty) {
			cart.SetSummary(response.CartQty);
		}
	}
	
	// send data to omniture and handle errors
	if (typeof(s) != "undefined") {
		if (response) {
			if (s_account) {
				if (response.Products.length > 0) {
					void(s.t());
					s = s_gi(s_account);
					s.events="scAdd";
					
					var products = "";
					var prop23 = "";
					var evar28 = "";
					for (var i=0; i<response.Products.length; i++) {
						var product = response.Products[i];
						
						if (product.Action != "failed") {
							if (products.length > 0) { products += ","; }
							products += product.ToOmniture();
						}

						if (request) {
							if (request.Ct) {
								if (request.Ct == 12565) {
									if (prop23.length > 0) prop23 += ",";
									prop23 += product.Id
									
									if (evar28.length > 0) evar28 += ",";
									evar28 += product.Id
								}
							}
						}
					}
					if (products.length > 0) { s.products = products; }
					
					if (cartOmnitureData) {
						s.prop19 = cartOmnitureData.PageName;
						s.prop20 = cartOmnitureData.PageType;
						s.prop21 = cartOmnitureData.Refinements;
						s.prop22 = cartOmnitureData.RefinementsOrdered;
						s.eVar20 = cartOmnitureData.PageName;
						s.eVar21 = cartOmnitureData.PageType;
						s.eVar22 = cartOmnitureData.Refinements;
						s.eVar23 = cartOmnitureData.RefinementsOrdered;
						
						if ((prop23.length > 0) && (evar28.length > 0)) {
							s.prop23 = prop23;
							s.eVar28 = evar28;
						}
						
						if (request) {
							if (request.Iid) {
								if (request.Iid.length > 0) {
									s.eVar3 = request.Iid;
									var containsIid = s.eVar3.indexOf("iid=");
									if (containsIid < 0) containsIid = s.eVar3.indexOf("Iid=");
									if (containsIid >= 0) {
										s.eVar3 = s.eVar3.substr(4);
									}
								}
							}
						}
					}
		
					s.tl(this,'o','Add to Cart');
				}
			}
		}
	}
	
	// Rich Relevance processing
	richRelevanceResponse(response);
	
	// check for errors
	if (response) {
		if (response.HasError) {
			var invalid = "";
			var qty = 1;
			for (var i=0; i<response.Products.length; i++) {
				var product = response.Products[i];
				
				if (product.Action == "failed") {
					if (invalid.length > 0) { invalid += ","; }
					invalid += product.Sku;
					
					if (product.Qty > qty) { qty = product.Qty; }
				}
			}
			
			// only redirect if there is an invalid product
			if (invalid.length > 0) {
				location.href = cartDefaultUrl + "?mode=add&pproduct_id=" + invalid + "&qty=" + qty;
			}
		}
	}
}

// is an object defined
function richRelevanceDefined() {
	try {
		if (typeof(R3_COMMON) == 'undefined') {
			return false;
		} else {
			if (R3_COMMON) {
				return true;
			} else {
				return false;
			}
		}
	} catch (er) {
		return false;
	}
}

// send stock info to Rich Relevance when the add to cart event is complete
function richRelevanceResponse(response) {
	var debugScreen = document.getElementById("TraceConsole");
	try {
		if (response) {
			if (richRelevanceDefined()) {
				R3_COMMON.placementTypes = '';
				R3_CATEGORY = undefined;
				R3_ITEM = undefined;
				R3_PURCHASED = undefined;
				R3_SEARCH = undefined;
				R3_HOME = undefined;
				R3_ERROR = undefined;
				
				for (var i=0; i<response.Products.length; i++) {
					var product = response.Products[i];
					
					if (product) {
						if (product.Action != "failed") {
							if (debugScreen) {
								debugScreen.value += "pproduct id " + product.Sku + " has a stock count of " + product.Stock + "\n";
							}
							try {
								R3_ITEM = new r3_item();
								R3_ITEM.setId(product.Id);
								if (product.Stock <= 10) {
									R3_ITEM.setInStock(false);
								} else {
								    if (product.NeverShow) {
								        if (product.NeverShow == "True") {
								            R3_ITEM.setInStock(false);
								        } else {
								            R3_ITEM.setInStock(true);
								        }
								    } else {
									    R3_ITEM.setInStock(true);
                                    }
								}
								r3();
							} catch (er) {
								if (debugScreen) {
									debugScreen.value += "error posting product id " + product.Id + ". " + er.message + " - " + er.description + "\n";
								}
							}
						}
					}
				}
			}
		}
	} catch (er) {
		if (debugScreen) {
			debugScreen.value += er.message + " - " + er.description + "\n";
		}
	}
}

// menu.js

function popupadd(pprod,qty) {
	var url = '/checkout/addwindow.asp?pproduct_id=' + pprod + '&qty=' + qty;
	var win;
	var lefttop;
	
	//center of screen
	//lefttop='left='+(screen.width-310)/2+',top='+(screen.height-150)/2+',toolbars=0,scrollbars=0,location=0,statusbars=0,menubars=0,resizable=0,width=310,height=150';
	
	//default
	lefttop='toolbars=0,scrollbars=0,location=0,statusbars=0,menubars=0,resizable=0,width=350,height=215';
	
	win = window.open(url,'popup',lefttop);
	win.window.focus();
}

function popup(strURL) {
	var url = strURL;
	var win;
	var lefttop;
	
	//center of screen
	//lefttop='left='+(screen.width-310)/2+',top='+(screen.height-150)/2+',toolbars=0,scrollbars=0,location=0,statusbars=0,menubars=0,resizable=0,width=310,height=150';
	
	//default
	lefttop='toolbars=0,scrollbars=0,location=0,statusbars=0,menubars=0,resizable=0,width=325,height=240';
	
	win = window.open(url,'popup',lefttop);
	win.window.focus();
}

function popupdefined(strURL,strHeight,strWidth) {
	var url = strURL;
	var height = strHeight;
	var width = strWidth
	var win;
	var lefttop;
	
	//default
	lefttop='toolbars=0,scrollbars=0,location=0,statusbars=0,menubars=0,resizable=0,width='+width+',height='+height;
	
	win = window.open(url,'popup',lefttop);
	win.window.focus();
}

function clearInput(inputvalue){
	if (inputvalue.defaultValue==inputvalue.value)
		inputvalue.value = ""
}

function SweepPopUp(URL) {
	day = new Date();
	id = day.getTime();
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=450,height=450');");
}


function PopUpDefinedScrolling(strURL,strHeight,strWidth) {

		var url = strURL;
		var height = strHeight;
		var width = strWidth
		var win;
		var lefttop;

		//default
		lefttop='toolbars=0,scrollbars=yes,location=0,statusbars=0,menubars=0,resizable=yes,width='+width+',height='+height;

		win = window.open(url,'popup',lefttop);
		win.window.focus();
}


// common.v2.js

// called from the onload event of the body tag
function pageInit() {
	cartInitialize();
	
	
	var thePage
	var aboutPagePath
	
	
	if (location.href.replace(document.domain, '').replace('http://', '').toLowerCase() == '/v6/aboutwine/')
	{
	    PreLoadAboutImages();
	}
	
	
}

function PreLoadAboutImages()
{
    for(i = 1; i< 15; i++)
    {
        document.getElementById("imgInfoPop" + i).src = "http://" + document.domain + "/images/aboutwine/aboutPop/imgPop" + i + ".jpg"; 
        //alert("http://" + document.domain + "/images/aboutwine/aboutPop/imgPop" + i + ".jpg");
    }
        
}

// function initializes the cart widget (the now famous "Kurt Widget")
function cartInitialize() {
	cart = new cartWidget();
	cart.Initialize();
	
	requests = new Array();
	
	if (typeof(s) != "undefined") {
		cartOmnitureData = new cartOmniture(s);
	}
}

// function called from the onclick add a case event of the button
function addACase(pproductId, clickId, iid, passedButton) {
	var request = new cartRequest();
	request.Button = passedButton;
	request.Qty = 12;
	request.SrcOn = "http://cache.wine.com/images/btnAdding.png";
	request.SrcOff = "http://cache.wine.com/images/btnAddAgain.png";
	request.PproductId = pproductId;
	request.Ct = clickId;
	request.Iid = iid;
	cartMakeRequest(request);
}

function addToCart(pproductId, clickId, iid, checkQTY, passedButton) 
{
    addToCart(pproductId, clickId, iid, checkQTY, passedButton, "False");
}

// function called from the onclick event of the button
function addToCart(pproductId, clickId, iid, checkQTY, passedButton, useNewImages) {
	var request = new cartRequest();
	request.Button = passedButton;
	if(useNewImages == "True")
	{
	    request.SrcOn = "http://cache.wine.com/images/btnAdding.png";
	    request.SrcOff = "http://cache.wine.com/images/btnAddAgain.png";
	}else
	{
        request.SrcOn = "/images/btn_add_cart_adding.gif";
	    request.SrcOff = "/images/btn_add_cart_again.gif";
	}
	request.Ct = clickId;
	request.Iid = iid;
	request.Qty = 1;
	request.PproductId = pproductId;
	
	// if we are adding a wine club, grab the pproduct id from the form
	// and swap in the color if a wineclub and color is passed
	if (pproductId =="wineclub") {
		request.PproductId = addToCartWineClub(pproductId);
	}
	if (pproductId =="giftcert") {
		request.PproductId = addToCartGiftCert(pproductId);
	}
	
	// grab the qty from the drop down if on the detail page
	// ONLY if we have the flag passed in
	if (checkQTY == "True") {
		request.Qty = addToCartQtyCheck(1);
	}
	
	cartMakeRequest(request);
}

// returns the appropriate pproduct id for a wine club
function addToCartWineClub(pproductId) {
	var output = pproductId;
	
	if (pproductId =="wineclub") {
		output = document.forms['addToCart'].elements['pproduct_id'].options[document.forms['addToCart'].elements['pproduct_id'].selectedIndex].value;
		
		if (pproductId =="wineclub" )
		{
			if (document.forms['addToCart'].elements['color'] !=null)
			{
				if (document.forms['addToCart'].elements['color'].options !=null)
				{
					output = replaceIt(output, "COLOR", document.forms['addToCart'].elements['color'].options[document.forms['addToCart'].elements['color'].options.selectedIndex].value);
				}
				else
				{
					output = replaceIt(output, "COLOR", document.forms['addToCart'].elements['color'].value);
				}	
			}
		}
	}
	
	return output;
}

// returns the appropriate pproduct id for a gift cert
function addToCartGiftCert(pproductId) {
	var output = pproductId;
	
	if (pproductId = "giftcert" && document.forms['addToCart'] != null) {
		output = document.forms['addToCart'].elements['pproduct_id'].options[document.forms['addToCart'].elements['pproduct_id'].selectedIndex].value;
	}
	
	if (pproductId = "giftcert" && document.forms['aspnetForm'] != null) {
		output = document.forms['aspnetForm'].elements['ctl00$MainBodyContentPlaceholder$ddlPProductId'].options[document.forms['aspnetForm'].elements['ctl00$MainBodyContentPlaceholder$ddlPProductId'].selectedIndex].value;
	}
	
	return output;
}

// returns the appropriate quantity to use
function addToCartQtyCheck(qty) {
	var output = qty;
	
	// grab the qty from the drop down if on the detail page
	if (document.forms['addToCart']!= null && document.forms['addToCart'].elements['qty'] != null) {
		if (document.forms['addToCart'].elements['qty'].options != null) {
			output = document.forms['addToCart'].elements['qty'].options[document.forms['addToCart'].elements['qty'].selectedIndex].value;
		} else {
			output = document.forms['addToCart'].elements['qty'].value;
		}
	}
	
	// check for the aspnet form name
	if (document.forms['aspnetForm']!= null && document.forms['aspnetForm'].elements['qty'] != null) {
		if (document.forms['aspnetForm'].elements['qty'].options != null) {
			output = document.forms['aspnetForm'].elements['qty'].options[document.forms['aspnetForm'].elements['qty'].selectedIndex].value;
		} else {
			output = document.forms['aspnetForm'].elements['qty'].value;
		}
	}
	
	return output;
}

// method opens up the rss page as a popup
function subscribe(querystring) {
	var theNewWin = window.open('/v6/rss/rss.aspx?' + querystring,'feed','width=900,height=640,resizable=yes,toolbar=no,location=yes,scrollbars=yes');
	if (typeof theNewWin != "undefined" && theNewWin != null ) {
		theNewWin.focus();
	}
	reloadWithShare("rss");
}
	
// reloads the page with a shared querystring parameter at the end
// this should be migrated to use omniture ajax to save a page load
function reloadWithShare(share) {
	if ((s) && (s_account)) {
		void(s.t());
		s = s_gi(s_account);
		s.prop8 = share
		s.tl(this,'o','Shared Click');
	}
}

// executes a search query, if nothing is entered into the search box, then show prompt
function doSearch(terms) {
	if ((terms.length > 0) && (terms != "Enter keyword")) {
		terms = escape(terms);
		document.location.href = "/v6/search/?term="+terms;
	} else {
		var termPrompt = document.getElementById("searchHints");
		if (termPrompt) {
			termPrompt.style.display = "";
		}
	}
	return false;
}

// method for hiding the search hints prompt
function hideSearchHints() {
	var termPrompt = document.getElementById("searchHints");
	if (termPrompt) {
		termPrompt.style.display = "none";
	}
}

// used for displaying the addthis popup for sharing / bookmarking
function doAddThisClick(url, lnk) {
	addthis_url = url;
	addthis_title = document.title;
	reloadWithShare("addthis");
	return addthis_click(lnk);
}

// clean way to change the state without a form post back
function gxStateChange(dropdown) {
	if (dropdown) {
		var path = location.href;
		var option = dropdown.options[dropdown.selectedIndex].value;
		location.href = '/home/newtransfercart.asp?new=' + option + '&next=' + escape(path);
		return true;
	}
}

function keyMouseOut() {
	keyTimeout(5000);
}

function keyMouseOutItem(key, time) {
	keyTimer = setTimeout("keyHide(" + key + ")", time);
}

function keyMouseOver() {
	clearTimeout(keyTimer);
}

function keyTimeout(time) {
	keyTimer = setTimeout("keyHideAll()", time);
}

function keyHideAll() {
	var key = document.getElementById("key");
	if (key) {
		keyHide(key);
	}
	
	key = document.getElementById("descriptionFullKey");
	if (key) {
		keyHide(key);
	}
	
	key = document.getElementById("recommendationsFullKey");
	if (key) {
		keyHide(key);
	}
	
	key = document.getElementById("shareWrapper");
	if (key) {
		keyHide(key);
	}
	
//	key = document.getElementById("appellationKey");
//	if (key) {
//		keyHide(key);
//	}
	
//	key = document.getElementById("varietalKey");
//	if (key) {
//		keyHide(key);
//	}
}

function keyHide(key) {
	keyHideNode(key);

	var child = key.firstChild;
	while (child != null) {
		keyHide(child);
		child = child.nextSibling;
	}
}

function keyHideNode(key) {
	if (key) {
		if (key.nodeType == 1) {
			if (key.id) {
				switch(key.id) {
					case "iconKey":
					case "ratingKey":
					case "recommendationsKey":
					case "descriptionFull":
					case "shareKey":
					case "feedKey":
					case "typeKey":
					case "winelistKey":
					case "suggestionsKey":
					case "appellationKey":
					case "varietalKey":
						key.className = "isHidden";
						break;
						
					case "wineReviewForm":
						key.style.display = "none";
						break;
				}
			}
		}
	}
}

function keyShow(icon, legend) {
	keyMouseOver();
	keyHideAll();
	keyTimeout(4000);
	
	// Get key and icon
	var key = document.getElementById(legend);
	var keyWidth = 400;
	if (key.style.width.length > 0) {
		keyWidth = key.style.width.replace("px", "");
	}
	
	// Get Browser Height and Width
	var browserDimensions = findBrowserDimensions();
	var browserOffset = findBrowserOffset();
	
	// Set up positioning
	var position = findPosition(icon);
	var intOffsetTop = 15 + position[1];
	var intOffsetBottom = position[1] - icon.offsetHeight - 10;
	var intOffsetLeft = 15 + position[0];
	var intOffsetRight = position[0] + icon.offsetWidth - keyWidth - 15;

	if (position[1] + key.offsetHeight >= (browserDimensions[1] * 1.15) + browserOffset[1]) {
		key.style.top = intOffsetBottom + "px";
	} else {
		key.style.top = intOffsetTop + "px";
	}
	if (position[0] >= (browserDimensions[0] / 2)) {
		key.style.left = intOffsetRight + "px";
	} else {
		key.style.left = intOffsetLeft + "px";
	}
	
	// Make visibile
	key.className = "isVisible";

	if (key.style.display == 'none') {
		key.style.display = '';
	}
	
	if (key.style.visibility == 'hidden') {
		key.style.visibility = "";
	}
}

function keyShowUnderAlignRight(icon, legend) {
	keyMouseOver();
	keyHideAll();
	keyTimeout(4000);
	
	// Get key and icon
	var key = document.getElementById(legend);
	
	// Get Browser Height and Width
	var browserDimensions = findBrowserDimensions();
	var browserOffset = findBrowserOffset();
	
	// Set up positioning
	var position = findPosition(icon);
	var intOffsetTop = 15 + position[1];
	var intOffsetLeft = position[0] - key.offsetWidth + icon.offsetWidth;
	
	key.style.top = intOffsetTop + "px";
	key.style.left = intOffsetLeft + "px";
	
	// Make visibile
	key.className = "isVisible";
	
	if (key.style.display == 'none') {
		key.style.display = '';
	}
	
	if (key.style.visibility == 'hidden') {
		key.style.visibility = "";
	}
}

// loads up possible suggestions based off a provided product/pproduct/state combination
function loadSuggestions(clickObject, productId, pproductId, state) {
	var loadImg = document.getElementById("suggestionsLoadingImg");
	if (loadImg) { loadImg.style.display = ""; }
	
	var loadText = document.getElementById("suggestionsLoadingText");
	if (loadText) { loadText.style.display = ""; }
	
	var content = document.getElementById("suggestionsContent");
	if (content) { content.style.display = "none"; }
	
	keyShowUnderAlignRight(clickObject, "suggestionsKey");
	clearTimeout(keyTimer);
	callSuggestions(productId, pproductId, state);
}

// makes the suggestions call
function callSuggestions(productId, pproductId, state) {
	var url = "/v6/services/productservice.aspx?method=recommendationsandaffinity&data=" + state + "|" + productId + "|" + pproductId;
	
    // branch for native XMLHttpRequest object
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
        req.onreadystatechange = showSuggestions;
        req.open("GET", url, true);
        req.send(null);
    // branch for IE/Windows ActiveX version
    } else if (window.ActiveXObject) {
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = showSuggestions;
            req.open("GET", url, true);
            req.send();
        }
    }
}

// shows the possible suggestions
function showSuggestions() {
	if (req.readyState == 4) {
		var content = document.getElementById("suggestionsContent");
		if (content) { content.innerHTML = req.responseText; }
		
		setTimeout("showSuggestionsFinished()", 1000);
	}
}

// hides the loading images	
function showSuggestionsFinished() {
	var loadImg = document.getElementById("suggestionsLoadingImg");
	if (loadImg) { loadImg.style.display = "none"; }
	
	var loadText = document.getElementById("suggestionsLoadingText");
	if (loadText) { loadText.style.display = "none"; }
	
	var content = document.getElementById("suggestionsContent");
	if (content) { content.style.display = ""; }
}

function getAppellation(target, appellationId) {
	var appellation = document.getElementById("appellationKeyContent");
	if (appellation) {
		appellation.innerHTML = "loading...";
	}
	
	keyShow(target, "appellationKey");
	clearTimeout(keyTimer);
	
	var url = "/v6/services/productservice.aspx?method=getappellation&data=" + appellationId;
	
    // branch for native XMLHttpRequest object
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
        req.onreadystatechange = showAppellation;
        req.open("GET", url, true);
        req.send(null);
    // branch for IE/Windows ActiveX version
    } else if (window.ActiveXObject) {
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = showAppellation;
            req.open("GET", url, true);
            req.send();
        }
    }
}

function showAppellation() {
	if (req.readyState == 4) {
		var appellation = document.getElementById("appellationKeyContent");
		if (appellation) {
			appellation.innerHTML = req.responseXML.firstChild.firstChild.nodeValue;
		}
	}
}

function getVarietal(target, varietalId) {
	var varietal = document.getElementById("varietalKeyContent");
	if (varietal) {
		varietal.innerHTML = "loading...";
	}
	
	keyShow(target, "varietalKey");
	clearTimeout(keyTimer);
	
	var url = "/v6/services/productservice.aspx?method=getvarietal&data=" + varietalId;
	
    // branch for native XMLHttpRequest object
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
        req.onreadystatechange = showVarietal;
        req.open("GET", url, true);
        req.send(null);
    // branch for IE/Windows ActiveX version
    } else if (window.ActiveXObject) {
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = showVarietal;
            req.open("GET", url, true);
            req.send();
        }
    }
}

function showVarietal() {
	if (req.readyState == 4) {
		var varietal = document.getElementById("varietalKeyContent");
		if (varietal) {
			varietal.innerHTML = req.responseXML.firstChild.firstChild.nodeValue;
		}
	}
}

// v6global.js

function HideDiv(divId)
{
    var item = document.getElementById(divId);
    if (item) {
        item.style.visibility = "hidden";
		item.style.display = "block";
		item.innerHTML = "";
	}
}

function HideElement(divId) {
	var item = document.getElementById(divId);
	if (item) {
		item.className = "isHidden";
		item.style.display = 'none';
	}
}


function CleanMyWineNote(field)
{
	if (field.value.indexOf("<") > -1)
	{
		field.value = field.value.replace("<","");
	}
	
	if (field.value.indexOf(">") > -1)
	{
		field.value = field.value.replace(">","");
	}
}


// communitylist.js
	function showReviewForm(icon, productId, name) {
		var reviewTitleDiv = document.getElementById("wineReviewTitle");
		if (reviewTitleDiv) {
			reviewTitleDiv.innerHTML = name + " Review";
		}

		var form = document.getElementById("wineReviewForm");
		if (form) {
			var inputs = form.getElementsByTagName('input');
			if (inputs) {
				for (var i = 0; i < inputs.length; ++i) {
					if (inputs[i].type == 'radio') {
						if (inputs[i].name == 'wineReviewBuyAgain') {
							if (inputs[i].value == 'true') {
								inputs[i].checked = true;
							} else {
								inputs[i].checked = false;
							}
						}
						
						if (inputs[i].name == 'wineReviewLocation') {
							if (inputs[i].value == 'true') {
								inputs[i].checked = true;
							} else {
								inputs[i].checked = false;
							}
						}
					}
					
					if (inputs[i].type == 'hidden') {
						if (inputs[i].name == 'wineReviewProductId') {
							inputs[i].value = productId;
						} else if (inputs[i].name == 'wineReviewSessionGuid') {
							// don't do anything
						} else {
							inputs[i].value = '';
							if (inputs[i].nextSibling.nodeName == 'A') {
								inputs[i].nextSibling.href = 'javascript: void(0);';
								var stars = inputs[i].nextSibling.childNodes;
								for (j = 0; j < stars.length; j++) {
									stars[j].className = 'ratingStar emptyRatingStar';
								}
							}
						}
					}
				}
			}
		}
		
		var comments = document.getElementById("wineReviewComments");
		if (comments) {
			comments.value = '';
		}
		
		var progressImg = document.getElementById("wineReviewSavingImg");
		if (progressImg) {
			progressImg.style.display = "none";
		}
		
		keyShow(icon, "wineReviewForm");
		clearTimeout(keyTimer);
	}
	
	function submitReviewForm() {
		var url = "/v6/services/productservice.aspx?method=review";
		
		var form = document.getElementById("wineReviewForm");
		var score = null;
		if (form) {
			var inputs = form.getElementsByTagName('input');
			if (inputs) {
				for (var i = 0; i < inputs.length; ++i) {
					if (inputs[i].type == 'radio') {
						if (inputs[i].name == 'wineReviewBuyAgain') {
							if (inputs[i].checked) {
								url += '&buyagain=' + inputs[i].value;
							}
						}
						
						if (inputs[i].name == 'wineReviewLocation') {
							if (inputs[i].checked) {
								url += '&showlocation=' + inputs[i].value;
							}
						}
					}
					
					if (inputs[i].type == 'hidden') {
						if (inputs[i].name == 'wineReviewProductId') {
							url += '&productId=' + inputs[i].value;
						} else if (inputs[i].name == 'wineReviewSessionGuid') {
							url += '&sessionGuid=' + inputs[i].value;
						} else {
							score = inputs[i];
						}
					}
				}
			}
		}
		
		if (score) {
			if (score.value.length > 0) {
				url += '&score=' + score.value;
			} else {
				alert('Please choose a rating!');
				return false;
			}
		}
		
		var title = document.getElementById("wineReviewTitle");
		url = url + "title=" + title.innerHTML + "&";
		
		var comments = document.getElementById("wineReviewComments");
		if (comments) {
			if (comments.value.length > 0) {
				url = url + "comments=" + comments.value;
			} else {
				alert('Please provide some comments!');
				return false;
			}
		}
		
		var progressImg = document.getElementById("wineReviewSavingImg");
		if (progressImg) {
			progressImg.style.display = "";
		}
		alert(url);
	}
	
	function addToCommunityWineList(productId, icon) {
		keyShowUnderAlignRight(icon, "winelistKey");

		clearTimeout(keyTimer);
		
		showCommunityWineListButtons();
		
		var radios = document.getElementById("winelistForm");
		if (radios) {
			var inputs = radios.getElementsByTagName('input');
			if (inputs) {
				for (var i = 0; i < inputs.length; ++i) {
					if (inputs[i].name == "wineListProductId") {
						inputs[i].value = productId;
					}
					
					if (inputs[i].type == 'radio' && inputs[i].name == 'wineListName')
						if (inputs[i].value == "NEW") {
							inputs[i].checked = true;
						} else {
							inputs[i].checked = false;
						}
				
				}
			}
		}
		
	}
	
	function addToCommunityWineListClick(form) {
		var radios = document.getElementById(form);
		if (radios) {
			var inputs = radios.getElementsByTagName('input');
			if (inputs) {
				for (var i = 0; i < inputs.length; ++i) {
					if (inputs[i].type == 'radio' && inputs[i].name == 'wineListName') {
						if (inputs[i].checked) {
							if (inputs[i].value == "NEW") {
								addToCommunityWineListNew();
							} else if (inputs[i].value == "WISHLIST") {
								addToCommunityWineListWishlist();
							} else {
								addToCommunityWineListExisting(inputs[i].value);
							}
						}
					}
				}
			}
			
			//Fixing IE 7 bug with Key Hiding and input boxes
			showCommunityWineListButtons();
		}
	}
	
	function showCommunityWineListButtons()
	{
	        
	        var saveButton = document.getElementById('wineListSaveButton');
	        var cancelButton = document.getElementById('wineListCancelButton') ;
	        var notesButton = document.getElementById('wineListNotes');
	        
	        if(saveButton)
	        {
	            document.getElementById('wineListSaveButton').style.display="inline";    
	        }
	        
	         if(cancelButton)
	        {
	            document.getElementById('wineListCancelButton').style.display="inline";
	        }
	        
	        if(notesButton)
	        {
	            document.getElementById('wineListNotes').style.display="inline";
	        }
	       
		    
	}
	
	function hideCommunityWineListButtons()
	{
	       var saveButton = document.getElementById('wineListSaveButton');
	        var cancelButton = document.getElementById('wineListCancelButton') ;
	        var notesButton = document.getElementById('wineListNotes');
	        if(saveButton)
	        {
	            document.getElementById('wineListSaveButton').style.display="none";    
	        }
	        
	         if(cancelButton)
	        {
	            document.getElementById('wineListCancelButton').style.display="none";
	        }
	        
	        if(notesButton)
	        {
	            document.getElementById('wineListNotes').style.display="none";
	        }
	       
	        
	}
	
	function addToCommunityWineListNew() {
		var url = "/v6/community/userlistform.aspx?";
		
		var productId = document.getElementById("wineListProductId");
		url = url + "productid=" + productId.value + "&";
		
		var notes = document.getElementById("wineListNotes");
		if (notes.value.length > 0) {
			url = url + "notes=" + notes.value;
		}
		
		location.href = url;
	}
	
	function addToCommunityWineListWishlist() {
		var url = "/v6/community/userlistservice.aspx?wishlist=true&";
		
		var sessionGuid = document.getElementById("wineListSessionGuid");
		url = url + "sessionGuid=" + sessionGuid.value + "&";
		
		var productId = document.getElementById("wineListProductId");
		url = url + "productid=" + productId.value + "&";
		
		var notes = document.getElementById("wineListNotes");
		if (notes.value.length > 0) {
			url = url + "notes=" + notes.value;
		}
		
		addToCommunityWineListPost(url);
	}
	
	function addToCommunityWineListExisting(id) {
		var url = "/v6/community/userlistservice.aspx?listid=" + id + "&";
		
		var productId = document.getElementById("wineListProductId");
		url = url + "productid=" + productId.value + "&";
		
		var notes = document.getElementById("wineListNotes");
		if (notes.value.length > 0) {
			url = url + "notes=" + notes.value;
		}
		
		addToCommunityWineListPost(url);
	}
	
	function addToCommunityWineListPost(url) {
		// Hide / Disable
		var cancel = document.getElementById("wineListCancelButton");
		cancel.style.display = "none";
		
		var save = document.getElementById("wineListSaveButton");
		save.style.display = "none";
		
		var img = document.getElementById("wineListSavingImg");
		img.style.display = "";
		
	    // branch for native XMLHttpRequest object
	    if (window.XMLHttpRequest) {
	        req = new XMLHttpRequest();
	        req.onreadystatechange = addToCommunityWineListComplete;
	        req.open("GET", url, true);
	        req.send(null);
	    // branch for IE/Windows ActiveX version
	    } else if (window.ActiveXObject) {
	        req = new ActiveXObject("Microsoft.XMLHTTP");
	        if (req) {
	            req.onreadystatechange = addToCommunityWineListComplete;
	            req.open("GET", url, true);
	            req.send();
	        }
	    }
	}
	
	function addToCommunityWineListComplete() {
		if (req.readyState == 4) {
			var rawData = req.responseXML;
			var returnData = rawData.firstChild;
			var level = returnData.attributes.getNamedItem("level");
			
			if (level.value != "Success") {
				var error = ""
				var messages = returnData.firstChild;
				for (var i=0; i<messages.childNodes.length; i++) {
					alert(messages.childNodes[i].nodeValue);
					error = error + "/n" + messages.childNodes[i].value;
				}
				
				alert("Error Encountered");
			}
			
			setTimeout("addToCommunityWineListFinished()", 1000);
		}
	}
	
	function addToCommunityWineListFinished() {
		var cancel = document.getElementById("wineListCancelButton");
		cancel.style.display = "";
	
		var save = document.getElementById("wineListSaveButton");
		save.style.display = "";
	
		var img = document.getElementById("wineListSavingImg");
		img.style.display = "none";
		
		hideCommunityWineListButtons();
		keyHideAll();
	}


//-->