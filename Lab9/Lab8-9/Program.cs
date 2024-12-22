using static System.Console;
using static System.Math;
using static System.Convert;


string line = ReadLine();
MyStack<string> ops = new MyStack<string>();
MyStack<double> nums = new MyStack<double>();
nums.Print();
ops.Print();

double res = MyStack<double>.CalculateRPN(line);


WriteLine(res);


public class MyStack<T> : MyVector<T>
{
    public MyStack()
        : base() { }

    public void Push(T item)
    {
        base.Add(item);
    }
    public T Pop()
    {
        T x = base.LastElement();
        base.Remove(LastIndexOf(x));
        return x;
    }
    public T Peek()
    {
        return base.LastElement();
    }
    public void Empty()
    {
        base.IsEmpty();
    }
    public int Seek(T item)
    {
        if (base.Contains(item))
            return base.IndexOf(item) + 1;
        return -1;
    }
    public int Size()
    {
        return base.Size();
    }
    private static int GetPriority(string op)
    {
        switch (op)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
            case "%":
            case "div":
                return 2;
            case "^":
                return 3;
            case "sqrt":
            case "abs":
            case "sign":
            case "sin":
            case "cos":
            case "tg":
            case "ln":
            case "lg":
            case "exp":
            case "trunc":
                return 4;
            default: return 0;
        }
    }
    private static double Operation(double num1, double num2, string op)
    {
        switch (op)
        {
            case "+": { return num1 + num2; }
            case "-": { return num1 - num2; }
            case "*": { return num1 * num2; }
            case "/": { return num1 / num2; }
            case "^": { return Pow(num1, num2); }
            case "min": { return Min(num1, num2); }
            case "max": { return Max(num1, num2); }
            case "%": { return ToInt32(num1) % ToInt32(num2); }
            case "div": { return ToInt32(num1) / ToInt32(num2); }
        }
        throw new NotImplementedException();
    }

    private static double Operation(double num, string op)
    {
        switch (op)
        {
            case "sqrt": { return Sqrt(num); }
            case "abs": { return Abs(num); }
            case "sign": { return Sign(num); }
            case "sin": { return Sin(num); }
            case "cos": { return Cos(num); }
            case "tg": { return Tan(num); }
            case "ln": { return Log(num); }
            case "lg": { return Log10(num); }
            case "exp": { return Exp(num); }
            case "trunc": { return Truncate(num); }
        }
        throw new NotImplementedException();
    }

    private static void DoOperation(MyStack<double> nums, MyStack<string> ops)
    {
        if (ops.Size() > 0)
        {
            string op = ops.Pop();
            if (op == "+" || op == "-" || op == "*" || op == "/" || op == "^" || op == "min" || op == "max" || op == "%" || op == "div")
            {
                double num1 = nums.Pop();
                double num2 = nums.Pop();
                nums.Push(Operation(num2, num1, op));
            }
            else
            {
                double num = nums.Pop();
                nums.Push(Operation(num, op));
            }
        }
        else throw new NotImplementedException("stek bez operazii!!!");
    }

    public static double CalculateRPN(string line)
    {
        MyStack<double> nums = new MyStack<double>();
        MyStack<string> ops = new MyStack<string>();
        string[] lines = line.Split(' ');
        for (int i = 0; i < lines.Length; i++)
        {
            if (double.TryParse(lines[i], out var x))
                nums.Push(x);
            switch (lines[i])
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "div":
                case "%":
                case "^":
                case "sqrt":
                case "sin":
                case "cos":
                case "tg":
                case "ln":
                case "log":
                case "min":
                case "max":
                case "exp":
                case "trunc":
                    {
                        while (GetPriority(ops.Peek()) >= GetPriority(lines[i]))
                        {
                            DoOperation(nums, ops);
                        }
                        ops.Push(lines[i]); break;
                    }
                case "(":
                    { ops.Push(lines[i]); break; }
                case ")":
                    {
                        while (ops.Peek() != "(")
                            DoOperation(nums, ops);
                        ops.Pop(); break;
                    }
                default: break;
            }
            Console.WriteLine($"current step:   {lines[i]}");
            nums.Print();
        }
        while (ops.Size() != 0)
        {
            DoOperation(nums, ops);
        }
        return nums.Pop();
    }

    public void Print()
    {
        base.Print();
    }
}
public class MyVector<T>
{
    private T[]? elementData;
    private int elementCount;
    private int capacityIncrement;

