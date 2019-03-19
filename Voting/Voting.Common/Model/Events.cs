namespace Voting.Common.Model
{
   
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
        public object StarDate { get; set; }

        [JsonProperty("endDate")]
        public object EndDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
        public override string ToString()
        {
            return $"{this.Name}{this.Decription}";
        }
    }
}
