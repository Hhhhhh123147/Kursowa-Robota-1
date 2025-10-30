using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using PostalSystem.Models;

namespace PostalSystem
{
    class Program
    {
        static List<Newspaper> newspapers = new List<Newspaper>();
        static List<PrintingHouse> printingHouses = new List<PrintingHouse>();
        static List<PostOffice> postOffices = new List<PostOffice>();

        static string[] paths = {
            "Data/List1.txt",
            "Data/List2.txt",
            "Data/List3.txt"
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("Завантаження даних...\n");
            LoadData();

            if (newspapers.Count == 0 || printingHouses.Count == 0 || postOffices.Count == 0)
            {
                Console.WriteLine("ПОМИЛКА: Не вдалося завантажити дані!");
                Console.WriteLine("Перевірте наявність файлів у папці Data/");
                Console.ReadKey();
                return;
            }

            RunMenu();
        }

        static void LoadData()
        {
            try
            {
                // Завантаження газет
                if (File.Exists(paths[0]))
                {
                    using (StreamReader reader = new StreamReader(paths[0], Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == 6)
                            {
                                newspapers.Add(new Newspaper(
                                    Convert.ToInt32(parts[0]),
                                    parts[1].Trim(),
                                    parts[2].Trim(),
                                    parts[3].Trim(),
                                    Convert.ToInt32(parts[4]),
                                    Convert.ToDouble(parts[5])
                                ));
                            }
                        }
                    }
                    Console.WriteLine($"✓ Завантажено газет: {newspapers.Count}");
                }

                // Завантаження друкарень
                if (File.Exists(paths[1]))
                {
                    using (StreamReader reader = new StreamReader(paths[1], Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == 3)
                            {
                                printingHouses.Add(new PrintingHouse(
                                    Convert.ToInt32(parts[0]),
                                    parts[1].Trim(),
                                    parts[2].Trim()
                                ));
                            }
                        }
                    }
                    Console.WriteLine($"✓ Завантажено друкарень: {printingHouses.Count}");
                }

                // Завантаження поштових відділень
                if (File.Exists(paths[2]))
                {
                    using (StreamReader reader = new StreamReader(paths[2], Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == 5)
                            {
                                postOffices.Add(new PostOffice(
                                    Convert.ToInt32(parts[0]),
                                    parts[1].Trim(),
                                    parts[2].Trim(),
                                    Convert.ToInt32(parts[3]),
                                    Convert.ToInt32(parts[4])
                                ));
                            }
                        }
                    }
                    Console.WriteLine($"✓ Завантажено записів пошти: {postOffices.Count}");
                }

