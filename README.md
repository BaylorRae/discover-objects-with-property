## Discover Objects with Property of Type

This is an excercise of finding all the objects within an assembly that have a
property of a type.

The [test project includes classes][test_classes] that might come from an e-commerce platform
that reference each other in different ways (`Category`, `Order`, `Product`,
`ProductImage`).

The classes are connected to each other with the following properties.

```c#
class Category     // (IEnumerable<Product> Products)
class Order        // (ICollection<Product> OrderItems)
class Product      // (Category Category, ICollection<ProductImage> Images)
class ProductImage // (Product Product)
```

## Usage

Objects can be discovered from all available assemblies or by specifying the assembly as the first parameter.

```c#
interface ObjectsWithPropertyType
{
    IList<Type> Discover<T>();
    IList<Type> Discover<T>(Assembly assembly);
}
```

[test_classes]: https://github.com/BaylorRae/discover-projects-with-property/tree/master/DiscoverObjectsWithPropertyType.Tests/Classes
