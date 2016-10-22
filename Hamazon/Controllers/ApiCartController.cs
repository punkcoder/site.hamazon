using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using Umbraco.Web.WebApi;

namespace Hamazon.Controllers
{
    public class CartController : UmbracoApiController
    {

        [HttpPost]
        public void AddToCart(String Sku)
        {
            TransactionLibrary.AddToBasket(1, Sku);
        }

        [HttpGet]
        public int GetCartCount()
        {
            int count = 0;

            try
            {
                var basket = TransactionLibrary.GetBasket();

                foreach (var purchaseOrderOrderLine in basket.PurchaseOrder.OrderLines)
                {
                    count += purchaseOrderOrderLine.Quantity;
                }
            }
            catch (Exception ex)
            {
                
            }
            return count;
        }

        [HttpPost]
        public bool RemoveCartItem(int CartLineItem)
        {
            TransactionLibrary.UpdateLineItem(CartLineItem, 0);
            return true;
        }

        [HttpPost]
        public bool ClearCart()
        {
            TransactionLibrary.ClearBasket();
            return true;
        }
    }
}