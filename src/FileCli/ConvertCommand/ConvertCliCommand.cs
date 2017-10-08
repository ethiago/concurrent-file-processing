using System;
using FileCli.Commons;
using Microsoft.Extensions.CommandLineUtils;

namespace FileCli.ConvertCommand
{
    public class ConvertCliCommand : ICliCommnad
    {
        private readonly IConvertService _service;
        public ConvertCliCommand(IConvertService service)
        {
            this._service = service;
        }
        public string CommandName => "convert";

        public void SetupCommand(CommandLineApplication command)
        {
            command.Description = "Convert a File format to standard output";
            command.HelpOption("-?|-h|--help");

            var fileArgument = command.Argument("[FILE]", "file to be processed", multipleValues: false);

            var fromArgument = command.Argument("[SRC]", "format of original file", multipleValues: false);

            var toArgument = command.Argument("[DEST]", "format of output file", multipleValues: false);

            var bufferSizeOption = command.Option("-n", "buffer length for each batch", CommandOptionType.SingleValue);

            command.OnExecute(() => {

                int bufferSize = 4*1024;

                if(  int.TryParse(bufferSizeOption.Value(), out int result) )
                {
                    bufferSize = result;
                } 
                
                return this._service.Run(fileArgument.Value, fromArgument.Value, toArgument.Value, bufferSize);
            });
        }
    }
}
