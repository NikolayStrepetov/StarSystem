using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class MySpace
    {
        static void WriteSomething(float coef, string name, Action<float, string> operation) => operation(coef, name);
        static void FirstPattern(float coef, string name) => Console.WriteLine($"{name} коэффициент землеподобия: {Math.Round(coef, 4)}");
        static void Main()
        {
            var starsystem = new StarSystem();

            Random rnd = new Random();
            int massDistrib;
            int nameKey;
            float preMass = 0;
            int countOfPlanets = 0;
            int planetTypeNumber;

            Planet planet;
            int planetNumber;

            int countOfSatelites = 0;
            Satelite satelite;

            string ?buffer;
            bool result;
            string ?sortPar = "";

            EarthlikeCollection<IEarthLike> newCollection = new EarthlikeCollection<IEarthLike>();

            Error error = new Error();

            ClassLogger logger = new();
            starsystem.AddSt += logger.AddSt;
            starsystem.AddPl += logger.AddPl;
            starsystem.ClearPl += logger.ClearPl;
            error.UserError += logger.UsertEx;
            error.StandartError += logger.StandartEx;

            ConsoleKeyInfo keyValue;
            ConsoleKeyInfo generationParam;
            ConsoleKeyInfo sateliteParam;

            string path = "C:/Учеба/Net/StarSystem/StarSystem/config.txt";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    sortPar = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                error.Create = $"{ex.Message}";
                error.Standart();
                Console.ReadKey();
            }

            do
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1)Для вывода звёздной системы на экран нажмите '1'");
                Console.WriteLine("2)Для генерации звезды нажмите '2'");
                Console.WriteLine("3)Для генерации планет нажмите '3'");
                Console.WriteLine("4)Для генерации спутников нажмите '4'");
                Console.WriteLine("5)Для вывода землеподобных объектов нажмите '5'");
                Console.WriteLine("6)Для вывода информации о космических аппаратах нажмите '6'");
                Console.WriteLine("7)Для сортировки землеподобных объектов нажмите '7'");
                Console.WriteLine("8)Для выхода нажмите 'Esc'");

                keyValue = Console.ReadKey();

                switch (keyValue.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        
                        if (starsystem.GetStar() == null)
                        {
                            error.Create = $"Планеты ещё не были сгенерированы!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }

                        Star systemStar = starsystem.GetStar();

                        Console.WriteLine($"Звезда: {systemStar.Name}");
                        Console.WriteLine($"Спектральный класс: {systemStar.SpectralClass}");
                        Console.WriteLine($"Масса: {Math.Round(systemStar.Mass, 4)} (массы Солнца)");
                        Console.WriteLine($"Радиус: {Math.Round(systemStar.Radius, 4)} (радиусы Солнца)");
                        Console.WriteLine($"Звездная величина: {systemStar.Magnitude}");
                        Console.WriteLine($"Температура: {systemStar.Temperature} (кельвины)");
                        Console.WriteLine($"Светимость: {Math.Round(systemStar.Luminosity, 4)} (светимости Солнца)");
                        Console.WriteLine();
                        Console.WriteLine("__________________________________________________\n\n");

                        if (starsystem.GetPlanets().Count() == 0)
                        {
                            error.Create = $"Планеты ещё не были сгенерированы!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }

                        List<Planet> planets = starsystem.GetPlanets();
                        int count = 1;

                        foreach (Planet systemPlanet in planets)
                        {
                            Console.WriteLine($"Планета {count}: {systemPlanet.Name}");
                            if ($"{systemPlanet.PType}" == "EarthType") Console.WriteLine($"Тип: Планета земного типа");
                            if ($"{systemPlanet.PType}" == "Giant") Console.WriteLine($"Тип: Планета гигант");
                            if ($"{systemPlanet.PType}" == "Dwarf") Console.WriteLine($"Тип: Планета карлик");
                            Console.WriteLine($"Масса: {Math.Round(systemPlanet.Mass, 4)} (массы Земли)");
                            if ($"{systemPlanet.PType}" == "Dwarf") Console.WriteLine($"Радиус: {systemPlanet.Radius} (км)");
                            else Console.WriteLine($"Радиус: {systemPlanet.Radius} (радиусы Земли)");
                            Console.WriteLine($"Эксцентриситет орбиты: {Math.Round(systemPlanet.Eccentricity, 4)}");
                            Console.WriteLine($"Альбедо: {Math.Round(systemPlanet.Albedo, 3)}");
                            Console.WriteLine($"Расстояние от звезды: {Math.Round(systemPlanet.Distance, 4)} (км)");
                            Console.WriteLine($"Период обращения: {Math.Round(systemPlanet.Period, 4)} (земные сутки)");
                            Console.WriteLine();

                            List<Satelite> planetSatelites = systemPlanet.GetSatelites();
                            int sateliteCount = 1;

                            if (planetSatelites.Count() == 0) Console.WriteLine("Спутников у планеты нет.");
                            else
                            {
                                foreach (Satelite systemSatelite in planetSatelites)
                                {
                                    Console.WriteLine($"{"\t"}Спутник {sateliteCount}: {systemSatelite.Name}");
                                    Console.WriteLine($"{"\t"}Масса: {Math.Round(systemSatelite.Mass, 7)} (массы земли)");
                                    Console.WriteLine($"{"\t"}Радиус: {Math.Round(systemSatelite.Radius, 7)} (км)");
                                    Console.WriteLine($"{"\t"}Расстояние от планеты: {Math.Round(systemSatelite.Distance, 4)} (км)");
                                    Console.WriteLine($"{"\t"}Период обращения: {Math.Round(systemSatelite.Period, 4)} (земные сутки)");
                                    Console.WriteLine();
                                    sateliteCount++;
                                }
                            }

                            Console.WriteLine();
                            Console.WriteLine("__________________________________________________\n\n");
                            count++;
                        }

                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("Генерация звезды\n");

                        Console.WriteLine("Для ввода вручную нажмите '1'");
                        Console.WriteLine("Для автоматической генерации нажмите '2'\n");

                        var star = new Star();

                        generationParam = Console.ReadKey();
                        switch (generationParam.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                Console.WriteLine("Ввод звезды вручную\n");
                                
                                Console.WriteLine("Введите название звезды:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    star.Name = buffer;
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите спектральный класс звезды [O, B, A, F, G, K, M]:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    star.SpectralClass = buffer;
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите массу звезды в массах Солнца:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = float.TryParse(buffer, out var value);
                                    if (result) star.Mass = value;
                                    else
                                    {
                                        error.Create = $"Введена неверная масса!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите радиус звезды в радиусах Солнца:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var value);
                                    if (result) star.Radius = value;
                                    else
                                    {
                                        error.Create = $"Введен неверный радиус!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите звездную величину в диапазоне от -40 до 50:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = float.TryParse(buffer, out var value);
                                    if (result) star.Magnitude = value;
                                    else
                                    {
                                        error.Create = $"Введена неверная звездная величина!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите температуру звезды в диапазоне от 2500 до 45000 в кельвинах:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = float.TryParse(buffer, out var value);
                                    if (result) star.Temperature = value;
                                    else
                                    {
                                        error.Create = $"Введена неверная температура!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите светимость звезды в диапазоне от 0.001 до 700000 светимостей Солнца:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var value);
                                    if (result) star.Luminosity = value;
                                    else
                                    {
                                        error.Create = $"Введена неверная светимость!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                starsystem.AddStar(star);

                                break;
                            case ConsoleKey.D2:
                                Console.Clear();
                                Console.WriteLine("Звезда сгенерирована случайно");

                                nameKey = rnd.Next(1, 6);
                                switch (nameKey)
                                {
                                    case 1:
                                        star.Name = "Gaia-" + rnd.Next(1, 99999).ToString();
                                        break;
                                    case 2:
                                        star.Name = "Hubble-" + rnd.Next(1, 99999).ToString();
                                        break;
                                    case 3:
                                        star.Name = "Spitzer-" + rnd.Next(1, 99999).ToString();
                                        break;
                                    case 4:
                                        star.Name = "Kepler-" + rnd.Next(1, 99999).ToString();
                                        break;
                                    case 5:
                                        star.Name = "Chandra-" + rnd.Next(1, 99999).ToString();
                                        break;
                                    case 6:
                                        star.Name = "Webb-" + rnd.Next(1, 99999).ToString();
                                        break;
                                }

                                massDistrib = rnd.Next(1, 100);
                                if (massDistrib >= 1 && massDistrib <= 70)
                                {
                                    preMass = rnd.Next(75, 500) * 0.001F;
                                }
                                if (massDistrib >= 71 && massDistrib <= 85)
                                {
                                    preMass = rnd.Next(500, 800) * 0.001F;
                                }
                                if (massDistrib >= 86 && massDistrib <= 92)
                                {
                                    preMass = rnd.Next(800, 1200) * 0.001F;
                                }
                                if (massDistrib >= 93 && massDistrib <= 95)
                                {
                                    preMass = rnd.Next(1200, 1600) * 0.001F;
                                }
                                if (massDistrib >= 96 && massDistrib <= 97)
                                {
                                    preMass = rnd.Next(1600, 3000) * 0.001F;
                                }
                                if (massDistrib >= 98 && massDistrib <= 99)
                                {
                                    preMass = rnd.Next(3000, 20000) * 0.001F;
                                }
                                if (massDistrib == 100)
                                {
                                    preMass = rnd.Next(20000, 120000) * 0.001F;
                                }
                                star.Mass = preMass;

                                if (star.Mass >= 20)
                                {
                                    star.SpectralClass = "O";
                                    star.Magnitude = (rnd.Next(0, 70) / -10);
                                    star.Temperature = rnd.Next(30000, 45000);
                                    star.Radius = rnd.Next(20, 1500);
                                    star.Luminosity = rnd.Next(50000, 700000);
                                }
                                if (star.Mass >= 3 && star.Mass < 20)
                                {
                                    star.SpectralClass = "B";
                                    star.Magnitude = (rnd.Next(-10, 70) / -10);
                                    star.Temperature = rnd.Next(10000, 30000);
                                    star.Radius = rnd.Next(28, 300) * 0.1;
                                    star.Luminosity = rnd.Next(100, 50000);
                                }
                                if (star.Mass >= 1.6 && star.Mass < 3)
                                {
                                    star.SpectralClass = "A";
                                    star.Magnitude = (rnd.Next(-25, 83) / -10);
                                    star.Temperature = rnd.Next(7400, 10000);
                                    star.Radius = rnd.Next(17, 2100) * 0.1;
                                    star.Luminosity = rnd.Next(7, 80);
                                }
                                if (star.Mass >= 1.2 && star.Mass < 1.6)
                                {
                                    star.SpectralClass = "F";
                                    star.Magnitude = (rnd.Next(-42, 85) / -10);
                                    star.Temperature = rnd.Next(6000, 7400);
                                    star.Radius = rnd.Next(132, 21500) * 0.01;
                                    star.Luminosity = rnd.Next(2, 6);
                                }
                                if (star.Mass >= 0.8 && star.Mass < 1.2)
                                {
                                    star.SpectralClass = "G";
                                    star.Magnitude = (rnd.Next(-57, 80) / -10);
                                    star.Temperature = rnd.Next(5000, 6000);
                                    star.Radius = rnd.Next(90, 14000) * 0.01;
                                    star.Luminosity = rnd.Next(4, 15) * 0.1;
                                }
                                if (star.Mass >= 0.5 && star.Mass < 0.8)
                                {
                                    star.SpectralClass = "K";
                                    star.Magnitude = (rnd.Next(-85, 80) / -10);
                                    star.Temperature = rnd.Next(3800, 5000);
                                    star.Radius = rnd.Next(80, 5000) * 0.01;
                                    star.Luminosity = rnd.Next(10, 40) * 0.1;
                                }
                                if (star.Mass < 0.5)
                                {
                                    star.SpectralClass = "M";
                                    star.Magnitude = (rnd.Next(-161, 73) / -10);
                                    star.Temperature = rnd.Next(2500, 3800);
                                    star.Radius = rnd.Next(80, 4000) * 0.01;
                                    star.Luminosity = rnd.Next(80) * 0.001;
                                }

                                starsystem.AddStar(star);

                                break;
                        }

                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine("Генерация планет звездной системы\n");

                        if (starsystem.GetStar() == null)
                        {
                            error.Create = $"Звезда ещё не была сгенерирована!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Для ввода вручную нажмите '1'");
                        Console.WriteLine("Для автоматической генерации нажмите '2'\n");

                        generationParam = Console.ReadKey();
                        switch (generationParam.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                starsystem.ClearPlanets();

                                Console.WriteLine("Ввод планет вручную\n");

                                Console.WriteLine("Введите количество планет от 1 до 10:");
                                try
                                {
                                    int value = int.Parse(Console.ReadLine());
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.Standart();
                                    Console.ReadLine();
                                }

                                for (int i = 1; i <= countOfPlanets; i++)
                                {

                                    Console.WriteLine("Выберите тип планеты:");
                                    Console.WriteLine("1)Нажмите '1' для ввода планеты земного типа");
                                    Console.WriteLine("2)Нажмите '2' для ввода планеты гиганта");
                                    Console.WriteLine("3)Нажмите '3' для ввода планеты карлика");
                                    
                                    switch (generationParam.Key)
                                    {
                                        case ConsoleKey.D1:
                                            Console.Clear();

                                            planet = new EarthType();

                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.EarthType;

                                            Console.WriteLine("Введите название планеты:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                planet.Name = buffer;
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите расстояние от планеты до звезды:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Distance = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное расстояние!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите массу планеты в количестве масс Земли от 0.1 до 10:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Mass = val;
                                                else
                                                {
                                                    error.Create = $"Введена неверная масса!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите радиус планеты в количестве радиусов Земли от 0.7 до 2:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Radius = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный радиус!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите эксцентриситет орбиты планеты от 0.005 до 0.09:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Eccentricity = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный эксцентриситет!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите адьбедо планеты от 0 до 1:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Albedo = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное альбедо!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите орбитальный период планеты в земных сутках:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Period = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный орбитальный период!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            starsystem.AddPlanet(planet);

                                            break;
                                        case ConsoleKey.D2:
                                            Console.Clear();

                                            planet = new Giant();

                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.Giant;

                                            Console.WriteLine("Введите название планеты:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                planet.Name = buffer;
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите расстояние от планеты до звезды:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Distance = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное расстояние!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите массу планеты в количестве масс Земли от 10 до 3000:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Mass = val;
                                                else
                                                {
                                                    error.Create = $"Введена неверная масса!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите радиус планеты в количестве радиусов Земли от 4 до 30:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Radius = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный радиус!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите эксцентриситет орбиты планеты от 0.005 до 0.09:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Eccentricity = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный эксцентриситет!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите адьбедо планеты от 0 до 1:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Albedo = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное альбедо!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите орбитальный период планеты в земных сутках:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Period = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный орбитальный период!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            starsystem.AddPlanet(planet);

                                            break;
                                        case ConsoleKey.D3:
                                            Console.Clear();

                                            planet = new Dwarf();

                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.Dwarf;

                                            Console.WriteLine("Введите название планеты:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                planet.Name = buffer;
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите расстояние от планеты до звезды:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Distance = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное расстояние!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите массу планеты в количестве масс Земли от 0.0001 до 0.001:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Mass = val;
                                                else
                                                {
                                                    error.Create = $"Введена неверная масса!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите радиус планеты километрах от 800 до 3000:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Radius = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный радиус!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите эксцентриситет орбиты планеты от 0.1 до 0.4:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Eccentricity = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный эксцентриситет!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите адьбедо планеты от 0 до 1:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = float.TryParse(buffer, out var val);
                                                if (result) planet.Albedo = val;
                                                else
                                                {
                                                    error.Create = $"Введено неверное альбедо!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            Console.WriteLine("Введите орбитальный период планеты в земных сутках:");
                                            buffer = Console.ReadLine();
                                            try
                                            {
                                                result = double.TryParse(buffer, out var val);
                                                if (result) planet.Period = val;
                                                else
                                                {
                                                    error.Create = $"Введен неверный орбитальный период!";
                                                    error.User();
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error.Create = $"{ex.Message}";
                                                error.User();
                                                break;
                                            }

                                            starsystem.AddPlanet(planet);

                                            break;
                                    }

                                }

                                break;
                            case ConsoleKey.D2:
                                Console.Clear();
                                starsystem.ClearPlanets();

                                Console.WriteLine("Автоматическая генерация планет");
                                                                
                                countOfPlanets = rnd.Next(1, 10);
                                
                                for (int i = 0; i < countOfPlanets; i++)
                                {
                                    planetTypeNumber = rnd.Next(1, 3);
                                    switch (planetTypeNumber)
                                    {
                                        case 1:
                                            planet = new EarthType();
                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.EarthType;
                                            planet.Name = starsystem.GetStar().Name + $"-{i+1}";
                                            planet.Albedo = rnd.Next(10, 70) * 0.01F;
                                            planet.Mass = rnd.Next(1, 100) * 0.1;
                                            planet.Radius = rnd.Next(7, 20) * 0.1F;
                                            planet.Eccentricity = rnd.Next(5, 90) * 0.001;
                                            if (i == 0)
                                            {
                                                planet.Distance = (starsystem.GetStar().Radius * rnd.Next(10, 15) * 100000);
                                            }
                                            else
                                            {
                                                planet.Distance = starsystem.GetPlanets()[i - 1].Distance * (rnd.Next(20, 30) * 0.1);
                                            }
                                            planet.Period = planet.Distance * 0.000001;
                                            planet.SetSatelites(new List<Satelite>());

                                            starsystem.AddPlanet(planet);
                                            break;
                                        case 2:
                                            planet = new Giant();
                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.Giant;
                                            planet.Name = starsystem.GetStar().Name + $"-{i + 1}";
                                            planet.Albedo = rnd.Next(10, 70) * 0.01F;
                                            planet.Mass = rnd.Next(10, 3000);
                                            planet.Radius = rnd.Next(40, 300) * 0.1F;
                                            planet.Eccentricity = rnd.Next(5, 90) * 0.001;
                                            if (i == 0)
                                            {
                                                planet.Distance = (starsystem.GetStar().Radius * rnd.Next(10, 15) * 100000);
                                            }
                                            else
                                            {
                                                planet.Distance = starsystem.GetPlanets()[i - 1].Distance * (rnd.Next(20, 30) / 10);
                                            }
                                            planet.Period = planet.Distance * 0.000001;
                                            planet.SetSatelites(new List<Satelite>());

                                            starsystem.AddPlanet(planet);
                                            break;
                                        case 3:
                                            planet = new Dwarf();
                                            planet.AddSat += logger.AddSat;
                                            planet.ClearSat += logger.ClearSat;

                                            planet.PType = Planet.PlanetType.Dwarf;
                                            planet.Name = starsystem.GetStar().Name + $"-{i + 1}";
                                            planet.Albedo = rnd.Next(10, 70) * 0.01F;
                                            planet.Mass = rnd.Next(10, 100) * 0.00001;
                                            planet.Radius = rnd.Next(800, 3000);
                                            planet.Eccentricity = rnd.Next(5, 90) * 0.001;
                                            if (i == 0)
                                            {
                                                planet.Distance = (starsystem.GetStar().Radius * rnd.Next(10, 15) * 100000);
                                            }
                                            else
                                            {
                                                planet.Distance = starsystem.GetPlanets()[i - 1].Distance * (rnd.Next(20, 30) / 10);
                                            }
                                            planet.Period = planet.Distance * 0.000001;
                                            planet.SetSatelites(new List<Satelite>());

                                            starsystem.AddPlanet(planet);
                                            break;
                                    }
                                }

                                break;
                        }
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();

                        Console.WriteLine("Генерация спутников планет\n");

                        if (starsystem.GetStar() == null)
                        {
                            error.Create = $"Звезда ещё не была сгенерирована!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }
                        if (starsystem.GetPlanets().Count() == 0)
                        {
                            error.Create = $"Планеты ещё не были сгенерированы!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Для ввода вручную нажмите '1'");
                        Console.WriteLine("Для автоматической генерации нажмите '2'\n");

                        sateliteParam = Console.ReadKey();

                        switch (sateliteParam.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                Console.WriteLine("Ввод спутников вручную\n");
                                
                                Console.WriteLine("Введите номер планеты, для которой хотите указать спутники:");

                                satelite = new Satelite();

                                result = int.TryParse(Console.ReadLine(), out var numb);
                                if (result && numb >= 1 && numb <= starsystem.GetPlanets().Count()) planetNumber = numb;
                                else
                                {
                                    error.Create = $"Номер планеты указан неверно!";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите количество спутников для планеты от 1 до 4:");
                                result = int.TryParse(Console.ReadLine(), out var satCount);
                                if (result && satCount >= 1 && satCount <= 4) countOfSatelites = satCount;
                                else
                                {
                                    error.Create = $"Количество спутников указано неверно!";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите название планеты:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    satelite.Name = buffer;
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите массу спутника в массах Земли (как минимум в 10 раз меньше массы планеты):");
                                Console.WriteLine($"Масса планеты: {Math.Round(starsystem.GetPlanets()[planetNumber].Mass, 4)}");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var parsedRes);
                                    if (result && parsedRes <= (starsystem.GetPlanets()[planetNumber].Mass * 0.1)) satelite.Mass = parsedRes;
                                    else
                                    {
                                        error.Create = $"Введена неверная масса!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите радиус спутника в от 400 до 3000 км:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var parsedRes);
                                    if (result) satelite.Radius = parsedRes;
                                    else
                                    {
                                        error.Create = $"Введен неверный радиус!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите расстояние от планеты до спутника в км:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var parsedRes);
                                    if (result) satelite.Distance = parsedRes;
                                    else
                                    {
                                        error.Create = $"Введено неверное расстояние!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                Console.WriteLine("Введите орбитальный период спутника в земных сутках:");
                                buffer = Console.ReadLine();
                                try
                                {
                                    result = double.TryParse(buffer, out var parsedRes);
                                    if (result) satelite.Period = parsedRes;
                                    else
                                    {
                                        error.Create = $"Введен неверный орбитальный период!";
                                        error.User();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Create = $"{ex.Message}";
                                    error.User();
                                    break;
                                }

                                starsystem.GetPlanets()[planetNumber].AddSatelite(satelite);

                                break;
                            case ConsoleKey.D2:
                                foreach (Planet systemPlanet in starsystem.GetPlanets())
                                {
                                    systemPlanet.ClearSatelites();
                                    countOfSatelites = rnd.Next(1, 4);
                                    for (int i = 1; i <= countOfSatelites; i++)
                                    {
                                        satelite = new Satelite();

                                        satelite.Name = systemPlanet.Name + $"-{i}";
                                        satelite.Mass = systemPlanet.Mass * rnd.Next(1, 10) * 0.01;
                                        satelite.Radius = rnd.Next(400, 3000);
                                        if (i == 1)
                                        {
                                            satelite.Distance = (systemPlanet.Radius * rnd.Next(15, 30) * 2000);
                                        }
                                        else
                                        {
                                            satelite.Distance = systemPlanet.GetSatelites()[i - 2].Distance * (rnd.Next(10, 20) * 0.1);
                                        }
                                        satelite.Period = satelite.Distance * 0.0001;

                                        systemPlanet.AddSatelite(satelite);
                                    }
                                }

                                break;
                        }

                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Console.WriteLine("Список землеподобных объектов\n");
                        newCollection.Clear();

                        if (starsystem.GetPlanets().Count() == 0)
                        {
                            error.Create = $"Планеты ещё не были сгенерированы!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }
                        foreach (Planet planet1 in starsystem.GetPlanets())
                        {
                            if(planet1.PType == Planet.PlanetType.EarthType)
                            {
                                newCollection.Add(planet1);
                            }
                            if (planet1.GetSatelites().Count() == 0) continue;
                            else
                            {
                                foreach(Satelite satelite1 in planet1.GetSatelites())
                                {
                                    newCollection.Add(satelite1);
                                }
                            }
                        }

                        foreach (IEarthLike obj in newCollection)
                        {
                            Console.WriteLine($"{obj.GetEarthLikeName()} коэффициент землеподобия: {Math.Round(obj.GetEarthLikeCoefficient(), 4)}");
                        }
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();

                        IFlyProgress<SpaceShip, Rocket> fly = new FlyProgress();
                        Rocket rocket = fly.MoveOn(500, "Союз", "Успешно запущена");
                        Console.WriteLine($"Ракета {rocket.Name} Масса: {rocket.Mass} Статус: {rocket.Status}");
                        fly.StartFly(new SpaceShip(300, "Пионер", "Взорвался при взлёте"));

                        IFlyProgress<SpaceShip, SpaceShip> progress = new FlyProgress();
                        SpaceShip spaceShip = progress.MoveOn(100, "Аполлон", "Миссия успешно завершена");
                        progress.StartFly(spaceShip);

                        IFlyProgress<Rocket, Rocket> flyProgress = new FlyProgress();
                        Rocket spaceRocket = flyProgress.MoveOn(700, "Протон", "Миссия успешно завершена");
                        flyProgress.StartFly(spaceRocket);

                        Console.ReadKey();
                        break;
                    case ConsoleKey.D7:
                        Console.Clear();
                        
                        if (sortPar == "название" || sortPar == "НАЗВАНИЕ" || sortPar == "Название" || sortPar == "коэффициент" || sortPar == "Коэффициент" || sortPar == "КОЭФФИЦИЕНТ")
                        {
                            foreach (IEarthLike obj in newCollection.SortCollection(sortPar))
                            {
                                WriteSomething(obj.GetEarthLikeCoefficient(), obj.GetEarthLikeName(), FirstPattern);
                            }
                        }
                        else
                        {
                            error.Create = $"Параметр сортировки указан неверно!";
                            error.User();
                            Console.ReadKey();
                            break;
                        }

                        Console.ReadKey();
                        break;
                }
            }
            while (keyValue.Key != ConsoleKey.Escape);
        }
    }
}
