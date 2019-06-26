using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public static class Serialize
    {
        public static string ToJson(this Patch self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
