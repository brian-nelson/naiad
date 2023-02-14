using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserRepo
{
    public User GetById(Guid userId);
    public User GetByEmail(string email);
    public IEnumerable<User> GetAll();
    public void Save(User user);
    public IEnumerable<User> GetByType(UserTypes userType);
}