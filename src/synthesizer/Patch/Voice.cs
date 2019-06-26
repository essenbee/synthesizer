using Newtonsoft.Json;
using System;

namespace synthesizer.Patch
{
    public partial class Voice
    {
        [JsonProperty("Level")]
        public float Level { get; set; }

        [JsonProperty("WaveType")]
        public int WaveType { get; set; }

        [JsonProperty("Attack")]
        public float Attack { get; set; }

        [JsonProperty("Decay")]
        public float Decay { get; set; }

        [JsonProperty("Sustain")]
        public float Sustain { get; set; }

        [JsonProperty("Release")]
        public float Release { get; set; }

        [JsonProperty("Octave")]
        public int Octave { get; set; }

        [JsonProperty("Semitone")]
        public int Semitone { get; set; }
    }
}
