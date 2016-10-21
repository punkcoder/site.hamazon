
jQuery(document).load()

function addSku(sku) {
    jQuery.post('/Umbraco/Api/Cart/AddToCart?sku=' + sku, null, function success() {
        console.log("Added to Cart");
    });
}