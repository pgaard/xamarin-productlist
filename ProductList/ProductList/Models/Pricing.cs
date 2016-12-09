using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    public class Pricing
    {
        public decimal regularPrice { get; set; }
        public object regularPriceDisplay { get; set; }
        public decimal extendedRegularPrice { get; set; }
        public string extendedRegularPriceDisplay { get; set; }
        public decimal extendedUnitActualPrice { get; set; }
        public string extendedUnitActualPriceDisplay { get; set; }
        public decimal actualPrice { get; set; }
        public string actualPriceDisplay { get; set; }
        public decimal extendedUnitNetPrice { get; set; }
        public string extendedUnitNetPriceDisplay { get; set; }
        public decimal extendedActualPrice { get; set; }
        public string extendedActualPriceDisplay { get; set; }
        public decimal unitCost { get; set; }
        public string unitCostDisplay { get; set; }
        public bool isOnSale { get; set; }
        //public List<object> regularBreakPrices { get; set; }
        ///public List<object> actualBreakPrices { get; set; }
        public object additionalResults { get; set; }
        public decimal unitActualPrice { get; set; }
        public string unitActualPriceDisplay { get; set; }
        public decimal unitNetPrice { get; set; }
        public string unitNetPriceDisplay { get; set; }
        public bool requiresRealTimePrice { get; set; }
    }
}
