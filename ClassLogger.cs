using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class ClassLogger
    {
        private string _filePath;

        public byte writeToLog<T>(string text)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine("[{0}] // {1}", DateTime.Now, text);
            }

            return 0;
        }

        public byte changeFilePath(string filePath)
        {
            _filePath = filePath;

            return 0;
        }

        public ClassLogger(string filePath = "logging.txt")
        {
            changeFilePath(filePath);
        }

        public byte readLog<T>()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            return 0;
        }

        public byte cleanLog()
        {
            File.WriteAllBytes(_filePath, new byte[0]);

            return 0;
        }

        public void AddSt(string name)
        {
            writeToLog<string>($"Добавление звезды {name} в звездную систему.");
        }
        public void AddPl(string name)
        {
            writeToLog<string>($"Добавление планеты {name} в звездную систему.");
        }
        public void AddSat(string name)
        {
            writeToLog<string>($"Добавление спутника {name} в звездную систему.");
        }
        public void ClearPl()
        {
            writeToLog<string>($"Удаление планет из звездной системы");
        }

        public void ClearSat()
        {
            writeToLog<string>($"Удаление спутников у планеты");
        }
        public void StandartEx(string name)
        {
            writeToLog<string>($"Вызвано стандартное исключение: {name}");
        }
        public void UsertEx(string name)
        {
            writeToLog<string>($"Вызвано пользовательское исключение: {name}");
        }
    }
}
