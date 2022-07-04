using System;
using System.Collections.Generic;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class ColorDetail
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public string Color { get; set; }

        public virtual Product PidNavigation { get; set; }
    }
}
