namespace Voting.Common.Model
{
    using System;
    using Newtonsoft.Json;
  

    public class Events
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("decription")]
        public string Decription { get; set; }

        [JsonProperty("starDate")]
        public DateTime StarDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("candidates")]
        public object Candidates { get; set; }

        [JsonProperty("numberCandidates")]
        public long NumberCandidates { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }

        [JsonProperty("vote")]
        public object Vote { get; set; }

        [JsonProperty("numberVotes")]
        public long NumberVotes { get; set; }
    }
}
