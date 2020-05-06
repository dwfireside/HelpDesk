window.focusElement = (id) => {
    var element = document.getElementById(id);

    if (element)
        element.focus();
};

window.scrollIntoView = (id) => {
    var element = document.getElementById(id);

    if (element)
        element.scrollIntoView();
};

window.playSound = (src) => {
    var audio = new Audio(src);
    audio.play();
}

