namespace Final_Work;

public class Counter : IDisposable
{
    private int _value;
    private bool _disposed = false;

    public void Add()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException("Counter");
        }
        _value++;
    }

    public int GetValue()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException("Counter");
        }
        return _value;
    }

    public void Dispose()
    {
        _disposed = true;
    }
}