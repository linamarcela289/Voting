namespace Voting.Common.Models
{
    using Newtonsoft.Json;

    public class Candidate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}
