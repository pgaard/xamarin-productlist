using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    public class ProductCollection
    {
        public string uri { get; set; }
        public Pagination pagination { get; set; }
        public List<Product> products { get; set; }
        public List<object> didYouMeanSuggestions { get; set; }
        public bool exactMatch { get; set; }
        public bool notAllProductsFound { get; set; }
        public bool notAllProductsAllowed { get; set; }
        public string originalQuery { get; set; }
        public object correctedQuery { get; set; }
        //public Properties2 properties { get; set; }
    }
}
