namespace Voting.Common.Models
{
    using Newtonsoft.Json;
    public partial class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
