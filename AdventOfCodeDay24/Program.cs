namespace AdventofCodeDay24 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            string[] logicInput = File.ReadAllLines("logicInput.txt");
            Dictionary<string, bool> gates = new Dictionary<string, bool>();
            foreach (string input in rawInput) {
                gates[input.Split(" ")[0].Remove(input.Split(" ")[0].Length - 1,1)] = ToBool(input.Split(" ")[1]);
            }

            /*for (int i = 0; i < logicInput.Length; i++) {
                if (logicInput[i].Split(" ")[4] == "frt") {
                    logicInput[i] = logicInput[i][0..^3] + "z23";
                }
                else if (logicInput[i].Split(" ")[4] == "z23") {
                    logicInput[i] = logicInput[i][0..^3] + "frt";
                }
                if (logicInput[i].Split(" ")[4] == "sps") {
                    logicInput[i] = logicInput[i][0..^3] + "z11";
                }
                else if (logicInput[i].Split(" ")[4] == "z11") {
                    logicInput[i] = logicInput[i][0..^3] + "sps";
                }
                if (logicInput[i].Split(" ")[4] == "tst") {
                    logicInput[i] = logicInput[i][0..^3] + "z05";
                }
                else if (logicInput[i].Split(" ")[4] == "z05") {
                    logicInput[i] = logicInput[i][0..^3] + "tst";
                }
                if (logicInput[i].Split(" ")[4] == "pmd") {
                    logicInput[i] = logicInput[i][0..^3] + "cgh";
                }
                else if (logicInput[i].Split(" ")[4] == "cgh") {
                    logicInput[i] = logicInput[i][0..^3] + "pmd";
                }
            }*/

            MethodSwap(gates, logicInput, 0, 0);

            //Console.WriteLine("fin");
        }

        private static bool ToBool(string str) {
            if (str == "0") {
                return false;
            }
            if (str == "1") {
                return true;
            }
            return false;
        }

        private static string FromBool(bool arg) {
            if (arg) {
                return "1";
            }
            else return "0";
        }

        private static bool AND(bool a, bool b) {
            if (a && b) {
                return true;
            }
            else return false;
        }

        private static bool OR(bool a, bool b) {
            if (a || b) {
                return true;
            }
            else return false;
        }

        private static bool XOR(bool a, bool b) {
            if ((a || b) && !(a && b)) {
                return true;
            }
            else return false;
        }

        private static int MethodSwap(Dictionary<string, bool> gates, string[] input, int a, int b) {
            Dictionary<string, bool> newGates = new Dictionary<string, bool>(gates);
            bool success = false;
            int iterations = 0;
            while (!success && iterations < 1000) {
                success = true;
                iterations++;
                for (int i = 0; i < input.Length; i++) {
                    string[] logic = input[i].Split(" ");
                    if (i == a) logic[4] = input[b].Split(" ")[4];
                    if (i == b) logic[4] = input[a].Split(" ")[4];
                    if (newGates.ContainsKey(input[i].Split(" ")[0]) && newGates.ContainsKey(logic[2])) {
                        if (logic[1] == "AND") {
                            newGates[logic[4]] = AND(newGates[logic[0]],newGates[logic[2]]);
                        }
                        if (logic[1] == "OR") {
                            newGates[logic[4]] = OR(newGates[logic[0]],newGates[logic[2]]);
                        }
                        if (logic[1] == "XOR") {
                            newGates[logic[4]] = XOR(newGates[logic[0]],newGates[logic[2]]);
                        }
                    }
                    else success = false;
                }
            }
            success = false;
            int count = 0;
            string xresult = "";
            string yresult = "";
            string zresult = "";
            while (!success) {
                string x = count.ToString();
                string y = count.ToString();
                string z = count.ToString();
                if (count < 10) {
                    x = "0" + x;
                    y = "0" + y;
                    z = "0" + z;
                }
                x = "x" + x;
                y = "y" + y;
                z = "z" + z;
                if (newGates.ContainsKey(z)) {
                    zresult = FromBool(newGates[z]) + zresult;
                    count++;
                }
                else success = true;
                if (newGates.ContainsKey(x)) {
                    xresult = FromBool(newGates[x]) + xresult;
                }
                if (newGates.ContainsKey(y)) {
                    yresult = FromBool(newGates[y]) + yresult;
                }
            }
            Console.WriteLine("x: " + Convert.ToInt64(xresult, 2));
            Console.WriteLine("y: " + Convert.ToInt64(yresult, 2));
            Console.WriteLine("z: " + Convert.ToInt64(zresult, 2));
            long difference = Convert.ToInt64(zresult, 2) - Convert.ToInt64(xresult, 2) - Convert.ToInt64(yresult, 2);
            Console.WriteLine(Convert.ToString(Math.Abs(difference), 2));
            return Convert.ToString(Math.Abs(difference), 2).Count(f => f == '1');
            //Console.WriteLine(input[a] + "," + input[b] + ": " + Convert.ToString(difference, 2);
        }
    }
}