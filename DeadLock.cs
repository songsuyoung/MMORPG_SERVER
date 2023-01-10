using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{
    //deadLock의 예시

    class SessionManager
    {
        static object _lock = new object();

        static public void TestSession()
        {
            lock (_lock)
            {

            }
        }

        static public void Test()
        {
            lock (_lock)
            {
                UserManager.TestUser();
            }
            
        }
    }

    class UserManager
    {

        static object _lock = new object();

        static public void TestUser()
        {
            lock (_lock)
            {

            }
        }

        static public void Test()
        {
            lock (_lock)
            {
                SessionManager.Test();
            }
            
        }
    }

    class Program
    {
        static int nums = 0;
        static object _obj = new object(); //데이터 저장용도가 아닌 키역할로 사용

        static void Thread_1()
        {
            for (int i = 0; i < 1000; i++)
                SessionManager.Test();

        }

        static void Thread_2()
        {
            for (int i = 0; i < 1000; i++)
                UserManager.Test();
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

//DeadLock은 발견하고 수정하는 것이 훨씬 쉬운 과정이다
