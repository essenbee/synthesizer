using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Delay
    {
        [JsonProperty("Ms")]
        public int Ms { get; set; }

        [JsonProperty("Feedback")]
        public float Feedback { get; set; }

        [JsonProperty("Mix")]
        public float Mix { get; set; }

        [JsonProperty("Wet")]
        public float Wet { get; set; }

        [JsonProperty("Dry")]
        public float Dry { get; set; }
    }
}
