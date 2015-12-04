using System.Collections.Generic;

namespace DiscoverObjectsWithPropertyType.Tests.Classes
{
    public class Order
    {
        public virtual ICollection<Product> OrderItems { get; set; }
    }
}
