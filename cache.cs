using System;

namespace ServerCore
{

    class Program
    {
        static void Main(string[] args)
        {
            int[,] arrs = new int[10000, 10000];
            //지역 변수로 만들기 위해 {}를 사용하는 것 같아 
            {
                long now = DateTime.Now.Ticks;
                //시간 계산 클래스를 이용하여 시간을 계산.
                

                for (int i = 0; i < 10000; i++)
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        arrs[i, j] = 1;
                    }
                }


                long end = DateTime.Now.Ticks;
                Console.WriteLine($"(end-now) 순서 걸린 시간 {end - now}");

            }

            {
                long now = DateTime.Now.Ticks;
                for (int j = 0; j < 10000; j++)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        arrs[i, j] = 1;
                    }
                }
                long end = DateTime.Now.Ticks;
                Console.WriteLine($"(end-now) 순서 걸린 시간 {end - now}");
            }
            
        }
    }
}

//캐시 작동 여부를 확인하기 위한 코딩
