namespace MockExam {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("TESTS FOR TASK 1:");
            TestTask1(1, 1, 1);
            TestTask1(7, 13, 1);
            TestTask1(4, 16, 4);
            TestTask1(6, 9, 3);

            Console.WriteLine("\nTESTS FOR TASK 2:");
            TestTask2();

            Console.WriteLine("\nTESTS FOR TASK 3:");
            TestTask3();
        }

        private static void TestTask1(int a, int b, int expected) {
            Console.WriteLine($"GCD({a}, {b}) == {GCD.GreatestCommonDivisor(a, b)} ({GCD.GreatestCommonDivisor(a, b) == expected})");
        }

        private static void TestTask2() {
            var cities = new List<City>() {
                new City("France", "Nice", 342000),
                new City("Germany", "Berlin", 3645000),
                new City("Austria", "Salzburg", 152000),
                new City("Germany", "Munich", 1472000),
                new City("Austria", "Vienna", 1897000),
                new City("Germany", "Stuttgart", 634000),
                new City("France", "Paris", 2161000),
                new City("Austria", "Innsbruck", 311000),
                new City("Germany", "Cologne", 1086000),
                new City("Austria", "Graz", 283000),     
            };

            cities.Sort();

            foreach (City c in cities) {
                Console.WriteLine(c.ToString());
            }
        }

        private static void TestTask3() {
            var data = new BinarySearchTree();

            Console.WriteLine($"Tree contains 10? {data.Contains(10)} (should be False)");
            data.Insert(10);
            Console.WriteLine($"Tree contains 10? {data.Contains(10)} (should be True)");

            data.Insert(5);
            data.Insert(3);
            data.Insert(7);
            data.Insert(6);
            data.Insert(1);
            data.Insert(16);
            data.Insert(105);
            data.Insert(64);
            data.Insert(11);
            data.Insert(11);

            Console.WriteLine($"Tree contains 105? {data.Contains(105)} (should be True)");
            Console.WriteLine($"Tree contains 104? {data.Contains(104)} (should be False)");

            Console.WriteLine($"Root is 10: {data?.Root?.Value == 10}");
            Console.WriteLine($"Leftmost leaf is 1: {data?.Root?.Left?.Left?.Left?.Value == 1}");
            Console.WriteLine($"Correct path to 11: {data?.Root?.Right?.Left?.Value == 11}");
        }
    }
}
