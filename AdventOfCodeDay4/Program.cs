namespace AdventofCodeDay3 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] puzzle = File.ReadAllLines("testFile.txt");
            char[,] puzzleChar = new char[puzzle.Length,puzzle[0].Length];
            for (int i = 0; i < puzzle.Length; i++) {
                for (int j = 0; j < puzzle[0].Length; j++) {
                    puzzleChar[i,j] = puzzle[i].ElementAt(j);
                }
            }
            int wordCount = 0;
            for (int i = 1; i < puzzle.Length - 1; i++) {
                for (int j = 1; j < puzzle[0].Length - 1; j++) {
                    int validShape = 0;
                    if (puzzleChar[i,j] == 'A') {
                        if (puzzleChar[i-1,j-1] == 'M' || puzzleChar[i-1,j-1] == 'S') {
                            validShape += 1;
                        }
                        if (puzzleChar[i+1,j-1] == 'M' || puzzleChar[i+1,j-1] == 'S') {
                            validShape += 1;
                        }
                        if (puzzleChar[i-1,j+1] == 'M' || puzzleChar[i-1,j+1] == 'S') {
                            validShape += 1;
                        }
                        if (puzzleChar[i+1,j+1] == 'M' || puzzleChar[i+1,j+1] == 'S') {
                            validShape += 1;
                        }
                        if (validShape == 4 && (puzzleChar[i-1,j-1] != puzzleChar[i+1,j+1]) && (puzzleChar[i-1,j+1] != puzzleChar[i+1,j-1])) {
                            wordCount += 1;
                        }
                    }
                }
            }
            Console.WriteLine("Result: " + wordCount);
        }
    }
}