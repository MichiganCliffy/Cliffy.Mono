// preload buttons
	var imgPath = "/cliffordcorner/travellogs/africa/images/";
	
	tn1_on = new Image();
	tn1_on.src = imgPath + "tn_intro_on.gif";
	tn2_on = new Image();
	tn2_on.src = imgPath + "tn_home_on.gif";
	tn3_on = new Image();
	tn3_on.src = imgPath + "tn_tarangire_on.gif";
	tn4_on = new Image();
	tn4_on.src = imgPath + "tn_ngorongoro_on.gif";
	tn5_on = new Image();
	tn5_on.src = imgPath + "tn_serengeti_on.gif";
	tn6_on = new Image();
	tn6_on.src = imgPath + "tn_mara_on.gif";
	
	tn1_off = new Image();
	tn1_off.src = imgPath + "tn_intro_off.gif";
	tn2_off = new Image();
	tn2_off.src = imgPath + "tn_home_off.gif";
	tn3_off = new Image();
	tn3_off.src = imgPath + "tn_tarangire_off.gif";
	tn4_off = new Image();
	tn4_off.src = imgPath + "tn_ngorongoro_off.gif";
	tn5_off = new Image();
	tn5_off.src = imgPath + "tn_serengeti_off.gif";
	tn6_off = new Image();
	tn6_off.src = imgPath + "tn_mara_off.gif";

	tn1_down = new Image();
	tn1_down.src = imgPath + "tn_intro_down.gif";
	tn2_down = new Image();
	tn2_down.src = imgPath + "tn_home_down.gif";
	tn3_down = new Image();
	tn3_down.src = imgPath + "tn_tarangire_down.gif";
	tn4_down = new Image();
	tn4_down.src = imgPath + "tn_ngorongoro_down.gif";
	tn5_down = new Image();
	tn5_down.src = imgPath + "tn_serengeti_down.gif";
	tn6_down = new Image();
	tn6_down.src = imgPath + "tn_mara_down.gif";
// end preload

//detect browser
var IE = false;
var NS = false;
var intField;
document.all ? IE = true : NS = true;

if (NS)
	intField = 13;
else
	intField = 15;

function imgOn(imgName) {
	document[imgName].src = eval(imgName + "_on.src");
}

function imgOff(imgName) {
	document[imgName].src = eval(imgName + "_off.src");
}

function imgDown(imgName) {
	document[imgName].src = eval(imgName + "_down.src");
}
