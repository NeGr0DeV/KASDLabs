Console.Write("Введите число n: ");
int n = Convert.ToInt32(Console.ReadLine());

StreamReader input = new StreamReader("input.txt");
MyArrayDeque<string> deque = new MyArrayDeque<string>();
MyArrayDeque<char> digits = new MyArrayDeque<char>();

for (int i = (int)'0'; i <= (int)'9'; i++)
    digits.Push((char)i);
string line;
int firstCount = 0;
if (!input.EndOfStream)
{
    line = input.ReadLine();
    deque.Push(line);
    firstCount = CountDeque(line, digits);
}
int digitsCount;
while (!input.EndOfStream)
{
    line = input.ReadLine();
    digitsCount = CountDeque(line, digits);
    if (digitsCount > firstCount)
        deque.AddLast(line);
    else
    {
        deque.AddFirst(line);
        firstCount = digitsCount;
    }
}
StreamWriter sorted = new StreamWriter("sorted.txt");
string[] D = deque.ToArray();
for (int i = 0; i < D.Length; i++)
    sorted.Write(D[i] + "\n");
for (int i = 0; i < D.Length; i++)
    if (CountChar(D[i], ' ') > n)
        deque.Remove(D[i]);
deque.Print();
input.Close();
sorted.Close();

static int CountDeque(string line, MyArrayDeque<char> alphabet)
{
    int digitsCount = 0;
    for (int i = 0; i < line.Length; i++)
        if (alphabet.Contains(line[i]))
            digitsCount++;
    return digitsCount;
}
static int CountChar(string line, char symbol)
{
    int digitsCount = 0;
    for (int i = 0; i < line.Length; i++)
        if (line[i] == symbol)
            digitsCount++;
    return digitsCount;
}
public class MyArrayDeque<T>
{
    private T[] elements;
    private int head;
    private int tail;

    public MyArrayDeque()
    {
        head = 0;
        tail = -1;
        elements = new T[16];
    }
    public MyArrayDeque(T[] a)
    {
        head = 0;
        tail = -1;
        elements = new T[a.Length];
        foreach(T x in a) 
            elements[++tail] = x;
    }
    public MyArrayDeque(int numElements)
    {
        head = 0;
        tail = numElements - 1;
        elements = new T[numElements];
    }
    public void Add(T e)
    {
        if (tail + 1 < elements.Length)
        {
            tail++;
            elements[tail] = e;
            return;
        }
        if (Size() < elements.Length && head != 0)
        {
            head--;
            for (int i = head; i < tail; i++)
                elements[i] = elements[i + 1];
            elements[tail] = e;
            return;
        }
        T[] newElements = new T[2 * (elements.Length + 1)];
        for (int i = head; i <= tail; i++)
            newElements[i] = elements[i];
        tail++;
        newElements[tail] = e;
        elements = newElements;
    }
    public void AddAll(T[] a)
    {
        foreach (T x in a)
            Add(x);
    }
    public void Clear()
    {
        elements = new T[elements.Length];
        head = 0;
        tail = -1;
    }
    public bool Contains(object o)
    {
        for (int i = head; i <= tail; i++)
            if (Equals(o, elements[i]))
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
    public int Size()
    {
        return tail - head + 1;
    }

    public bool IsEmpty()
    {
        if (tail == -1) return true;
        return false;
    }
    public void Remove(object o)
    {
        for (int i = head; i <= tail; i++)
            if (Equals(o, elements[i]))
            {
                for (int j = i; j < tail; j++)
                    elements[j] = elements[j + 1];
                tail--;
                i--;
                break;
            }
    }
    public void RemoveAll(T[] a)
    {
        foreach(T x in a)
            Remove(x);
    }
    public void RetainAll(T[] a)
    {
        {
            bool flag;
            for (int i = head; i <= tail; i++)
            {
                flag = false;
                for (int j = 0; j < a.Length; j++)
                    if (Equals(elements[i], a[j]))
                        flag = true;
                if (!flag)
                    Remove(a[i]);
            }
        }
    }
    public T[] ToArray()
    {
        T[] A = new T[Size()];
        int index = 0;
        for (int i = head; i <= tail; i++)
        {
            A[index] = elements[i];
            index++;
        }
        return A;
    }
    public void ToArray(ref T[] A)
    {
        if (A == null)
        {
            A = ToArray();
            return;
        }
        int index = 0;
        if (A.Length == Size())
        {
            for (int i = head; i <= tail; i++)
            {
                A[index] = elements[i];
                index++;
            }
            return;
        }
        A = new T[Size()];
        for (int i = head; i <= tail; i++)
        {
            A[index] = elements[i];
            index++;
        }
    }
    public T Element()
    {
        if (Size() == 0)
            throw new Exception("Deque is empty");
        return elements[head];
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
    public T? Peek()
    {
        if (Size() != 0)
            return elements[head];
        return default;        
    }
    public T? Poll()
    {
        if (Size() != 0)
        {
            head++;
            T obj = elements[head-1];
            return obj;
        }
        return default;
    }
    public void AddFirst(T obj)
    {
        if (head - 1 >= 0)
        {
            head--;
            elements[head] = obj;
            return;
        }
        if (Size() < elements.Length)
        {
            tail++;
            for (int i = tail; i > head; i--)
                elements[i] = elements[i - 1];
            elements[head] = obj;
            return;
        }
        T[] newElements = new T[2 * (elements.Length + 1)];
        for (int i = head; i <= tail; i++)
            newElements[i + 1] = elements[i];
        newElements[head] = obj;
        elements = newElements;
    }
    public void AddLast(T obj)
    {
        Add(obj);
    }
    public T GetFirst()
    {
        return elements[head];
    }
    public T GetLast()
    {
        return elements[tail];
    }
    public bool OfferFirst(T obj)
    {
        if (Size() == elements.Length)
            return false;
        AddFirst(obj);
        return true;
    }
    public bool OfferLast(T obj)
    {
        if (Size() == elements.Length)
            return false;
        AddLast(obj);
        return true;
    }
    public T? Pop()
    {
        if (Size() == 0)
            throw new Exception("Deque is empty");
        return Poll();
    }
    public void Push(T element)
    {
        AddFirst(element);
    }
    public T? PeekFirst()
    {
        return Peek();
    }
    public T? PeekLast()
    {
        if (Size() == 0)
            return default;
        return elements[tail];
    }
    public T? PollFirst()
    {
        return Poll();
    }
    public T? PollLast()
    {
        if (Size() == 0)
            return default;
        tail--;
        return elements[tail + 1];
    }
    public T? RemoveFirst()
    {
        return Pop();
    }
    public T? RemoveLast()
    {
        if (Size() == 0)
            throw new Exception("Deque is empty");
        tail--;
        return elements[tail + 1];
    }
    public bool RemoveFirstOccurance(object obj)
    {
        if (Contains(obj))
        {
            Remove(obj);
            return true;
        }
        return false;
    }
    public bool RemoveLastOccurance(object obj)
    {
        if (Contains(obj))
        {
            for (int i = tail; i >= head; i--)
                if (Equals(obj, elements[i]))
                {
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    return true;
                }
            return false;
        }
        return false;
    }
    public void Print()
    {
        for (int i = head; i <= tail; i++)
            Console.Write($"{elements[i]}\n");
        Console.WriteLine($"\nhead - {head}   tail - {tail}");
    }
}