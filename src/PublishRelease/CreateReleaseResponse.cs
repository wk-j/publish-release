using Newtonsoft.Json;

namespace PublishRelease {
    public class CreateReleaseResponse {
        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("upload_url")]
        public string UploadUrl { set; get; }
    }
}
