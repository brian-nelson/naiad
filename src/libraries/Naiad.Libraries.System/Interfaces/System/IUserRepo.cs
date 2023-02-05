using Naiad.Libraries.System.Models.System;
using System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IUserRepo
{
    public User GetById(Guid userId);
    public User GetByEmail(string email);
    public void Save(User user);
}