using BookMyEvent.Web.Bff.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace BookMyEvent.Web.Bff.Extensions
{
    public static class RequestCookieCollection
    {
        public static Guid GetCurrentBasketId(this IRequestCookieCollection cookies, Settings settings)
        {
            Guid.TryParse(cookies[settings.BasketIdCookieName], out Guid basketId);
            return basketId;
        }
    }
}
