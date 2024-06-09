using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementClassLibrary.Models
{
    public class Procurement
    {
        public int ProcurementId { get; set; }
        public DateTime ProcurementDate {  get; set; }
        public DateTime RequisitionDate { get; set; }
        public decimal Amount {  get; set; }
        public ICollection<ProcurementDetails>? ProcurementDetails { get; set;}
    }
}
