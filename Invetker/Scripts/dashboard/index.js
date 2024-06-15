$("#layout-sider-btn").click(function () {
    document.cookie = `slider-collapsed=${$("body")[0].classList.contains("slider-collapsed") ? "0" : "1"}; path=/`;
    $("body")[0].classList.toggle("slider-collapsed");
});