                Console.WriteLine("\nДані успішно завантажено!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ПОМИЛКА при завантаженні: {ex.Message}");
            }
        }

        static void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("\n⚠ Це не число! Натисніть Enter...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Query1_AllNewspapers();
                        break;
                    case 2:
                        Query2_PrintingHousesWithNewspapers();
                        break;
                    case 3:
                        Query3_PostOfficesInfo();
                        break;
                    case 4:
                        Query4_PrintingHousesByNewspaper();
                        break;
                    case 5:
                        Query5_EditorByPrintingHouse();
                        break;
                    case 6:
                        Query6_TotalCirculationCost();
                        break;
                    case 7:
                        Query7_PostOfficeWithMostNewspapers();
                        break;
                    case 8:
                        Query8_PostOfficesByNewspaper();
                        break;
                    case 9:
                        Query9_PostOfficeWithMaxCost();
                        break;
                    case 0:
                        Console.WriteLine("Вихід з програми. До побачення!");
                        return;
                    default:
                        Console.WriteLine("⚠ Невірний номер! Натисніть Enter...");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                }

                Console.WriteLine("\n" + new string('─', 60));
                Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   СИСТЕМА РОЗПОДІЛУ ГАЗЕТ ПО ПОШТОВИХ ВІДДІЛЕННЯХ        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  1 → Відомості про газети");
            Console.WriteLine("  2 → Відомості про друкарні");
            Console.WriteLine("  3 → Відомості про поштові відділення");
            Console.WriteLine("  4 → У яких друкарнях друкуються газети (пошук)");
            Console.WriteLine("  5 → Прізвище редактора газети у друкарні");
            Console.WriteLine("  6 → Загальна вартість тиражу заданої газети");
            Console.WriteLine("  7 → Відділення з найбільшою кількістю газет");
            Console.WriteLine("  8 → До яких відділень надходить дана газета");
            Console.WriteLine("  9 → Відділення з максимальною вартістю газет");
            Console.WriteLine();
            Console.WriteLine("  0 → Вихід");
            Console.WriteLine();
            Console.WriteLine(new string('─', 60));
            Console.Write("Ваш вибір: ");
        }

        // ============ ЗАПИТ 1: Всі газети ============
        static void Query1_AllNewspapers()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ВІДОМОСТІ ПРО ГАЗЕТИ                         ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            int counter = 1;
            foreach (var newspaper in newspapers.OrderBy(n => n.Name))
            {
                Console.WriteLine($"[{counter}] {newspaper.Name}");
                Console.WriteLine($"    Індекс: {newspaper.Index}");
                Console.WriteLine($"    Редактор: {newspaper.Editor}");
                Console.WriteLine($"    Тираж: {newspaper.Circulation:N0} екземплярів");
                Console.WriteLine($"    Ціна: {newspaper.Price:F2} грн");
                Console.WriteLine(new string('─', 60));
                counter++;
            }
            
            Console.WriteLine($"\nВсього газет: {newspapers.Count}");
        }

        // ============ ЗАПИТ 2: Друкарні та газети ============
        static void Query2_PrintingHousesWithNewspapers()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          ДРУКАРНІ ТА ГАЗЕТИ, ЯКІ ВОНИ ДРУКУЮТЬ           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            var query = from ph in printingHouses
                        join po in postOffices on ph.Key equals po.PrintingHouseKey
                        join n in newspapers on po.NewspaperKey equals n.Key
                        group n by new { ph.Key, ph.Name, ph.Address } into g
                        select new
                        {
                            g.Key.Name,
                            g.Key.Address,
                            Newspapers = g.Select(n => n.Name).Distinct()
                        };

            int counter = 1;
            foreach (var item in query)
            {
                Console.WriteLine($"[{counter}] {item.Name}");
                Console.WriteLine($"    Адреса: {item.Address}");
                Console.WriteLine($"    Друкують газети:");
                foreach (var newspaper in item.Newspapers)
                {
                    Console.WriteLine($"      • {newspaper}");
                }
                Console.WriteLine(new string('─', 60));
                counter++;
            }
        }

        // ============ ЗАПИТ 3: Поштові відділення ============
        static void Query3_PostOfficesInfo()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             ВІДОМОСТІ ПРО ПОШТОВІ ВІДДІЛЕННЯ              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            var query = from po in postOffices
                        group po by new { po.Number, po.Address } into g
                        select new
                        {
                            g.Key.Number,
                            g.Key.Address,
                            NewspapersCount = g.Count()
                        };

            int counter = 1;
            foreach (var item in query.OrderBy(x => x.Number))
            {
                Console.WriteLine($"[{counter}] Відділення №{item.Number}");
                Console.WriteLine($"    Адреса: {item.Address}");
                Console.WriteLine($"    Кількість найменувань газет: {item.NewspapersCount}");
                
                // Показуємо які газети
                var newspapersInOffice = from po in postOffices
                                        where po.Number == item.Number
                                        join n in newspapers on po.NewspaperKey equals n.Key
                                        select n.Name;
                
                Console.WriteLine($"    Газети:");
                foreach (var name in newspapersInOffice.Distinct())
                {
                    Console.WriteLine($"      • {name}");
                }
                
                Console.WriteLine(new string('─', 60));
                counter++;
            }
        }

        // ============ ЗАПИТ 5: Редактор у друкарні ============
