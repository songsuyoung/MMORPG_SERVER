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
                int disired = 1; //변경되길 바라는 값
                int expected = 0; // location값이 예상 되는 값
                //즉 저 함수 자체가
                /*
                 * int originalValue=_lock;
                 *  if(_lock==expected){
                 *      _lock=disired;
                 *  }
                 *  if(originalValue==0)
                 *      break;
                 *      
                 *      를 한줄로 나타낸 함수 (즉 원자성 확보)
                 */
                int originalValue = Interlocked.CompareExchange(ref _lock, disired, expected);

                if (originalValue == 0)
                    break;
            }
        }

        public void Release()
        {
            _lock = 0; //문은 단일 스레드로
                       //혼자 할 수 있는 것이기 때문에!! 그냥 변수 

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
