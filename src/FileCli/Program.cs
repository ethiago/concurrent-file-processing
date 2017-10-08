using System;
using System.Linq;
using FileCli.Commons;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace FileCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Bootstraper();

            var app = new CommandLineApplication();
            app.Name = "file-cli";
            app.HelpOption("-?|-h|--help");

             app.OnExecute(() => {
                 Console.WriteLine(app.GetHelpText());
                 return 0;
             });
            

            foreach(ICliCommnad command in container.GetCliComands())
            {
                app.Command( command.CommandName, (cmd) => command.SetupCommand(cmd), throwOnUnexpectedArg: false);
            }

            app.Execute(args);
        }
    }
}
