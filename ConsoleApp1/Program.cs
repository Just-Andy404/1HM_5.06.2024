/*
 
Запрограммируйте класс Money (объект класса оперирует одной
валютой) для работы с деньгами.
В классе должны быть предусмотрены: поле для хранения
целой части денег (доллары, евро, гривны и т.д.) и поле
для хранения копеек (центы, евроценты, копейки и т.д.)
Реализовать методы вывода суммы на экран, задание
значений частей.
На базе класса Money создать класс Product для работы
с продуктом или товаром. Реализовать метод, который позволяет
уменьшить цену на заданное число.
Для каждого из классов реализовать необходимые методы и
поля.

 */

using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        public class Money
        {
            public int Dolars { get; set; }
            public int Cents { get; set; }

            public Money(int dolars, int cents)
            {
                Dolars = dolars;
                Cents = cents;
                Normalize();
            }

            private void Normalize()
            {
                if (Cents >= 100)
                {
                    Dolars += Cents / 100;
                    Cents %= 100;
                }
                else if (Cents < 0)
                {
                    int deficit = (Math.Abs(Cents) + 99) / 100;
                    Dolars -= deficit;
                    Cents += deficit * 100;
                }

                if (Dolars < 0 && Cents > 0)
                {
                    Dolars += 1;
                    Cents -= 100;
                }
            }

            public void Display()
            {
                Console.WriteLine($"{Dolars},{Cents:D2}");
            }

            public void SetValues(int dolars, int cents)
            {
                Dolars = dolars;
                Cents = cents;
                Normalize();
            }
        }

        public class Product
        {
            public string Name { get; set; }
            public Money Price { get; set; }

            public Product(string name, Money price)
            {
                Name = name;
                Price = price;
            }

            public void DecreasePrice(Money amount)
            {
                int totalCents = Price.Dolars * 100 + Price.Cents;
                int decreaseCents = amount.Dolars * 100 + amount.Cents;
                int newTotalCents = totalCents - decreaseCents;

                int newDolars = newTotalCents / 100;
                int newCents = newTotalCents % 100;

                if (newCents < 0)
                {
                    newCents += 100;
                    newDolars -= 1;
                }

                Price.SetValues(newDolars, newCents);
            }

            public void Display()
            {
                Console.WriteLine($"{Name} цена: ");
                Price.Display();
            }
        }

        static void Main(string[] args)
        {
            Money initialPrice = new Money(10, 50);
            Money discount = new Money(2, 75);

            Product product = new Product("Молоко", initialPrice);

            product.Display();

            product.DecreasePrice(discount);

            product.Display();
        }
    }
}
