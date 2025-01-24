namespace LinkedList
{
    class MyLinkedList<T>
    {
        private Node<T> first;
        private Node<T> last;
        private int size;
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinkedList(T[] A)
        {
            switch (A.Length)
            {
                case 0:
                    {
                        first = null;
                        last = null;
                        size = 0;
                        break;
                    }
                case 1:
                    {
                        Node<T> node = new Node<T>(A[0], null, null);
                        first = node;
                        last = node;
                        size = 1;
                        break;
                    }
                default:
                    {
                        Node<T> head = new Node<T>(A[0], null, null);
                        Node<T> p = head;
                        Node<T> q = new Node<T>();
                        Node<T> end;
                        for (int i = 1; i < A.Length; i++)
                        {
                            q = new Node<T>(A[i], null, p);
                            p.next = q;
                            p = q;
                        }
                        end = q;
                        first = head;
                        last = end;
                        size = A.Length;
                        break;
                    }
            }
        }

        public void Add(T e)
        {
            Node<T> node = new Node<T>(e, null, last);
            if (size != 0)
            {
                last.next = node;
                last = node;
                size++;
            }
            else
            {
                first = node;
                last = node;
                size++;
            }
        }
        public void AddAll(T[] a)
        {

            foreach (T x in a)
                Add(x);
        }
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object o)
        {
            Node<T> p = first;
            while (p != null)
            {
                if (Equals(p.value, o))
                    return true;
                p = p.next;
            }
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
            if (size == 0)
                return true;
            return false;
        }
        public void Remove(object o)
        {
            if (size != 0)
            {
                if (Contains(o))
                {
                    Node<T> p = first.next;
                    if (Equals(first.value, o))
                    {
                        first = first.next;
                        if (first != null)
                            first.prev = null;
                        size--;
                        return;
                    }
                    while (p.next != null)
                    {
                        if (Equals(p.value, o))
                        {
                            p.prev.next = p.next;
                            p.next.prev = p.prev;
                            size--;
                            return;
                        }
                        p = p.next;
                    }
                    if (Equals(last.value, o))
                    {
                        last = last.prev;
                        if (last != null)
                            last.next = null;
                        size--;
                        return;
                    }
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            foreach (T x in a)
                Remove(x);
        }
        public void RetainAll(T[] array)
        {
            Node<T> p = first;
            bool flag;
            while (p != null)
            {
                flag = false;
                for (int i = 0; i < array.Length && !flag; i++)
                    if (Equals(array[i], p.value))
                        flag = true;
                if (!flag)
                    Remove(p.value);
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] array = new T[size];
            Node<T> p = first;
            int i = 0;
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
            return array;
        }
        public void ToArray(ref T[] array)
        {
            if (array == null)
            {
                array = ToArray();
                return;
            }
            Node<T> p = first;
            int i = 0;
            if (array.Length == size)
            {
                while (p != null)
                {
                    array[i] = p.value;
                    i++;
                    p = p.next;
                }
                return;
            }
            array = new T[size];
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index > size)
                throw new IndexOutOfRangeException("Incorrect index");
            if (index == 0)
            {
                Node<T> node = new Node<T>(element, first, null);
                if (first != null)
                    first.prev = node;
                first = node;
                if (last == null)
                    last = first;
                size++;
                return;
            }
            if (index == size)
            {
                Add(element);
                return;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            Node<T> newNode = new Node<T>(element, p, p.prev);
            p.prev.next = newNode;
            p.prev = newNode;
            size++;
        }
        public void AddAll(int index, T[] array)
        {
            if (index < 0 || index > size)
                throw new IndexOutOfRangeException("Incorrect index");
            for (int i = array.Length - 1; i >= 0; i--)
                Add(index, array[i]);
        }
        public T Get(int index)
        {
            if (index < 0 || index > size - 1)
                throw new IndexOutOfRangeException("Incorrect index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            return p.value;
        }
        public int IndexOf(object o)
        {
            Node<T> p = first;
            int index = 0;
            while (p != null)
            {
                if (Equals(p.value, o))
                    return index;
                index++;
                p = p.next;
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            Node<T> p = last;
            int index = size - 1;
            while (p != null)
            {
                if (Equals(p.value, o))
                    return index;
                index--;
                p = p.prev;
            }
            return -1;
        }
        public T RemoveAt(int index)
        {
            if (index < 0 || index > size - 1)
                throw new IndexOutOfRangeException("Incorrect index");
            if (index == 0)
            {
                T element = first.value;
                first = first.next;
                if (first != null)
                    first.prev = null;
                if (first == null)
                    last = null;
                size--;
                return element;
            }
            if (index == size - 1)
            {
                T element = last.value;
                last = last.prev;
                if (last != null)
                    last.next = null;
                if (last == null)
                    first = null;
                size--;
                return element;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            T value = p.value;
            p.prev.next = p.next;
            p.next.prev = p.prev;
            size--;
            return value;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index > size - 1)
                throw new IndexOutOfRangeException("Incorrect index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            p.value = element;
        }
        public MyLinkedList<T> SubList(int begin, int end)
        {
            if (begin < 0 || begin > size - 1)
                throw new IndexOutOfRangeException("BeginIndex");
            if (end < 0 || end > size)
                throw new IndexOutOfRangeException("EndIndex");
            MyLinkedList<T> list = new MyLinkedList<T>();
            Node<T> p = first;
            for (int i = 0; i < begin; i++)
                p = p.next;
            for (int i = 0; i < end - begin; i++)
            {
                list.Add(p.value);
                p = p.next;
            }
            return list;
        }
        public T Element()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Get(0);
        }
        public bool Offer(T element)
        {
            try
            {
                Add(element);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public T Peek()
        {
            if (size == 0)
                return default;
            return Get(0);
        }
        public T Poll()
        {
            if (size == 0)
                return default;
            T element = Get(0);
            RemoveAt(0);
            return element;
        }
        public void AddFirst(T element)
        {
            Add(0, element);
        }
        public void AddLast(T element)
        {
            Add(element);
        }
        public T GetFirst()
        {
            return Get(0);
        }
        public T GetLast()
        {
            return Get(size - 1);
        }
        public bool OfferFirst(T element)
        {
            try
            {
                Add(0, element);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool OfferLast(T element)
        {
            return Offer(element);
        }
        public T Pop()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Poll();
        }
        public void Push(T element)
        {
            AddFirst(element);
        }
        public T PeekFirst()
        {
            if (size == 0)
                return default;
            return GetFirst();
        }
        public T PeekLast()
        {
            if (size == 0)
                return default;
            return GetLast();
        }
        public T PollFirst()
        {
            return Poll();
        }
        public T PollLast()
        {
            if (size == 0)
                return default;
            T element = Get(Size() - 1);
            RemoveAt(Size() - 1);
            return element;
        }
        public T RemoveLast()
        {
            return RemoveAt(size - 1);
        }
        public T RemoveFirst()
        {
            return RemoveAt(0);
        }
        public bool RemoveFirstOccurrence(object obj)
        {
            int index = IndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
        public bool RemoveLastOccurrence(object obj)
        {
            int index = LastIndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
        public Node<T> GetFirstNode()
        {
            return first;
        }
        public void Print()
        {
            Node<T> p = first;
            while (p != null)
            {
                Console.Write(p.value + " ");
                p = p.next;
            }
            Console.Write('\n');
        }
    }
    class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;
        public Node()
        {
            value = default;
            next = null;
            prev = null;
        }
        public Node(T value, Node<T> next, Node<T> prev)
        {
            this.value = value;
            this.next = next;
            this.prev = prev;
        }
    }
}
