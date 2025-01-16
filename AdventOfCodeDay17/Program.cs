using System.Diagnostics;

namespace AdventofCodeDay17 {
    class Program {
        static long regA;
        static long regB;
        static long regC;
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            string[] programString = rawInput[4].Remove(0,9).Split(",");
            int[] program = new int[programString.Length];
            
            for (int i = 0; i < programString.Length; i++) {
                program[i] = Convert.ToInt32(programString[i]);
            }
            long test = 236555995274861;
            //List<long> output = new List<long>();
            //Recurse(program, test, 0);
            Compute(program, test);
        }

        private static void Recurse(int[] program, string input, int digit) {
            for (int i = 0; i < 8; i++) {
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(input);
                strBuilder[digit] = Convert.ToString(i)[0];
                string newInput = strBuilder.ToString();
                if (Convert.ToInt64(newInput, 8) > 0) {
                    List<long> output = Compute(program, Convert.ToInt64(newInput, 8));
                    if (output[15 - digit] == program[15 - digit] && digit < 15) {
                        Recurse(program, newInput, digit + 1);
                    }
                    if (digit == 15) {
                        bool success = true;
                        for (int j = 0; j < program.Length; j++) {
                            if (output[j] != program[j]) success = false;
                        }
                        if (success) {Console.WriteLine("Result: " + Convert.ToInt64(newInput, 8));}
                    }
                }
            }
        }

        private static List<long> Compute(int[] program, long value) {
            int pointer = 0;
            List<long> output = new List<long>();
            regA = value;
            regB = 0;
            regC = 0;
            bool jump = true;
            pointer = 0;
            while (pointer < program.Length) {
                switch (program[pointer]) {
                    case 0:
                        regA = (long)(regA / Math.Pow(2,Combo(program[pointer + 1])));
                        break;
                    case 1:
                        regB = XOR(regB,program[pointer + 1]);
                        break;
                    case 2:
                        regB = Combo(program[pointer + 1]) % 8;
                        break;
                    case 3:
                        if (regA != 0) {
                            pointer = program[pointer + 1];
                            jump = false;
                        }
                        break;
                    case 4:
                        regB = XOR(regB,regC);
                        break;
                    case 5:
                        long temp = Combo(program[pointer + 1]) % 8;
                        output.Add(temp);
                        break;
                    case 6:
                        regB = (long)(regA / Math.Pow(2,Combo(program[pointer + 1])));
                        break;
                    case 7:
                        regC = (long)(regA / Math.Pow(2,Combo(program[pointer + 1])));
                        break;
                }
                if (jump) pointer += 2;
                jump = true;
            }
            Console.Write(value + ": ");
            for (int i = 0; i < output.Count; i++) {
                Console.Write(output[i] + ",");
            }
            Console.WriteLine();
            return output;
        }
        private static long Combo(long operand) {
            switch (operand) {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return regA;
                case 5:
                    return regB;
                case 6:
                    return regC;
                case 7:
                    return 0;
                default:
                    return 0;
            }
        }

        private static long XOR(long a, long b) {
            string bitA = Convert.ToString(a,2);
            string bitB = Convert.ToString(b,2);
            string bitResult = "";
            while (bitA.Length != bitB.Length) {
                if (bitA.Length < bitB.Length) bitA = "0" + bitA;
                if (bitA.Length > bitB.Length) bitB = "0" + bitB;
            }
            for (int i = 0; i < bitA.Length; i++) {
                if (bitA[i] != bitB[i]) {bitResult += "1";}
                else {bitResult += "0";}
            }
            return Convert.ToInt64(bitResult, 2);
        }
    }
}