using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Data
{
    public class ItemLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }

        public Guid Request_by { get; set; }

        public Guid Approved_By { get; set; }

        public DateTime Taken_Date { get; set; }

    }
}
