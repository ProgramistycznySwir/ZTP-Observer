// See https://aka.ms/new-console-template for more information
Observable<float> exchangeRate_EUR = new Observable<float>(5f); // PLN/EUR
Observable<float> exchangeRate_USD = new Observable<float>(4f); // PLN/USD

Product wine = new Product("Wine", 1000, "EUR").SubscribeTo(exchangeRate_EUR);
Product cheese = new Product("Cheese", 3000, "EUR").SubscribeTo(exchangeRate_EUR);
Product potatoes = new Product("Potatoes", 200, "USD").SubscribeTo(exchangeRate_USD);

void ListProducts(params Product[] products) => Console.WriteLine(string.Join(
            "\n",
            products.Select(product => product.ToString())));


while(true)
{
    Console.Write("\nChoose currency [EUR, USD] (or hit enter to list prices):");
    string chosenCurrency = Console.ReadLine();
    if(chosenCurrency is "")
    {
        ListProducts(wine, cheese, potatoes);
        continue;
    }
    Console.Write("\nChoose new exchange rate:");
    float newExchangeRate = Convert.ToSingle(Console.ReadLine());
    
    ((Observable<float>)(chosenCurrency switch {
        "EUR" => exchangeRate_EUR,
        "USD" => exchangeRate_USD,
        _ => throw new ArgumentException()
    })).Value = newExchangeRate;
}
