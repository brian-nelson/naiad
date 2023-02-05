using Naiad.Libraries.System.Models.System;
using System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserAccessRepo
{
    public UserAccess GetById(Guid userAccessId);
    public UserAccess GetByUserId(Guid userId);
    public void Save(UserAccess userAccess);
}