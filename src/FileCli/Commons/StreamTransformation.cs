using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileCli.Commons
{
    public class StreamTransformation
    {
        private int _wordLength;

        public StreamTransformation(int wordLength)
        {
            this._wordLength = wordLength;
        }
        public void TransformAsync(Stream source, Stream detination, Func<Chunk, Chunk> clientCall )
        {
            if (source == null)
            {
                throw new System.ArgumentNullException(nameof(source));
            }

            if (detination == null)
            {
                throw new System.ArgumentNullException(nameof(detination));
            }

            if (clientCall == null)
            {
                throw new System.ArgumentNullException(nameof(clientCall));
            }
            
            LazyRead lazyRead = new LazyRead(source, this._wordLength);
            var transformedBytes = this.LazzyTransform(lazyRead, clientCall);
            var buffered = new BufferedEnumerable<Chunk>(transformedBytes, 10);

            var orederedBytes = buffered.Values();

            foreach(var chunk in orederedBytes)
            {
                detination.Write(chunk.Data, 0, chunk.Length);
            }
            
        }

        private IEnumerable<Task<Chunk>> LazzyTransform(LazyRead read, Func<Chunk, Chunk> clientCall)
        {
            foreach(var chunk in read.Read())
            {
                var task = new Task<Chunk>( () => {
                    return clientCall.Invoke(chunk);
                });
                task.Start();
                yield return task;
            }
        }

        public class LazyRead
        {
            private Stream _stream;
            private int _wordLength;
            public LazyRead(Stream stream, int wordLenth)
            {
                this._stream = stream;
                this._wordLength = wordLenth;
            }

            public IEnumerable<Chunk> Read()
            {
                var chunk = ReadChunk(this._stream,  this._wordLength);
                if(chunk.Length > 0)
                {
                    yield return chunk;
                }else
                {
                    yield break;
                }
            }

            private Chunk ReadChunk(Stream stream, int count)
            {
                var buffer = new byte[count];
                int bytesReaded = stream.Read(buffer, 0, count);

                return new Chunk {Data = buffer, Length = bytesReaded };
            }
        }
    }
}