using System;
using System.Threading;

namespace ServerCore
{

    class Program
    {
        static int nums = 0;
        static void Thread_1()
        {
            Interlocked.Increment(ref nums);
        }

        static void Thread_2()
        {
            Interlocked.Decrement(ref nums);
        }
        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(nums);
        }

    }
}
