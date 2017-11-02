using System;
using System.Collections.Generic;
using System.Linq;
using DbConnection;

namespace _07_mysql_crud
{
    class Program
    {
        static string GetFavNum()
        {
            string fav_num;
            System.Console.Write("What is your favorite number? : ");
            fav_num = Console.ReadLine();
            return fav_num;
        }

        static void Main(string[] args)
        {
            // gather user inputs
            string first_name = "";
            string last_name = "";
            string fav_num = "";
            int int_num = 0;

            System.Console.Write("Please enter your first name : ");
            first_name = Console.ReadLine();
            System.Console.Write("Now enter your last name : ");
            last_name = Console.ReadLine();
            fav_num = GetFavNum();

            // check and convert fav_num to an integer
            bool testIntVal = true;
            while (testIntVal)
            {
                try
                {
                    int_num = Convert.ToInt32(fav_num);
                    testIntVal = false;
                }
                catch
                {
                    fav_num = GetFavNum();
                }
            }

            // insert user information into MySQL database
            string query = $"INSERT INTO users (first_name, last_name, fav_num) VALUES ('{first_name}', '{last_name}', {int_num})";
            DbConnector.Execute(query);

            // read all users and write in console
            query = "SELECT * from users";
            List<Dictionary<string, object>> Users = DbConnector.Query(query);
            foreach (Dictionary<string, object> user in Users)
            {
                Console.WriteLine("ID #: {0};  First Name : {1};  Last Name : {2};  Favorite # : {3}", user["id"], user["first_name"], user["last_name"], user["fav_num"]);
            }

        }
    }
}
