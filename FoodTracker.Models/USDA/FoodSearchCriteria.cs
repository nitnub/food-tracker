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
        public string Query { get; set; } = "";
        public string? GeneralSearchInput { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public string SortBy { get; set; } = "dataType.keyword";
        public string SortOrder { get; set; } = "ASC";
        public int NumberOfResultsPerPage { get; set; } = 25;
        public int PageSize { get; set; } = 25;
        public string? RequireAllWords { get; set; } = null;
    }
}
