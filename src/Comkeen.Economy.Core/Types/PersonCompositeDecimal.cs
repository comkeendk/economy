using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Abstractions.Types;

namespace Comkeen.Economy.Core.Types
{
    public class PersonCompositeDecimal : IPersonCompositeDecimal
    {
        private readonly decimal[] values;

        public PersonCompositeDecimal()
        {
            values = new decimal[3] { 0m, 0m, 0m };
        }

        public PersonCompositeDecimal(decimal[] initValues)
            : this()
        {
            if(initValues == null) { throw new ArgumentNullException(nameof(initValues)); }

            for (int i = 0, length = initValues.Length; i < length; i++)
            {
                this[i] = initValues[i];
            }
        }

        public decimal this[Person person]
        {
            get
            {
                return values[(int)person];
            }
            set
            {
                SetValue((int)person, value);
            }
        }

        public decimal this[int personIndex]
        {
            get
            {
                return values[personIndex];
            }
            set
            {
                if(personIndex > (int)Person.Both) { throw new ArgumentOutOfRangeException(nameof(personIndex)); }
                SetValue(personIndex, value);
            }
        }

        public IPersonCompositeDecimal Add(IPersonCompositeDecimal other)
        {
            var newValues = new decimal[2]
            {
                this[Person.Person1] + other[Person.Person1],
                this[Person.Person2] + other[Person.Person2]
            };

            return new PersonCompositeDecimal(newValues);
        }

        public IPersonCompositeDecimal Subtract(IPersonCompositeDecimal other)
        {
            var newValues = new decimal[2]
            {
                this[Person.Person1] - other[Person.Person1],
                this[Person.Person2] - other[Person.Person2]
            };

            return new PersonCompositeDecimal(newValues);
        }

        private void SetValue(int index, decimal value)
        {
            if(index == (int)Person.Both)
            {
                return;
            }
            values[index] = value;

            values[(int)Person.Both] = values[(int)Person.Person1] + values[(int)Person.Person2];
        }
    }
}