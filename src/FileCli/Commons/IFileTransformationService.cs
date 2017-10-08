namespace FileCli.Commons
{
    public interface IFileTransformationService
    {
        int Run(string argumentFilePah, int bufferSize);
    }
    
}