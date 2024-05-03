using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public delegate void newDelegate5(string name);
    public class Error
    {
        public event newDelegate5 StandartError;
        public event newDelegate5 UserError;

        string _content;

        public Error() { }

        public string Create
        {
            set { _content = value; }
        }
        public void Standart()
        {
            Console.WriteLine($"Стандартное исключение: {_content}");
            StandartError(_content);
        }
        public void User()
        {
            Console.WriteLine($"Пользовательское исключение: {_content}");
            UserError(_content);
        }
    }
}
