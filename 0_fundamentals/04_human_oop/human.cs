using System;

namespace _04_human_oop
{
    public class Human
    {
        public string name;
        public int strength=3;
        public int intelligence=3;
        public int dexterity=3;
        public int health=100;

        public Human(string _name)
        {
            name = _name;
        }

        public Human(string _name, int _strength)
        {
            name = _name;
            strength = _strength;
        }

        public Human(string _name, int _strength=3, int _intelligence=3, int _dexterity=3, int _health=100)
        {
            name = _name;
            strength = _strength;
            intelligence = _intelligence;
            dexterity = _dexterity;
            health = _health;
        }

        public void Attack(Human otherHuman)
        {
            int attackStrength = strength * 5;
            otherHuman.health -= attackStrength;
        }
    }
}
