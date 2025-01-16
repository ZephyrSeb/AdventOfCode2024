using System.Text.RegularExpressions;

namespace AdventofCodeDay3 {
    class Program {
        public static void Main() {
            Console.Clear();
            string rawMemory = File.ReadAllText("testFile.txt");
            string pattern = @"(mul\((0|1|2|3|4|5|6|7|8|9|,)*?\)|do\(\)|don\'t\(\))";
            List<string> memory = new List<string>();
            Regex r = new Regex(pattern);
            Match m = r.Match(rawMemory);
            int matchCount = 0;
            bool test = true;
            while (m.Success) {
                if (m.ToString() == "do()") {
                    test = true;
                }
                else if (m.ToString() == "don't()") {
                    test = false;
                }
                else {
                    if (test) {
                        memory.Add(m.ToString());
                        matchCount++;
                    }
                    //Console.WriteLine(m);
                }
                m = m.NextMatch();
            }
            int result = 0;
            foreach (string equation in memory) {
                string equation1 = equation.TrimStart(new char[] {'m', 'u', 'l', '('});
                equation1 = equation1.TrimEnd(')');
                string[] argumentString = equation1.Split(',');
                int argument1 = Convert.ToInt32(argumentString[0]);
                int argument2 = Convert.ToInt32(argumentString[1]);
                result += argument1 * argument2;
            }
            Console.WriteLine("Result: " + result);
        }
    }
}