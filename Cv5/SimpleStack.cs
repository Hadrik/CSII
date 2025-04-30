namespace Cv5;

public class SimpleStack<T>
{
    private readonly List<T> _data = [];

    public T Top
    {
        get
        {
            lock (_data)
            {
                var idx = this._data.Count - 1;
                if (idx == -1)
                {
                    throw new StackEmptyException();
                }

                return _data[idx];
            }
        }
    }


    public bool IsEmpty => this._data.Count == 0;


    public void Push(T val)
    {
        lock (_data)
        {
            this._data.Add(val);
        }
    }


    public bool TryPop(out T val)
    {
        lock (_data)
        {
            var idx = this._data.Count - 1;
            if (idx == -1)
            {
                val = default;
                return false;
            }

            val = this._data[idx];
            this._data.RemoveAt(idx);
            return true;
        }
    }


    private class StackEmptyException : Exception { }
}