using System;

namespace Naiad.Libraries.System.Interfaces;

public interface IDataRepository<T>
{
    public T GetById(Guid id);
    public void Save(T obj);
}