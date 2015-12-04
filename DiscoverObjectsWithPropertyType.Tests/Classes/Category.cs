using System.Collections.Generic;

namespace DiscoverObjectsWithPropertyType.Tests.Classes
{
    public class Category
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
