using Newtonsoft.Json;

namespace ZeissDataIngestion.Model
{
    public class MachineData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
       

        [JsonProperty(PropertyName = "machine_id")]
        public string MachineId { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
