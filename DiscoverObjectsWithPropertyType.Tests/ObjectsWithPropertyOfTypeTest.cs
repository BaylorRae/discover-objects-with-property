using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DiscoverObjectsWithPropertyType.Tests
{
    using Classes;

    [TestFixture]
    public class ObjectsWithPropertyOfTypeTest
    {
        private readonly Assembly CurrentAssembly = Assembly.GetAssembly(typeof(ObjectsWithPropertyOfTypeTest));

        [Test]
        public void ItFindsObjectWithSamePropertyName()
        {
            ObjectsShouldContainType<Category>(typeof(Product));
        }

        [Test]
        public void ItFindsObjectsFromEnumerable()
        {
            ObjectsShouldContainType<Product>(
                typeof(Category),
                typeof(Order),
                typeof(ProductImage),
                typeof(RevisedProductionListing)
                );
        }

        [Test]
        public void ItReturnsDictionaryWithPropertyNames()
        {
            var matchingProperties = ObjectsWithPropertyType
                .DiscoverWithMapping<ProductImage>(CurrentAssembly);

            Assert.That(matchingProperties, Is.EqualTo(
                new Dictionary<Type, IEnumerable<string>> {
                    {typeof(Product), new[] {"Images"}}
                }));
        }

        [Test]
        public void ItReturnsObjectWithMultipleMatchingProperties()
        {
            var matchingProperties = ObjectsWithPropertyType
                .DiscoverWithMapping<Product>(CurrentAssembly);

            Assert.That(matchingProperties, Is.EqualTo(
                new Dictionary<Type, IEnumerable<string>> {
                    {typeof(Category), new[] {"Products"}},
                    {typeof(Order), new[] {"OrderItems"}},
                    {typeof(ProductImage), new[] {"Product"}},
                    {typeof(RevisedProductionListing), new[] {"OldProduct", "NewProduct"}}
                }));
        }

        [Test]
        public void ItDoesntFailIfNoObjectsAreFound()
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<Foo>(CurrentAssembly);

            Assert.That(matchingObjects, Is.EqualTo(
                new List<Type>()
                ));
        }

        [Test]
        public void ItDoesntFailIfNoPropertiesAreFound()
        {
            var matchingProperties = ObjectsWithPropertyType
                .DiscoverWithMapping<Foo>(CurrentAssembly);

            Assert.That(matchingProperties, Is.EqualTo(new Dictionary<Type, IEnumerable<string>>()));
        }

        [Test]
        public void ItChecksIfObjectHasPropertyWithType()
        {
            Assert.That(ObjectsWithPropertyType.HasPropertyWithType<Product>(typeof(Category)), Is.True);
        }

        [Test]
        public void ItDoesntFailIfNoPropertiesExist()
        {
            Assert.That(ObjectsWithPropertyType.HasPropertyWithType<Foo>(typeof(Product)), Is.False);
        }

        private void ObjectsShouldContainType<T>(params Type[] types)
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<T>(CurrentAssembly);

            Assert.That(matchingObjects, Is.EqualTo(types));
        }
    }
}
