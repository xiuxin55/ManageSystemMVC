var isBusy = false;
function createLoadingDiv() {
    var div = document.createElement("div");
    div.setAttribute("style", "position:absolute; left:0; top:40%; width:100%; line-height:10%; height:10%;");
    div.innerHTML = '<div id="divLoading" style="display:block; font-size:0.3rem; width:100%; text-align:center;">'
                  + '    <img alt="" src="/images/loading.gif"" border="0" style="margin-left:auto; margin-right:auto;display:block;width:5.2rem;height:4.2rem" />'
                  + '</div>';
    document.body.appendChild(div);
}

function showLoading(isVisible) {
    if (isVisible) {
        isBusy = true;
    }

    if (!document.getElementById("divLoading")) {
        createLoadingDiv();
    }
    if (isVisible) {
        document.getElementById("divLoading").style.display = "block";
    } else {
        document.getElementById("divLoading").style.display = "none";
    }

    if (!isVisible) {
        isBusy = false;
    }
}
 