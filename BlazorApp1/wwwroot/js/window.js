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


