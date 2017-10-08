using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCli.Commons
{
    
    public class BufferedEnumerable<T>
    {
        private readonly IEnumerable<Task<T>> Enumerable;

        private bool FimDePapo = false;

        private readonly int BufferSize;

        private Queue<Task<T>> Buffer;

        public BufferedEnumerable(IEnumerable<Task<T>> enumerable, int bufferSize = 10)
        {
            this.Enumerable = enumerable;
            this.BufferSize = bufferSize;
            this.Buffer = new Queue<Task<T>>(this.BufferSize);
        }

        public IEnumerable<T> Values()
        {
            while(true)
            {
                var itensToTake = this.BufferSize - Buffer.Count;
                while(!this.FimDePapo && itensToTake > 0)
                {
                    try{
                        var task = Enumerable.First();
                        Buffer.Enqueue(task);
                        itensToTake--;
                    }catch(InvalidOperationException)
                    {
                        this.FimDePapo = true;
                    } 
                }

                if(Buffer.Count > 0)
                {
                    yield return Buffer.Dequeue().Result;
                }else
                {
                    yield break;
                }
            }
        }
    }
}