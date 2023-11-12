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
    var goods = new Goods();
    var dialog = new Dialog($"Выберите тип товара для {position} позиции. Если создали, нажмите ESC. Если не выберите - рандом.");
    dialog.AddOption("Базовый класс (Товар)", () => { goods = new Goods(); goods.Init(); });
    dialog.AddOption("Игрушка", () => { goods = new Toy(); goods.Init(); });
    dialog.AddOption("Продукт", () => { goods = new Product(); goods.Init(); });
    dialog.AddOption("Молочный продукт", () => { goods = new MilkProduct(); goods.Init(); });
    dialog.Start();

    Console.WriteLine("Созданный товар:\n" + goods);
    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
    Console.ReadKey(true);

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
        product.SelfShow();
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

static void DisplayBinarySearch(Goods[] products)
{
    var goodsList = new List<Goods>(products);

    goodsList.Sort((x, y) => x.Name.CompareTo(y.Name));

    Console.Write("Введите название товара, который ищете: ");
    string? name = Console.ReadLine();

    var result = Requests.BinarySearchByName(goodsList, name);

    if (result is not null)
        Console.WriteLine("Найденный товар:\n" + result);
    else
        Console.WriteLine("Товар не найден.");

}

static void RequestsMenu(Goods[] products)
{
    var dialog = new Dialog("Запросы");
    dialog.AddOption("Сумма товаров заданного наименования", () => DisplayTotalPrices(products));
    dialog.AddOption("Количество товаров заданного наименования", () => DisplayCountProducts(products));
    dialog.AddOption("Самые дорогие и дешевые игрушки", () => DisplayMinAndMaxToysPrices(products));
    dialog.AddOption("Бинарный поиск по названию товара", () => DisplayBinarySearch(products));
    dialog.Start();
}

static void Display(Goods[] products)
{
    var dialog = new Dialog("Вывод элементов массива");
    dialog.AddOption("Вывод товаров (с виртуальным методом)", () => DisplayVirtual(products));
    dialog.AddOption("Вывод товаров (без виртуального метода", () => DisplayNoVirtual(products));
    dialog.Start();
}


static void DisplayIInit()
{
    Console.WriteLine("\tИнтерфейс IInit: ");
    IInit[] objects = new IInit[]
       {
            new Goods(),
            new Product(),
            new MilkProduct(),
            new Toy(),
            new NonHierarchicalClass()
       };

    Console.WriteLine("IInit[5] objects = new IInit[5] - состоит из объектов Goods, Product, MilkProduct, Toy, NonHierarhicalClass");

    int count = 1;
    foreach (var item in objects)
    {
        Console.WriteLine($"Создается объект под номером {count++}: {item.GetType()}");
        item.RandomInit();
        Console.WriteLine(item + "\n");
    }
}

static void DisplaySortIComparable()
{
    Goods[] products = CreateRandomGoodsArray();

    Array.Sort(products);
    Console.WriteLine("Отсортированный массив: ");
    DisplayVirtual(products);
}

static void DisplaySortICompare()
{
    Goods[] products = CreateRandomGoodsArray();

    Array.Sort(products, new SortByPrice());
    Console.WriteLine("Отсортированный массив по цене: ");
    DisplayVirtual(products); 


}

static void DisplayClone()
{
    var originalProduct = new Goods();
    originalProduct.Tags = new List<string> { "1", "2", "3" };
    var clonedProduct = (Goods)originalProduct.Clone();

    Console.WriteLine("До изменения полное копирование:");
    foreach (var item in clonedProduct.Tags)
        Console.Write(item + " ");
    Console.WriteLine();

    originalProduct.Tags.Clear();

    Console.WriteLine("После изменеия полное копирование:");
    foreach (var item in clonedProduct.Tags)
        Console.Write(item + " ");
    Console.WriteLine();

    
    originalProduct = new Goods();
    originalProduct.Tags = new List<string> { "1", "2", "3" };
    var shallowCopyProduct = originalProduct.ShallowCopy();

    Console.WriteLine("До изменения неполное копирование:");
    foreach (var item in shallowCopyProduct.Tags)
        Console.Write(item + " ");
    Console.WriteLine();

    originalProduct.Tags.Clear();

    Console.WriteLine("После изменения неполное копирование:");
    foreach (var item in shallowCopyProduct.Tags)
        Console.Write(item + " ");
    Console.WriteLine();


}

static void Task3()
{
    var dialog = new Dialog("Задание 3");
    dialog.AddOption("Демонстрация интерфейса IInit", DisplayIInit);
    dialog.AddOption("Демонстрация сортировки, используя стандартный интерфейс IComparable", DisplaySortIComparable);
    dialog.AddOption("Демонстация сортировки по цене, используя ICompare", DisplaySortICompare);
    dialog.AddOption("Создание клонов, их демонстрация", DisplayClone);
    dialog.Start();

}


void Main()
{
    var products = Array.Empty<Goods>();

    var dialog = new Dialog("10-ая Лабораторная работа");
    dialog.AddOption("Создание массива товаров", () => products = CreateArray(), true);
    dialog.AddOption("Вывод товаров", () => Display(products), true);
    dialog.AddOption("Выполнение запросов", () => RequestsMenu(products), true);
    dialog.AddOption("Задание 3. Клоны, сортировки и т.д.", Task3, true);

    dialog.Start();
}

Main(); 
