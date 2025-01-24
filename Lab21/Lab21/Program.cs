using lab18;
class MyTreeMap<K, V> where K : IComparable<K>
{
    private Comparer<K> comparator;
    private Node<Pair<K, V>> root;
    private int size;
    public MyTreeMap()
    {
        comparator = Comparer<K>.Default;
        root = null;
        size = 0;
    }
    public MyTreeMap(Comparer<K> comparator)
    {
        this.comparator = comparator;
        root = null;
        size = 0;
    }
   
    public void Clear()
    {
        root = null;
        size = 0;
    }
    public bool ContainsKey(object key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare((K)key, p.value.key) == 0)
                return true;
            if (comparator.Compare((K)key, p.value.key) > 0)
                p = p.right;
            else
                p = p.left;
        }
        return false;
    }
    public bool ContainsValue(object value)
    {
        bool flag = false;
        RecContainsValue(value, ref flag, root);
        return flag;
    }
    private void RecContainsValue(object value,
        ref bool flag, Node<Pair<K, V>> p)
    {
        if (p != null)
        {
            if (Equals(value, p.value.value))
            {
                flag = true;
                return;
            }
            RecContainsValue(value, ref flag, p.left);
            RecContainsValue(value, ref flag, p.right);
        }
    }
    public List<Pair<K,V>> EntrySet()
    {
        List<Pair<K, V>> set = new List<Pair<K, V>>();
        RecEntrySet(set, root);
        return set;
    }
    private void RecEntrySet(List<Pair<K,V>> set, Node<Pair<K, V>> p)
    {
        if (p != null)
        {
            RecEntrySet(set, p.left);
            Pair<K, V> pair = new Pair<K, V>(p.value.key, p.value.value);
            set.Add(pair);
            RecEntrySet(set, p.right);
        }
    }
    public V Get(object key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare((K)key, p.value.key) == 0)
                return p.value.value;
            if (comparator.Compare((K)key, p.value.key) > 0)
                p = p.right;
            else
                p = p.left;
        }
        return default;
    }
    public bool IsEmpty()
    {
        return size == 0;
    }
    public List<K> KeySet()
    {
        List<K> set = new List<K>();
        RecKeySet(set, root);
        return set;
    }
    private void RecKeySet(List<K> set,
        Node<Pair<K, V>> p)
    {
        if (p != null)
        {
            RecKeySet(set, p.left);
            set.Add(p.value.key);
            RecKeySet(set, p.right);
        }
    }
    public void Put(K key, V value)
    {
        Pair<K, V> pair = new Pair<K, V>(key, value);
        if (root == null)
        {
            root = new Node<Pair<K, V>>(pair, null, null);
            size++;
            return;
        }
        Node<Pair<K, V>> p = root;
        Node<Pair<K, V>> q;
        do
        {
            q = p;
            if (comparator.Compare(pair.key, p.value.key) == 0)
            {
                p.value = pair;
                return;
            }
            if (comparator.Compare(pair.key, p.value.key) > 0)
                p = p.right;
            else
                p = p.left;
        }
        while (p != null);
        p = new Node<Pair<K, V>>(pair, null, null);
        if (comparator.Compare(pair.key, q.value.key) > 0)
            q.right = p;
        else
            q.left = p;
        size++;
    }
    public void Remove(object key)
    {
        if (comparator.Compare((K)key, root.value.key) == 0)
        {
            if (root.left == null && root.right == null)
            {
                root = null;
                size--;
                return;
            }
            if (root.right == null)
            {
                root = root.left;
                size--;
                return;
            }
            if (root.left == null)
            {
                root = root.right;
                size--;
                return;
            }
            Node<Pair<K, V>> r = root.right;
            while (r.left != null)
                r = r.left;
            r.left = root.left;
            root = root.right;
            size--;
            return;
        }
        Node<Pair<K, V>> p = root;
        Node<Pair<K, V>> q = root;
        bool isRight = false;
        if (comparator.Compare((K)key, p.value.key) > 0)
        {
            p = p.right;
            isRight = true;
        }
        else
        {
            p = p.left;
            isRight = false;
        }
        while (p != null)
        {
            if (comparator.Compare((K)key, p.value.key) == 0)
            {
                if (p.left == null && p.right == null)
                {
                    if (isRight)
                    {
                        q.right = null;
                        size--;
                        return;
                    }
                    q.left = null;
                    size--;
                    return;
                }
                if (p.right == null)
                {
                    if (isRight)
                    {
                        q.right = p.left;
                        size--;
                        return;
                    }
                    q.left = p.left;
                    size--;
                    return;
                }
                if (p.left == null)
                {
                    if (isRight)
                    {
                        q.left = p.right;
                        size--;
                        return;
                    }
                    q.right = p.right;
                    size--;
                    return;
                }
                Node<Pair<K, V>> r = p.right;
                while (r.left != null)
                    r = r.left;
                r.left = p.left;
                if (isRight)
                    q.right = p.right;
                else
                    q.left = p.right;
                size--;
                return;
            }
            if (comparator.Compare((K)key, p.value.key) > 0)
            {
                p = p.right;
                if (isRight)
                    q = q.right;
                else
                    q = q.left;
                isRight = true;
            }
            else
            {
                p = p.left;
                if (isRight)
                    q = q.right;
                else
                    q = q.left;
                isRight = false;
            }
        }
    }
    public int Size()
    {
        return size;
    }
    public MyTreeMap<K, V> HeadMap(K end)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        RecHeadMap(treeMap, end, root);
        return treeMap;
    }
    private void RecHeadMap(MyTreeMap<K, V> treeMap, K end,
        Node<Pair<K, V>> p)
    {
        if (comparator.Compare(p.value.key, end) < 0 &&
            p != null)
        {
            RecHeadMap(treeMap, end, p.left);
            treeMap.Put(p.value.key, p.value.value);
            RecHeadMap(treeMap, end, p.right);
        }
    }
    public MyTreeMap<K, V> SubMap(K start, K end)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        RecSubMap(treeMap, start, end, root);
        return treeMap;
    }
    private void RecSubMap(MyTreeMap<K, V> treeMap, K start, K end,
        Node<Pair<K, V>> p)
    {
        if (comparator.Compare(p.value.key, start) >= 0 &&
            comparator.Compare(p.value.key, end) < 0 &&
            p != null)
        {
            RecSubMap(treeMap, start, end, p.left);
            treeMap.Put(p.value.key, p.value.value);
            RecSubMap(treeMap, start, end, p.right);
        }
    }
    public MyTreeMap<K, V> TailMap(K start, K end)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        RecTailMap(treeMap, start, end, root);
        return treeMap;
    }
    private void RecTailMap(MyTreeMap<K, V> treeMap, K start, K end,
        Node<Pair<K, V>> p)
    {
        if (comparator.Compare(p.value.key, start) >= 0 &&
            p != null)
        {
            RecTailMap(treeMap, start, end, p.left);
            treeMap.Put(p.value.key, p.value.value);
            RecTailMap(treeMap, start, end, p.right);
        }
    }
    public Pair<K, V> LowerEntry(K key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare(p.value.key, key) < 0)
                return p.value;
            p = p.left;
        }
        throw new InvalidOperationException("No such element");
    }
    public Pair<K, V> FloorEntry(K key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare(p.value.key, key) <= 0)
                return p.value;
            p = p.left;
        }
        throw new InvalidOperationException("No such element");
    }
    public Pair<K, V> HigherEntry(K key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare(p.value.key, key) > 0)
                return p.value;
            p = p.right;
        }
        throw new InvalidOperationException("No such element");
    }
    public Pair<K, V> CeilingEntry(K key)
    {
        Node<Pair<K, V>> p = root;
        while (p != null)
        {
            if (comparator.Compare(p.value.key, key) >= 0)
                return p.value;
            p = p.right;
        }
        throw new InvalidOperationException("Element has not found");
    }
    public K LowerKey(K key)
    {
        return LowerEntry(key).key;
    }
    public K FloorKey(K key)
    {
        return FloorEntry(key).key;
    }
    public K HigherKey(K key)
    {
        return HigherEntry(key).key;
    }
    public K CeilingKey(K key)
    {
        return CeilingEntry(key).key;
    }
    public Pair<K, V> PollFirstEntry()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Tree is empty");
        Node<Pair<K, V>> p = root;
        Pair<K, V> pair;
        while (p.left != null)
            p = p.left;
        pair = p.value;
        Remove(pair.key);
        return pair;
    }
    public Pair<K, V> PollLastEntry()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Tree is empty");
        Node<Pair<K, V>> p = root;
        Pair<K, V> pair;
        while (p.right != null)
            p = p.right;
        pair = p.value;
        Remove(pair.key);
        return pair;
    }
    public Pair<K, V> FirstEntry()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Tree is empty");
        Node<Pair<K, V>> p = root;
        while (p.left != null)
            p = p.left;
        return p.value;
    }
    public Pair<K, V> LastEntry()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Tree is empty");
        Node<Pair<K, V>> p = root;
        while (p.right != null)
            p = p.right;
        return p.value;
    }
    public K FirstKey()
    {
        return FirstEntry().key;
    }
    public K LastKey()
    {
        return LastEntry().key;
    }
    public void Print()
    {
        Print(root);
        Console.WriteLine();
    }
    public void Print(Node<Pair<K, V>> p)
    {
        if (p != null)
        {
            Print(p.left);
            Console.Write($"({p.value.key}: {p.value.value}) ");
            Print(p.right);
        }
    }
}
class Node<T>
{
    public T value;
    public Node<T> left;
    public Node<T> right;
    public Node()
    {
        value = default;
        left = null;
        right = null;
    }
    public Node(T value, Node<T> left, Node<T> right)
    {
        this.value = value;
        this.left = left;
        this.right = right;
    }
}