    public MyVector(int InitialCapacity, int CapacityIncrement)
    {
        capacityIncrement = CapacityIncrement;
        elementCount = 0;
        elementData = new T[InitialCapacity];
    }
    public MyVector(int InitialCapacity)
    {
        capacityIncrement = 0;
        elementCount = 0;
        elementData = new T[InitialCapacity];
    }
    public MyVector()
    {
        capacityIncrement = 0;
        elementCount = 0;
        elementData = new T[10];
    }
    public MyVector(T[] a)
    {
        foreach (T x in a) elementCount++;
        elementData = new T[elementCount];
        elementData = a;
        capacityIncrement = 0;
    }
    public void Add(T e)
    {
        if (elementCount >= elementData.Length)
        {
            if (capacityIncrement != 0)
            {
                MyVector<T> vec = new MyVector<T>(elementData);
                elementData = new T[elementCount + capacityIncrement];
                for (int i = 0; i < elementCount; i++)
                    elementData[i] = vec.elementData[i];
            }
            else
            {
                MyVector<T> vec = new MyVector<T>(elementData);
                elementData = new T[(elementCount + 1) * 2];
                for (int i = 0; i < elementCount; i++)
                    elementData[i] = vec.elementData[i];
            }
        }
        elementData[elementCount] = e;
        elementCount++;
    }
    public void AddAll(T[] a) { foreach (T x in a) Add(x); }

    public void Clear() { elementData = new T[elementData.Length]; elementCount = 0; }

    public bool Contains(object? o)
    {
        foreach (T x in elementData)
            if (o != null)
                if (o.Equals(x)) return true;
        return false;
    }

    public bool ContainsAll(T[] a)
    {
        foreach (T x in a)
            if (!Contains(x)) return false;
        return true;
    }

    public bool IsEmpty()
    {
        if (elementCount == 0) return true;
        return false;
    }

    public void Remove(object o)
    {
        if (Contains(o))
        {
            if (IndexOf(o) != -1)
            {
                int j = 0;
                MyVector<T> arr = new MyVector<T>(elementCount - 1);
                for (int i = 0; i < elementCount; i++)
                {
                    if (i != IndexOf(o))
                    { arr.elementData[j] = elementData[i]; j++; }
                    else continue;
                }
                elementData = arr.elementData;
                elementCount--;
            }
        }
    }

    public void RemoveAll(T[] a)
    {
        foreach (T x in a) Remove(x);
    }

    public void RetainAll(T[] a)
    {
        MyVector<T> vec = new MyVector<T>(a);
        int j = 0; int tmp = elementCount; elementCount = 0;
        for (int i = 0; i < vec.elementCount; i++)
        {
            if (Contains(vec.elementData[i])) { elementData[j] = vec.elementData[i]; j++; elementCount++; }
        }
        for (int i = elementCount; i < tmp; i++) elementData[i] = default(T);
    }
    public int Size()
    {
        return elementCount;
    }

    public T[] ToArray()
    {
        T[] a = new T[elementCount];
        for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
        return a;
    }

    public void ToArray(ref T[] a)
    {
        if (a != null)
        {
            MyVector<T> b = new MyVector<T>(a);
            b.AddAll(elementData);
            b.Print();
            a = new T[b.elementCount];
            a = b.ToArray();
        }
        else
        {
            a = new T[elementCount];
            a = elementData;
        }
    }

    public void Add(int index, T e)
    {
        if (index <= elementCount)
        {
            int j = 0;
            T[] tmp = elementData;
            elementCount++;
            elementData = new T[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                if (i == index) elementData[i] = e;
                else { elementData[i] = tmp[j]; j++; }
            }
        }
        else { Console.WriteLine("Index is too big"); return; }
    }

    public void AddAll(int index, T[] a)
    {
        foreach (T x in a) { Add(index, x); index++; }
    }

    public T Get(int index)
    {
        return elementData[index];
    }
    public int IndexOf(object? o)
    {
        if (Contains(o))
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals(o)) return i;
        return -1;
    }
    public int LastIndexOf(object? o)
    {
        int ind = -1;
        if (Contains(o))
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals(o)) ind = i;
        return ind;
    }

    public T? Remove(int index)
    {

        T t = elementData[index];
        for (int i = index; i < elementCount - 1; i++)
            elementData[i] = elementData[i + 1];
        elementData[elementCount - 1] = default(T);
        elementCount--;
        return t;
    }

    public void Set(int index, T e)
    {
        elementData[index] = e;
    }

    public T[] SubList(int fromIndex, int toIndex)
    {
        int j = 0;
        T[] b = new T[toIndex - fromIndex];
        for (int i = fromIndex; i < toIndex; i++) { b[j] = elementData[i]; j++; }
        return b;
    }

    public T? FirstElement()
    {
        return elementData[0];
    }

    public T? LastElement()
    {
        if (elementCount != 0)
            return elementData[elementCount - 1];
        else return default(T);
    }

    public void RemoveElementAt(int pos)
    {
        try
        {
            Remove(pos);
        }
        catch { return; }
    }

    public void RemoveRange(int begin, int end)
    {
        for (int i = begin; i <= end; i++) Remove(begin);
    }
    public void Print()
    {
        foreach (T? x in elementData)
            Console.WriteLine($"{x}");
        Console.WriteLine();
        Console.Write($"elementCount = {elementCount}\n"); 
    }
}