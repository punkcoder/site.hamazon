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
    public class CartController: UmbracoApiController
    {

        [HttpPost]
        public void AddToCart(String Sku)
        {
            TransactionLibrary.AddToBasket(1, Sku);
        }
    }
}