using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    public class State
    {
        public string uri { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public object properties { get; set; }
    }

    public class Country
    {
        public string uri { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public List<object> states { get; set; }
        public object properties { get; set; }
    }

    public class BillTo
    {
        public string uri { get; set; }
        public string shipTosUri { get; set; }
        public bool isGuest { get; set; }
        public string email { get; set; }
        public object label { get; set; }
        public string budgetEnforcementLevel { get; set; }
        public string costCodeTitle { get; set; }
        public object customerCurrencySymbol { get; set; }
        public object costCodes { get; set; }
        public List<object> shipTos { get; set; }
        public object validation { get; set; }
        public bool isDefault { get; set; }
        public string id { get; set; }
        public string customerNumber { get; set; }
        public string customerSequence { get; set; }
        public string customerName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public string phone { get; set; }
        public string fullAddress { get; set; }
        public object properties { get; set; }
    }  

    public class ShipTo
    {
        public string uri { get; set; }
        public bool isNew { get; set; }
        public object label { get; set; }
        public object validation { get; set; }
        public bool isDefault { get; set; }
        public string id { get; set; }
        public string customerNumber { get; set; }
        public string customerSequence { get; set; }
        public string customerName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public string phone { get; set; }
        public string fullAddress { get; set; }
        public object properties { get; set; }
    }

    public class Language
    {
        public string uri { get; set; }
        public string id { get; set; }
        public string languageCode { get; set; }
        public string cultureCode { get; set; }
        public string description { get; set; }
        public string imageFilePath { get; set; }
        public bool isDefault { get; set; }
        public bool isLive { get; set; }
        public object properties { get; set; }
    }

    public class Currency
    {
        public string uri { get; set; }
        public string id { get; set; }
        public string currencyCode { get; set; }
        public string description { get; set; }
        public string currencySymbol { get; set; }
        public bool isDefault { get; set; }
        public object properties { get; set; }
    }

    public class Session
    {
        public string uri { get; set; }
        public bool isAuthenticated { get; set; }
        public bool hasRfqUpdates { get; set; }
        public string userName { get; set; }
        public string userLabel { get; set; }
        public string userRoles { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public object newPassword { get; set; }
        public bool resetPassword { get; set; }
        public bool activateAccount { get; set; }
        public object resetToken { get; set; }
        public bool displayChangeCustomerLink { get; set; }
        public bool redirectToChangeCustomerPageOnSignIn { get; set; }
        public BillTo billTo { get; set; }
        public ShipTo shipTo { get; set; }
        public Language language { get; set; }
        public Currency currency { get; set; }
        public string deviceType { get; set; }
        public string persona { get; set; }
        public bool dashboardIsHomepage { get; set; }
        public bool isSalesPerson { get; set; }
        public string customLandingPage { get; set; }
        public object properties { get; set; }
    }
}
