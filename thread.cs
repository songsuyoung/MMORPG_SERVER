using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{

    class Program
    {
        static void MainThread()
        {
            Console.WriteLine("Hello world");
        }

        static void Main(string[] args)
        {

            Thread thread = new Thread(MainThread); //함수 연결이 가능.

            thread.IsBackground=true; //뒤로 실행
            thread.Name = "Test Thread"; //쓰레드 이름 지정도 가능하다는 점

            thread.Start(); // 쓰레드 실행
            //C#의 경우 포라운드로 만들어진다는 것이 특징임
            //main이 종료될때 이 백라운드로 만들어진 함수도 종료된다. -> 이를 막기 위해서 Join함수를 이용한다

            thread.Join();

            Console.WriteLine("Hello World");
        }
    }
}
