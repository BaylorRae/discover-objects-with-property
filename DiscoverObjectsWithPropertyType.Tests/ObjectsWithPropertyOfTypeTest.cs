using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DiscoverObjectsWithPropertyType.Tests
{
    using Classes;
    using System.Reflection;

    [TestFixture]
    public class ObjectsWithPropertyOfTypeTest
    {
        private readonly Assembly CurrentAssembly = Assembly.GetAssembly(typeof(ObjectsWithPropertyOfTypeTest));

        [Test]
        public void ItFindsObjectWithSamePropertyName()
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<Category>(CurrentAssembly);

            Assert.That(matchingObjects, Is.EqualTo(
                new List<Type> { typeof(Product) }
                ));
        }

        [Test]
        public void ItFindsObjectsFromEnumerable()
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<Product>(CurrentAssembly);

            Assert.That(matchingObjects, Is.EqualTo(
                new List<Type> { typeof(Category), typeof(Order), typeof(ProductImage) }
                ));
        }
    }
}
