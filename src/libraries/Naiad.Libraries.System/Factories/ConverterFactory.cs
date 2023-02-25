using Naiad.Libraries.System.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Naiad.Libraries.System.Factories
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly List<IDataTableConverter> _converters;

        public ConverterFactory(IEnumerable<IDataTableConverter> converters)
        {
            _converters = converters.ToList();
        }

        public IDataTableConverter GetConverter(string mimeType)
        {
            foreach (var converter in _converters)
            {
                if (converter.SupportedMimeTypes.Contains(mimeType))
                {
                    return converter;
                }
            }

            return null;
        }
    }
}
