namespace Naiad.Libraries.Core.Interfaces;

public interface IRunnableService
{
    public void Start();

    public void Stop();

    public bool IsRunning { get; }

    public bool IsStopping { get; }
}