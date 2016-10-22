/// <reference path="jquery.intellisense.js" />
/// <reference path="jquery.js" />

jQuery(document).ready(function () {
    // Load the cart
    loadTheCart();
});

function addSku(sku) {
    jQuery.post('/Umbraco/Api/Cart/AddToCart?sku=' + sku, null, function success() {
        loadTheCart();
    });
}
function loadTheCart() {
    jQuery.get('/Umbraco/Api/Cart/GetCartCount', null, function success(data) {
        jQuery("#cartcount").text('(' + data + ')');
    });
}
function removeCartItem(cartLineItem) {
    jQuery.post('/Umbraco/Api/Cart/RemoveCartItem?CartLineItem=' + cartLineItem, null, function success(data) {

    });
}
function ClearCart() {
    jQuery.post('/Umbraco/Api/Cart/ClearCart', null, function success(data) {
    });
}