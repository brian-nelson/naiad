using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.ActivityPub.Interfaces;

public interface IWebfingerRepo : IDataRepository<Webfinger>
{
    public Webfinger GetBySubject(string subject);
}