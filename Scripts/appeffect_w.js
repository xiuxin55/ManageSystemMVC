/* 
* @Author: anchen
* @Date:   2016-11-23 11:12:47
* @Last Modified by:   anchen
* @Last Modified time: 2017-02-14 13:19:30
*/
(function (doc, win) {
        var docEl = doc.documentElement,
        resizeEvt = 'orientationchange' in window ? 'orientationchange' : 'resize';
        
        recalc = function (){
            var clientWidth = docEl.clientWidth;
            if (!clientWidth) 
                {
                    return;
                }
              docEl.style.fontSize=(clientWidth*30)/750+'px';
        };
        if (!doc.addEventListener)
             {
                return;
             } 
        win.addEventListener(resizeEvt, recalc, false);
        doc.addEventListener('DOMContentLoaded', recalc, false);
})(document, window);



