using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace FileCli.Commons
{
    public abstract class AbstractFileTransformationService
    {
        public int Run(string argumentFilePah, int bufferSize)
        {
            string filePath = argumentFilePah != null ? argumentFilePah.Trim() : string.Empty;
            if( ! filePath.Equals(string.Empty) && ! File.Exists(filePath) )
            {
                Console.Error.WriteLine("Unable to find the file");
            }

            try{
                using(Stream input = File.Exists(filePath)?File.OpenRead(filePath):Console.OpenStandardInput())
                using(Stream output = Console.OpenStandardOutput())
                {
                    StreamTransformation transformation = new StreamTransformation(bufferSize);
                    transformation.TransformAsync(
                        input,
                        Console.OpenStandardOutput(),
                        this.Transformation
                    );
                }
            }catch(Exception ex)
            {
                Console.Error.WriteLine($"A erro occurred: {ex.Message}");
            }

            return 0;
        }

        public abstract Chunk Transformation(Chunk inputChunk);
    }



}