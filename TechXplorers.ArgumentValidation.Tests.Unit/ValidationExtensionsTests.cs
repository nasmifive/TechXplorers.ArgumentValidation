using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechXplorers.ArgumentValidation.Tests.Unit
{
    [TestFixture]
    public class ValidationExtensionsTests
    {
        public enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        [Test]
        public void IsNotNull_Valid_Test()
        {
            var objUnderTest = new object();
            Assert.DoesNotThrow(() =>
                Check.That(() => objUnderTest).IsNotNull());
        }

        [Test]
        public void IsNotNull_Invalid_Test()
        {
            var objUnderTest = null as object;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => objUnderTest).IsNotNull());
        }

        [Test]
        public void IsIn_Valid_Test()
        {
            var obj1UnderTest = 5;
            Assert.DoesNotThrow(() =>
                Check.That(() => obj1UnderTest).IsIn(4, 5, 6));

            var obj2UnderTest = "test";
            Assert.DoesNotThrow(() =>
                Check.That(() => obj2UnderTest).IsIn("", "test", "abc"));

            var obj3UnderTest = TestEnum.Value1;
            Assert.DoesNotThrow(() =>
                Check.That(() => obj3UnderTest).IsIn(TestEnum.Value1, TestEnum.Value2));

            var obj4UnderTest = null as object;
            Assert.DoesNotThrow(() =>
                Check.That(() => obj4UnderTest).IsIn(null, "x", "y"));
        }

        [Test]
        public void IsIn_Invalid_Test()
        {
            var obj1UnderTest = 5;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj1UnderTest).IsIn(4, 6, 7));

            var obj2UnderTest = "test";
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj2UnderTest).IsIn("", "xyz"));

            var obj3UnderTest = TestEnum.Value1;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj3UnderTest).IsIn(TestEnum.Value2, TestEnum.Value3));
        }

        [TestCase("test", "t")]
        [TestCase("test", "est")]
        [TestCase("test", "")]
        public void Contains_Valid_Test(string str, string subStr)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => str).Contains(subStr));
        }

        [TestCase("test", " ")]
        [TestCase("test", "xst")]
        [TestCase("test", "x")]
        public void Contains_Invalid_Test(string str, string subStr)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => str).Contains(subStr));
        }

        [TestCase("test")]
        [TestCase(" ")]
        public void IsNotEmpty_Str_Valid_Test(string str)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => str).IsNotEmpty());
        }

        [TestCase("")]
        public void IsNotEmpty_Str_Invalid_Test(string str)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => str).IsNotEmpty());
        }

        [TestCase("test")]
        public void IsNotWhitespace_Valid_Test(string str)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => str).IsNotWhitespace());
        }

        [TestCase("")]
        [TestCase("   ")]
        public void IsNotWhitespace_Invalid_Test(string str)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => str).IsNotWhitespace());
        }

        [Test]
        public void IsNotEmpty_Valid_Test()
        {
            var obj1UnderTest = new[] { 1, 2, 3 }.ToList();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj1UnderTest).IsNotEmpty());

            var obj2UnderTest = new[] { "" }.AsEnumerable();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj2UnderTest).IsNotEmpty());

            var obj3UnderTest = new int[1];
            Assert.DoesNotThrow(() =>
                Check.That(() => obj3UnderTest).IsNotEmpty());

            var obj4UnderTest = new string[] { null };
            Assert.DoesNotThrow(() =>
                Check.That(() => obj4UnderTest).IsNotEmpty());
        }

        [Test]
        public void IsNotEmpty_Invalid_Test()
        {
            var obj1UnderTest = new int[] { };
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj1UnderTest).IsNotEmpty());

            var obj2UnderTest = Empty.ArrayOf<string>();
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj2UnderTest).IsNotEmpty());

            var obj3UnderTest = Enumerable.Empty<object>();
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj3UnderTest).IsNotEmpty());

            var obj4UnderTest = null as IEnumerable<object>;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj4UnderTest).IsNotEmpty());
        }

        [Test]
        public void ContainsAll_Valid_Test()
        {
            var obj1UnderTest = new[] { 1, 2, 3 }.ToList();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj1UnderTest).ContainsEach(x => x > 0));

            var obj2UnderTest = new[] { "" }.AsEnumerable();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj2UnderTest).ContainsEach(x => x != null));

            var obj3UnderTest = new int[1];
            Assert.DoesNotThrow(() =>
                Check.That(() => obj3UnderTest).ContainsEach(x => x == default(int)));

            var obj4UnderTest = new string[] { null };
            Assert.DoesNotThrow(() =>
                Check.That(() => obj4UnderTest).ContainsEach(x => x == null));

            var obj5UnderTest = Empty.ArrayOf<string>();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj5UnderTest).ContainsEach(x => x != null));
        }

        [Test]
        public void ContainsAll_Invalid_Test()
        {
            var obj1UnderTest = new[] { 1, 2, 3 };
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj1UnderTest).ContainsEach(x => x > 1));

            var obj2UnderTest = new[] { "", null, " " };
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj2UnderTest).ContainsEach(x => !string.IsNullOrEmpty(x)));

            var obj3UnderTest = null as IEnumerable<object>;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj3UnderTest).ContainsEach(x => x != null));
        }

        [Test]
        public void ContainsAny_Valid_Test()
        {
            var obj1UnderTest = new[] { 1, 2, 3 }.ToList();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj1UnderTest).ContainsEach(x => x > 0));

            var obj2UnderTest = new[] { "" }.AsEnumerable();
            Assert.DoesNotThrow(() =>
                Check.That(() => obj2UnderTest).ContainsAny(x => x != null));

            var obj3UnderTest = new int[1];
            Assert.DoesNotThrow(() =>
                Check.That(() => obj3UnderTest).ContainsAny(x => x == default(int)));

            var obj4UnderTest = new[] { null, "" };
            Assert.DoesNotThrow(() =>
                Check.That(() => obj4UnderTest).ContainsAny(x => x != null));
        }

        [Test]
        public void ContainsAny_Invalid_Test()
        {
            var obj1UnderTest = new[] { 1, 2, 3 };
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj1UnderTest).ContainsAny(x => x > 3));

            var obj2UnderTest = new[] { "", null, " " };
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj2UnderTest).ContainsAny(x => !string.IsNullOrWhiteSpace(x)));

            var obj3UnderTest = null as IEnumerable<object>;
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj3UnderTest).ContainsAny(x => x != null));

            var obj4UnderTest = Empty.ArrayOf<string>();
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => obj4UnderTest).ContainsAny(x => x != null));
        }

        [TestCase(1, 0)]
        [TestCase(-1, -2)]
        [TestCase(200, 100)]
        [TestCase(Int32.MaxValue, Int32.MinValue)]
        public void IsGreaterThanLessThan_Int_Test(int value1, int value2)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => value1).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2).IsGreaterThan(value1));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1).IsLessThan(value2));
        }

        [TestCase(1, 0)]
        [TestCase(-1, -2)]
        [TestCase(200, 100)]
        [TestCase(Int32.MaxValue, Int32.MinValue)]
        public void IsGreaterThanLessThan_Nullable_Int_Test(int value1, int value2)
        {
            int? nullInt = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullInt).IsGreaterThan(default(int)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value1 as int?).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2 as int?).IsGreaterThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullInt).IsLessThan(default(int)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2 as int?).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1 as int?).IsLessThan(value2));
        }

        [TestCase(1L, 0L)]
        [TestCase(-1L, -2L)]
        [TestCase(200L, 100L)]
        [TestCase(Int64.MaxValue, Int64.MinValue)]
        public void IsGreaterThanLessThan_Long_Test(long value1, long value2)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => value1).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2).IsGreaterThan(value1));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1).IsLessThan(value2));
        }

        [TestCase(1L, 0L)]
        [TestCase(-1L, -2L)]
        [TestCase(200L, 100L)]
        [TestCase(Int64.MaxValue, Int64.MinValue)]
        public void IsGreaterThanLessThan_Nullable_Long_Test(long value1, long value2)
        {
            long? nullLong = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullLong).IsGreaterThan(default(long)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value1 as long?).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2 as long?).IsGreaterThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullLong).IsLessThan(default(long)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2 as long?).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1 as long?).IsLessThan(value2));
        }

        [TestCase(1.0f, 0.0f)]
        [TestCase(0.00005f, 0.0f)]
        [TestCase(-1.23f, -2.2f)]
        [TestCase(200.005f, 100.0987f)]
        [TestCase(1.999f, 1.998f)]
        [TestCase(Single.MaxValue, Single.MinValue)]
        public void IsGreaterThanLessThan_Float_Test(float value1, float value2)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => value1).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2).IsGreaterThan(value1));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1).IsLessThan(value2));
        }

        [TestCase(1.0f, 0.0f)]
        [TestCase(0.00005f, 0.0f)]
        [TestCase(-1.23f, -2.2f)]
        [TestCase(200.005f, 100.0987f)]
        [TestCase(1.999f, 1.998f)]
        [TestCase(Single.MaxValue, Single.MinValue)]
        public void IsGreaterThanLessThan_Nullable_Float_Test(float value1, float value2)
        {
            float? nullFloat = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullFloat).IsGreaterThan(default(float)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value1 as float?).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2 as float?).IsGreaterThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullFloat).IsLessThan(default(float)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2 as float?).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1 as float?).IsLessThan(value2));
        }

        [Test]
        public void IsGreaterThanLessThan_Decimal_Test()
        {
            //decimal value with 'm' suffix cannot be constants
            var testCases = new[]
            {
                new {Value1 = 1.0m, Value2 = 0.0m},
                new {Value1 = 0.00005m, Value2 = 0.0m},
                new {Value1 = -1.23m, Value2 = -2.2m},
                new {Value1 = 200.005m, Value2 = 100.0987m},
                new {Value1 = 1.999m, Value2 = 1.998m},
                new {Value1 = decimal.MaxValue, Value2 = decimal.MinValue}
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value1).IsGreaterThan(testCase.Value2));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value2).IsGreaterThan(testCase.Value1));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value2).IsLessThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value1).IsLessThan(testCase.Value2));
            }
        }

        [Test]
        public void IsGreaterThanLessThan_Nullable_Decimal_Test()
        {
            //decimal value with 'm' suffix cannot be constants
            var testCases = new[]
            {
                new {Value1 = 1.0m, Value2 = 0.0m},
                new {Value1 = 0.00005m, Value2 = 0.0m},
                new {Value1 = -1.23m, Value2 = -2.2m},
                new {Value1 = 200.005m, Value2 = 100.0987m},
                new {Value1 = 1.999m, Value2 = 1.998m},
                new {Value1 = decimal.MaxValue, Value2 = decimal.MinValue}
            };

            decimal? nullDecimal = null;

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDecimal).IsGreaterThan(default(decimal)));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value1 as decimal?).IsGreaterThan(testCase.Value2));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value2 as decimal?).IsGreaterThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDecimal).IsLessThan(default(decimal)));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value2 as decimal?).IsLessThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value1 as decimal?).IsLessThan(testCase.Value2));
            }
        }

        [TestCase(1.0d, 0.0d)]
        [TestCase(0.00005d, 0.0d)]
        [TestCase(-1.23d, -2.2d)]
        [TestCase(200.005d, 100.0987d)]
        [TestCase(1.999d, 1.998d)]
        [TestCase(Double.MaxValue, Double.MinValue)]
        public void IsGreaterThanLessThan_Double_Test(double value1, double value2)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => value1).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2).IsGreaterThan(value1));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1).IsLessThan(value2));
        }

        [TestCase(1.0d, 0.0d)]
        [TestCase(0.00005d, 0.0d)]
        [TestCase(-1.23d, -2.2d)]
        [TestCase(200.005d, 100.0987d)]
        [TestCase(1.999d, 1.998d)]
        [TestCase(Double.MaxValue, Double.MinValue)]
        public void IsGreaterThanLessThan_Nullable_Double_Test(double value1, double value2)
        {
            double? nullDouble = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullDouble).IsGreaterThan(default(double)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value1 as double?).IsGreaterThan(value2));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value2 as double?).IsGreaterThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullDouble).IsLessThan(default(double)));

            Assert.DoesNotThrow(() =>
                Check.That(() => value2 as double?).IsLessThan(value1));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => value1 as double?).IsLessThan(value2));
        }

        [Test]
        public void IsGreaterThanLessThan_DateTime_Test()
        {
            var testCases = new[]
            {
                new {Value1 = new DateTime(2001, 10, 10).AddSeconds(1), Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = new DateTime(2001, 10, 11), Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = DateTime.MaxValue, Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = new DateTime(2001, 10, 10), Value2 = DateTime.MinValue},
                new {Value1 = DateTime.MaxValue, Value2 = DateTime.MinValue},
                new {Value1 = DateTime.Today, Value2 = DateTime.Today.AddSeconds(-1)},
                new {Value1 = DateTime.Now, Value2 = DateTime.Now.AddDays(-1)},
                new {Value1 = DateTime.Today.AddDays(2), Value2 = DateTime.Today},
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value1).IsGreaterThan(testCase.Value2));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value2).IsGreaterThan(testCase.Value1));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value2).IsLessThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value1).IsLessThan(testCase.Value2));
            }
        }

        [Test]
        public void IsGreaterThanLessThan_Nullable_DateTime_Test()
        {
            var testCases = new[]
            {
                new {Value1 = new DateTime(2001, 10, 10).AddSeconds(1), Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = new DateTime(2001, 10, 11), Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = DateTime.MaxValue, Value2 = new DateTime(2001, 10, 10)},
                new {Value1 = new DateTime(2001, 10, 10), Value2 = DateTime.MinValue},
                new {Value1 = DateTime.MaxValue, Value2 = DateTime.MinValue},
                new {Value1 = DateTime.Today, Value2 = DateTime.Today.AddSeconds(-1)},
                new {Value1 = DateTime.Now, Value2 = DateTime.Now.AddDays(-1)},
                new {Value1 = DateTime.Today.AddDays(2), Value2 = DateTime.Today},
            };

            DateTime? nullDateTime = null;

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDateTime).IsGreaterThan(default(DateTime)));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value1 as DateTime?).IsGreaterThan(testCase.Value2));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value2 as DateTime?).IsGreaterThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDateTime).IsLessThan(default(DateTime)));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.Value2 as DateTime?).IsLessThan(testCase.Value1));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.Value1 as DateTime?).IsLessThan(testCase.Value2));
            }
        }

        [TestCase(10, 10, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(-2, -1, -3)]
        [TestCase(150, 200, 100)]
        [TestCase(200, 200, 200)]
        [TestCase(10000, Int32.MaxValue, Int32.MinValue)]
        public void IsBetween_Int_Test(int x, int y, int z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(11, 10, 0)]
        [TestCase(-1, 2, 0)]
        [TestCase(-4, -1, -3)]
        [TestCase(250, 200, 100)]
        [TestCase(201, 200, 200)]
        [TestCase(Int32.MinValue, Int32.MaxValue, 0)]
        public void IsBetween_NotValid_Int_Test(int x, int y, int z)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(10, 10, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(-2, -1, -3)]
        [TestCase(150, 200, 100)]
        [TestCase(200, 200, 200)]
        [TestCase(10000, Int32.MaxValue, Int32.MinValue)]
        public void IsBetween_Nullable_Int_Test(int? x, int y, int z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(11, 10, 0)]
        [TestCase(-1, 2, 0)]
        [TestCase(-4, -1, -3)]
        [TestCase(250, 200, 100)]
        [TestCase(201, 200, 200)]
        [TestCase(Int32.MinValue, Int32.MaxValue, 0)]
        public void IsBetween_NotValid_Nullable_Int_Test(int? x, int y, int z)
        {
            int? nullInt = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullInt).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullInt).IsBetween(z, and: y));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(10L, 10L, 0L)]
        [TestCase(1L, 2L, 0L)]
        [TestCase(-2L, -1L, -3L)]
        [TestCase(150L, 200L, 100L)]
        [TestCase(200L, 200L, 200L)]
        [TestCase(100000L, Int64.MaxValue, Int64.MinValue)]
        public void IsBetween_Long_Test(long x, long y, long z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(11L, 10L, 0L)]
        [TestCase(-1L, 2L, 0L)]
        [TestCase(-4L, -1L, -3L)]
        [TestCase(250L, 200L, 100L)]
        [TestCase(201L, 200L, 200L)]
        [TestCase(Int64.MaxValue, Int64.MinValue, 100000L)]
        public void IsBetween_NotValid_Long_Test(long x, long y, long z)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(10L, 10L, 0L)]
        [TestCase(1L, 2L, 0L)]
        [TestCase(-2L, -1L, -3L)]
        [TestCase(150L, 200L, 100L)]
        [TestCase(200L, 200L, 200L)]
        [TestCase(100000L, Int64.MaxValue, Int64.MinValue)]
        public void IsBetween_Nullable_Long_Test(long? x, long y, long z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(11L, 10L, 0L)]
        [TestCase(-1L, 2L, 0L)]
        [TestCase(-4L, -1L, -3L)]
        [TestCase(250L, 200L, 100L)]
        [TestCase(201L, 200L, 200L)]
        [TestCase(Int64.MaxValue, Int64.MinValue, 100000L)]
        public void IsBetween_NotValid_Nullable_Long_Test(long? x, long y, long z)
        {
            long? nullLong = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullLong).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullLong).IsBetween(z, and: y));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(0.99f, 1.0f, 0.0f)]
        [TestCase(0.00f, 0.00f, 0.00f)]
        [TestCase(0.00004f, 0.00005f, 0.0f)]
        [TestCase(-2.0f, -1.23f, -2.2f)]
        [TestCase(-2.2f, -1.2f, -2.2f)]
        [TestCase(150.0f, 200.005f, 100.0987f)]
        [TestCase(1.9985f, 1.999f, 1.998f)]
        [TestCase(678.567f, Single.MaxValue, Single.MinValue)]
        public void IsBetween_Float_Test(float x, float y, float z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(1.01f, 1.0f, 0.0f)]
        [TestCase(0.00006f, 0.00005f, 0.0f)]
        [TestCase(-2.3f, -1.23f, -2.2f)]
        [TestCase(-2.21f, -1.2f, -2.2f)]
        [TestCase(250.0f, 200.005f, 100.0987f)]
        [TestCase(1.9995f, 1.999f, 1.998f)]
        [TestCase(Single.MaxValue, Single.MinValue, 678.567f)]
        public void IsBetween_NotValid_Float_Test(float x, float y, float z)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(0.99f, 1.0f, 0.0f)]
        [TestCase(0.00f, 0.00f, 0.00f)]
        [TestCase(0.00004f, 0.00005f, 0.0f)]
        [TestCase(-2.0f, -1.23f, -2.2f)]
        [TestCase(-2.2f, -1.2f, -2.2f)]
        [TestCase(150.0f, 200.005f, 100.0987f)]
        [TestCase(1.9985f, 1.999f, 1.998f)]
        [TestCase(678.567f, Single.MaxValue, Single.MinValue)]
        public void IsBetween_Nullable_Float_Test(float? x, float y, float z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(1.01f, 1.0f, 0.0f)]
        [TestCase(0.00006f, 0.00005f, 0.0f)]
        [TestCase(-2.3f, -1.23f, -2.2f)]
        [TestCase(-2.21f, -1.2f, -2.2f)]
        [TestCase(250.0f, 200.005f, 100.0987f)]
        [TestCase(1.9995f, 1.999f, 1.998f)]
        [TestCase(Single.MaxValue, Single.MinValue, 678.567f)]
        public void IsBetween_NotValid_Nullable_Float_Test(float? x, float y, float z)
        {
            float? nullFloat = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullFloat).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullFloat).IsBetween(z, and: y));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [Test]
        public void IsBetween_Decimal_Test()
        {
            //decimal value with 'm' suffix cannot be constants
            var testCases = new[]
            {
                new {X = 1.0m, Y = 0.0m, Z = 1.1m},
                new {X = 0.00005m, Y = 0.0m, Z = 0.0006m},
                new {X = -1.23m, Y = -2.2m, Z = 1.0m},
                new {X = 200.005m, Y = 100.0987m, Z = 200.005m},
                new {X = 1.9985m, Y = 1.998m, Z = 1.999m},
                new {X = 99.99m, Y = decimal.MinValue, Z = decimal.MaxValue}
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        public void IsBetween_NotValid_Decimal_Test()
        {
            //decimal value with 'm' suffix cannot be constants
            var testCases = new[]
            {
                new {X = 1.2m, Y = 0.0m, Z = 1.1m},
                new {X = 0.00007m, Y = 0.0m, Z = 0.0006m},
                new {X = -2.23m, Y = -2.2m, Z = 1.0m},
                new {X = 200.005m, Y = 100.0987m, Z = 100.1m},
                new {X = 200.006m, Y = 100.0987m, Z = 200.005m},
                new {X = 1.997m, Y = 1.998m, Z = 1.999m},
                new {X = decimal.MaxValue, Y = decimal.MinValue, Z = 99.99m}
            };

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [Test]
        public void IsBetween_Nullable_Decimal_Test()
        {
            var testCases = new[]
            {
                new {X = 1.0m as decimal?, Y = 0.0m, Z = 1.1m},
                new {X = 0.00005m as decimal?, Y = 0.0m, Z = 0.0006m},
                new {X = -1.23m as decimal?, Y = -2.2m, Z = 1.0m},
                new {X = 200.005m as decimal?, Y = 100.0987m, Z = 200.005m},
                new {X = 1.9985m as decimal?, Y = 1.998m, Z = 1.999m},
                new {X = 99.99m as decimal?, Y = decimal.MinValue, Z = decimal.MaxValue}
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                   Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        public void IsBetween_NotValid_Nullable_Decimal_Test()
        {
            //decimal value with 'm' suffix cannot be constants
            var testCases = new[]
            {
                new {X = 1.2m as decimal?, Y = 0.0m, Z = 1.1m},
                new {X = 0.00007m as decimal?, Y = 0.0m, Z = 0.0006m},
                new {X = -2.23m as decimal?, Y = -2.2m, Z = 1.0m},
                new {X = 200.005m as decimal?, Y = 100.0987m, Z = 100.1m},
                new {X = 200.006m as decimal?, Y = 100.0987m, Z = 200.005m},
                new {X = 1.997m as decimal?, Y = 1.998m, Z = 1.999m},
                new {X = decimal.MaxValue as decimal?, Y = decimal.MinValue, Z = 99.99m}
            };

            decimal? nullDecimal = null;

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDecimal).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDecimal).IsBetween(testCase.Z, and: testCase.Y));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [TestCase(0.99d, 1.0d, 0.0d)]
        [TestCase(0.00004d, 0.00005d, 0.0d)]
        [TestCase(-1.25d, -1.23d, -2.2d)]
        [TestCase(-2.2d, -2.2d, -2.2d)]
        [TestCase(200.005d, 200.005d, 100.0987d)]
        [TestCase(1.9985d, 1.999d, 1.998d)]
        [TestCase(100.1d, Double.MaxValue, Double.MinValue)]
        public void IsBetween_Double_Test(double x, double y, double z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(1.1d, 1.0d, 0.0d)]
        [TestCase(0.00006d, 0.00005d, 0.0d)]
        [TestCase(-1.22d, -1.23d, -2.2d)]
        [TestCase(200.006d, 200.005d, 100.0987d)]
        [TestCase(2.0d, 1.999d, 1.998d)]
        [TestCase(Double.MinValue, 100.1d, Double.MaxValue)]
        public void IsBetween_NotValid_Double_Test(double x, double y, double z)
        {
            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(0.99d, 1.0d, 0.0d)]
        [TestCase(0.00004d, 0.00005d, 0.0d)]
        [TestCase(-1.25d, -1.23d, -2.2d)]
        [TestCase(-2.2d, -2.2d, -2.2d)]
        [TestCase(200.005d, 200.005d, 100.0987d)]
        [TestCase(1.9985d, 1.999d, 1.998d)]
        [TestCase(100.1d, Double.MaxValue, Double.MinValue)]
        public void IsBetween_Nullable_Double_Test(double? x, double y, double z)
        {
            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.DoesNotThrow(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [TestCase(1.1d, 1.0d, 0.0d)]
        [TestCase(0.00006d, 0.00005d, 0.0d)]
        [TestCase(-1.22d, -1.23d, -2.2d)]
        [TestCase(200.006d, 200.005d, 100.0987d)]
        [TestCase(2.0d, 1.999d, 1.998d)]
        [TestCase(Double.MinValue, 100.1d, Double.MaxValue)]
        public void IsBetween_NotValid_Nullable_Double_Test(double? x, double y, double z)
        {
            double? nullDouble = null;

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullDouble).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => nullDouble).IsBetween(z, and: y));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(y, and: z));

            Assert.Throws<ArgumentNotValidException>(() =>
                Check.That(() => x).IsBetween(z, and: y));
        }

        [Test]
        public void IsBetween_DateTime_Test()
        {
            var testCases = new[]
            {
                new {X = new DateTime(2001, 10, 10).AddMilliseconds(1), Y = new DateTime(2001, 10, 10).AddSeconds(1), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 11), Y = new DateTime(2001, 10, 12), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(1), Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10), Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(-1), Y = new DateTime(2001, 10, 10), Z = DateTime.MinValue},
                new {X = DateTime.Now, Y = DateTime.MaxValue, Z = DateTime.MinValue},
                new {X = DateTime.Today.AddSeconds(-1), Y = DateTime.Today, Z = DateTime.Today.AddSeconds(-2)},
                new {X = DateTime.Today.AddDays(1), Y = DateTime.Today.AddDays(2), Z = DateTime.Today}
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [Test]
        public void IsBetween_NotValid_DateTime_Test()
        {
            var testCases = new[]
            {
                new {X = new DateTime(2001, 10, 10).AddSeconds(2), Y = new DateTime(2001, 10, 10).AddSeconds(1), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 13), Y = new DateTime(2001, 10, 12), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(-1), Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(1), Y = new DateTime(2001, 10, 10), Z = DateTime.MinValue},
                new {X = DateTime.MinValue, Y = DateTime.MaxValue, Z = DateTime.Now},
                new {X = DateTime.Today.AddSeconds(1), Y = DateTime.Today, Z = DateTime.Today.AddSeconds(-2)},
                new {X = DateTime.Today.AddDays(-1), Y = DateTime.Today.AddDays(2), Z = DateTime.Today}
            };

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [Test]
        public void IsBetween_Nullable_DateTime_Test()
        {
            var testCases = new[]
            {
                new {X = new DateTime(2001, 10, 10).AddMilliseconds(1) as DateTime?, Y = new DateTime(2001, 10, 10).AddSeconds(1), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 11) as DateTime?, Y = new DateTime(2001, 10, 12), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(1) as DateTime?, Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10) as DateTime?, Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(-1) as DateTime?, Y = new DateTime(2001, 10, 10), Z = DateTime.MinValue},
                new {X = DateTime.Now as DateTime?, Y = DateTime.MaxValue, Z = DateTime.MinValue},
                new {X = DateTime.Today.AddSeconds(-1) as DateTime?, Y = DateTime.Today, Z = DateTime.Today.AddSeconds(-2)},
                new {X = DateTime.Today.AddDays(1) as DateTime?, Y = DateTime.Today.AddDays(2), Z = DateTime.Today}
            };

            foreach (var testCase in testCases)
            {
                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.DoesNotThrow(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [Test]
        public void IsBetween_NotValid_Nullable_DateTime_Test()
        {
            var testCases = new[]
            {
                new {X = new DateTime(2001, 10, 10).AddSeconds(2) as DateTime?, Y = new DateTime(2001, 10, 10).AddSeconds(1), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 13) as DateTime?, Y = new DateTime(2001, 10, 12), Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(-1) as DateTime?, Y = DateTime.MaxValue, Z = new DateTime(2001, 10, 10)},
                new {X = new DateTime(2001, 10, 10).AddDays(1) as DateTime?, Y = new DateTime(2001, 10, 10), Z = DateTime.MinValue},
                new {X = DateTime.MinValue as DateTime?, Y = DateTime.MaxValue, Z = DateTime.Now},
                new {X = DateTime.Today.AddSeconds(1) as DateTime?, Y = DateTime.Today, Z = DateTime.Today.AddSeconds(-2)},
                new {X = DateTime.Today.AddDays(-1) as DateTime?, Y = DateTime.Today.AddDays(2), Z = DateTime.Today}
            };

            DateTime? nullDateTime = null;

            foreach (var testCase in testCases)
            {
                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDateTime).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => nullDateTime).IsBetween(testCase.Z, and: testCase.Y));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Y, and: testCase.Z));

                Assert.Throws<ArgumentNotValidException>(() =>
                    Check.That(() => testCase.X).IsBetween(testCase.Z, and: testCase.Y));
            }
        }

        [Test]
        public void IsEqualTo_Test()
        {
            Assert.DoesNotThrow(() => Check.That(() => 16d).IsEqualTo(16.00));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16d).IsEqualTo(16.01));

            Assert.DoesNotThrow(() => Check.That(() => "test").IsEqualTo("test"));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => "test").IsEqualTo("TEST"));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => "test").IsEqualTo(null));

            Assert.DoesNotThrow(() => Check.That(() => 16.008m).IsEqualTo(16.008m));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16.008m).IsEqualTo(16.009m));

            Assert.DoesNotThrow(() => Check.That(() => 16).IsEqualTo(16));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16).IsEqualTo(17));

            Assert.DoesNotThrow(() => Check.That(() => true).IsEqualTo(true));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => true).IsEqualTo(false));

            Assert.DoesNotThrow(() => Check.That(() => DateTime.Today).IsEqualTo(DateTime.Now.Date));
            Assert.Throws<ArgumentNotValidException>(
                () => Check.That(() => DateTime.Today).IsEqualTo(DateTime.Today.AddDays(2)));

            var testObj = new TestObject
            {
                IntValue = 1,
                StringValue = "xyz"
            };

            Assert.DoesNotThrow(() => Check.That(() => testObj).IsEqualTo(testObj));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => testObj).IsEqualTo(new TestObject()));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => testObj).IsEqualTo(null));
        }

        [Test]
        public void IsEqualTo_Nullable_Test()
        {
            Assert.DoesNotThrow(() => Check.That(() => (double?)16).IsEqualTo(16.00));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (double?)null).IsEqualTo(16.01));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (double?)16).IsEqualTo(16.01));

            Assert.DoesNotThrow(() => Check.That(() => (decimal?)16.008).IsEqualTo(16.008m));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (decimal?)null).IsEqualTo(16.009m));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (decimal?)16.008).IsEqualTo(16.009m));

            Assert.DoesNotThrow(() => Check.That(() => (int?)16).IsEqualTo(16));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (int?)null).IsEqualTo(17));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (int?)16).IsEqualTo(17));

            Assert.DoesNotThrow(() => Check.That(() => (bool?)true).IsEqualTo(true));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (bool?)null).IsEqualTo(false));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (bool?)true).IsEqualTo(false));

            Assert.DoesNotThrow(() => Check.That(() => (DateTime?)DateTime.Today).IsEqualTo(DateTime.Now.Date));
            Assert.Throws<ArgumentNotValidException>(
                () => Check.That(() => (DateTime?)null).IsEqualTo(DateTime.Today.AddDays(2)));
            Assert.Throws<ArgumentNotValidException>(
                () => Check.That(() => (DateTime?)DateTime.Today).IsEqualTo(DateTime.Today.AddDays(2)));
        }

        [Test]
        public void IsNotEqualTo_Test()
        {
            Assert.DoesNotThrow(() => Check.That(() => 16d).IsNotEqualTo(16.01));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16d).IsNotEqualTo(16.00));

            Assert.DoesNotThrow(() => Check.That(() => "test").IsNotEqualTo("TEST"));
            Assert.DoesNotThrow(() => Check.That(() => "test").IsNotEqualTo(null));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => "test").IsNotEqualTo("test"));

            Assert.DoesNotThrow(() => Check.That(() => 16.008m).IsNotEqualTo(16.009m));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16.008m).IsNotEqualTo(16.008m));

            Assert.DoesNotThrow(() => Check.That(() => 16).IsNotEqualTo(17));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 16).IsNotEqualTo(16));

            Assert.DoesNotThrow(() => Check.That(() => true).IsNotEqualTo(false));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => true).IsNotEqualTo(true));

            Assert.DoesNotThrow(() => Check.That(() => DateTime.Today).IsNotEqualTo(DateTime.Today.AddDays(-2)));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => DateTime.Today).IsNotEqualTo(DateTime.Today));

            Assert.DoesNotThrow(() => Check.That(() => Guid.NewGuid()).IsNotEqualTo(Guid.Empty));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => new Guid()).IsNotEqualTo(Guid.Empty));

            var testObj = new TestObject
            {
                IntValue = 1,
                StringValue = "xyz"
            };

            Assert.DoesNotThrow(() => Check.That(() => testObj).IsNotEqualTo(new TestObject()));
            Assert.DoesNotThrow(() => Check.That(() => testObj).IsNotEqualTo(null));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => testObj).IsNotEqualTo(testObj));
        }

        [Test]
        public void IsNotEqualTo_Nullable_Test()
        {
            Assert.DoesNotThrow(() => Check.That(() => (double?)16).IsNotEqualTo(16.01));
            Assert.DoesNotThrow(() => Check.That(() => (double?)null).IsNotEqualTo(16.01));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (double?)16).IsNotEqualTo(16.00));

            Assert.DoesNotThrow(() => Check.That(() => (decimal?)16.008).IsNotEqualTo(16.009m));
            Assert.DoesNotThrow(() => Check.That(() => (decimal?)null).IsNotEqualTo(16.009m));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (decimal?)16.008).IsNotEqualTo(16.008m));

            Assert.DoesNotThrow(() => Check.That(() => (int?)16).IsNotEqualTo(17));
            Assert.DoesNotThrow(() => Check.That(() => (int?)null).IsNotEqualTo(17));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (int?)16).IsNotEqualTo(16));

            Assert.DoesNotThrow(() => Check.That(() => (bool?)true).IsNotEqualTo(false));
            Assert.DoesNotThrow(() => Check.That(() => (bool?)null).IsNotEqualTo(false));
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (bool?)true).IsNotEqualTo(true));

            Assert.DoesNotThrow(
                () => Check.That(() => (DateTime?)DateTime.Today).IsNotEqualTo(DateTime.Today.AddDays(2)));
            Assert.DoesNotThrow(() => Check.That(() => (DateTime?)null).IsNotEqualTo(DateTime.Today.AddDays(2)));
            Assert.Throws<ArgumentNotValidException>(
                () => Check.That(() => (DateTime?)DateTime.Today).IsNotEqualTo(DateTime.Now.Date));

            Assert.DoesNotThrow(() => Check.That(() => (Guid?)Guid.NewGuid()).IsNotEqualTo(Guid.Empty));
            Assert.DoesNotThrow(() => Check.That(() => (Guid?)Guid.NewGuid()).IsNotEqualTo(null));
            Assert.DoesNotThrow(() => Check.That(() => (Guid?)null).IsNotEqualTo(Guid.Empty));
            Assert.Throws<ArgumentNotValidException>(
                () => Check.That(() => (Guid?)(new Guid())).IsNotEqualTo(Guid.Empty));
        }

        [Test]
        public void IsValidEnum_Test()
        {
            Assert.DoesNotThrow(() => Check.That(() => TestEnum.Value1).IsValidEnum<TestEnum>());
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => 2).IsValidEnum());

            Assert.DoesNotThrow(() => Check.That(() => (TestEnum?)TestEnum.Value1).IsValidEnum());
            Assert.DoesNotThrow(() => Check.That(() => (TestEnum?)null).IsValidEnum());
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (int?)2).IsValidEnum());
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (TestEnum)9999).IsValidEnum());
            Assert.Throws<ArgumentNotValidException>(() => Check.That(() => (int?)null).IsValidEnum());
        }
    }
}
