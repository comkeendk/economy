using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Types;
using Xunit;

namespace Comkeen.Economy.Core.Tests.Types
{
    public class PersonCompositeDecimalTests
    {
        public class Constructor : PersonCompositeDecimalTests
        {
            [Fact]
            public void InitializesPersonValues()
            {
                const decimal valPerson1 = 1m;
                const decimal valPerson2 = 2m;
                var composite = new PersonCompositeDecimal(new decimal[] { valPerson1, valPerson2 });

                Assert.Equal(valPerson1, composite[Person.Person1]);
                Assert.Equal(valPerson2, composite[Person.Person2]);
            }

            [Fact]
            public void SumsPersonValues_Correctly()
            {
                const decimal valPerson1 = 1m;
                const decimal valPerson2 = 2m;
                const decimal inputValBoth = 5m;
                var composite = new PersonCompositeDecimal(new decimal[] { valPerson1, valPerson2, inputValBoth });

                Assert.Equal(valPerson1, composite[Person.Person1]);
                Assert.Equal(valPerson2, composite[Person.Person2]);
                Assert.NotEqual(inputValBoth, composite[Person.Both]);
            }
        }

        public class Add : PersonCompositeDecimalTests
        {
            [Fact]
            public void ReturnsNewObject()
            {
                var originalComposite = new PersonCompositeDecimal(new decimal[] { 1m, 2m });
                var otherComposite = new PersonCompositeDecimal(new decimal[] { 3m, 4m });

                var result = originalComposite.Add(otherComposite);

                Assert.NotEqual(originalComposite, result);
                Assert.NotEqual(otherComposite, result);
            }

            [Fact]
            public void ReturnsExpectedValues()
            {
                var originalComposite = new PersonCompositeDecimal(new []{ 1m, 2m });
                var otherComposite = new PersonCompositeDecimal(new decimal[] { 3m, 4m });

                var result = originalComposite.Add(otherComposite);

                Assert.Equal(result[Person.Person1], originalComposite[Person.Person1] + otherComposite[Person.Person1]);
                Assert.Equal(result[Person.Person2], originalComposite[Person.Person2] + otherComposite[Person.Person2]);
            }
        }

        public class Subtract : PersonCompositeDecimalTests
        {
            [Fact]
            public void ReturnsNewObject()
            {
                var originalComposite = new PersonCompositeDecimal(new decimal[] { 10m, 20m });
                var otherComposite = new PersonCompositeDecimal(new decimal[] { 3m, 4m });

                var result = originalComposite.Subtract(otherComposite);

                Assert.NotEqual(originalComposite, result);
                Assert.NotEqual(otherComposite, result);
            }

            [Fact]
            public void ReturnsExpectedValues()
            {
                var originalComposite = new PersonCompositeDecimal(new []{ 10m, 20m });
                var otherComposite = new PersonCompositeDecimal(new decimal[] { 3m, 4m });

                var result = originalComposite.Subtract(otherComposite);

                Assert.Equal(result[Person.Person1], originalComposite[Person.Person1] - otherComposite[Person.Person1]);
                Assert.Equal(result[Person.Person2], originalComposite[Person.Person2] - otherComposite[Person.Person2]);
            }
        }
    }
}