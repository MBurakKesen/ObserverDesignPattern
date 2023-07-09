using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverDesignPattern
{
    public class Program
    {
        static public void Main(string[] args)
        {
            Amazon amazon = new Amazon();

            amazon.Register(new BurakClient(), new Product { FullName="Iphone" ,Date=DateTime.Now});
            amazon.NotifyForProduct(new Product { FullName = "Iphone" });

        }
    }
    interface IObserver
    {
        void Notify(Product product);

    }

    class Product
    {
        public string FullName { get; set; }
        public DateTime Date { get; set; }

    }

    class Amazon
    {
        Dictionary<IObserver, Product> database = new Dictionary<IObserver, Product>();

        public void Register(IObserver observer, Product product)
        {
            database.Add(observer, product);
        }
        public void Unregister(IObserver observer)
        {
            database.Remove(observer);
        }

        public void NotifyForProduct(Product product)
        {
            foreach (var kv in database)
            {
                if (kv.Value.FullName == product.FullName)
                {
                    kv.Key.Notify(product);
                }
            }
        }

        public void NotifyAll(Product product)
        {
            foreach (var kv in database)
            {
                kv.Key.Notify(product);
            }
        }
    }

    class BurakClient : IObserver
    {
        public void Notify(Product product)
        {
            Console.WriteLine($"Ürününüz {product.FullName} stock da {product.Date.Hour} tarihinden itibaren mevcut");
        }
    }
}
