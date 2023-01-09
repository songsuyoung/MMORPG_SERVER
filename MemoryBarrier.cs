using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{

    class Program
    {

        static int x = 0;
        static int y = 0;

        static int r1 = 0;
        static int r2 = 0;

        static void Thread_1()
        {
            y = 1; //store

            Thread.MemoryBarrier(); //경계선을 취해, 메모리 정보를 업데이트(가시성)
            r1 = x; //load
        }

        static void Thread_2()
        {
            x = 1; //store

            Thread.MemoryBarrier();

            r2 = y; //load
        }
        static void Main(string[] args)
        {
            int count = 0;

            while (true)
            {
                count++;
                 

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);
                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2);
                //t1,t2끝날때까지 대기

                if (r1 == 0 && r2 == 0)
                    break;

            }
            Console.WriteLine($"{count}번에 빠져나옴");
        }

    }
}
