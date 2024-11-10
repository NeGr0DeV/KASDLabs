using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Reflection;

namespace WindowsFormsApp7
{
	public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeGraph();
        }
        int selectedArrayTypeIndex = -1;
        int selectedGroupIndex = -1;

        readonly static string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        readonly static string pathToTime = directory + @"\time.txt";
        readonly static string pathToResult = directory + @"\result.txt";

        string[] names;
        List<double[]> list;
        List<int[]> data = new List<int[]>();
        List<int[]> result = new List<int[]>();

        readonly System.Drawing.Color[] colors = new System.Drawing.Color[6] { System.Drawing.Color.Red, System.Drawing.Color.Olive, System.Drawing.Color.Purple, System.Drawing.Color.Green, System.Drawing.Color.Blue, System.Drawing.Color.Aqua };

        private void SelectArrayType(object sender, EventArgs e)
        {
            selectedArrayTypeIndex = ArrayTypeCombobox.SelectedIndex;
        }

        private void SelectGroup(object sender, EventArgs e)
        {
            selectedGroupIndex = GroupCombobox.SelectedIndex;
        }

        private double[] SpeedOfSorting(Func<int, int[]> Generate, int length, params Func<int[], int[]>[] SortMethods)
        {
            double[] sum = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(length);
                try
                {
                    int[] sortedArray = null;
                    int index = 0;
                    foreach (Func<int[], int[]> Method in SortMethods)
                    {
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        sortedArray = Method((int[])array.Clone());
                        timer.Stop();
                        sum[index] += timer.ElapsedMilliseconds;
                        index++;
                    }
                    data.Add(array);
                    result.Add(sortedArray);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                };
            }

            return sum;
        }

        private void VisGraph(object sender, EventArgs e)
        {
            var arrayType = new Func<int, int[]>[] { Generate.Random, Generate.RandomSub, Generate.RandomBySwap, Generate.RandomBySwapAndRepeat };

            list = new List<double[]>();

            switch (selectedGroupIndex)
            {
                case 0:
                    names = new string[5] { "сортировка пузырьком", "шейкерная сортировка", "сортировка вставками", "гномья сортировка", "сортировка выбором" };
                    for (int index = 1; index <= 4; index++)
                        list.Add(SpeedOfSorting(arrayType[selectedArrayTypeIndex], (int)Math.Pow(10, index), Sorting.BubbleSort, Sorting.ShakerSort, Sorting.InsertionSort, Sorting.GnomeSort, Sorting.SelectionSort));
                    break;
                case 1:
                    names = new string[3] { "сортировка Шелла", "сортировка деревом", "битонная сортировка" };
                    for (int index = 1; index <= 4; index++)
                        list.Add(SpeedOfSorting(arrayType[selectedArrayTypeIndex], (int)Math.Pow(10, index), Sorting.ShellSort, Sorting.TreeSort, Sorting.BitonicSort));
                    break;
                case 2:
                    names = new string[6] { "сортировка расчёской", "пирамидальная сортировка", "быстрая сортировка", "сортировка слиянием", "сортировка подсчётом", "поразрядная сортировка" };
                    for (int index = 1; index <= 4; index++)
                        list.Add(SpeedOfSorting(arrayType[selectedArrayTypeIndex], (int)Math.Pow(10, index), Sorting.CombSort, Sorting.HeapSort, Sorting.QuickSort, Sorting.MergeSort, Sorting.CountingSort, Sorting.RadixSort));
                    break;
            }

            PaintGraph(list);
        }

        private void PaintGraph(List<double[]> list)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            int countOfSorts = list[0].Length;

            List<PointPairList> arrayOfLists = new List<PointPairList>();
            for (int index = 0; index < countOfSorts; index++)
            {
                arrayOfLists.Add(new PointPairList());
            }

            for (int i = 0; i < list.Count(); i++)
            {
                for (int index = 0; index < countOfSorts; index++)
                {
                    arrayOfLists[index].Add(Math.Pow(10, i + 1), list[i][index]);
                }
            }

            for (int index = 0; index < countOfSorts; index++)
            {
                pane.AddCurve(names[index], arrayOfLists[index], colors[index], SymbolType.None);
            }

            pane.XAxis.Scale.Max = Math.Pow(10, list.Count());

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void Export(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(pathToTime);
            list.ForEach(x => streamWriter.WriteLine(string.Join(" ", x)));
            streamWriter.Close();
            streamWriter = new StreamWriter(pathToResult);
            data.ForEach(x => {
                streamWriter.WriteLine("Array:");
                streamWriter.WriteLine(string.Join(" ", x));
            });
            result.ForEach(x => {
                streamWriter.WriteLine("Result:");
                streamWriter.WriteLine(string.Join(" ", x));
            });

            streamWriter.Close();
        }

        private void InitializeGraph()
        {
            zedGraphControl1.GraphPane.Title.Text = "Зависимость времени сортировки от размера массива";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Размер массива, шт";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Время сортировки, мс";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
    public class Generate
    {
        //Случайные числа по модулю 1000
        public static int[] Random(int length)
        {
            int[] array = new int[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(0, 1000);
            }
            return array;
        }
        public static double[] RandomDouble(int length)
        {
            double[] array = new double[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = (double)random.Next(1, 100) / 100;
            }
            return array;
        }

        //Разбитые на несколько отсортированных подмасивов разного размера
        public static int[] RandomSub(int length)
        {
            Random random = new Random();
            int modul = random.Next(0, length);
            int newLength = random.Next(2, length) % modul;
            if (newLength < 2) newLength = 2;
            int[] array = new int[length];
            int countOfArray = 0;

            int i = 0;
            while (i < length)
            {
                int exp = random.Next(0, 1000);
                int elementBase = 0;
                countOfArray++;

                while (i < length && i < newLength * countOfArray)
                {
                    elementBase++;
                    array[i] = elementBase * exp;
                    i++;
                }
            }

            return array;
        }

        //Изначально отсортированные с некторым количеством перестановок
        public static int[] RandomBySwap(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++) array[i] = i;

            Random random = new Random();
            int countOfSwap = random.Next(0, length / 3);
            for (int i = 0; i < countOfSwap; i++)
            {
                int firstIndex = random.Next(0, array.Length - 1);
                int secondIndex = random.Next(0, array.Length - 1);
                int temp = array[firstIndex];
                array[firstIndex] = array[secondIndex];
                array[secondIndex] = temp;
            }
            return array;
        }

        public static int[] RandomBySwapAndRepeat(int length)
        {
            int[] array = RandomBySwap(length);
            Random random = new Random();
            int indexOfRepeat = random.Next(0, length - 1);
            int countOfRepeat = random.Next(0, length / 3);

            while (countOfRepeat > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[indexOfRepeat])
                {
                    array[randomIndex] = array[indexOfRepeat];
                    countOfRepeat--;
                }

            }
            return array;
        }
    }
    public class TreeNode
{
    public TreeNode(int data)
    {
        Data = data;
    }

