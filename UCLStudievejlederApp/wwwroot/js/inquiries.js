const allButtons = document.querySelectorAll(".form-title");
const allDropdowns = document.querySelectorAll(".select-dropdown");
const inquiryResult = document.querySelectorAll(".inquiry-result-title");
const radioBtns = document.querySelectorAll(".ucl-radiobtn");

window.onload = init();

function init() {
    allDropdowns[0].onchange = () => {
        setChosenMonth();
    }

    allButtons.forEach(function (item) {
        item.onclick = () => {
            closeAll();
            item.nextElementSibling.classList.toggle("show");
        };
    });

    allButtons[1].onclick();
    setChosenMonth();

    radioBtns.forEach(function (item) {
        item.onclick = (event) => {
            setResultTitle(item, event);
        };

        if (item.checked === true) {
            item.click();
            goToNext(1);
        }
    });

}

function setResultTitle(item, event) {
    var resultSpan = item.closest('.question-container').children[0].children[1];
    resultSpan.innerHTML = event.target.labels[0].innerText
}

function setChosenMonth() {
    allButtons[0].getElementsByClassName("inquiry-result-title")[0].innerHTML = allDropdowns[0].value;
    goToNext(1);
}


function closeAll() {
    const allDrops = document.querySelectorAll(".form-drop-style");
    allDrops.forEach(function (drop) {
        drop.classList.remove("show");
    });
}

function goToNext(number) {
    closeAll();
    const allDropDowns = document.getElementsByClassName("form-drop-style");
    var dropdownArray = Array.from(allDropDowns[number].children[0].children);
    dropdownArray.forEach(function (item) {
        if (item.className === "radio-button-container") {
            Array.from(item.children).forEach(function (radioButton) {
                if (radioButton.children[0].checked === true) {
                    closeAll();
                    goToNext(number + 1);
                    allDropDowns[number].classList.toggle("show");
                }
            })
        }
    })

    allDropDowns[number].classList.toggle("show");
}

// Date

let monthControl = document.querySelector('input[type="month"]');

let objectDate = new Date();
let month = objectDate.getMonth() + 1;

let year = objectDate.getFullYear();

monthControl.value = year + '-' +  month;