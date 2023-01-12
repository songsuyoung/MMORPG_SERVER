using System;
using System.Threading;

namespace ServerCore
{

    class SpinLock
    {
        volatile int _lock=0; //Interlocked.Exchange or Interlocked.CompareExchange 모두 정수형의 파라미터만
        //받기 때문에 bool -> int형으로 변환


        public void Aquire()
        {
            while (true)
            {
                int originalValue = Interlocked.Exchange(ref _lock, 1);
                //0->1 (대기 -> 잠금) 1->1 (잠금 -> 잠금)

                if (originalValue == 0)
                    break; //위 과정이 대기하고 잠금을 모두 시행
            }
        }

        public void Release()
        {
            _lock = 0; //잠금 풀림

        }
    }

    class Program
    {

        static SpinLock _lock = new SpinLock();
        static int nums = 0; // 공유 변수 선언

        static void Thread_1()
        {
            for (int i = 0; i < 1000000; i++)
            {
                _lock.Aquire();
                nums++;
                _lock.Release();

            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 1000000; i++)
            {
                _lock.Aquire();
                nums--;
                _lock.Release();

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
