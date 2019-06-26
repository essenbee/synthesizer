using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Phaser
    {
        [JsonProperty("Dry")]
        public float Dry { get; set; }

        [JsonProperty("Wet")]
        public float Wet { get; set; }

        [JsonProperty("Feedback")]
        public float Feedback { get; set; }

        [JsonProperty("Freq")]
        public float Freq { get; set; }

        [JsonProperty("Width")]
        public float Width { get; set; }

        [JsonProperty("Sweep")]
        public float Sweep { get; set; }
    }
}
