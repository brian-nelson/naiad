namespace Naiad.Libraries.System.Interfaces;

public interface IConverterFactory
{
    public IDataTableConverter GetConverter(string mimeType);
}