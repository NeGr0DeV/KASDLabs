string lines = File.ReadAllText("input.txt");
MyArrayList<string> list = new MyArrayList<string>();
int ind1 = 0, ind2 = 0, f = 0;
for (int i = 0; i < lines.Length; i++)
{
    if (lines[i] == '<') ind1 = i;
    if (lines[i] == '>') { ind2 = i; f = 1; }
    if (ind2 > ind1)
        if (f == 1)
        {
            string l = lines[ind1..(ind2 + 1)];
            l = l.ToLower();
            if (!list.Contains(l))
                list.Add(l);
            f = 0;
        }
}
list.Print();
class MyArrayList<T>
{
    public T[] elementData;
    public int size;

    public MyArrayList()
    {
        elementData = new T[0];
        size = 0;
    }
    public MyArrayList(T[] a)
    {
        foreach (T x in a) size++;
        elementData = new T[size];
        elementData = a;
    }
    public MyArrayList(int capacity)
    {
        elementData = new T[capacity];
        size = 0;
    }
    public void Add(T e)
    {

        if (size >= elementData.Length)
        {
            MyArrayList<T> a = new MyArrayList<T>(elementData);
            elementData = new T[(int)(size * 1.5 + 1)];
            for (int i = 0; i < size; i++)
                elementData[i] = a.elementData[i];
        }
        elementData[size] = e;
        size++;
    }
    public void AddAll(T[] a) { foreach (T x in a) Add(x); }
    public void Clear() { elementData = new T[elementData.Length]; size = 0; }
    public bool Contains(object o)
    {
        foreach (T x in elementData)
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
        if (size <= 0) return true;
        return false;
    }

    public void Remove(object o)
    {
        if (Contains(o))
        {
            if (IndexOf(o) != -1)
            {
                int j = 0;
                MyArrayList<T> arr = new MyArrayList<T>(size - 1);
                for (int i = 0; i < size; i++)
                {
                    if (i != IndexOf(o))
                    { arr.elementData[j] = elementData[i]; j++; }
                    else continue;
                }
                elementData = arr.elementData;
                size--;
            }
        }
    }

    public void RemoveAll(T[] a)
    {
        foreach (T? x in a)
            Remove(x);
    }
    public void RetainAll(T[] a)
    {
        MyArrayList<T> arr = new MyArrayList<T>(a);
        int j = 0; int tmp = size; size = 0;
        for (int i = 0; i < arr.size; i++)
        {
            if (Contains(arr.elementData[i])) { elementData[j] = arr.elementData[i]; j++; size++; }
        }
        for (int i = size; i < tmp; i++) elementData[i] = default(T);
    }
    public int Size()
    {
        return size;
    }

    public T[] ToArray()
    {
        T[] a = new T[size];
        for (int i = 0; i < size; i++) a[i] = elementData[i];
        return a;
    }

    public void ToArray(ref T[] a)
    {
        if (a != null)
        {
            MyArrayList<T> b = new MyArrayList<T>(a);
            b.AddAll(elementData);
            a = new T[b.size];
            a = b.ToArray();
        }
        else
        {
            a = new T[size];
            a = elementData;
        }
    }

    public void Add(int index, T e)
    {
        if (index <= size)
        {
            int j = 0;
            T[] tmp = elementData;
            size++;
            elementData = new T[size];
            for (int i = 0; i < size; i++)
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
    public int IndexOf(object o)
    {
        if (Contains(o))
            for (int i = 0; i < size; i++)
                if (elementData[i].Equals(o)) return i;
        return -1;
    }
    public int LastIndexOf(object o)
    {
        int ind = -1;
        if (Contains(o))
            for (int i = 0; i < size; i++)
                if (elementData[i].Equals(o)) ind = i;
        return ind;
    }

    public T? Remove(int index)
    {

        T t = elementData[index];
        for (int i = index; i < size - 1; i++)
            elementData[i] = elementData[i + 1];
        elementData[size - 1] = default(T);
        size--;
        return t;
    }

    public void Set(int index, T e)
    {
        elementData[index] = e;
    }

    public T?[] SubList(int fromIndex, int toIndex)
    {
        try
        {
            if (toIndex != fromIndex)
            {
                int j = 0;
                T[] b = new T[toIndex - fromIndex];
                for (int i = fromIndex; i < toIndex; i++) { b[j] = elementData[i]; j++; }
                return b;
            }
            else return null;
        }
        catch
        {
            return null;
        }

    }

    public void Print()
    {
        foreach (object? x in elementData)
            Console.WriteLine($"{x}");
        Console.WriteLine();
        Console.Write($"size = {size}\n");
    }

}