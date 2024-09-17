using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrud.Models
{
    public class User
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
    }
}
