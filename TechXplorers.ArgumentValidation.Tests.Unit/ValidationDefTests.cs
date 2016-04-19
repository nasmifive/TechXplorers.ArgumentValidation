using System;
using NUnit.Framework;

namespace TechXplorers.ArgumentValidation.Tests.Unit
{
    [TestFixture]
    public class ValidationDefTests
    {
        [Test]
        public void Is_Valid_Test()
        {
            var objUnderTest = new object();

            Assert.DoesNotThrow(() =>
                ValidationDef<Object>.Create(objUnderTest, "objUnderTest").Is(x => x != null, "x should not be null"));

            Assert.DoesNotThrow(() =>
               ValidationDef<Object>.Create(null, "objUnderTest").Is(x => x == null));
        }

        [Test]
        public void Is_Invalid_Test()
        {
            var objUnderTest = new object();

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<Object>.Create(() => objUnderTest).Is(x => x == null, "x should be null"));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<Object>.Create(() => objUnderTest).Is(x => x == null));
        }

        [Test]
        public void Is_Returns_NonNullInstance_Test()
        {
            Assert.That(ValidationDef<Object>.Create(new object(), "foo").Is(x => x != null), Is.Not.Null);
        }

        [Test]
        public void IsNot_Valid_Test()
        {
            var objUnderTest = new object();

            Assert.DoesNotThrow(() =>
                ValidationDef<Object>.Create(objUnderTest, "objUnderTest").IsNot(x => x == null, "x should not be null"));

            Assert.DoesNotThrow(() =>
                ValidationDef<Object>.Create(objUnderTest, "objUnderTest").IsNot(x => x == null));
        }

        [Test]
        public void IsNot_Invalid_Test()
        {
            var objUnderTest = new object();

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<Object>.Create(() => objUnderTest).IsNot(x => x != null, "x should not be non null"));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<Object>.Create(() => objUnderTest).IsNot(x => x != null));
        }

        [Test]
        public void IsNot_Returns_NonNullInstance_Test()
        {
            Assert.That(ValidationDef<Object>.Create(new object(), "foo").IsNot(x => x == null), Is.Not.Null);
        }

        [Test]
        public void When_Test()
        {
            Assert.DoesNotThrow(() =>
               ValidationDef<int>.Create(() => 1000).When(true).IsGreaterThan(1));

            Assert.DoesNotThrow(() =>
                ValidationDef<int>.Create(() => 1000).When(false).IsGreaterThan(1001));

            Assert.DoesNotThrow(() =>
                ValidationDef<int?>.Create(() => 1000).When(It.HasValue).IsGreaterThan(1));

            Assert.DoesNotThrow(() =>
                ValidationDef<int>.Create(() => 1000).When(x => x == 1000).IsGreaterThan(1));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<int?>.Create(() => 1000).When(It.HasValue).IsGreaterThan(1001));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<int>.Create(() => 1000).When(x => x < 2000).IsGreaterThan(1001));
        }

        [Test]
        public void WhenNot_Test()
        {
            Assert.DoesNotThrow(() =>
               ValidationDef<int>.Create(() => 1000).WhenNot(_ => false).IsGreaterThan(1));

            Assert.DoesNotThrow(() =>
                ValidationDef<int>.Create(() => 1000).WhenNot(_ => true).IsGreaterThan(1001));

            Assert.DoesNotThrow(() =>
                ValidationDef<int?>.Create(() => 1000).WhenNot(It.HasValue).IsGreaterThan(1001));

            Assert.DoesNotThrow(() =>
                ValidationDef<int>.Create(() => 1000).WhenNot(x => x != 1000).IsGreaterThan(1));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<int?>.Create(() => 1000).WhenNot(_ => false).IsGreaterThan(1001));

            Assert.Throws<ArgumentNotValidException>(() =>
                ValidationDef<int>.Create(() => 1000).WhenNot(_ => false).IsGreaterThan(1001));
        }

        [Test]
        public void When_Returns_NonNullInstance_Test()
        {
            Assert.That(ValidationDef<Object>.Create(new object(), "foo").When(x => x != null), Is.Not.Null);
            Assert.That(ValidationDef<Object>.Create(new object(), "foo").When(true), Is.Not.Null);
        }

        [Test]
        public void WhenNot_Returns_NonNullInstance_Test()
        {
            Assert.That(ValidationDef<Object>.Create(new object(), "foo").WhenNot(x => x != null), Is.Not.Null);
        }
    }
}
