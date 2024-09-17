using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Models
{
    public class PathPartnerDTO
    {
        public long partnerId { get; set; }
        public string name { get; set; }
        public string phone_no { get; set; }
        public bool isBold { get; set; }
        public FontAttributes style { get; set; } = new FontAttributes();
    }
}
