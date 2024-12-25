using System.Diagnostics;

StreamWriter log = new StreamWriter("log.txt");
Console.WriteLine("Введите количество шагов: ");
int n = Convert.ToInt32(Console.ReadLine());
int globalNumberOfReq = 1;

MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>(10);
Random rand = new Random();

Stopwatch stopwatcher = new Stopwatch();
stopwatcher.Start();

for (int i = 1; i <= n; i++)
{
int numbOfReq = rand.Next(1, 11);
for (int j = 0; j < numbOfReq; j++)
{
Request request = new Request(rand.Next(1, 6), globalNumberOfReq++, i);
queue.Add(request);
log.WriteLine($"ADD: request - {request.number}  priority - {request.priority}  step - {request.step}");
}
Remove(log, queue);
}

Console.WriteLine("Last request: ");
while (!queue.IsEmpty())
{
if (queue.Size() == 1)
Console.WriteLine($"request - {queue.Peek().number}  priority - {queue.Peek().priority}  step - {queue.Peek().step}");
Remove(log, queue);
}
stopwatcher.Stop();
log.Close();
Console.WriteLine($"\nExecution time: {stopwatcher.Elapsed}");


static void Remove(StreamWriter stream, MyPriorityQueue<Request> queue)
{
Request? removing = queue.Poll();
if (removing != null) stream.WriteLine($"Remove: request - {removing.number}" +
    $"  priority - {removing.priority}  step - {removing.step}");
}

class Request : IComparable<Request>
{
    public int priority;
    public int number;
    public int step;
    public Request(int priority, int number, int step)
    {
        this.priority = priority;
        this.number = number;
        this.step = step;
    }
    public int CompareTo(Request request)
    {
        return priority - request.priority;
    }
}



