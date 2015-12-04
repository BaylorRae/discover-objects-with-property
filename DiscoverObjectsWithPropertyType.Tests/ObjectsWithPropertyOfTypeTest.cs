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
        [Test]
        public void ItFindsObjectWithSamePropertyName()
        {
            var matchingObjects = ObjectsWithPropertyType
                .Discover<Category>();

            Assert.That(matchingObjects, Is.EqualTo(
                new List<Type> { typeof(Product) }
                ));
        }
    }
}
