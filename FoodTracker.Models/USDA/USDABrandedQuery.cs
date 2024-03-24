using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.USDA
{
    public class USDABrandedQuery
    {
        public string Query { get; set; }
        public List<string> DataType { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
