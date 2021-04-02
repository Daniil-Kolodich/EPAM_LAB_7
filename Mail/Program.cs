using System;
using System.Threading;
namespace Mail {
    class Program {
        public static int amount;

        public delegate void MailDelegate();

        public static event MailDelegate MailEvent;

        public static MailAgent[] agents = new MailAgent[3];
        public static void OnMailEvent(MailEventArgs e) {
            Thread.Sleep(1000);
            if (e.Index == 0)
                MailEvent();
            else
                agents[e.Index - 1].onMailAgentEvent(e);
        }

        static void Main(string[] args) {
            while (true) {
                try {
                    Console.WriteLine("Enter amount of mails : ");
                    amount = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException) {
                    Console.WriteLine("Invalid input");
                }
            }

            MailEvent += () => {
                Console.WriteLine("End of program");
                return;
            };

            for (int i = 0; i < 3; i++)
                agents[i] = new MailAgent();

            OnMailEvent(new MailEventArgs(1, 0));
        }

        private static void Program_MailEvent(object sender, MailEventArgs e) {
            throw new NotImplementedException();
        }
    }

    class MailEventArgs : EventArgs {
        public MailEventArgs(int index, int value) {
            Index = index;
            Value = value;
        }

        public int Index {
            get; set;
        }
        public int Value {
            get; set;
        }
    }
    class MailAgent {
        public MailAgent() {
            MailAgentEvent += HandleMail;
        }

        public delegate void MailAgentDelegate(MailEventArgs e);
        public event MailAgentDelegate MailAgentEvent;
        public void onMailAgentEvent(MailEventArgs e) {
            if (MailAgentEvent != null)
                MailAgentEvent(e);
        }
        public void HandleMail(MailEventArgs e) {
            Console.WriteLine($"Mail Agent [{e.Index}] get data : {e.Value}");
            Random rand = new Random();
            Program.OnMailEvent(new MailEventArgs(--Program.amount == 0 ? 0 : rand.Next(1, 4), rand.Next(1, 100)));
        }
    }
}
