using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using Umbraco.Web.WebApi;


namespace Hamazon.Controllers
{
    public class CartController : UmbracoApiController
    {

        [HttpGet]
        public int GetBasketCount()
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

        [HttpGet]
        public HttpResponseMessage GetBasket()
        {
            var basket = TransactionLibrary.GetBasket(true);
            dynamic currentbasket = new ExpandoObject();    
            var lineItems = new List<ExpandoObject>();
            var subtotal = 0.0m;

            foreach (var line in basket.PurchaseOrder.OrderLines)
            {
                dynamic item = new ExpandoObject();
                item.lineid = line.OrderLineId;
                item.qty = line.Quantity;
                item.name = line.ProductName;
                item.price = line.Price;
                item.linetotal = line.Price * line.Quantity;
                lineItems.Add(item);
                subtotal += item.linetotal;
            }

            currentbasket.basket = lineItems;
            currentbasket.subtotal = subtotal;

            return Request.CreateResponse(HttpStatusCode.OK, (object)currentbasket);
        }

        [HttpPost]
        public void AddToCart(String Sku)
        {
            TransactionLibrary.AddToBasket(1, Sku);
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