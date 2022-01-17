

class Product : IObserver<float>
{
    private IDisposable unsubscribers;
    string Name { get; }
    int Cost { get; } // in 100-th of said currency
    string CurrencyTag { get; } 
    
    public Product(string name, int cost, string currencyTag)
        => (Name, Cost, CurrencyTag) = (name, cost, currencyTag);
    
    public virtual Product SubscribeTo(IObservable<float> provider)
    {
        if (provider is not null)
            unsubscribers = provider.Subscribe(this);
        return this;
    }
    public virtual void Unsubscribe()
        => unsubscribers.Dispose();
    
    public void OnCompleted()
        => Unsubscribe();
    public void OnError(Exception error)
        => throw new Exception();
    public void OnNext(float newValue)
    {
        Console.WriteLine($"{Name} costs {Cost/100f:F2}{CurrencyTag} and {GetPLN(Cost, newValue)/100f:F2}PLN");
    }
    
    int GetPLN(int cost, float exchangeRate)
        => (int)(cost * exchangeRate);

    public override string ToString()
        => $"{Name}: {Cost/100f:F2}{CurrencyTag}";
}