function dropDown() {
    document.getElementById("form-drop").classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.form-title')) {
        var dropdowns = document.getElementsByClassName("form-drop-style");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}