using System;

namespace _04_human_oop
{
    class Program
    {
        static void Main(string[] args)
        {
            Human warrior = new Human("warrior");
            Human ninja = new Human("ninja", 4);
            Console.WriteLine("Warrior health = " + warrior.health + " (strength " + warrior.strength + ")");
            Console.WriteLine("Ninja health = " + ninja.health + " (strength " + ninja.strength + ")");

            ninja.Attack(warrior);
            Console.WriteLine("Ninja attacked warrior");
            Console.WriteLine("Warrior health = " + warrior.health);
            Console.WriteLine("Ninja health = " + ninja.health);
        }
    }
}
