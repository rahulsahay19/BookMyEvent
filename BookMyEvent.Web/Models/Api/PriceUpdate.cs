﻿using System;

namespace BookMyEvent.Web.Models.Api
{
    public class PriceUpdate
    {
        public Guid EventId { get; set; }
        public int Price { get; set; }
    }
}
