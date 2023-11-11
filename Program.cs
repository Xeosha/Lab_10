using Lab_10;
using Lab_10lib;
using InputKeyboard;
using Menu;
using System.ComponentModel.DataAnnotations;

static Goods[] CreateRandomGoodsArray()
{
    int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
    Goods[] products = new Goods[size];
    for (int i = 0; i < size; i++)
    {
        var rnd = new Random();
        int item = rnd.Next(1, 4);
        switch (item)
        {
            case 1:
                products[i] = new Goods(); break;
            case 2:
                products[i] = new Toy(); break;
            case 3:
                products[i] = new Product(); break;
            case 4:
                products[i] = new MilkProduct(); break;
        }
    }
    return products;
}

static Goods GetMenuTypeProducts(int position)
{
    Goods goods = new Goods();
    var dialog = new Dialog($"Выберите тип товара для {position} позиции. Если создали, нажмите ESC. Если не выберите - рандом.");
    dialog.AddOption("Базовый класс (Товар)", () => { goods = new Goods(); goods.Init(); });
    dialog.AddOption("Игрушка", () => { goods = new Toy(); goods.Init(); });
    dialog.AddOption("Продукт", () => { goods = new Product(); goods.Init(); });
    dialog.AddOption("Молочный продукт", () => { goods = new MilkProduct(); goods.Init(); });
    dialog.Start();
    return goods;
}

static Goods[] CreateKeyboardGoodsArray()
{
    int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
    Goods[] products = new Goods[size];
    for (int i = 0; i < size; i++)
    {
        products[i] = GetMenuTypeProducts(i+1);
    }

    return products;
}

static Goods[] CreateArray()
{
    Goods[] goods = Array.Empty<Goods>();

    var dialog = new Dialog("Создание массива товаров");
    dialog.AddOption("Рандомное создание", () => goods = CreateRandomGoodsArray());
    dialog.AddOption("Создание с клавиатуры", () => goods = CreateKeyboardGoodsArray(), true);
    dialog.Start();

    return goods;
}

static void DisplayVirtual(Goods[] products)
{
    if (products is null || products.Length == 0)
    {
        Console.WriteLine("Массив товаров пуст.");
        return;
    }
    int count = 1;
    foreach (var product in products)
    {
        Console.WriteLine(count++);
        Console.WriteLine(product.GetType());
        product.Show();
        Console.WriteLine();
    }
}


static void DisplayNoVirtual(Goods[] products)
{
    if(products is null || products.Length == 0)
    {
        Console.WriteLine("Массив товаров пуст.");
        return;
    }
    int count = 1;
    foreach (Goods product in products)
    {
        Console.WriteLine(count++);
        Console.WriteLine(product.GetType());
        product.Show();
        Console.WriteLine();
    }
}

static void DisplayTotalPrices(Goods[] products)
{
    Console.WriteLine("Ввдите название товара: ");
    var name = Console.ReadLine();
    Console.WriteLine($"Сумма товаров заданного наименования ({name}): " + Requests.GetTotalPriceOfGoods(products, name));
}

static void DisplayCountProducts(Goods[] products)
{
    Console.WriteLine("Ввдите название товара: ");
    var name = Console.ReadLine();
    Console.WriteLine($"Количество товаров заданного наименования ({name}): " + Requests.GetCountOfGoods(products, name));
}

static void DisplayMinAndMaxToysPrices(Goods[] products)
{
    Goods? min = Requests.MostExpensiveToys(products);
    Goods? max = Requests.CheapestToys(products);
    if (min is null || max is null)
        Console.WriteLine("Игрушек в товарах нет.");
    else
        Console.WriteLine($"Название самых дорогих и дешевых игрушек: {max.Name} {min.Name}");
}

static void RequestsMenu(Goods[] products)
{
    var dialog = new Dialog("Запросы");
    dialog.AddOption("Сумма товаров заданного наименования", () => DisplayTotalPrices(products));
    dialog.AddOption("Количество товаров заданного наименования", () => DisplayCountProducts(products));
    dialog.AddOption("Самые дорогие и дешевые игрушки", () => DisplayMinAndMaxToysPrices(products));
    dialog.Start();
}

static void Display(Goods[] products)
{
    var dialog = new Dialog("Вывод элементов массива");
    dialog.AddOption("Вывод товаров (с виртуальным методом)", () => DisplayVirtual(products));
    dialog.AddOption("Вывод товаров (без виртуального метода", () => DisplayNoVirtual(products));
    dialog.Start();
}

var products = Array.Empty<Goods>();

var dialog = new Dialog("10-ая Лабораторная работа");
dialog.AddOption("Создание массива товаров", () => products = CreateArray(), true);
dialog.AddOption("Вывод товаров", () => Display(products), true);
dialog.AddOption("Выполнение запросов", () => RequestsMenu(products), true);

dialog.Start();
