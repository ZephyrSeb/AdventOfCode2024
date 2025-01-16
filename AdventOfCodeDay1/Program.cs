namespace AdventofCodeDay1 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] unsortedArray = File.ReadAllLines("testFile.txt");
            string[][] sortedArray = new string[unsortedArray.Length][];
            int[] array1 = new int[unsortedArray.Length];
            int[] array2 = new int[unsortedArray.Length];
            int i = 0;
            foreach (string word in unsortedArray) {
                sortedArray[i] = word.Split(' ');
                i++;
            }
            for (i = 0; i < unsortedArray.Length; i++) {
                array1[i] = Convert.ToInt32(sortedArray[i][0]);
                array2[i] = Convert.ToInt32(sortedArray[i][3]);
            }
            array1 = Sort(array1);
            array2 = Sort(array2);
            int distance = 0;
            for (int j = 0; j < unsortedArray.Length; j++) {
                distance += Math.Abs(array1[j] - array2[j]);
            }
            Console.WriteLine("Distance: " + distance);
            int similarity = 0;
            for (i = 0; i < array1.Length; i++) {
                int count = 0;
                for (int j = 0; j < array2.Length; j++) {
                    if (array1[i] == array2[j]) {
                        count += 1;
                    }
                }
                similarity += array1[i] * count;
            }
            Console.WriteLine("Similarity Score: " + similarity);
        }

        private static int[] Sort(int[] array) {
            bool loop = false;
            while (!loop) {
                loop = true;
                for (int i = 0; i < array.Length - 1; i++) {
                    if (array[i] > array[i + 1]) {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        loop = false;
                    }
                }
            }
            return array;
        }
    }
}