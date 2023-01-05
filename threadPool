
using System;
using System.Threading; //thread를 이용하기 위해서 필요

namespace ServerCore
{
	class Program
	{
		static void MainThread(object state) //object 타입을 이용해서 받아야한다는 점
		{
		Console.WriteLine("Hello world");
		}
	
		static void Main(string[] args)
		{
			ThreadPool.SetMinThreads(1, 1);
			//최소 쓰레드 설정
			ThreadPool.SetMaxThreads(5, 5);
			//최대 쓰레드 설정
			
			for(int i=0;i<5;i++)
				Task t = new Task(() => { while (true) ; },TaskCreationOptions.LongRunning);
			
			ThreadPool.QueueUserWorkItem(MainThread); 
			//위 task에서 선언된 TaskCreationOptions.LongRunning에 의해서 오랜 시간동안 작업할 테스크임을 미리
			//빼놓는다. 그렇기 때문에 아래 ThreadPool에서 필요한 함수가 시작가능하다.
			
		}
	
	}
}
