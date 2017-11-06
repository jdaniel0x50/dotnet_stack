using System;
using System.Collections.Generic;

namespace dojodachi
{
    public class Dachi
    {
        public int fullness = 20;
        public int happiness = 20;
        public int energy = 50;
        public int meals = 3;

        private int RandomEarned(int min, int max)
        {
            Random rand = new Random();
            int food_play = rand.Next(min, (max + 1));
            return food_play;
        }

        private bool RandomLike()
        {
            Random rand = new Random();
            int like_or_not = rand.Next(0,4);
            bool Like;
            if (like_or_not <1)
            {
                Like = false;
            }
            else
            {
                Like = true;
            }
            return Like;
        }

        public int Feed()
        {
            if (meals == 0)
            {
                return 0;
            }

            meals -= 1;
            if (!RandomLike())
            {
                return -1;
            }
            int foodAte = RandomEarned(5, 10);
            fullness += foodAte;
            return foodAte;
        }

        public int Play()
        {
            if (energy < 5)
            {
                return 0;
            }

            energy -= 5;
            if (!RandomLike())
            {
                return -1;
            }
            int happinessGained = RandomEarned(5, 10);
            happiness += happinessGained;
            return happinessGained;
        }

        public int Work()
        {
            if (energy < 5)
            {
                return 0;
            }

            energy -= 5;
            int mealsGained = RandomEarned(1, 3);
            meals += mealsGained;
            return mealsGained;
        }

        public int Sleep()
        {
            if (fullness < 5 || happiness < 5)
            {
                return 0;
            }

            fullness -= 5;
            happiness -= 5;
            energy += 15;
            return 15;
        }
    }
}
