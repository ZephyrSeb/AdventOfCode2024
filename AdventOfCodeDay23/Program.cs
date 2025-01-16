using System.IO.Pipelines;

namespace AdventofCodeDay23 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            List<(string,string)> edges = new List<(string, string)>();
            List<string> vertices = new List<string>();
            foreach (string edge in rawInput) {
                string v1 = edge.Split("-")[0];
                string v2 = edge.Split("-")[1];
                if (!edges.Contains((v1,v2)) && !edges.Contains((v2,v1))) edges.Add((v1,v2));
                if (!vertices.Contains(v1)) vertices.Add(v1);
                if (!vertices.Contains(v2)) vertices.Add(v2);
            }
            vertices.Sort();
            List<string> result = BronKerbosch(new List<string>(), vertices, new List<string>(), vertices, edges);
            foreach (string vertex in result) {
                Console.Write(vertex + ",");
            }
        }

        private static List<string> BronKerbosch(List<string> R, List<string> P, List<string> X, List<string> set, List<(string,string)> edges) {
            List<string> privateP = new List<string>(P);
            List<string> privateX = new List<string>(X);
            if (P.Count == 0 && X.Count == 0) {
                if (R.Count > 9) {
                    foreach (string vertex in R) {
                        Console.Write(vertex + ",");
                    }
                    Console.WriteLine();
                }
                return R;
            }
            else {
                List<string> maximalClique = new List<string>();
                while (privateP.Count > 0) {
                    List<string> n = N(privateP[0], set, edges);
                    List<string> newR = new List<string>(R);
                    List<string> newP = Intersection(n,privateP);
                    List<string> newX = Intersection(n,privateX);
                    newR.Add(privateP[0]);
                    List<string> temp = BronKerbosch(newR,newP,newX,set,edges);
                    temp.Distinct();
                    if (temp.Count > maximalClique.Count) maximalClique = temp;
                    privateX.Add(privateP[0]);
                    privateP.Remove(privateP[0]);
                }
                return maximalClique;
            }
        }

        private static List<string> N(string vertex, List<string> set, List<(string,string)> edges) {
            List<string> neighbours = new List<string>();
            foreach(string n in set) {
                if (edges.Contains((vertex,n)) || edges.Contains((n,vertex))) {
                    neighbours.Add(n);
                }
            }
            return neighbours;
        }

        private static List<string> Intersection(List<string> list1, List<string> list2) {
            List<string> output = new List<string>();
            foreach (var v in list1) {
                if (list2.Contains(v)) output.Add(v);
            }
            return output;
        }
    }
}