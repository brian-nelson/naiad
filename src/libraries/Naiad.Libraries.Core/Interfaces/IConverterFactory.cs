namespace Naiad.Libraries.Core.Interfaces;

public interface IConverterFactory
{
    public IDataTableConverter GetConverter(string mimeType);
}