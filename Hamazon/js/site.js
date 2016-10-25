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
function clearCart() {
    jQuery.post('/Umbraco/Api/Cart/clearCart', null, function success(data) {
        getBasket();
    });
}
function login() {
    var emailaddress = jQuery("#txtEmailAddress").val();
    var password = jQuery("#txtPassword").val();

    var request = {
        "email": emailaddress,
        "password": password
    }

    jQuery.post('/Umbraco/Api/User/LoginUser', request, function success(data) {
        if (data.substring(0,5) != "false") {
            document.cookie = "jwt=" + data + ";" + document.cookie;
            location.reload();
        }
    });
}
function logout() {
    jQuery.post('/Umbraco/Api/User/LogoutUser', null, function success(data) {
        document.cookie = 'jwt=; expires=Thu, 01-Jan-70 00:00:01 GMT;';
        location.reload();
    });
}
function addComment() {
    var request = {
        'ProductId': jQuery('#intProductId').val(),
        'Subject': jQuery('#txtSubject').val(),
        'Comments': jQuery('#txtComments').val(),
        'EmailAddress': jQuery('#txtEmailAddress').val()
    };

    jQuery.post('/Umbraco/Api/Comment/AddComment', request, function success(data) {
        jQuery('#intProductId').val('');
        jQuery('#txtSubject').val('');
        jQuery('#txtComments').val('');
        jQuery('#txtEmailAddress').val('');

        loadComments();
    });  
}

function loadComments() {
    var productId = jQuery('#intProductId').val();
    jQuery.get('/Umbraco/Api/Comment/GetComments?ProductId='+productId , null, function success(data) {
        var source = jQuery("#comments").html();
        var template = Handlebars.compile(source);
        var context = data;
        var html = template(context);
        jQuery("#commentItems").html(html);
    });
}