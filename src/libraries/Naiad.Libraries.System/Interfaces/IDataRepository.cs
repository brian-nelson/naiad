using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces;

public interface IDataRepository<T>
{
    public T GetById(Guid id);
    public void Save(T obj);
    public IEnumerable<T> GetAll();
}