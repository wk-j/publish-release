namespace PublishRelease {
    public class CreateReleaseOptions {
        public string Tag { set; get; }
        public string Name { set; get; }
        public string Body { set; get; }
        public string Repo { set; get; }
        public string Owner { set; get; }

        public bool Draft { set; get; }
        public bool Prerelease { set; get; }
    }
}