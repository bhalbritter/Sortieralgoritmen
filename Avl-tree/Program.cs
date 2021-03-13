using System;

namespace Aufgabe3
{
    class Program
    {
        static void Main(string[] args)
        {
            AvlTree tree = new AvlTree();
            tree.Insert(7);
            tree.Insert(12);
            tree.Insert(15);
            tree.Insert(18);
            tree.Insert(23);
            tree.Insert(27);
            tree.Insert(34);

            tree.ausgeben();

            Console.WriteLine("Jetzt wird gelöscht!!");
            tree.delete(7);
            tree.ausgeben();
            tree.delete(12);
            tree.ausgeben();
            tree.delete(15);
            tree.ausgeben();
            tree.delete(18);
            tree.ausgeben();
            tree.delete(23);
            tree.ausgeben();
            tree.delete(27);
            tree.ausgeben();
            tree.delete(34);
            tree.ausgeben();

        }
    }
}
