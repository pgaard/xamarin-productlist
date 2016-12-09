using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Models
{
    public class Pagination
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int defaultPageSize { get; set; }
        public int totalItemCount { get; set; }
        public int numberOfPages { get; set; }
        public List<int> pageSizeOptions { get; set; }
        public List<SortOption> sortOptions { get; set; }
        public string sortType { get; set; }
        public object nextPageUri { get; set; }
        public object prevPageUri { get; set; }
    }

    public class SortOption
    {
        public string displayName { get; set; }
        public string sortType { get; set; }
    }
}
