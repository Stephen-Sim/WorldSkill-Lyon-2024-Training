using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class CollectParcelRequest
    {
        public int uid{ get; set; }
        public int parcelid { get; set; }
        public byte[] img { get; set; }
        public string desc { get; set; }
    }
}
