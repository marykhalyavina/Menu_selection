using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace database
{
    public static class helper
    {
        public static int iduser = -1;
        public static string role = "unreg";
        public static string login = "";
        public static bool usermenu = false;
        public static bool dailycalories = false;
        public static double calories=0.0;
        public static int idmenu = -1;
        public static int mymenu = -1;
        public static int iddish = -1;
        public static int menucalories = 0;
        public static int menuf = 0;
        public static int menuc = 0;
        public static int menup = 0;
        private static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - birthDate.Year;
            if (birthDate.AddYears(age) > today)
            {
                age--;
            }
            return age;
        }
        public static double Getcalories(string sex, DateTime date, double weight, double height, double act, double goal)
        {
            if (sex=="female")//женщина
            {
                return ((10 * weight) + (6.25 * height) - (5 * CalculateAge(date))) * act *goal;
            }
            else
            {
                return (5 + (10 * weight) + (6.25 * height) - (5 * CalculateAge(date))) * act * goal;
            }
        }
    }
}