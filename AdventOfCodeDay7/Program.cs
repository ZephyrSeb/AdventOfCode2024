using System.Security.Cryptography;

namespace AdventofCodeDay7 {
    class Program {
        public static void Main() {
            Console.Clear();
            long result = 0;
            string[] rawFile = File.ReadAllLines("testInput.txt");
            for (int i = 0; i < rawFile.Length; i++) {
                string[] equation = rawFile[i].Split(": ");
                string[] temp = equation[1].Split(" ");
                int[] operands = new int[temp.Length];
                for (int j = 0; j < temp.Length; j++) {
                    operands[j] = Convert.ToInt32(temp[j]);
                }
                bool success = CanOperate(Convert.ToInt64(equation[0]), operands);
                if (success) {
                    result += Convert.ToInt64(equation[0]);
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static bool CanOperate(long result, int[] operands) {
            long i;
            bool success = false;
            for (i = 0; i < Math.Pow(3, operands.Length - 1); i++) {
                string binary = ToBase3(i);
                while (binary.Length < operands.Length - 1) {
                    binary = "0" + binary;
                }
                long output = operands[0];
                for (int j = 1; j < operands.Length; j++) {
                    if (binary[binary.Length - j] == '0') {
                        output += operands[j];
                    }
                    else if (binary[binary.Length - j] == '1') {
                        output *= operands[j];
                    }
                    else {
                        output = Convert.ToInt64(output.ToString() + operands[j].ToString());
                    }
                }
                if (output == result) {
                    return true;
                }
            }
            return success;
        }

        private static string ToBase3(long number) {
            string result = "";
            while (number > 0) {
                result = (number % 3).ToString() + result;
                number /= 3;
            }
            return result;
        }
    }
}