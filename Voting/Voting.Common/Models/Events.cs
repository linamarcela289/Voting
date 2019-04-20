namespace Voting.Common.Model
{
    using Newtonsoft.Json;
    using System;

    public partial class Events
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

        [JsonProperty("user")]
        public User User { get; set; }

    }
}
