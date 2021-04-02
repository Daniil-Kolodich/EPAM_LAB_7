using System;

namespace Delegates {

    // Action <Func<bool>,double,double>
    public delegate void MyDelegate(Func<bool> function, double arg1, double arg2);
    class Program {
        static void Main(string[] args) {
            MyDelegate userDelegate = UserMethod3;
            userDelegate += UserMethod2;
            userDelegate += UserMethod1;
            userDelegate(() => false, 5, 3);
            userDelegate(() => true, 3, 6);
        }

        public static void UserMethod1(Func<bool> function, double arg1, double arg2) {
            string result = function() switch {
                true => $"Sum of args : {arg1 + arg2}",
                _ => $"Mult of args : {arg1 * arg2}",
            };
            Console.WriteLine(result);
        }

        public static void UserMethod2(Func<bool> function, double arg1, double arg2) {
            string result = function() switch {
                false => $"Sub of args : {arg1 - arg2}",
                _ => $"Div of args : {arg1 / arg2}",
            };
            Console.WriteLine(result);
        }
        public static void UserMethod3(Func<bool> function, double arg1, double arg2) {
            Console.WriteLine($"Args are _({arg1},{arg2})_ with bool var set _{function()}_");
        }
    }
}
