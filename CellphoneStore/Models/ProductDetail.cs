using System;
using System.Collections.Generic;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class ProductDetail
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public string Screen { get; set; }
        public string Os { get; set; }
        public string Rearcam { get; set; }
        public string Frontcam { get; set; }
        public string Soc { get; set; }
        public string Ram { get; set; }
        public string Sim { get; set; }
        public string Battery { get; set; }

        public virtual Product PidNavigation { get; set; }
    }
}
