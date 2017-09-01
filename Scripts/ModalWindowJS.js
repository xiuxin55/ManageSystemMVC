function showwin(url, iWidth, iHeight,isEn) {

    var iTop = (window.screen.availHeight - 30 - iHeight) / 2;       //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;           //获得窗口的水平位置;

    var agent = navigator.userAgent.toLowerCase();
    if (agent.indexOf("chrome") > 0) {
        window.open(url, "newwindow", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + " ,left=" + iLeft + "");
    }
    else {
        if (isEn)
            window.showModalDialog(url, window, "dialogWidth=" + iWidth + "px;dialogHeight=" + iHeight + "px;dialogtop=" + iTop + "px ;dialogleft=" + iLeft + "px");
        else
            window.showModalDialog(url, window, 'dialogWidth:620px;dialogHeight:500px;edge:raised;resizable:yes;scroll:yes;status:yes;center:yes;help:no;minimize:yes;maximize:yes;');
    }
}

function keyPress() {
    var keyCode = event.keyCode;
    if ((keyCode >= 48 && keyCode <= 57)) {
        event.returnValue = true;
    } else {
        event.returnValue = false;
    }
}