using System.Threading.Channels;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;
using DTOLibrary;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;

namespace TestProject
{
    internal class Program
    {
        private static async Task PrepareCofeeAsync()
        {
            Console.WriteLine("Add water");
            Console.WriteLine("Start boiling some water ");
            await Task.Delay(2500);
            Console.WriteLine("Water is boiling.");
            Console.WriteLine("Add some coffee");
            Console.WriteLine("Add some sugar");
            Console.WriteLine("Add some water");
            Console.WriteLine("Stir your coffee");
            Console.WriteLine("Coffee drink is ready");
}

        private static async Task PrepareToastAsync(int count)
        {
            for (int i = 1; i <= count; i++)
                Console.WriteLine("Put bread into the toaster");
            Console.WriteLine("Turn on the toaster");
            Console.WriteLine("Start toasting");
            await Task.Delay(8000);
            Console.WriteLine("Toasts popped out ");
            Console.WriteLine("Spread with a butter");
            Console.WriteLine(" Spread with a jam");
            Console.WriteLine("Toast is ready");
        }

        private static async Task PrepareAggsAsync(int count)
        {
            Console.WriteLine("Heating a pan");
           await  Task.Delay(1000);
            Console.WriteLine("Pour oil into the pan.");
            for (int i = 1; i <= count; i++)
                Console.WriteLine("Break the egg into pan");
            Console.WriteLine("Frying eggs") ;
            await Task.Delay(7000);
            Console.WriteLine("Put eggs on a plate");
            Console.WriteLine("Fried eggs are ready");
        }
        static async Task GetBreakFastAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Start making cofee");
            Task cofee= PrepareCofeeAsync();
            Console.WriteLine("Start making toasts");
            Task toas= PrepareToastAsync(2);
            Console.WriteLine("Start making eggs");
            Task aggs = PrepareAggsAsync(2);
            await Task.WhenAll(cofee, toas, aggs);
            Console.WriteLine("Breakfast is ready");
            stopwatch.Stop();
            Console.WriteLine($"Time taken to prepare breakfast: {stopwatch.ElapsedMilliseconds} ms");
        }
        static async Task Main(string[] args)
        {
            TestModel();
            Console.ReadLine(); 
        }

        static void TestModel()
        {
            Author author = new Author()
            {
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                AuthorId = "3",
                AuthorPicture = "test.jpg",
                AuthorYear = 1234,
                CountryId = 2
            };
            ValidationContext context = new ValidationContext(author, null, null);

            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(author, context, results, true);
            if (isValid == true)
            {
                Console.WriteLine("The Author is correct");
            }
            else
            { 
                Console.WriteLine("The Author is not correct");
                foreach (var validationResult in results)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
            }
            }


        static void Convert()
        {
            using System;
            using System.Globalization;
            using System.IO;
            using System.Linq;
            using System.Xml.Linq;

class Program
        {
            static void Main(string[] args)
            {
                // Укажите пути к вашим файлам
                string inputTcxPath = @"C:\SwimData\workout.tcx";
                string outputCsvPath = @"C:\SwimData\swimming_laps.csv";

                if (!File.Exists(inputTcxPath))
                {
                    Console.WriteLine($"Ошибка: Файл {inputTcxPath} не найден.");
                    return;
                }

                try
                {
                    // Загружаем TCX как XML-документ
                    XDocument tcxDoc = XDocument.Load(inputTcxPath);
                    XNamespace ns = tcxDoc.Root.GetDefaultNamespace();

                    // Пространство имен для расширений (где часто хранятся данные о гребках)
                    XNamespace extensionNs = "http://garmin.com";

                    // Выбираем все отрезки (Laps)
                    var laps = tcxDoc.Descendants(ns + "Lap");

                    using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
                    {
                        // Записываем заголовки столбцов в CSV
                        writer.WriteLine("Номер бассейна,Время начала,Длительность (сек),Калории,Пульс (ср),Гребков на отрезок,Интенсивность");

                        int lapCounter = 1;

                        foreach (var lap in laps)
                        {
                            // Извлекаем базовые атрибуты и элементы отрезка
                            string startTime = (string)lap.Attribute("StartTime") ?? "";
                            double totalTime = (double?)lap.Element(ns + "TotalTimeSeconds") ?? 0.0;
                            int calories = (int?)lap.Element(ns + "Calories") ?? 0;

                            // Средний пульс на отрезке
                            var avgHeartRateEl = lap.Element(ns + "AverageHeartRateBpm");
                            int avgHeartRate = avgHeartRateEl != null ? (int?)avgHeartRateEl.Element(ns + "Value") ?? 0 : 0;

                            // Извлекаем количество гребков (в TCX они часто лежат внутри Extensions)
                            int totalStrokes = 0;
                            var extensions = lap.Element(ns + "Extensions");
                            if (extensions != null)
                            {
                                // Пытаемся найти LX (LapExtension) от Garmin или Huawei
                                var lx = extensions.Descendants().FirstOrDefault(e => e.Name.LocalName == "LX");
                                if (lx != null)
                                {
                                    totalStrokes = (int?)lx.Element(extensionNs + "Steps") ?? 0;
                                    // В некоторых плавательных профилях Steps используется как общее число гребков на отрезок
                                }
                            }

                            // Если структура другая, проверяем стандартный Cadence (может хранить частоту или гребки)
                            if (totalStrokes == 0)
                            {
                                totalStrokes = (int?)lap.Element(ns + "Cadence") ?? 0;
                            }

                            string intensity = (string)lap.Element(ns + "Intensity") ?? "Active";

                            // Записываем строку данных
                            writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
                                "{0},{1},{2},{3},{4},{5},{6}",
                                lapCounter, startTime, totalTime, calories, avgHeartRate, totalStrokes, intensity));

                            lapCounter++;
                        }
                    }

                    Console.WriteLine($"Успешно! Данные о гребках обработаны и сохранены в: {outputCsvPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка при обработке файла: {ex.Message}");
                }
            }
        }

    }
}
       
    }

     

