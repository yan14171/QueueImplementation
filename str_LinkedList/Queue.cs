namespace str_Queue;

using System;
using System.Collections;
using System.Collections.Generic;
public class Queue<T> : ICollection<T>
{
    public Queue(T element)
    {
        QueuePopulatedEvent?.Invoke(this, new QueueEventArgs<T>("First element added to a queue in the constructor", element));
        _list = new LinkedList<T>(element);
    }
    public Queue()
    {
        _list = new LinkedList<T>();
    }
    public Queue(ICollection<T> elementCollection)
    {
        _list = new LinkedList<T>(elementCollection);
    }

    private LinkedList<T> _list;
    public int Count => _list.Count;
    public bool IsReadOnly => false;
    public delegate void QueueEventHandler(object sender, QueueEventArgs<T> e);
    public event QueueEventHandler QueuePopulatedEvent;
    public event QueueEventHandler QueueEmptyEvent;
    public event QueueEventHandler QueueDequeuedElementEvent;
    public event QueueEventHandler QueueEnqueuedElementEvent;
    public event QueueEventHandler QueueTryEnqueueNullElementEvent;
    public T Dequeue()
    {
        if (_list.Count == 0)
        {
            QueueEmptyEvent?.Invoke(this,
                new QueueEventArgs<T>("In a dequeue call the queue appears to be empty." +
                "Populate the queue first."));

            throw new InvalidOperationException("Queue is empty");
        }
        var last = _list.RemoveLast();

        QueueDequeuedElementEvent?.Invoke(this, new QueueEventArgs<T>("Dequeueing an element. " +
                "Reference given in the Value field", last));

        return last;
    }
    public T Peek()
    {
        var last = _list.GetLast();

        if (last is null)
        {
            QueueEmptyEvent?.Invoke(this,
                new QueueEventArgs<T>("In a peek call the queue appears to be empty." +
                "Populate the queue first."));

            throw new InvalidOperationException("Queue is empty");
        }
        else
            QueueDequeuedElementEvent?.Invoke(this, new QueueEventArgs<T>("Peeking an element. " +
                "Reference given in the Value field", last));

        return last;
    }
    public void Enqueue(T item)
    {
        if (item is null)
        {
            QueueTryEnqueueNullElementEvent?.Invoke(this, new QueueEventArgs<T>("Trying to enqueue an element, which is null. " +
                "Operation aborted. Suffice an elegable element to enqueue."));
            
            return;
        }

        QueuePopulatedEvent?.Invoke(this, new QueueEventArgs<T>("First element added to the queue in an Enqueue method", item));
        QueueEnqueuedElementEvent?.Invoke(this, new QueueEventArgs<T>("Element added to the queue. " +
            "Reference to the element is in the Value field", item));

        _list.AddFirst(item);
    }
    public void Add(T item) => Enqueue(item);
    public void Clear()
    {
        while (Count > 0)
        {
            Dequeue();
        }
    }
    public bool Contains(T item) => _list.Contains(item);
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, "Argument out of range");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("Argument out of range");
        }
        _list.CopyTo(array, arrayIndex);
    }
    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
    public bool Remove(T item) => _list.Remove(item);
    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
}

public class QueueEventArgs<T>
{
    public QueueEventArgs(string text) { Text = text; }
    public QueueEventArgs(string text, T value) { Text = text; Value = value; }
    public string Text { get; }
    public T Value { get; }
}
