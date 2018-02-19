using Newtonsoft.Json;

namespace PublishRelease {
    public class UploadAssetReponse {
        [JsonProperty("url")]
        public string Url { set; get; }
    }
}
