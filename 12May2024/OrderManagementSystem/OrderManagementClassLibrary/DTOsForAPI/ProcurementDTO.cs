using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementClassLibrary.DTOsForAPI
{
    public class ProcurementDTO
    {
        public DateTime ProDate { get; set; }
        public DateTime ReqDate { get; set; }
        public string ProcurementDetails { get; set; }
    }
}
