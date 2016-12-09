using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    using Xamarin.Forms;

    public class Product
    {
        public string id { get; set; }
        public object orderLineId { get; set; }
        public string name { get; set; }
        public object customerName { get; set; }
        public string shortDescription { get; set; }
        public string erpNumber { get; set; }
        public string erpDescription { get; set; }
        public string urlSegment { get; set; }
        public decimal basicListPrice { get; set; }
        public decimal basicSalePrice { get; set; }
        public object basicSaleStartDate { get; set; }
        public object basicSaleEndDate { get; set; }
        public string smallImagePath { get; set; }
        public string mediumImagePath { get; set; }
        public string largeImagePath { get; set; }
        public Pricing pricing { get; set; }
        public string currencySymbol { get; set; }
        public decimal qtyOnHand { get; set; }
        public bool isConfigured { get; set; }
        public bool isFixedConfiguration { get; set; }
        public bool isActive { get; set; }
        public bool isHazardousGood { get; set; }
        public bool isDiscontinued { get; set; }
        public bool isSpecialOrder { get; set; }
        public bool isGiftCard { get; set; }
        public bool isBeingCompared { get; set; }
        public bool isSponsored { get; set; }
        public bool quoteRequired { get; set; }
        public string manufacturerItem { get; set; }
        public string packDescription { get; set; }
        public string altText { get; set; }
        public object customerUnitOfMeasure { get; set; }
        public bool canBackOrder { get; set; }
        public bool trackInventory { get; set; }
        public int multipleSaleQty { get; set; }
        public object htmlContent { get; set; }
        public string productCode { get; set; }
        public string priceCode { get; set; }
        public string sku { get; set; }
        public string upcCode { get; set; }
        public string modelNumber { get; set; }
        public string taxCode1 { get; set; }
        public string taxCode2 { get; set; }
        public string taxCategory { get; set; }
        public string shippingClassification { get; set; }
        public string shippingLength { get; set; }
        public string shippingWidth { get; set; }
        public string shippingHeight { get; set; }
        public string shippingWeight { get; set; }
        public decimal qtyPerShippingPackage { get; set; }
        public object shippingAmountOverride { get; set; }
        public object handlingAmountOverride { get; set; }
        public string metaDescription { get; set; }
        public string metaKeywords { get; set; }
        public string pageTitle { get; set; }
        public bool allowAnyGiftCardAmount { get; set; }
        public int sortOrder { get; set; }
        public bool hasMsds { get; set; }
        public string unspsc { get; set; }
        public string roundingRule { get; set; }
        public object vendorNumber { get; set; }
        public object configurationDto { get; set; }
        public string unitOfMeasure { get; set; }
        public string unitOfMeasureDisplay { get; set; }
        public string unitOfMeasureDescription { get; set; }
        public string selectedUnitOfMeasure { get; set; }
        public string selectedUnitOfMeasureDisplay { get; set; }
        public string productDetailUrl { get; set; }
        public bool canAddToCart { get; set; }
        public bool allowedAddToCart { get; set; }
        public bool canAddToWishlist { get; set; }
        public bool canViewDetails { get; set; }
        public bool canShowPrice { get; set; }
        public bool canShowUnitOfMeasure { get; set; }
        public bool canEnterQuantity { get; set; }
        public bool canConfigure { get; set; }
        public bool isStyleProductParent { get; set; }
        public decimal numberInCart { get; set; }
        public decimal qtyOrdered { get; set; }
        public Availability availability { get; set; }
        public List<object> styleTraits { get; set; }
        public List<object> styledProducts { get; set; }
        public List<object> attributeTypes { get; set; }
        public List<object> documents { get; set; }
        public List<object> specifications { get; set; }
        public List<object> crossSells { get; set; }
        public List<object> accessories { get; set; }
        public List<object> productUnitOfMeasures { get; set; }
        //public Properties properties { get; set; }
        public double score { get; set; }
        public int searchBoost { get; set; }
        public string salePriceLabel { get; set; }

        public ImageSource ProductSmallImageSource { get; set; }
        public ImageSource ProductLargeImageSource { get; set; }
    }
}
