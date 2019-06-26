using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Chorus
    {
        [JsonProperty("Delay")]
        public float Delay { get; set; }

        [JsonProperty("Sweep")]
        public float Sweep { get; set; }

        [JsonProperty("Width")]
        public float Width { get; set; }
    }
}
