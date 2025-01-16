using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace AdventofCodeDay9 {
    class Program {
        public static void Main() {
            Console.Clear();
            string map = File.ReadAllText("testInput.txt");
            //Part 1
            /*List<string> idMap = new List<string>();
            for (int i = 0; i < map.Length; i++) {
                if (i % 2 == 0 && map[i] > 0) {
                    for (int j = 0; j < Convert.ToInt32(Convert.ToString(map[i])); j++) {
                        idMap.Add(Convert.ToString(i / 2));
                    }
                }
                if (i % 2 == 1 && map[i] > 0) {
                    for (int j = 0; j < Convert.ToInt32(Convert.ToString(map[i])); j++) {
                        idMap.Add(".");
                    }
                }
            }
            
            for (int i = 0; i < idMap.ToArray().Length; i++) {
                Console.Write(idMap[i]);
            }
            Console.WriteLine("");

            for (int i = 0; i < idMap.ToArray().Length; i++) {
                while (idMap[idMap.ToArray().Length - 1] == ".") {
                        idMap.RemoveAt(idMap.ToArray().Length - 1);
                }
                if (i < idMap.ToArray().Length) {
                    if (idMap[i] == ".") {
                        idMap[i] = idMap.Last();
                        idMap.RemoveAt(idMap.ToArray().Length - 1);
                    }
                }
            }*/

            //Part 2
            List<string> fileMap = new List<string>();
            List<int> idMap = new List<int>();

            for (int i = 0; i < map.Length; i++) {
                if (i % 2 == 0) {
                    fileMap.Add(Convert.ToString(i / 2));
                }
                if (i % 2 == 1) {
                    fileMap.Add(".");
                }
                idMap.Add(Convert.ToInt32(Convert.ToString(map[i])));
            }

            for (int i = 0; i < idMap.ToArray().Length; i++) {
                Console.Write(fileMap[i]);
            }
            Console.WriteLine("");

            for (int i = 0; i < idMap.ToArray().Length; i++) {
                Console.Write(idMap[i]);
            }
            Console.WriteLine("");

            for (int i = map.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    if (fileMap[j] == "." && idMap[j] >= idMap[i] && fileMap[i] != ".") {
                        fileMap[j] = fileMap[i];
                        fileMap[i] = ".";
                        int temp = idMap[j] - idMap[i];
                        idMap[j] = idMap[i];
                        if (temp > 0) {
                            fileMap.Insert(j + 1,".");
                            idMap.Insert(j + 1,temp);
                        }
                        break;
                    }
                }
            }

            List<string> finalMap = new List<string>();
            for (int i = 0; i < map.Length; i++) {
                for (int j = 0; j < idMap[i]; j++) {
                    finalMap.Add(fileMap[i]);
                }
            }

            for (int i = 0; i < finalMap.ToArray().Length; i++) {
                Console.Write(finalMap[i]);
            }
            Console.WriteLine("");

            long result = 0;
            for (int i = 0; i < finalMap.ToArray().Length; i++) {
                if (finalMap[i] != ".") {
                    result += Convert.ToInt32(finalMap[i]) * i;
                }
            }
            Console.WriteLine("Result: " + result);
        }
    }
}