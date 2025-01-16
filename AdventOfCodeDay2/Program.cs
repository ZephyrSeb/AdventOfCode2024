using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace AdventofCodeDay2 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] report = File.ReadAllLines("testFile.txt");
            int safeReports = 0;
            foreach (string word in report) {
                string[] rawLevels = word.Split(' ');
                int[] levels = new int[rawLevels.Length];
                int i = 0;
                foreach (string entry in rawLevels) {
                    levels[i] = Convert.ToInt32(entry);
                    i++;
                }
                if (TestLevels(levels)) {
                    safeReports += 1;
                }
            }
            Console.WriteLine("Safe Reports:" + safeReports);
        }

        private static bool TestLevels(int[] array) {
            bool test = true;
            string type = "";
            if (array[0] < array[1]) {
                type = "ascending";
            }
            if (array[0] > array[1]) {
                type = "descending";
            }
            for (int i = 0; i < array.Length - 1; i++) {
                if (type == "ascending" && array[i] > array[i + 1]) {
                    test = false;
                }
                if (type == "descending" && array[i] < array[i + 1]) {
                    test = false;
                }
                if (array[i] == array[i + 1]) {
                    test = false;
                }
                if (Math.Abs(array[i] - array[i + 1]) > 3) {
                    test = false;
                }
            }
            if (!test) {
                for (int j = 0; j < array.Length; j++) {
                    List<int> subArray = array.ToList();
                    subArray.RemoveAt(j);
                    if (TestSublevels(subArray.ToArray())) {
                        test = true;
                    }
                }
            }
            return test;
        }

        private static bool TestSublevels(int[] array) {
            bool test = true;
            string type = "";
            if (array[0] < array[1]) {
                type = "ascending";
            }
            if (array[0] > array[1]) {
                type = "descending";
            }
            for (int i = 0; i < array.Length - 1; i++) {
                if (type == "ascending" && array[i] > array[i + 1]) {
                    test = false;
                }
                if (type == "descending" && array[i] < array[i + 1]) {
                    test = false;
                }
                if (array[i] == array[i + 1]) {
                    test = false;
                }
                if (Math.Abs(array[i] - array[i + 1]) > 3) {
                    test = false;
                }
            }
            return test;
        }
    }
}