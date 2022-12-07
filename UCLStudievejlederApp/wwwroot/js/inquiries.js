const allButtons = document.querySelectorAll(".form-title");


allButtons.forEach(function (item) {
    item.addEventListener("click", function () {
        closeAll();
        item.nextElementSibling.classList.toggle("show");
    });
});


function closeAll() {
    const allDrops = document.querySelectorAll(".form-drop-style");
    allDrops.forEach(function (drop) {
        drop.classList.remove("show");
    });
}

function goToNext(number) {
    closeAll();
    const allDropDowns = document.getElementsByClassName("form-drop-style");
    allDropDowns[number].classList.toggle("show");
}


