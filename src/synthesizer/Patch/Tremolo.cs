using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Tremolo
    {
        [JsonProperty("Freq")]
        public int Freq { get; set; }

        [JsonProperty("FreqMult")]
        public int FreqMult { get; set; }

        [JsonProperty("Enabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool Enabled { get; set; }
    }
}
