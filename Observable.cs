

class Observable<T> : IObservable<T>
{
    private readonly List<IObserver<T>> observers = new List<IObserver<T>>();
    T observedValue;
    public T Value {
        get => observedValue;
        set {
            observedValue = value;
            foreach(var observer in observers) observer.OnNext(observedValue);
        }
    }
    
    public Observable()
        => this.observedValue = default;
    public Observable(T observedValue)
        => this.observedValue = observedValue;
    
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (! observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber(observers, observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private List<IObserver<T>>_observers;
        private IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }        
    public void EndTransmission()
    {
        foreach (var observer in observers.ToArray())
            if (observers.Contains(observer))
                observer.OnCompleted();

        observers.Clear();
    }
}