class MyPriorityQueue<T> where T : IComparable<T>
{
    private T[] queue;
    private int size;
    public Comparer<T> comparator;
    public MyPriorityQueue()
    {
        queue = new T[11];
        size = 0;
        comparator = Comparer<T>.Default;
    }
    public MyPriorityQueue(T[] a)
    {
        size = a.Length;
        queue = new T[size];
        for (int i = 0; i < size; i++)
            queue[i] = a[i];
        comparator = Comparer<T>.Default;
        Rebuild();
    }
    public MyPriorityQueue(int initialCapacity)
    {
        queue = new T[initialCapacity];
        size = 0;
        comparator = Comparer<T>.Default;
    }
    public MyPriorityQueue(int initialCapacity, Comparer<T> comparator)
    {
        queue = new T[initialCapacity];
        size = 0;
        this.comparator = comparator;
    }
    public MyPriorityQueue(MyPriorityQueue<T> q)
    {
        queue = q.ToArray();
        size = q.Size();
        comparator = q.comparator;
    }
    public void Print()
    {
        for (int i = 0; i < size; i++)
            Console.Write(queue[i] + " ");
        Console.Write('\n');
    }
    private void Rebuild()
    {
        MyHeap<T> myHeap = new MyHeap<T>(queue, size);
        myHeap.comparator = comparator;
        for (int i = 0; i < size; i++)
            queue[i] = myHeap.Get(i);
    }
    public void Add(T element)
    {
        if (size < queue.Length)
        {
            queue[size] = element;
            size++;
            Rebuild();
            return;
        }
        int newSize;
        if (queue.Length < 64)
            newSize = queue.Length + 2;
        else
            newSize = (int)(queue.Length * 1.5);
        T[] newQueue = new T[newSize];
        for (int i = 0; i < size; i++)
            newQueue[i] = queue[i];
        newQueue[size] = element;
        queue = newQueue;
        size++;
        Rebuild();
    }
    public void AddAll(T[] a)
    {
        foreach (T x in a)
            Add(x);
    }
    public void Clear()
    {
        size = 0;
    }
    public bool Contains(object obj)
    {
        for (int i = 0; i < size; i++)
            if (comparator.Compare((T)obj, queue[i]) == 0)
                return true;
        return false;
    }
    public bool ContainsAll(T[] a)
    {
        foreach (T x in a)
        {
            if (Contains(x))
                continue;
            return false;
        }
        return true;
    }
    public bool IsEmpty()
    {
        if (size == 0) return true;
        return false;
    }
    public void Remove(object obj)
    {
        for (int i = 0; i < size; i++)
        {
            if (comparator.Compare((T)obj, queue[i]) == 0)
            {
                for (int j = i; j < size - 1; j++)
                    queue[j] = queue[j + 1];
                size--;
                i--;
            }
        }
        Rebuild();
    }
    public void RemoveAll(T[] a)
    {
        foreach(T x in a)
            Remove(x);
    }
    public void RetainAll(T[] A)
    {
        bool flag;
        for (int i = 0; i < size; i++)
        {
            flag = false;
            for (int j = 0; j < A.Length; j++)
                if (comparator.Compare(A[i], queue[j]) == 0)
                    flag = true;
            if (!flag)
                Remove(A[i]);
        }
        Rebuild();
    }
    public int Size()
    {
        return size;
    }
    public T[] ToArray()
    {
        T[] A = new T[size];
        for (int i = 0; i < size; i++)
            A[i] = queue[i];
        return A;
    }
    public void ToArray(ref T[] A)
    {
        if (A == null)
        {
            A = ToArray();
            return;
        }
        if (A.Length == size)
        {
            for (int i = 0; i < size; i++)
                A[i] = queue[i];
            return;
        }
        A = new T[size];
        for (int i = 0; i < size; i++)
            A[i] = queue[i];
    }
    public T Element()
    {
        if (size == 0)
            throw new ArgumentException("Queue is empty");
        return queue[0];
    }
    public bool Offer(T obj)
    {
        try
        {
            Add(obj);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    public T Peek()
    {
        if (size == 0)
            return default;
        return queue[0];
    }
    public T Poll()
    {
        if (size == 0)
            return default;
        T element = queue[0];
        for (int i = 0; i < size - 1; i++)
            queue[i] = queue[i + 1];
        size--;
        Rebuild();
        return element;
    }
}

class MyHeap<T> where T : IComparable<T>
{
    private T[] elements;
    private int count;
    public Comparer<T> comparator;
    public MyHeap()
    {
        elements = new T[10];
        count = 0;
        comparator = Comparer<T>.Default;
    }
    public MyHeap(T[] A)
    {
        count = A.Length;
        elements = new T[count];
        for (int i = 0; i < count; i++)
            elements[i] = A[i];
        comparator = Comparer<T>.Default;
        Rebuild();
    }
    public MyHeap(T[] A, int size)
    {
        count = size;
        elements = new T[count];
        for (int i = 0; i < count; i++)
            elements[i] = A[i];
        comparator = Comparer<T>.Default;
        Rebuild();
    }
    public void Print()
    {
        for (int i = 0; i < count; i++)
            Console.Write(elements[i] + " ");
        Console.Write('\n');
    }
    private void Rebuild()
    {
        int left;
        int right;
        int parent;
        T temp;
        bool flag;
        for (int i = count - 1; i >= 0; i--)
        {
            parent = i;
            do
            {
                flag = false;
                left = 2 * i + 1;
                right = 2 * i + 2;
                if (left < count &&
                    comparator.Compare
                    (elements[left], elements[parent]) > 0)
                    parent = left;
                if (right < count &&
                    comparator.Compare
                    (elements[right], elements[parent]) > 0)
                    parent = right;
                if (parent != i)
                {
                    temp = elements[parent];
                    elements[parent] = elements[i];
                    elements[i] = temp;
                    i = parent;
                    flag = true;
                }
            }
            while (flag);
        }
    }
    public T GetMaxValue()
    {
        if (count == 0)
            throw new ArgumentOutOfRangeException("Heap is empty");
        return elements[0];
    }
    public T PopMaxValue()
    {
        if (count == 0)
            throw new ArgumentOutOfRangeException("Heap is empty");
        T maxValue = elements[0];
        for (int i = 0; i < count - 1; i++)
            elements[i] = elements[i + 1];
        count--;
        Rebuild();
        return maxValue;
    }
    public void Set(int index, T element)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException("Wrong Index");
        elements[index] = element;
        Rebuild();
    }
    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException("Wrong Index");
        return elements[index];
    }
    public void Add(T e)
    {
        if (count < elements.Length)
        {
            elements[count] = e;
            count++;
            Rebuild();
            return;
        }
        T[] el = new T[2 * elements.Length + 1];
        for (int i = 0; i < count; i++)
            el[i] = elements[i];
        el[count] = e;
        elements = el;
        count++;
        Rebuild();
    }
    public int Size()
    {
        return count;
    }
    public void Merge(MyHeap<T> h)
    {
        T[] el = new T[count + h.Size()];
        for (int i = 0; i < count; i++)
            el[i] = elements[i];
        for (int i = 0; i < h.Size(); i++)
            el[count + i] = h.Get(i);
        elements = el;
        count += h.Size();
        Rebuild();
    }
}