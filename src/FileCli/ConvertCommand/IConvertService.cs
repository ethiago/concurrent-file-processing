namespace FileCli.ConvertCommand
{
    public interface IConvertService
    {
        int Run(string fileName, string from, string to, int bufferSize);
    }
}