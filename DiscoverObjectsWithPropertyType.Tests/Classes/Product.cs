using System.Collections.Generic;

namespace DiscoverObjectsWithPropertyType.Tests.Classes
{
    public class Product
    {
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
    }
}
