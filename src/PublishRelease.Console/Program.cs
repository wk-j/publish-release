using System;
using System.Threading.Tasks;
using Terminal.Gui;

namespace PublishRelease.Console {
    class Program {
        static bool Quit() {
            var n = MessageBox.Query(50, 7, "Quit", "Are you sure you want to quit this app?", "Yes", "No");
            return n == 0;
        }

        static async Task Main(string[] args) {

            Application.Init();

            var top = Application.Top;
            var win = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height - 1), "Publish Release");
            top.Add(win);

            var menu = new MenuBar(
                new MenuBarItem[] {
                    new MenuBarItem ("_File", new MenuItem [] {
                        new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
                    })
            });

            win.Add(menu);


            win.Add(
                new Label(3, 2, "Owner"),
                new TextField(14, 2, 40, ""),

                new Label(3, 4, "Repo"),
                new TextField(14, 4, 40, ""),

                new Label(3, 6, "Name"),
                new TextField(14, 6, 40, ""),

                new Label(3, 8, "Body"),
                new TextField(14, 8, 100, ""),


                new Button(3, 19, "OK") {
                    Clicked = () => {

                    }
                },

                new Button(10, 19, "Cancel") {
                    Clicked = () => {
                        if (Quit()) top.Running = false;
                    }
                }
            );


            Application.Run();

            /* 
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
            */
        }
    }
}
