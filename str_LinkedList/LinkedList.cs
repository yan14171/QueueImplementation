using System.Collections;
using System.Collections.Generic;

namespace str_Queue
{
    public class LinkedList<T> : ICollection<T>
    {
        internal LinkedListNode<T> head;

        internal int count = 1;

        public LinkedList()
        {
            count = 0;
        }
        public LinkedList(T value)
        {
            head = new LinkedListNode<T>(value);
        }
        public LinkedList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                AddLast(item);
            }
        }
        public T? RemoveLast()
        {
            var cur = head;

            if (cur is null)
            {
                return default;
            }

            if (cur.next is null)
            {
                var curValue = cur.Value;
                head = null;
                count--;
                return curValue;
            }

            while (cur.next is not null)
            {
                cur = cur.next;
            }

            var returned = cur.Value;
            cur.prev!.next = null;
            count--;

            return returned;
        }
        public T? GetLast()
        {
            var cur = head;

            if (cur is null)
            {
                return default;
            }

            if (cur.next is null)
            {
                var curValue = cur.Value;
                return curValue;
            }

            while (cur.next is not null)
            {
                cur = cur.next;
            }

            var returned = cur.Value;

            return returned;
        }
        public T? RemoveFirst()
        {
            T headValue;

            if(head.next is null)
            {
                headValue = head.Value;
                head = null;
                return headValue;
            }

            headValue = head.Value;
            head = head.next;
            return headValue;
        }
        public LinkedListNode<T> AddFirst(T value)
        {
            if (head is null)
            {
                head = new LinkedListNode<T>(value);
                count = 1;
                return head;
            }

            LinkedListNode<T> cur = new LinkedListNode<T>(this, value);
            cur.next = head;
            head.prev = cur;
            head = cur;
            count++;

            return head;
        }
        public LinkedListNode<T> AddLast(T value)
        {
            if(head is null)
            {
                head = new LinkedListNode<T>(value);
                return head;
            }

            LinkedListNode<T> cur = head;

            while (cur?.Next() is not null)
            {
                cur = cur.Next();
            }

            cur.next = new LinkedListNode<T>(value);
            cur.next.prev = cur;
            count++;
            return cur.next;
        }
        public void Add(T item)
        {
            AddLast(item);
        }
        public void Clear()
        {
            while (RemoveLast() != null)
                ;
        }
        public bool Contains(T item)
        {
            if (item is null)
                return false;

            foreach (var iterated in this)
            {
                if (Equals(iterated,item))
                    return true;
            }

            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            int iteratedIndex = 0, 
                arrayIteratedIndex = 0;
            foreach (var iterated in this)
            {
                if (iteratedIndex >= arrayIndex)
                    array[arrayIteratedIndex] = iterated;
                iteratedIndex++;
            }
        }
        public bool Remove(T item)
        {
            var localHead = head;
            while(localHead.next is not null)
            {
                if(Equals(localHead.Value, item))
                {
                    RemoveNode(localHead);
                    return true;
                }
            }
            return false;
        }
        private void RemoveNode(LinkedListNode<T> node)
        {
            if(node.prev is null)
            {
                if (node.next is null)
                    node.Value = default;
                else
                    node = node.next;
            }
            else if (node.next is null)
                node = node.prev;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator<T>(this); GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
        public int Count => count;
        public bool IsReadOnly => false;
    }

    public sealed class LinkedListNode<T>
    {
        internal LinkedList<T>? list;
        internal LinkedListNode<T>? next;
        internal LinkedListNode<T>? prev;
        internal T item;

        public LinkedListNode(T value)
        {
            item = value;
        }

        internal LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            item = value;
        }

        public LinkedList<T>? List
        {
            get { return list; }
        }

        public LinkedListNode<T>? Next()
        {
            return next;
        }

        public LinkedListNode<T>? Previous()
        {
            return prev;
        }

        public T Value
        {
            get { return item; }
            set { item = value; }
        }
    }

    public struct Enumerator<T> : IEnumerator<T>
    {
        private readonly LinkedList<T> _list;
        private LinkedListNode<T>? _node;
        private T? _current;

        internal Enumerator(LinkedList<T> list)
        {
            _list = list;
            _node = list.head;
            _current = default;
        }

        public T Current => _current!;

        object IEnumerator.Current => _current!;

        public bool MoveNext()
        {
            if (_node == null)
            {
                return false;
            }

            _current = _node.item;
            _node = _node.next;
            if (_node == _list.head)
            {
                _node = null;
            }
            return true;
        }

        void IEnumerator.Reset()
        {
            _current = default;
            _node = _list.head;
        }

        public void Dispose()
        {

        }
    }
}