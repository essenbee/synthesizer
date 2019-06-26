using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Lpf
    {
        [JsonProperty("Enabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool Enabled { get; set; }

        [JsonProperty("Cutoff")]
        public int Cutoff { get; set; }

        [JsonProperty("Q")]
        public float Q { get; set; }
    }
}
