using System;
namespace stage0
{
    partial class program
    {
        static void Main(string[] args)
        {
            Welcome6687();
            Welcome6956();
            Console.ReadKey();
        }

        private static void Welcome6687()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void Welcome6956();
    }
}

/*
 Enter your name: rachelli
rachelli, welcome to my first console application

 */