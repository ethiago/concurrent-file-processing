using System;
using System.Text;
using FileCli.Commons;

namespace FileCli.ConvertCommand
{
    public class ConvertService : AbstractFileTransformationService, IConvertService
    {
        private Encoding _src;
        private Encoding _dest;

        public int Run(string fileName,  string from, string to, int  buffersSize)
        {
            this._src = GetEncodingFromTextName(from);
            this._dest = GetEncodingFromTextName(to);
            
            return base.Run(fileName, buffersSize);
        }

        private Encoding GetEncodingFromTextName(string name)
        {
            switch(name.Trim().ToUpper())
            {
                case "ASCII":
                    return Encoding.ASCII;
                case "UNICODE":
                    return Encoding.Unicode;
                case "UTF8":
                    return Encoding.UTF8;
                case "EBCDIC":
                    return Encoding.GetEncoding(500);
                
                default:
                    throw new ArgumentException($"Encoding {name} not supported");
            }
        }

        public override Chunk Transformation(Chunk inputChunk)
        {
            return new Chunk()
            {
                Data = Encoding.Convert(this._src, this._dest, inputChunk.Data),
                Length = inputChunk.Length,
            };
        }
    }

}