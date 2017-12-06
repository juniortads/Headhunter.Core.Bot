(function () {
    var div = document.createElement("div");
    document.getElementsByTagName('body')[0].appendChild(div);
    div.outerHTML = "<div id='botDiv' style='height: 38px; position: fixed; bottom: 0; z-index: 1000; background-color: #fff; margin-left: 910px'><div id='botTitleBar' style='height: 38px; width: 500px; position:fixed; cursor: pointer;'></div><iframe width='400px' height='600px' src='https://webchat.botframework.com/embed/aec-cbot-ti?s=z2loxCHRTqY.cwA.8_I.IEwtmyiKk56b8olrLB0RVAkXT8pyZOiSQ0svLMbNyHw'></iframe></div>"; 

    document.querySelector('body').addEventListener('click', function (e) {
        e.target.matches = e.target.matches || e.target.msMatchesSelector;
        if (e.target.matches('#botTitleBar')) { 
            var botDiv = document.querySelector('#botDiv'); 
            botDiv.style.height = botDiv.style.height == '600px' ? '38px' : '600px';
        };
    });
}());

