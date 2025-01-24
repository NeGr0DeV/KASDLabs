using LinkedList;
class MyHashMap<K, V>
{
    private MyLinkedList<Pair<K, V>>[] table;
    private int size;
    private double loadFactor;
    public MyHashMap()
    {
        table = new MyLinkedList<Pair<K, V>>[16];
        for (int i = 0; i < 16; i++)
            table[i] = new MyLinkedList<Pair<K, V>>();
        size = 0;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCapacity)
    {
        if (initialCapacity <= 0)
            throw new ArgumentException("Initial capacity");
        table = new MyLinkedList<Pair<K, V>>[initialCapacity];
        for (int i = 0; i < initialCapacity; i++)
            table[i] = new MyLinkedList<Pair<K, V>>();
        size = 0;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCapacity, double loadFactor)
    {
        if (initialCapacity <= 0)
            throw new ArgumentException("Initial capacity");
        if (loadFactor <= 0 || loadFactor >= 1)
            throw new ArgumentException("Load factor");
        table = new MyLinkedList<Pair<K, V>>[initialCapacity];
        for (int i = 0; i < initialCapacity; i++)
            table[i] = new MyLinkedList<Pair<K, V>>();
        size = 0;
        this.loadFactor = loadFactor;
    }
    private int GetHashIndex(object obj)
    {
        return Math.Abs(obj.GetHashCode()) % table.Length;
    }
    private int GetNewHashIndex(object obj, int module)
    {
        return Math.Abs(obj.GetHashCode()) % module;
    }

    public void Clear()
    {
        for (int i = 0; i < table.Length; i++)
            table[i] = new MyLinkedList<Pair<K, V>>();
        size = 0;
    }
    public bool ContainsKey(object key)
    {
        int index = GetHashIndex(key);
        if (table[index].Size() == 0)
            return false;
        Node<Pair<K, V>> p = table[index].GetFirstNode();
        while (p != null)
        {
            if (Equals(p.value.key, key))
                return true;
            p = p.next;
        }
        return false;
    }
    public bool ContainsValue(object value)
    {
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i].Size() == 0)
                continue;
            Node<Pair<K, V>> p = table[i].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.value, value))
                    return true;
                p = p.next;
            }
        }
        return false;
    }
    public List<Pair<K, V>> EntrySet()
    {
        List<Pair<K, V>> set = new List<Pair<K, V>>();
        for (int i = 0; i < table.Length; i++)
            for (int j = 0; j < table[i].Size(); j++)
                set.Add(table[i].Get(j));
        return set;
    }
    public V Get(object key)
    {
        int index = GetHashIndex(key);
        if (table[index].Size() == 0)
            return default;
        Node<Pair<K, V>> p = table[index].GetFirstNode();
        while (p != null)
        {
            if (Equals(p.value.key, key))
                return p.value.value;
            p = p.next;
        }
        return default;
    }
    public bool IsEmpty()
    {
        if (size == 0)
            return true;
        return false;
    }
    public List<K> KeySet()
    {
        List<K> set = new List<K>();
        for (int i = 0; i < table.Length; i++)
            for (int j = 0; j < table[i].Size(); j++)
                set.Add(table[i].Get(j).key);
        return set;
    }
    public void Resize()
    {
        MyLinkedList<Pair<K, V>>[] newTable =
            new MyLinkedList<Pair<K, V>>[table.Length * 2];
        for (int i = 0; i < table.Length * 2; i++)
            newTable[i] = new MyLinkedList<Pair<K, V>>();
        int index;
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i].Size() == 0)
                continue;
            index = GetNewHashIndex
                (table[i].GetFirstNode().value.key, newTable.Length);
            newTable[index] = table[i];
            table[i] = new MyLinkedList<Pair<K, V>>();
        }
        table = newTable;
    }
    public void Put(K key, V value)
    {
        if ((double)size / table.Length > loadFactor)
            Resize();
        int index = GetHashIndex(key);
        if (table[index].Size() == 0)
        {
            table[index] = new MyLinkedList<Pair<K, V>>();
            Pair<K, V> pair = new Pair<K, V>(key, value);
            table[index].Add(pair);
            size++;
            return;
        }
        Node<Pair<K, V>> p = table[index].GetFirstNode();
        while (p != null)
        {
            if (Equals(p.value.key, key))
            {
                p.value.value = value;
                size++;
                return;
            }
            p = p.next;
        }
        Pair<K, V> newPair = new Pair<K, V>(key, value);
        table[index].Add(newPair);
        size++;
    }
    public void Remove(object key)
    {
        int index = GetHashIndex(key);
        if (table[index].Size() == 0)
            return;
        Node<Pair<K, V>> p = table[index].GetFirstNode();
        while (p != null)
        {
            if (Equals(p.value.key, key))
            {
                Pair<K, V> pair =
                    new Pair<K, V>((K)key, p.value.value);
                table[index].Remove(pair);
                size--;
                return;
            }
            p = p.next;
        }
    }
    public int Size()
    {
        return size;
    }
    public void Print()
    {
        for (int i = 0; i < table.Length; i++)
            if (table[i].Size() != 0)
            {
                for (int j = 0; j < table[i].Size(); j++)
                    Console.Write("(" + table[i].Get(j).key + ": " +
                        table[i].Get(j).value + ") ");
                Console.Write("\n");
            }
        Console.WriteLine();
    }
    public void Print(List<Pair<K, V>> EntrySet)
    {
        foreach (Pair<K, V> x in EntrySet)
            Console.WriteLine($"({x.key}: {x.value})");
        Console.WriteLine();
    }
    public void Print(List<K> KeySet)
    {
        foreach (K x in KeySet)
            Console.WriteLine($"({x})");
        Console.WriteLine();
    }
}
class Pair<K, V>
{
    public K key;
    public V value;
    public Pair(K key, V value)
    {
        this.key = key;
        this.value = value;
    }
    public override string ToString()
    {
        return "(" + key.ToString() + "; " +
            value.ToString() + ")";
    }
}
