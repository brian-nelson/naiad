using System.Data;
using System.IO;

namespace Naiad.Libraries.System.Interfaces
{
    internal interface IDataTableConverter
    {
        public DataTable Convert(Stream sourceFile);
    }
}
