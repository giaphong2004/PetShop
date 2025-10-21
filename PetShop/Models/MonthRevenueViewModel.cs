using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class MonthRevenueViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public decimal Revenue { get; set; }
    }
}
