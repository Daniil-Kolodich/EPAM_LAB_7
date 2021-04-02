using System;
using System.Threading;
namespace Ping_Pong {
    class Program {
        public static int GameTime;
        static void Main(string[] args) {
            Ping ping = new Ping();
            Pong pong = new Pong();

            while (true) {
                try {
                    Console.Write("Enter Game time  : ");
                    GameTime = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException) {
                    Console.WriteLine("Invalid input, try again");
                }
            }

            ping.PingEvent += (sender, e) => {
                Console.WriteLine("Pong received Ping");
                pong.Play();
            };

            pong.PongEvent += (sender, e) => {
                Console.WriteLine("Ping received Pong");
                ping.Play();
            };

            Console.WriteLine("Game started : \n");
            pong.Play();
            Console.WriteLine("Game ended.");
        }

        public static void PingPongDelay(int size, int direction) {
            const int maxTime = 1000;
            var field = new char[size];
            for (int i = 0; i < size; i++)
                field[i] = ' ';

            field[0] = '|';
            field[^1] = '|';
            field[direction == 1 ? 1 : ^2] = '#';
            for (int i = direction == 1 ? 1 : size - 2; direction == 1 ? i < size - 1 : i > 1; i += direction) {
                Console.WriteLine(field);
                field[i] = ' ';
                field[i + direction] = '#';
                Thread.Sleep(maxTime / size);
            }
        }
    }


    class Ping {
        public event EventHandler PingEvent;
        public void OnPingEvent() {
            if (PingEvent != null)
                PingEvent(this, new EventArgs());
        }

        public void Play() {
            if (Program.GameTime == 0)
                return;
            Program.GameTime--;
            Program.PingPongDelay(10, 1);
            OnPingEvent();
        }
    }

    class Pong {
        public event EventHandler PongEvent;
        public void OnPongEvent() {
            if (PongEvent != null)
                PongEvent(this, new EventArgs());
        }
        public void Play() {
            if (Program.GameTime == 0)
                return;
            Program.GameTime--;
            Program.PingPongDelay(10, -1);
            OnPongEvent();
        }
    }
}
