using System.Collections.Generic;
using FileCli.CatCommand;
using FileCli.Commons;
using FileCli.ConvertCommand;
using Microsoft.Extensions.DependencyInjection;

namespace FileCli
{
    public class Bootstraper
    {
        private ServiceProvider _serviceProvider;
        public Bootstraper()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<ICatService, CatService>()
                .AddSingleton<IConvertService, ConvertService>()
                .AddSingleton<ICliCommnad, CatCliCommand>()
                .AddSingleton<ICliCommnad, ConvertCliCommand>()
                .BuildServiceProvider();
        }
        public IEnumerable<ICliCommnad> GetCliComands()
        {
            return this._serviceProvider.GetServices<ICliCommnad>();
        }
    }
}