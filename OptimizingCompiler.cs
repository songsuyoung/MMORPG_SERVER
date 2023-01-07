using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{

    class Program
    {

        static bool isStop = false;
        //static 변수는 모든 스레드가 공통적으로 접근할 수 있다
        static void ThreadMain()
        {
            Console.WriteLine("쓰레드 시작");
            //C+w tab tab => Console.WriteLine 자동완성


            while (!isStop)
            {
                //누군가 stop해줄때까지 대기한다
            }
            Console.WriteLine("쓰레드 종료");
        }

        static void Main(string[] args)
        {
            Task task = new Task(ThreadMain);
            //ThreadPool 작업라인에 ThreadMain함수를 넣어놓음

            task.Start();

            Thread.Sleep(1000); //1s동안 프로그램 정지
                                //sleep함수는 모든 프로그램을 정지 시키기 때문에 사용하지 않는 것이 좋
            isStop = true;
            //ThreadMain 실행할 수 있는 시간을 제공한다
            //1초 뒤에 isStop=true로 만듬으로써 쓰레드 종료를 뿌려준

            Console.WriteLine("Stop 호출");
            Console.WriteLine("종료 대기");

            task.Wait();
            //쓰레드가 끝날때까지 대기를 해줌 (메인 함수가 바로 끝나서 모든 작업이 안끝나지는 경우의 수를 제외함
            //남은 쓰레드 업무가 끝난 뒤, 다음 라인을 호출하고 그리고 나서 프로그램을 종료한다

            Console.WriteLine("종료 성공");
        }

    }
}
