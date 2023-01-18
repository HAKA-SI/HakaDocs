using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductSNAddingDto
    {
         public int StoreId { get; set; }
        public int SubProductId { get; set; }
        public int? Quantity { get; set; }
        public DateTime MvtDate { get; set; }
        public List<string> sns { get; set; }
         public string RefNum { get; set; }
        public string Note { get; set; }
    }
}