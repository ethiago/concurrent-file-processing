using System;
using Microsoft.Extensions.CommandLineUtils;

namespace FileCli.Commons
{
    public interface ICliCommnad
    {
        string CommandName { get; }

        void SetupCommand(CommandLineApplication app);
    }
}