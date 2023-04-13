using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PhysicalBasketDto
    {
        public int CustomerId { get; set; }
        public int InserUserId { get; set; }
        public int Total { get; set; }
        public int SubTotal { get; set; }
        public PhysicalBasketDetails Details { get; set; }
        public List<PhysicalSubProds> Products { get; set; }
    }

    public class PhysicalBasketDetails
    {
        public DateTime OrderDate { get; set; }
        public string OrderNum { get; set; }
        public string Observation { get; set; }
        public Boolean Delivered { get; set; } 
        public List<int> InvoiceSendingType { get; set; } = new List<int>();
        public int PaimentType { get; set; }

        public int? AmountPaid { get; set; }
    }

    public class PhysicalSubProds : SubProductListDto
    {
        public List<SubProductSnListDto> SubProductSNs { get; set; }
    }
}