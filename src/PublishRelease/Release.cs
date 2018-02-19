using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Net.Http.Headers;

namespace PublishRelease {

    public static class Release {
        const string UserAgent = "publish-release 0.1.0  (https://github.com/wk-j/publish-release)";
        const string ApiRoot = "https://api.github.com";

        private static string FindMediaType(string fileExtension) {
            switch (fileExtension) {
                case ".zip":
                    return "application/zip";
                default:
                    return "application/octet-stream";
            }
        }

        private static async Task<UploadAssetReponse> UploadAssetsAsync(HttpClient client, string uploadUrl, string asset) {
            var fileInfo = new FileInfo(asset);
            var cleanedUrl = uploadUrl.Replace("{?name,label}", "") + $"?name={fileInfo.Name}&label={fileInfo.Name}";
            var mediaType = FindMediaType(fileInfo.Extension);

            using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                content.Headers.ContentLength = fileInfo.Length;
                content.Add(new StreamContent(new MemoryStream(File.ReadAllBytes(asset))), fileInfo.Name, fileInfo.Name);
                using (var message = await client.PostAsync(cleanedUrl, content)) {
                    var input = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UploadAssetReponse>(input);
                }
            }
        }

        public static async Task<CreateReleaseResponse> PublishRelaseAsync(CommandLineOptions options) {
            using (var client = new HttpClient()) {
                var token = options.Token;
                client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                var createResponse = await CreateReleaseAsync(client, new CreateReleaseOptions {
                    Repo = options.Repo,
                    Owner = options.Owner,
                    Prerelease = options.Prerelease,
                    Draft = options.Draft,
                    Body = options.Body,
                    Tag = options.Tag,
                    Name = options.Name
                });
                foreach (var item in options.Assets) {
                    await UploadAssetsAsync(client, createResponse.UploadUrl, item);
                }
                return (createResponse);
            }
        }
        private static async Task<CreateReleaseResponse> CreateReleaseAsync(HttpClient client, CreateReleaseOptions options) {
            var owner = options.Owner;
            var repo = options.Repo;
            var url = $"{ApiRoot}/repos/{owner}/{repo}/releases";
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
            var responseBody = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == HttpStatusCode.Created) {
                return JsonConvert.DeserializeObject<CreateReleaseResponse>(responseBody);
            }
            throw new HttpRequestException(result.StatusCode.ToString());
        }
    }
}