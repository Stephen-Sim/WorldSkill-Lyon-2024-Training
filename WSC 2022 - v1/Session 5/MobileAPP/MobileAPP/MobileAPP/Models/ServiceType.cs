using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileAPP.Models
{
    public class ServiceType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
