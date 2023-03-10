using System.Threading;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.Managers;

public abstract class AbstractRunnableService : IRunnableService
{
    private Thread _thread;
    private bool _isRunning;
    private bool _isStopping;

    public abstract void DoWork();
    public abstract void OnStopping();

    public void Start()
    {
        if (!IsRunning)
        {
            _isRunning = true;

            ThreadStart ts = InternalDoWork;
            _thread = new Thread(ts);
            _thread.Start();
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            _isStopping = true;
            _isRunning = false;
        }
    }

    public void InternalDoWork()
    {
        while (_isRunning)
        {
            DoWork();

            Thread.Sleep(100);
        }

        OnStopping();

        _isStopping = false;
    }

    public bool IsRunning => _isRunning;
    public bool IsStopping => _isStopping;
}