    //данные
    public int Data { get; set; }

    //левая ветка дерева
    public TreeNode Left { get; set; }

    //правая ветка дерева
    public TreeNode Right { get; set; }

    //рекурсивное добавление узла в дерево
    public void Insert(TreeNode node)
    {
        if (node.Data < Data)
        {
            if (Left == null)
            {
                Left = node;
            }
            else
            {
                Left.Insert(node);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = node;
            }
            else
            {
                Right.Insert(node);
            }
        }
    }

    //преобразование дерева в отсортированный массив
    public int[] Transform(List<int> elements = null)
    {
        if (elements == null)
        {
            elements = new List<int>();
        }

        Left?.Transform(elements);

        elements.Add(Data);

        Right?.Transform(elements);

        return elements.ToArray();
    }
}
    public class Sorting
    {
        public static int[] BubbleSort(int[] array)
    {
        int varStorage;
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    varStorage = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = varStorage;
                }
            }
        }
        return array;
    }

    public static int[] ShakerSort(int[] array)
    {
        for (var i = 0; i < array.Length / 2; i++)
        {
            var noSwap = true;
            //проход слева направо
            for (var j = i; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j + 1], array[j]) = (array[j], array[j + 1]);
                    noSwap = false;
                }
            }
            //проход справа налево
            for (var j = array.Length - 2 - i; j > i; j--)
            {
                if (array[j - 1] > array[j])
                {
                    (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    noSwap = false;
                }
            }
            //если обменов не было выходим
            if (noSwap) break;
        }
        return array;
    }

    public static int[] CombSort(int[] array)
    {
        int varStorage;
        var arrayLength = array.Length;
        var currentStep = arrayLength - 1;

        while (currentStep > 1)
        {
            for (int i = 0; i + currentStep < array.Length; i++)
            {
                if (array[i] > array[i + currentStep])
                {
                    varStorage = array[i];
                    array[i] = array[i + currentStep];
                    array[i + currentStep] = varStorage;
                }
            }

            currentStep = GetNextStep(currentStep);
        }

        //сортировка пузырьком
        for (var i = 1; i < arrayLength; i++)
        {
            var noSwap = true;
            for (var j = 0; j < arrayLength - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    varStorage = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = varStorage;
                    noSwap = false;
                }
            }

            if (noSwap) break;
        }
        return array;
    }
    static int GetNextStep(int s)
    {
        s = s * 1000 / 1247;
        return s > 1 ? s : 1;
    }

    public static int[] InsertionSort(int[] array)
    {
        for (var i = 1; i < array.Length; i++)
        {
            var key = array[i];
            var j = i;
            while ((j > 1) && (array[j - 1] > key))
            {
                (array[j], array[j - 1]) = (array[j - 1], array[j]);
                j--;
            }
            array[j] = key;
        }
        return array;
    }

    public static int[] ShellSort(int[] array)
    {
        //расстояние между элементами, которые сравниваются
        var d = array.Length / 2;
        while (d >= 1)
        {
            for (var i = d; i < array.Length; i++)
            {
                var j = i;
                while ((j >= d) && (array[j - d] > array[j]))
                {
                    (array[j - d], array[j]) = (array[j], array[j - d]);
                    j -= d;
                }
            }

            d /= 2;
        }
        return array;
    }

   

    public static int[] GnomeSort(int[] array)
    {
        var index = 1;
        var nextIndex = index + 1;

        while (index < array.Length)
        {
            if (array[index - 1] < array[index])
            {
                index = nextIndex;
                nextIndex++;
            }
            else
            {
                (array[index], array[index - 1]) = (array[index - 1], array[index]);
                index--;
                if (index == 0)
                {
                    index = nextIndex;
                    nextIndex++;
                }
            }
        }
        return array;
    }

    public static int[] SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int extremum = array[i];
            int indexOfExtremum = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] > extremum || array[j] < extremum)
                {
                    indexOfExtremum = j;
                    extremum = array[j];
                }
            }

            array[indexOfExtremum] = array[i];
            array[i] = extremum;
        }

        return array;
    }

    public static int[] HeapSort(int[] array)
    {
        int N = array.Length;
        for (int i = N / 2 - 1; i >= 0; i--)
            Heapify(array, N, i);
        for (int i = N - 1; i > 0; i--)
        {
            (array[i], array[0]) = (array[0], array[i]);
            Heapify(array, i, 0);
        }
        return array;
    }
    static void Heapify(int[] array, int N, int i)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;
        if (l < N && array[l] > array[largest])
            largest = l;
        if (r < N && array[r] > array[largest])
            largest = r;
        if (largest != i)
        {
            (array[largest], array[i]) = (array[i], array[largest]);
            Heapify(array, N, largest);
        }
    }

    public static void QuickSort(int[] array, int left, int right)
    {
        while (left < right)
        {
            int pivotIndex = Partition(array, left, right);

            if (pivotIndex - left < right - pivotIndex)
            {
                QuickSort(array, left, pivotIndex - 1);
                left = pivotIndex + 1;
            }
            else
            {
                QuickSort(array, pivotIndex + 1, right);
                right = pivotIndex - 1;
            }
        }
    }
    public static int[] QuickSort(int[] array)
    {
        QuickSort(array, 0, array.Length - 1);
        return array;
    }
    static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                (array[j], array[i]) = (array[i], array[j]);
            }
        }

        (array[right], array[i + 1]) = (array[i + 1], array[right]);
        return i + 1;
    }


    static void Merge(int[] array, int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;
        int[] L = new int[n1];
        int[] R = new int[n2];
        int i, j;
        for (i = 0; i < n1; ++i)
            L[i] = array[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = array[m + 1 + j];
        i = 0;
        j = 0;
        int k = l;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                array[k] = L[i];
                i++;
            }
            else
            {
                array[k] = R[j];
                j++;
            }
            k++;
        }
        while (i < n1)
        {
            array[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            array[k] = R[j];
            j++;
            k++;
        }
    }
    public static void MergeSort(int[] array, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            MergeSort(array, l, m);
            MergeSort(array, m + 1, r);
            Merge(array, l, m, r);
        }
    }
    public static int[] MergeSort(int[] array)
    {
        MergeSort(array, 0, array.Length - 1);
        return array;
    }

    static int FindMaxValue(int[] arr)
    {
        int maxValue = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > maxValue)
            {
                maxValue = arr[i];
            }
        }
        return maxValue;
    }
    static int FindMinValue(int[] arr)
    {
        int minValue = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < minValue)
            {
                minValue = arr[i];
            }
        }
        return minValue;
    }
    public static int[] CountingSort(int[] array)
    {

        int max = FindMaxValue(array);
        int min = FindMinValue(array);

        int range = max - min + 1;
        var count = new int[range];

        for (var i = 0; i < array.Length; i++) count[array[i] - min]++;

        var index = 0;
        for (var i = 0; i < count.Length; i++)
        {
            for (var j = 0; j < count[i]; j++)
            {
                array[index] = i + min;
                index++;
            }
        }
        return array;
    }

    public static int[] RadixSort(int[] array)
    {
        int i, j;
        int[] tempArray = new int[array.Length];
        for (int shift = 31; shift > -1; --shift)
        {
            j = 0;
            for (i = 0; i < array.Length; ++i)
            {
                bool move = (array[i] << shift) >= 0;
                if (shift == 0 ? !move : move)
                    array[i - j] = array[i];
                else
                    tempArray[j++] = array[i];
            }
            Array.Copy(tempArray, 0, array, array.Length - j, j);
        }
        return array;
    }

    public static int[] TreeSort(int[] array)
    {
        var treeNode = new TreeNode(array[0]);
        for (int i = 1; i < array.Length; i++)
        {
            treeNode.Insert(new TreeNode(array[i]));
        }

        return treeNode.Transform();
    }

    static void BitSeqSort(int[] array, int left, int right, bool inv)
    {
        if (right - left <= 1) return;
        int mid = left + (right - left) / 2;

        for (int i = left, j = mid; i < mid && j < right; i++, j++)
        {
            if (inv ^ (array[i] > array[j]))
            {
                (array[j], array[i]) = (array[i], array[j]);
            }
        }

        BitSeqSort(array, left, mid, inv);
        BitSeqSort(array, mid, right, inv);
    }
    static void MakeBitonic(int[] arr, int left, int right)
    {
        if (right - left <= 1) return;
        int mid = left + (right - left) / 2;

        MakeBitonic(arr, left, mid);
        BitSeqSort(arr, left, mid, false);
        MakeBitonic(arr, mid, right);
        BitSeqSort(arr, mid, right, true);
    }
    public static int[] BitonicSort(int[] array)
    {
        int n = 1;
        int inf = array.Max() + 1;
        int length = array.Length;

        while (n < length) n *= 2;

        int[] temp = new int[n];
        Array.Copy(array, temp, length);

        for (int i = length; i < n; i++)
        {
            temp[i] = inf;
        }

        MakeBitonic(temp, 0, n);
        BitSeqSort(temp, 0, n, false);

        Array.Copy(temp, array, length);
        return array;
    }
  }
}
