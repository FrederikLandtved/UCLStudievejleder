// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// OVERLAY
function openNav() {
    //document.getElementById("myNav").style.display = "block";
    document.getElementById("myNav").style.height = "60%";
    document.getElementById("bg-blur").style.filter = "blur(8px)";
    
}

function closeNav() {
    document.getElementById("myNav").style.height = "0%";
    document.getElementById("bg-blur").style.filter = "blur(0px)";
}