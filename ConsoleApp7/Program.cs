using System;
using System.Text;

Console.WriteLine("List of assets: ");
Console.WriteLine();

List<Asset>assetList = new List <Asset> ();

//assetList.Add(new Laptop("Dell", "XPS 13", "USA", new DateTime(2021, 01, 15), 200m));
//assetList.Add(new Laptop("Asus", "ZenBook", "USA", new DateTime(2020, 01, 15), 300m));
//assetList.Add(new Laptop("Lenovo", "ThinkPad", "Sweden", new DateTime(2022, 01, 15), 350m));
//assetList.Add(new Laptop("HP", "Elitebook", "Sweden", new DateTime(2021, 03, 15), 950m));

//assetList.Add(new MobilePhone("Samsung", "Galaxy S21", "Spain", new DateTime(2021, 12, 10), 150m));
//assetList.Add(new MobilePhone("Apple", "iPhone 13", "Spain", new DateTime(2020, 12, 10), 250m));
//assetList.Add(new MobilePhone("Samsung", "Galaxy S20", "Spain", new DateTime(2022, 02, 10), 100m));

assetList.Add(new Laptop("Dell", "XPS 13", "USA", new DateTime(2021, 01, 15), 200m, new Office("USA", "$")));
assetList.Add(new Laptop("Asus", "ZenBook", "USA", new DateTime(2020, 01, 15), 300m, new Office("USA", "$")));
assetList.Add(new Laptop("Lenovo", "ThinkPad", "Sweden", new DateTime(2022, 01, 15), 350m, new Office("Sweden", "SEK")));
assetList.Add(new Laptop("HP", "Elitebook", "Sweden", new DateTime(2021, 03, 15), 950m, new Office("Sweden", "SEK")));

assetList.Add(new MobilePhone("Samsung", "Galaxy S21", "Spain", new DateTime(2021, 12, 10), 150m, new Office("Spain", "€")));
assetList.Add(new MobilePhone("Apple", "iPhone 13", "Spain", new DateTime(2020, 12, 10), 250m, new Office("Spain", "€")));
assetList.Add(new MobilePhone("Samsung", "Galaxy S20", "Spain", new DateTime(2022, 02, 10), 100m, new Office("Spain", "€")));



Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Asset Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Currency".PadRight(15) + "Price".PadRight(15) + "Price USD");
Console.ResetColor();



// sort by asset type
assetList.Sort((a1, a2) => a1.AssetType.CompareTo(a2.AssetType));

// sort by purchase date
assetList.Sort((a1, a2) => a1.PurchaseDate.CompareTo(a2.PurchaseDate));


// Exchange rates
decimal usdToEuroRate = 0.85m; // Example: 1 USD = 0.85 EUR
decimal usdToSekRate = 8.83m;  // Example: 1 USD = 8.83 SEK



foreach (var asset in assetList)
{
    //convert price
    decimal priceInLocalCurrency = asset.Price; 
    decimal priceInUsd = asset.Price;
    string localCurrencySymbol = "";
    Console.OutputEncoding = Encoding.UTF8;  // Set the console encoding to UTF-8 to support Unicode characters


    switch (asset.office.Name)
    {
        case "USA":
            localCurrencySymbol = "$";
            break;

        case "Sweden":
            priceInLocalCurrency /= usdToSekRate;
            localCurrencySymbol = "Sek";
            break;

        case "Spain":
            priceInLocalCurrency *= usdToEuroRate;
            localCurrencySymbol = "€";
            break;

    }

    


    //Mark items based on purchased date
    DateTime today = DateTime.Now;

    TimeSpan timeUntillEndOfLife = asset.PurchaseDate.AddYears(3) - today;
    if (timeUntillEndOfLife < TimeSpan.FromDays(90))
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (timeUntillEndOfLife < TimeSpan.FromDays(180))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }


    Console.WriteLine($"{asset.AssetType.PadRight(15)}{asset.Brand.PadRight(15)}{asset.Model.PadRight(15)}{asset.office.Name.PadRight(15)}{asset.PurchaseDate.ToShortDateString().PadRight(20)}{localCurrencySymbol.PadRight(10)} {priceInLocalCurrency:C}".PadRight(10) + $"{priceInUsd:C}".PadLeft(15));


    Console.ResetColor();


}



class Office
{
    public Office(string name, string currency)
    {
        Name = name;
        Currency = currency;
    }

    public string Name { get; set; }
    public string Currency { get; set; }
}

class Asset
{
    public Asset(string assetType, string brand, string model, DateTime purchaseDate, decimal price, Office office)
    {
        AssetType = assetType;
        Brand = brand;
        Model = model;
        PurchaseDate = purchaseDate;
        Price = price;
        this.office = office;
    }

    public string AssetType {  get; set; }  
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public Office office {  get; set; }
}

class Laptop : Asset
{
    public Laptop(string assetType, string brand, string model, DateTime purchaseDate, decimal price, Office office)
        : base(assetType, brand, model, purchaseDate, price, office)
    {
    }
}

class MobilePhone : Asset
{
    public MobilePhone(string assetType, string brand, string model, DateTime purchaseDate, decimal price, Office office)
        : base(assetType, brand, model, purchaseDate, price, office)
    {
    }
}
