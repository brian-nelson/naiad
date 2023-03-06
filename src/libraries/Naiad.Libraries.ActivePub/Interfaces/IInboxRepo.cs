using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.ActivityPub.Interfaces;

public interface IInboxRepo : IDataRepository<Post>
{
    
}