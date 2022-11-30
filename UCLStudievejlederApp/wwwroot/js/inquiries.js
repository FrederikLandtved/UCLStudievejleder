const allButtons = document.querySelectorAll(".form-title");

allButtons.forEach(function (item) {
    item.addEventListener("click", function () {
        closeAll();
        item.nextElementSibling.classList.toggle("show");
    })
});

function closeAll() {
    const allDrops = document.querySelectorAll(".form-drop-style");
    allDrops.forEach(function (drop) {
        drop.classList.remove("show");
    })
}

//function dropDown() {
//    document.getElementById("form-drop").classList.toggle("show");
//}

//function dropDown2() {
//    document.getElementById("form-drop2").classList.toggle("show");
//}

//function dropDown3() {
//    document.getElementById("form-drop3").classList.toggle("show");
//}

//window.onclick = function (event) {
//    if (!event.target.matches('.form-title')) {
//        var dropdowns = document.getElementsByClassName("form-drop-style");
//        var i;
//        for (i = 0; i < dropdowns.length; i++) {
//            var openDropdown = dropdowns[i];
//            if (openDropdown.classList.contains('show')) {
//                openDropdown.classList.remove('show');
//            }
//        }
//    }
//}