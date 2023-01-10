using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{

    class Program
    {
        static int nums = 0;
        static object _obj = new object(); //데이터 저장용도가 아닌 키역할로 사용

        static void Thread_1()
        {

            try
            {
                Monitor.Enter(_obj);
                nums++;
                return;
            }
            finally
            {
                //finally는 try후 적어도 1번은 실행됌
                Monitor.Exit(_obj);
                //Monitor 잠금 해제
                //return 후에도 Finally를 실행하기 때문에 코드 번거로움을 제거할 수 있음
            }

        }

        static void Thread_2()
        {
            try
            {
                Monitor.Enter(_obj);
                nums--;
                return;
            }
            finally
            {
                //finally는 try후 적어도 1번은 실행됌
                Monitor.Exit(_obj);
                //Monitor 잠금 해제
                //return 후에도 Finally를 실행하기 때문에 코드 번거로움을 제거할 수 있음
            }
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
