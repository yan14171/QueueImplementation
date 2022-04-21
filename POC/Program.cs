// test here
using System;

var q = new str_Queue.Queue<int>();

q.QueueEnqueuedElementEvent += (sender, args) =>
{
    Console.WriteLine(args.Text + args.Value );
};
q.QueueDequeuedElementEvent += (sender, args) =>
{
    Console.WriteLine(args.Text + args.Value);
};
q.QueueEmptyEvent += (sender, args) =>
{
    Console.WriteLine(args.Text + args.Value);
};

q.Enqueue(1);
q.Enqueue(2);
q.Enqueue(3);
q.Enqueue(4);

System.Console.WriteLine(q.Dequeue());
System.Console.WriteLine(q.Dequeue());
System.Console.WriteLine(q.Dequeue());
q.Clear();

try
{
    q.Dequeue();
}
catch(Exception ex)
{
    Console.WriteLine("Exception: \n " +ex.Message);
}