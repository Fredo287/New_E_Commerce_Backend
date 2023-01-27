using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Requests
{
    public class ModifyOrderRequest
    {
        public string OrderReferenceID { get; set;} = string.Empty;
        public int ProductID { get; set;}
    }
}
