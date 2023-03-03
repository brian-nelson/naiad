using Naiad.Libraries.System.Models.System;
using System;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserAccessRepo : IDataRepository<UserAccess>
{
    public UserAccess GetByUserId(Guid userId);
}