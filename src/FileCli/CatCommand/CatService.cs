using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileCli.Commons;
using Microsoft.Extensions.CommandLineUtils;

namespace FileCli.CatCommand
{
    public class CatService : AbstractFileTransformationService, ICatService
    {
        public override Chunk Transformation(Chunk chunk)
        {
            return chunk;
        }
    }
}