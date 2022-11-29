function search() {
    var inputText = document.getElementById('field-of-study-search').value;
    var noResults = document.getElementById('no-results');

    var results = document.getElementsByClassName('field-name');
    var arr = Array.from(results);

    arr.forEach(function (item) {
        if (!item.innerHTML.toLowerCase().includes(inputText.toLowerCase())) {
            item.closest("article").style.display = 'none';
        } else {
            item.closest("article").style.display = 'unset';
        }
    });
}