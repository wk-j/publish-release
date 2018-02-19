using System;
using System.Threading.Tasks;

namespace PublishRelease.Console {
    class Program {
        static async Task Main(string[] args) {
            await Release.PublishRelaseAsync(new CommandLineOptions {
                Token = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN"),
                Repo = "temporary",
                Owner = "wk-j",
                Name = "Hello",
                Tag = "0.14.0",
                Body = ":tada: Hello, world",
                Assets = new[] {
                    "/Users/wk/Source/PublishRelease/README.md"
                }
            });
        }
    }
}
