using Newtonsoft.Json;

namespace OdvezAuto.Entities
{
    public class Car
    {
        [JsonIgnore]
        public long Id { get; set ; }
        [JsonProperty("id")]
        public int IdSetter { set => Id = value; }
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("kilometers")]
        public int Kilometers { get; set; }
        [JsonProperty("power")]
        public int Power { get; set; }
        [JsonProperty("torque")]
        public int Torque { get; set; }
        [JsonProperty("price")]
        public float Price { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonIgnore]
        public string CreatedAt { get; set; }
        [JsonProperty("createdAt")]
        public string CreatedAtSetter { set => CreatedAt = value; }
        [JsonIgnore]
        public string UpdatedAt { get; set; }
        [JsonProperty("updatedAt")]
        public string UpdatedAtSetter { set => UpdatedAt = value; }
    }
}