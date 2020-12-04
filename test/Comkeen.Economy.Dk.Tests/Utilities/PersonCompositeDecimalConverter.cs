using System;
using Comkeen.Economy.Core.Types;
using Newtonsoft.Json;

namespace Comkeen.Economy.Dk.Tests.Utilities
{
    internal class PersonCompositeDecimalConverter : JsonConverter
    {
        public PersonCompositeDecimalConverter()
        { }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PersonCompositeDecimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var decimalArray = serializer.Deserialize<decimal[]>(reader);
            if(decimalArray.Length > 0)
            {
                return new PersonCompositeDecimal(decimalArray);
            }
            return new PersonCompositeDecimal();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}