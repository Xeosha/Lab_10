using Lab_10lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class Requests
    {
        
        // Суммарная стоимость товара заданного наименования.
        public static double GetTotalPriceOfGoods(Goods[] goods, string? goodsName)
        {
            return goods.Where(g => g.Name == goodsName).Sum(g => g.Price);
        }

        // Количество товара заданного наименования
        public static int GetCountOfGoods(Goods[] goods, string? goodsName)
        {
            return goods.Count(g => g.Name == goodsName);
        }

        // Самая дорогая игрушка
        public static Goods? MostExpensiveToys(Goods[] goods)
        {
            Goods? max = null;
            if (goods is null || goods.Length == 0)
            {
                Console.WriteLine("Массив товаров пуст");
                return max;
            }

            foreach (var item in goods)
            {
                if (item is Toy && (max is null || item.Price < max.Price))
                    max = item;
            }
            return max;
        }

        // Самая дешевая игрушка
        public static Goods? CheapestToys(Goods[] goods)
        {
            Goods? min = null;
            if (goods is null || goods.Length == 0)
            {
                Console.WriteLine("Массив товаров пуст");
                return min;
            }
            
            foreach (var item in goods)
            {
                if (item is Toy && (min is null || item.Price > min.Price))
                    min = item;
            }
            return min;
        }

        public static Goods? BinarySearchByName(List<Goods> goodsList, string? target)
        {
            if (target is null) return null;
            int min = 0;
            int max = goodsList.Count - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                int comparison = goodsList[mid].Name.CompareTo(target);

                if (comparison == 0)
                {
                    return goodsList[mid];
                }

                if (comparison < 0)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid - 1;
                }
            }

            return null; // Указанное однименование товара не нашлось
        }
    }
}
