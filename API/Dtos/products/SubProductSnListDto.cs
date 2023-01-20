using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SubProductSnListDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }

          public int SubProductId { get; set; }
        public SubProductListDto SubProduct { get; set; }
        public string SN { get; set; }
        public int Quantity { get; set; }=1;
    }
}