using System.Collections.Generic;

namespace DiscoverObjectsWithPropertyType.Tests.Classes
{
    public class Category
    {
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
