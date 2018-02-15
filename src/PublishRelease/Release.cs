using System;
using Newtonsoft.Json;
using System.Net.Http;

namespace PublishRelease {

    public class CommandLineOptions {
        public string Name { set; get; } = "";

        public string Owner { set; get; } = "";

        public string Repo { set; get; } = "";

        public string Tag { set; get; } = "";

        public bool Draft { set; get; }

        public bool Prerelease { set; get; }

        public string[] Assets { set; get; } = { };
    }

    public class CreateReleaseResponse {
        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("upload_url")]
        public string UploadUrl { set; get; }
    }

    public class UploadAssetReponse {
        [JsonProperty("url")]
        public string Url { set; get; }
    }

    public static class Release {
        public static void CreateRelease(CommandLineOptions options) {
            using (var client = new HttpClient()) {

            }
        }
    }
}
