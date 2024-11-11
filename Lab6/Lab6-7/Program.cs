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
                elementData = new T[elementCount * 2];
                for (int i = 0; i < elementCount; i++)
                    elementData[i] = vec.elementData[i];
            }
        elementData[elementCount] = e;
        elementCount++;
    }
    public void AddAll(T[] a) { foreach (T x in a) Add(x); }

    public void Clear() { elementData = new T[elementData.Length]; elementCount = 0; } //maybe change to manual annulation

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

    public T LastElement()
    {
        return elementData[elementCount - 1];
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
        Console.Write($"elementCount = {elementCount}, capacityIncrement = {capacityIncrement}\n");
    }
}