static void Query5_EditorByPrintingHouse()
{
    Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
    Console.WriteLine("║         РЕДАКТОРИ ГАЗЕТ У ЗАЗНАЧЕНІЙ ДРУКАРНІ             ║");
    Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
    
    Console.Write("Введіть назву друкарні (або частину): ");
    string searchName = Console.ReadLine();
    
    Console.WriteLine($"\n🔍 Пошук для: '{searchName}'\n");
    
    var query = from ph in printingHouses
                where ph.Name.ToLower().Contains(searchName.ToLower())
                join po in postOffices on ph.Key equals po.PrintingHouseKey
                join n in newspapers on po.NewspaperKey equals n.Key
                select new { PrintingHouseName = ph.Name, ph.Address, NewspaperName = n.Name, n.Editor };

    var results = query.Distinct().ToList();
    
    if (results.Count == 0)
    {
        Console.WriteLine("❌ Друкарню не знайдено.");
        return;
    }

    var grouped = results.GroupBy(r => new { r.PrintingHouseName, r.Address });
    
    foreach (var group in grouped)
    {
        Console.WriteLine($"🏭 Друкарня: {group.Key.PrintingHouseName}");
        Console.WriteLine($"   Адреса: {group.Key.Address}");
        Console.WriteLine($"   Друкують {group.Count()} газет(и):\n");
        
        int counter = 1;
        foreach (var item in group)
        {
            Console.WriteLine($"   [{counter}] {item.NewspaperName}");
            Console.WriteLine($"       Редактор: {item.Editor}");
            counter++;
        }
        Console.WriteLine(new string('─', 60));
    }
}

        // ============ ЗАПИТ 5: Редактор у друкарні ============
        static void Query5_EditorByPrintingHouse()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         РЕДАКТОРИ ГАЗЕТ У ЗАЗНАЧЕНІЙ ДРУКАРНІ             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            Console.Write("Введіть назву друкарні (або частину): ");
            string searchName = Console.ReadLine();
            
            Console.WriteLine($"\n🔍 Пошук для: '{searchName}'\n");
            
            var query = from ph in printingHouses
                        where ph.Name.ToLower().Contains(searchName.ToLower())
                        join po in postOffices on ph.Key equals po.PrintingHouseKey
                        join n in newspapers on po.NewspaperKey equals n.Key
                        select new { PrintingHouseName = ph.Name, ph.Address, NewspaperName = n.Name, n.Editor };

            var results = query.Distinct().ToList();
            
            if (results.Count == 0)
            {
                Console.WriteLine("❌ Друкарню не знайдено.");
                return;
            }

            var grouped = results.GroupBy(r => new { r.Name, r.Address });
            
            foreach (var group in grouped)
            {
                Console.WriteLine($"🏭 Друкарня: {group.Key.Name}");
                Console.WriteLine($"   Адреса: {group.Key.Address}");
                Console.WriteLine($"   Друкують {group.Count()} газет(и):\n");
                
                int counter = 1;
                foreach (var item in group)
                {
                    Console.WriteLine($"   [{counter}] {item.Name}");
                    Console.WriteLine($"       Редактор: {item.Editor}");
                    counter++;
                }
                Console.WriteLine(new string('─', 60));
            }
        }

        // ============ ЗАПИТ 6: Вартість тиражу ============
        static void Query6_TotalCirculationCost()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ЗАГАЛЬНА ВАРТІСТЬ ТИРАЖУ ГАЗЕТИ                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            Console.Write("Введіть назву газети: ");
            string searchName = Console.ReadLine();
            
            Console.WriteLine($"\n🔍 Пошук для: '{searchName}'\n");
            
            var query = from n in newspapers
                        where n.Name.ToLower().Contains(searchName.ToLower())
                        select new 
                        { 
                            n.Name, 
                            n.Circulation, 
                            n.Price, 
                            TotalCost = n.Circulation * n.Price 
                        };

            var results = query.ToList();
            
            if (results.Count == 0)
            {
                Console.WriteLine("❌ Газету не знайдено.");
                return;
            }

            foreach (var item in results)
            {
                Console.WriteLine($"📰 Газета: {item.Name}");
                Console.WriteLine($"   Тираж: {item.Circulation:N0} екземплярів");
                Console.WriteLine($"   Ціна за екземпляр: {item.Price:F2} грн");
                Console.WriteLine($"   ════════════════════════════════");
                Console.WriteLine($"   💰 ЗАГАЛЬНА ВАРТІСТЬ: {item.TotalCost:N2} грн");
                Console.WriteLine(new string('─', 60));
            }
        }

        // ============ ЗАПИТ 7: Відділення з найбільшою кількістю ============
        static void Query7_PostOfficeWithMostNewspapers()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      ВІДДІЛЕННЯ З НАЙБІЛЬШОЮ КІЛЬКІСТЮ ГАЗЕТ              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            var query = from po in postOffices
                        group po by new { po.Number, po.Address } into g
                        orderby g.Count() descending
                        select new
                        {
                            g.Key.Number,
                            g.Key.Address,
                            Count = g.Count()
                        };

            var result = query.FirstOrDefault();
            
            if (result != null)
            {
                Console.WriteLine($"🏆 ЛІДЕР:");
                Console.WriteLine($"   Відділення №{result.Number}");
                Console.WriteLine($"   Адреса: {result.Address}");
                Console.WriteLine($"   Кількість газет: {result.Count}");
                Console.WriteLine();
                
                var newspapersInOffice = from po in postOffices
                                        where po.Number == result.Number
                                        join n in newspapers on po.NewspaperKey equals n.Key
                                        select new { n.Name, n.Price };
                
                Console.WriteLine($"   Газети у цьому відділенні:");
                int counter = 1;
                foreach (var item in newspapersInOffice.Distinct())
                {
                    Console.WriteLine($"      {counter}. {item.Name} ({item.Price:F2} грн)");
                    counter++;
                }
            }
        }

        // ============ ЗАПИТ 8: Відділення для газети ============
        static void Query8_PostOfficesByNewspaper()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      ДО ЯКИХ ВІДДІЛЕНЬ НАДХОДИТЬ ДАНА ГАЗЕТА             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            Console.Write("Введіть назву газети: ");
            string searchName = Console.ReadLine();
            
            Console.WriteLine($"\n🔍 Пошук для: '{searchName}'\n");
            
            var query = from n in newspapers
                        where n.Name.ToLower().Contains(searchName.ToLower())
                        join po in postOffices on n.Key equals po.NewspaperKey
                        select new { n.Name, po.Number, po.Address };

            var results = query.Distinct().ToList();
            
            if (results.Count == 0)
            {
                Console.WriteLine("❌ Газету не знайдено або вона не надходить до жодного відділення.");
                return;
            }

            var grouped = results.GroupBy(r => r.Name);
            
            foreach (var group in grouped)
            {
                Console.WriteLine($"📰 Газета: {group.Key}");
                Console.WriteLine($"   Надходить до {group.Count()} відділення(нь):\n");
                
                int counter = 1;
                foreach (var item in group)
                {
                    Console.WriteLine($"   [{counter}] Відділення №{item.Number}");
                    Console.WriteLine($"       Адреса: {item.Address}");
                    counter++;
                }
                Console.WriteLine(new string('─', 60));
            }
        }

        // ============ ЗАПИТ 9: Відділення з максимальною вартістю ============
        static void Query9_PostOfficeWithMaxCost()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     ВІДДІЛЕННЯ З МАКСИМАЛЬНОЮ ВАРТІСТЮ ГАЗЕТ              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            var query = from po in postOffices
                        join n in newspapers on po.NewspaperKey equals n.Key
                        group new { po, n } by new { po.Number, po.Address } into g
                        select new
                        {
                            g.Key.Number,
                            g.Key.Address,
                            TotalCost = g.Sum(x => x.n.Price * x.n.Circulation),
                            Newspapers = g.Select(x => new { x.n.Name, x.n.Price, x.n.Circulation }).Distinct()
                        };

            var result = query.OrderByDescending(x => x.TotalCost).FirstOrDefault();
            
            if (result != null)
            {
                Console.WriteLine($"🏆 ЛІДЕР ЗА ВАРТІСТЮ:");
                Console.WriteLine($"   Відділення №{result.Number}");
                Console.WriteLine($"   Адреса: {result.Address}");
                Console.WriteLine($"   💰 Загальна вартість: {result.TotalCost:N2} грн\n");
                
                Console.WriteLine($"   Газети у цьому відділенні:");
                int counter = 1;
                foreach (var newspaper in result.Newspapers.OrderByDescending(n => n.Price * n.Circulation))
                {
                    double cost = newspaper.Price * newspaper.Circulation;
                    Console.WriteLine($"      {counter}. {newspaper.Name}");
                    Console.WriteLine($"         Вартість: {cost:N2} грн (тираж: {newspaper.Circulation:N0}, ціна: {newspaper.Price:F2})");
                    counter++;
                }
            }
        }
    }
}