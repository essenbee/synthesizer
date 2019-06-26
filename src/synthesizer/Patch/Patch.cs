using Newtonsoft.Json;

namespace synthesizer.Patch
{
    public partial class Patch
    {
        [JsonProperty("PatchName")]
        public string PatchName { get; set; }

        [JsonProperty("Level")]
        public float Level { get; set; }

        [JsonProperty("WaveType")]
        public long WaveType { get; set; }

        [JsonProperty("Attack")]
        public float Attack { get; set; }

        [JsonProperty("Decay")]
        public float Decay { get; set; }

        [JsonProperty("Sustain")]
        public float Sustain { get; set; }

        [JsonProperty("Release")]
        public float Release { get; set; }

        [JsonProperty("Voice")]
        public Voice[] Voice { get; set; }

        [JsonProperty("SubOscEnabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool SubOscEnabled { get; set; }

        [JsonProperty("VibratoEnabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool VibratoEnabled { get; set; }

        [JsonProperty("MidiEnabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool MidiEnabled { get; set; }

        [JsonProperty("LPF")]
        public Lpf Lpf { get; set; }

        [JsonProperty("Tremolo")]
        public Tremolo Tremolo { get; set; }

        [JsonProperty("Chorus")]
        public Chorus Chorus { get; set; }

        [JsonProperty("Phaser")]
        public Phaser Phaser { get; set; }

        [JsonProperty("Delay")]
        public Delay Delay { get; set; }

        public static Patch FromJson(string json) => JsonConvert.DeserializeObject<Patch>(json, Converter.Settings);
    }
}
