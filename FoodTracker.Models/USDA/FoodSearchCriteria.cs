using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.USDA
{
    public class FoodSearchCriteria
    {
        public string Query { get; set; }
        public string GeneralSearchInput { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int NumberOfResultsPerPage { get; set; }
        public int PageSize { get; set; }
        public string RequireAllWords { get; set; }
    }
}
