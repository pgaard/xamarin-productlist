using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    using Xamarin.Forms;

    public class ShipVia
    {
        public string id { get; set; }
        public string description { get; set; }
        public bool isDefault { get; set; }
    }

    public class Carrier
    {
        public string id { get; set; }
        public string description { get; set; }
        public List<ShipVia> shipVias { get; set; }
    }

    public class ProductSubscription
    {
        public bool subscriptionAddToInitialOrder { get; set; }
        public bool subscriptionAllMonths { get; set; }
        public bool subscriptionApril { get; set; }
        public bool subscriptionAugust { get; set; }
        public string subscriptionCyclePeriod { get; set; }
        public bool subscriptionDecember { get; set; }
        public bool subscriptionFebruary { get; set; }
        public bool subscriptionFixedPrice { get; set; }
        public bool subscriptionJanuary { get; set; }
        public bool subscriptionJuly { get; set; }
        public bool subscriptionJune { get; set; }
        public bool subscriptionMarch { get; set; }
        public bool subscriptionMay { get; set; }
        public bool subscriptionNovember { get; set; }
        public bool subscriptionOctober { get; set; }
        public int subscriptionPeriodsPerCycle { get; set; }
        public bool subscriptionSeptember { get; set; }
        public object subscriptionShipViaId { get; set; }
        public int subscriptionTotalCycles { get; set; }
    }

    public class CartLine
    {
        public string uri { get; set; }
        public string productUri { get; set; }
        public string id { get; set; }
        public int line { get; set; }
        public string productId { get; set; }
        public object requisitionId { get; set; }
        public string smallImagePath { get; set; }
        public string altText { get; set; }
        public string productName { get; set; }
        public string manufacturerItem { get; set; }
        public object customerName { get; set; }
        public string shortDescription { get; set; }
        public string erpNumber { get; set; }
        public string unitOfMeasure { get; set; }
        public string unitOfMeasureDisplay { get; set; }
        public string unitOfMeasureDescription { get; set; }
        public string baseUnitOfMeasure { get; set; }
        public string baseUnitOfMeasureDisplay { get; set; }
        public decimal qtyPerBaseUnitOfMeasure { get; set; }
        public string costCode { get; set; }
        public string notes { get; set; }
        public decimal qtyOrdered { get; set; }
        public decimal qtyLeft { get; set; }
        public Pricing pricing { get; set; }
        public bool isPromotionItem { get; set; }
        public bool isDiscounted { get; set; }
        public bool isFixedConfiguration { get; set; }
        public bool quoteRequired { get; set; }
        public List<object> breakPrices { get; set; }
        public List<object> sectionOptions { get; set; }
        public Availability availability { get; set; }
        public decimal qtyOnHand { get; set; }
        public bool canAddToCart { get; set; }
        public bool isQtyAdjusted { get; set; }
        public bool hasInsufficientInventory { get; set; }
        public bool canBackOrder { get; set; }
        public string salePriceLabel { get; set; }
        public bool isSubscription { get; set; }
        public ProductSubscription productSubscription { get; set; }
        public object properties { get; set; }

        public UriImageSource ProductSmallImageSource { get; set; }
    }

    public class Cart
    {
        public string uri { get; set; }
        public string cartLinesUri { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string statusDisplay { get; set; }
        public string type { get; set; }
        public string typeDisplay { get; set; }
        public string orderNumber { get; set; }
        public string orderDate { get; set; }
        public object billTo { get; set; }
        public object shipTo { get; set; }
        public string userLabel { get; set; }
        public object userRoles { get; set; }
        public object shipToLabel { get; set; }
        public string notes { get; set; }
        public Carrier carrier { get; set; }
        public ShipVia shipVia { get; set; }
        public object paymentMethod { get; set; }
        public string poNumber { get; set; }
        public object promotionCode { get; set; }
        public string initiatedByUserName { get; set; }
        public decimal totalQtyOrdered { get; set; }
        public int lineCount { get; set; }
        public int totalCountDisplay { get; set; }
        public int quoteRequiredCount { get; set; }
        public decimal orderSubTotal { get; set; }
        public string orderSubTotalDisplay { get; set; }
        public decimal totalTax { get; set; }
        public string totalTaxDisplay { get; set; }
        public decimal shippingAndHandling { get; set; }
        public string shippingAndHandlingDisplay { get; set; }
        public decimal orderGrandTotal { get; set; }
        public string orderGrandTotalDisplay { get; set; }
        public object costCodeLabel { get; set; }
        public bool isAuthenticated { get; set; }
        public bool isSalesperson { get; set; }
        public bool isSubscribed { get; set; }
        public bool requiresPoNumber { get; set; }
        public bool displayContinueShoppingLink { get; set; }
        public bool canModifyOrder { get; set; }
        public bool canSaveOrder { get; set; }
        public bool canBypassCheckoutAddress { get; set; }
        public bool canRequisition { get; set; }
        public bool canRequestQuote { get; set; }
        public bool canEditCostCode { get; set; }
        public bool showTaxAndShipping { get; set; }
        public bool showLineNotes { get; set; }
        public bool showCostCode { get; set; }
        public bool showNewsletterSignup { get; set; }
        public bool showPoNumber { get; set; }
        public bool showCreditCard { get; set; }
        public bool showPayPal { get; set; }
        public bool isAwaitingApproval { get; set; }
        public bool requiresApproval { get; set; }
        public string approverReason { get; set; }
        public string salespersonName { get; set; }
        public object paymentOptions { get; set; }
        public List<object> costCodes { get; set; }
        public List<object> carriers { get; set; }
        public List<CartLine> cartLines { get; set; }
        public bool canCheckOut { get; set; }
        public bool hasInsufficientInventory { get; set; }
        public string currencySymbol { get; set; }
        public object requestedDeliveryDate { get; set; }
        public bool cartNotPriced { get; set; }
        public object properties { get; set; }
    }
}
