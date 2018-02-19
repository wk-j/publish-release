using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace PublishRelease {

    public static class Release {
        const string UserAgent = "github-release 0.1.0  (https://github.com/wk-j/github-release)";
        const string ApiRoot = "https://api.github.com";

        public static async Task<CreateReleaseResponse> CreateRelease(CommandLineOptions options) {
            var owner = options.Owner;
            var repo = options.Repo;
            var token = options.Token;
            var url = $"{ApiRoot}/repos/{owner}/{repo}/releases";
            using (var client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                var body = new {
                    tag_name = options.Tag,
                    target_commitish = "master",
                    name = options.Name,
                    body = options.Body,
                    draft = options.Draft,
                    prerelease = options.Prerelease
                };
                var json = JsonConvert.SerializeObject(body);
                var result = await client.PostAsync(url, new StringContent(json));
                if (result.StatusCode == HttpStatusCode.OK) {
                    var responseBody = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CreateReleaseResponse>(responseBody);
                }
                return new CreateReleaseResponse();
            }
        }
    }
}
