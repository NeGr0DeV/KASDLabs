public class Heap<T> where T : IComparable<T>
{
    private int key;
    private List<T> elements = new List<T>();
    private int count;
    public Heap(T[] a)
    {
        count = 0;
        foreach (T x in a)
            Add(x);
    }

    public Heap()
    {
        count = 0;
        elements = new List<T>();
    }

    public void Add(T item)
    {
        elements.Add(item);
        count++;
        Heapify(elements.Count - 1);
    }
    public void Heapify(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;

            if (elements[parent].CompareTo(elements[index]) >= 0)
                return;
            Swap(parent, index);
            index = parent;
        }
    }

    public T Max()
    {
        return elements[0];
    }

    public T RemoveMax()
    {
        T res = Max();
        elements.RemoveAt(0);
        for (int i = 0; i < elements.Count; i++)
            Heapify(i);
        return res;
    }

    public void Merge(Heap<T> heap)
    {
        foreach (T x in heap.elements)
            Add(x);
    }

    private void Swap(int ind1, int ind2)
    {
        T tmp = elements[ind1];
        elements[ind1] = elements[ind2];
        elements[ind2] = tmp;
    }

    public void KeyIncrease(int ind, T key)
    {
        elements[ind] = key;
        for (int i = 0; i < elements.Count; i++)
            Heapify(i);
    }
    public List<T> GetElements()
    {
        return elements;
    }

    public void Print()
    {
        foreach (T x in elements)
            Console.Write($"{x}   ");
        Console.WriteLine();
    }
}