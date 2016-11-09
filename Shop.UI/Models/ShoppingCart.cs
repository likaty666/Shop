﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.UI.Models
{
    public class ShoppingCart
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string GoodName { get; set; }
        public int GoodId { get; set; }
        public int Count { get; set; }
        public decimal Money { get; set; }
    }
}