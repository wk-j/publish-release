namespace PublishRelease {
    public class CommandLineOptions {
        public string Token { set; get; } = "";
        public string Name { set; get; } = "";

        public string Body { set; get; } = ":tada:";

        public string Owner { set; get; } = "";

        public string Repo { set; get; } = "";

        public string Tag { set; get; } = "";

        public bool Draft { set; get; }

        public bool Prerelease { set; get; }

        public string[] Assets { set; get; } = { };
    }
}
