<!--
function moreClicked(link, more) {
    if (link) {
        var item = document.getElementById(more);
        if (link.className == "selected") {
            moreOff(link, item);
        } else {
            moreOn(link, item);
        }
    }
}

function moreOn(link, more) {
    link.className = "selected";
    more.style.display = "";
}

function moreOff(link, more) {
    link.className = "";
    more.style.display = "none";
}

function pageInit() {
    var archives = document.getElementById("archives");
    if (archives) {
        archives.style.display = "none";
        
        var archivesArray = new Array();
        while (archives.childNodes.length > 0) {
            var archive = archives.firstChild;
            archivesArray.push(archive);
            archives.removeChild(archive);
        }
        
        while (archivesArray.length > 0) {
            var archive = archivesArray.pop();
            archives.appendChild(archive);
        }
        
        archives.style.display = "";
    }
}
//-->
