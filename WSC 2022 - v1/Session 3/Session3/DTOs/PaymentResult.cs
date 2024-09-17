using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3.DTOs
{
    public class PaymentResult
    {
        public string cardNo { get; set; }
        public string orderId { get; set; }
        public string extraInfo { get; set; }
        public string status { get; set; }
        public string trackId { get; set; }
        public decimal price { get; set; }
        public string callBackUrl { get; set; }
        public bool verify { get; set; }
    }
}
