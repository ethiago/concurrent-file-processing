using System;
using FileCli.Commons;
using Microsoft.Extensions.CommandLineUtils;

namespace FileCli.CatCommand
{
    public class CatCliCommand : ICliCommnad
    {
        private readonly ICatService _service;
        public CatCliCommand(ICatService service)
        {
            this._service = service;
        }
        public string CommandName => "cat";

        public void SetupCommand(CommandLineApplication command)
        {
            command.Description = "Copy a File to standard output";
            command.HelpOption("-?|-h|--help");

            var fileArgument = command.Argument("[FILE]", "file to be copied" );

            var bufferSizeCommand = command.Option("-n", "buffer length for each batch", CommandOptionType.SingleValue);

            command.OnExecute(() => {
                int bufferSize = 4*1024;

                if(  int.TryParse(bufferSizeCommand.Value(), out int result) )
                {
                    bufferSize = result;
                } 

                return this._service.Run(fileArgument.Value, bufferSize);
            });
        }
    }
}
