using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        //Сокет для взаимодействия с клиентом.
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Массив для передачи в метод получения данных от клиента.
        private static byte[] buffer = new byte[1024];
        static void Main(string[] args)
        {
            //Привязываем сокет к конечной точке сети, с адресом 127.0.0.1 (соответствует локалхосту) и портом 2000.
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000));

            //Переводим сокет в режим прослушивания (ожидания подключений) и оповещаем пользователя об этом.
            socket.Listen(5);
            Console.WriteLine("Server started.");

            //Ожидаем клиента и принимаем его.
            Socket client = socket.Accept();
            Console.WriteLine("New user connected.");

            //Ввиду слова "простейший" в условии лабы делаем наш сервер однопользовательским
            while(true)
            {
                //Принимаем данные от клиента помещая их в заранее заготовленный буффер.
                int n = client.Receive(buffer);

                //Декодируем сообщение из байтов в строку.
                string message = Encoding.UTF8.GetString(buffer, 0, n);

                //Обнуляем буффер дабы не поймать муссор при следующих приёмах данных от клиента.
                buffer = new byte[1024];

                //Выводим сообщение пользователя.
                Console.WriteLine(message);
            }
        }
    }
}