using System;
using System.Threading.Tasks;

namespace PublishRelease.Console {
    class Program {
        static async Task Main(string[] args) {
            await Release.CreateRelease(new CommandLineOptions {
                Token = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN"),
                Repo = "temporary",
                Owner = "wk-j",
                Name = "Hello",
                Tag = "0.6.0",
                Body = ":tada: Hello, world"
            });
        }
    }
}
