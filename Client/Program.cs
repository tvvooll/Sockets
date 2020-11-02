using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        //Сокет для взаимодействия с сервером.
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        //Массив для хранения отправляемого сообщения.
        private static byte[] buffer = new byte[1024]; 
        static void Main(string[] args)
        {
            //Подключаемся к точке сети к которой привязан наш сервер и оповещаем пользователя в случае удачи.
            socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000));
            Console.WriteLine("Connected to server.");

            //Даём пользователю возможность неограниченого долго слать сообщения на сервер.
            //Выхода не будет - это маркетинг. В следующий раз нельзя будет даже на крестик нажать :)
            while (true)
            {
                //Считываемм сообщение от пользователя, кодируем их в UTF-8, сохраняя в заранее подготовленый буффер.
                Console.Write("Type your message: ");
                string message = Console.ReadLine();
                buffer = Encoding.UTF8.GetBytes(message);

                //Отправляем закодированное сообщение на сервер.
                socket.Send(buffer);

                //Обнуляем буффер дабы не отправить муссор при следующем сообщении к серверу.
                buffer = new byte[1024];
            }
        }
    }
}
