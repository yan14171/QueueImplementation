using System.Collections.Generic;
using Xunit;
using str_Queue;
using System;
using System.Linq;

namespace str_Queue.Tests
{
    public class QueueTests
    {
        Dictionary<int, StudentName> students;
        
        public QueueTests()
        {
            students = new Dictionary<int, StudentName>()
            {
                { 111, new StudentName ( FirstName: "Sachin", LastName: "Karnik", ID: 211 ) },
                { 112, new StudentName ( FirstName: "Dina", LastName: "Salimzianova", ID: 317 ) },
                { 113, new StudentName ( FirstName: "Andy", LastName: "Ruth", ID: 198 ) }
            };
        }
        [Fact]
        public void StackShouldAddItemsConsequently()
        {
            var queue = new str_Queue.Queue<StudentName>();

            foreach (var item in students)
            {
                queue.Enqueue(item.Value);
            }

            Assert.Equal(3, queue.Count);
        }
        [Fact]
        public void StackShouldImplementFIFO()
        {
            var queue = new str_Queue.Queue<StudentName>();

            foreach (var item in students)
            {
                queue.Enqueue(item.Value);
            }
            
            var student3 = queue.Dequeue();

            Assert.Equal("Sachin", student3.FirstName);
            Assert.DoesNotContain(queue, d => d.FirstName == "Sachin");
        }
        [Fact]
        public void StackShouldDeleteConsequently()
        {
            var queue = new str_Queue.Queue<StudentName>();

            foreach (var item in students)
            {
                queue.Enqueue(item.Value);
            }

            while (queue.Count > 0)
                queue.Dequeue();

            Assert.Equal(0, queue.Count);
            Assert.Empty(queue);
        }
        [Fact]
        public void StackShouldPeekWithoutDeleting()
        {
            var queue = new str_Queue.Queue<StudentName>();

            foreach (var item in students)
            {
                queue.Enqueue(item.Value);
            }

            var firstStudent = queue.Peek();

            Assert.Equal(students.First().Value, firstStudent);
            Assert.Equal(3, queue.Count);
        }
        [Fact]
        public void StackShouldWorkProperlyWithConstructors()
        {
            var queue = new str_Queue.Queue<StudentName>(students.Values);

            Assert.Contains(queue, f => f.ID == 211);
            Assert.Contains(queue, f => f.ID == 317);
            Assert.Contains(queue, f => f.ID == 198);

            Assert.Equal(3, queue.Count);
        }
        [Fact]
        public void StackShouldThrowInvalidOperationOnEmptyDequeue()
        {
            var queue = new str_Queue.Queue<StudentName>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }

    record StudentName(string FirstName, string LastName, int ID);

}