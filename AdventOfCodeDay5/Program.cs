using System.Data;
using System.Formats.Tar;

namespace AdventofCodeDay5 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] pages = File.ReadAllLines("testInput.txt");
            string[] rules = File.ReadAllLines("rulesInput.txt");
            int result = 0;
            //turns the input into an array of integers
            foreach (string page in pages) {
                string[] splitPage = page.Split(',');
                int[] splitPageInt = new int[splitPage.Length];
                for (int i = 0; i < splitPage.Length; i++) {
                    splitPageInt[i] = Convert.ToInt32(splitPage[i]);
                }
                bool check = true;
                for (int i = 0; i < pages.Length; i++) {
                    for (int j = 0; j < rules.Length; j++) {
                        string[] rule = rules[j].Split("|");
                        if (!IsOrdered(splitPageInt,Convert.ToInt32(rule[0]),Convert.ToInt32(rule[1]))) {
                            check = false;
                        }
                    }
                }
                if (!check) {
                    splitPageInt = Order(splitPageInt, rules);
                    int half = Convert.ToInt32(Math.Floor(Convert.ToDecimal(splitPageInt.Length)/2));
                    foreach (int pageInt in splitPageInt) {
                        Console.Write(pageInt + " ");
                    }
                    Console.WriteLine("");
                    result += splitPageInt[half];
                }
            }
            
            //If the pages are correctly ordered, find the middle element and add it to result
            Console.WriteLine("Result: " + result);
        }

        private static bool IsOrdered(int[] array, int a, int b) {
            int posA = -1;
            int posB = -1;
            for (int i = 0; i < array.Length; i++) {
                if (array[i] == a) {
                    posA = i;
                }
                if (array[i] == b) {
                    posB = i;
                }
            }
            if (posA == -1 || posB == -1) {
                return true;
            }
            else if (posA < posB) {
                return true;
            }
            else {
                return false;
            }
        }

        private static int[] Order(int[] array, string[] rules) {
            bool check = false;
            while (!check) {
                check = true;
                foreach (string rule in rules) {
                    string[] ruleSplit = rule.Split("|");
                    if (!IsOrdered(array, Convert.ToInt32(ruleSplit[0]),Convert.ToInt32(ruleSplit[1]))) {
                        int a = -1;
                        int b = -1;
                        for (int i = 0; i < array.Length; i++) {
                            if (array[i] == Convert.ToInt32(ruleSplit[0])) {
                                a = i;
                            }
                            if (array[i] == Convert.ToInt32(ruleSplit[1])) {
                                b = i;
                            }
                        }
                        if (a > -1 && b > -1) {
                            (array[b], array[a]) = (array[a], array[b]);
                        }
                        check = false;
                    }
                }
            }
            return array;
        }
    }
}