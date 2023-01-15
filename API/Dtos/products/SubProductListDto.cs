using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SubProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public string Type { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public bool Discontinued { get; set; }
        public int UnitInStock { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityPerUnite { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public string PhotoUrl { get; set; }
        public bool WithSerialNumber { get; set; }
        public string Note { get; set; }
        public List<PhotoDto> Photos { get; set; }

    }
}