using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementClassLibrary.Models
{
    public class ProcurementDetails
    {
        public int ProcurementDetailsId { get; set; }
        public int ProcurementId { get; set; }
        public decimal ProcurementQuantity {  get; set; }
        public Procurement? Procurement { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public decimal ItemUnitPrice{get;set;}
        public decimal ItemTotalPrice { get; set; }
    }
}
