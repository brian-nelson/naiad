using Naiad.Libraries.System.Models.System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserRepo : IDataRepository<User>
{
    public User GetByEmail(string email);
    public IEnumerable<User> GetByType(UserTypes userType);
}