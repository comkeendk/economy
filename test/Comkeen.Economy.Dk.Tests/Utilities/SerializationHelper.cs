using Newtonsoft.Json;

namespace Comkeen.Economy.Dk.Tests.Utilities
{
    public class SerializationHelper
    {
        public static T DeserializeFromFile<T>(string filepath)
        {
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(filepath), new PersonCompositeDecimalConverter());
        }
    }
}
