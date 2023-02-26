using Naiad.Libraries.System.Models.System;
using System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserAccessRepo : IDataRepository<UserAccess>
{
    public UserAccess GetByUserId(Guid userId);
}