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
            ObjectsShouldContainType<Category>(typeof(Product));
        }

        [Test]
        public void ItFindsObjectsFromEnumerable()
        {
            ObjectsShouldContainType<Product>(typeof(Category), typeof(Order), typeof(ProductImage));
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

        private void ObjectsShouldContainType<T>(params Type[] types)
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<T>(CurrentAssembly);

            Assert.That(matchingObjects, Is.EqualTo(types));
        }
    }
}
