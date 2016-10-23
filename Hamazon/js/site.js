/// <reference path="jquery.intellisense.js" />
/// <reference path="jquery.js" />
/// <reference path="handlebars.js" />

jQuery(document).ready(function () {
    // Load the cart
    getBasketCount();
});

function addSku(sku) {
    jQuery.post('/Umbraco/Api/Cart/AddToCart?sku=' + sku, null, function success() {
        getBasketCount();
    });
}
function getBasketCount() {
    jQuery.get('/Umbraco/Api/Cart/GetBasketCount', null, function success(data) {
        jQuery("#cartcount").text('(' + data + ')');
    });
}
function getBasket() {
    jQuery.get('/Umbraco/Api/Cart/GetBasket', null, function success(data) {
        var source = jQuery("#cart-line").html();
        var template = Handlebars.compile(source);
        var context = data;
        var html = template(context);
        jQuery("#lineitems").html(html);
        jQuery("#subtotal").html("$" + data.subtotal);
    });
}
function removeCartItem(cartLineItem) {
    jQuery.post('/Umbraco/Api/Cart/RemoveCartItem?CartLineItem=' + cartLineItem, null, function success(data) {
        getBasket();
    });
}
function ClearCart() {
    jQuery.post('/Umbraco/Api/Cart/ClearCart', null, function success(data) {
        getBasket();
    });
}
function Login() {
    var emailaddress = jQuery("#txtEmailAddress").val();
    var password = jQuery("#txtPassword").val();

    var request = {
        "email": emailaddress,
        "password": password
    }

    jQuery.post('/Umbraco/Api/User/LoginUser', request, function success(data) {
        if (data.substring(0,5) != "false") {
            document.cookie = "jwt=" + data + ";" + document.cookie;
            console.log("set cookie");
        }
    });
}