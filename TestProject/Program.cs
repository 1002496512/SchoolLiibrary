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
using LibraryWebService.ORM;
using System.Data;
using LibraryWebService;

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
            await Task.Delay(1000);
            Console.WriteLine("Pour oil into the pan.");
            for (int i = 1; i <= count; i++)
                Console.WriteLine("Break the egg into pan");
            Console.WriteLine("Frying eggs");
            await Task.Delay(7000);
            Console.WriteLine("Put eggs on a plate");
            Console.WriteLine("Fried eggs are ready");
        }
        static async Task GetBreakFastAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Start making cofee");
            Task cofee = PrepareCofeeAsync();
            Console.WriteLine("Start making toasts");
            Task toas = PrepareToastAsync(2);
            Console.WriteLine("Start making eggs");
            Task aggs = PrepareAggsAsync(2);
            await Task.WhenAll(cofee, toas, aggs);
            Console.WriteLine("Breakfast is ready");
            stopwatch.Stop();
            Console.WriteLine($"Time taken to prepare breakfast: {stopwatch.ElapsedMilliseconds} ms");
        }
        static async Task Main(string[] args)
        {
            TestCreateAuthor();
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

        static void TestDbContext()
        {
            DbContext dbContext = new DbContext();
            dbContext.OpenConnection();
            dbContext.ChangeDb("delete from cities where cityId=1273");
            dbContext.CloseConnection();
        }


        static void TestCreateAuthor()
        {
            DbContext dbContext = new DbContext();
            dbContext.OpenConnection();
            string sql = "select * from Authors";
            IDataReader reader = dbContext.Select(sql);
            List<Author> list = new List<Author>();
            UOWModelCreators uOWModelCreators = new UOWModelCreators();
            while (reader.Read() == true)
            {
                Author author = uOWModelCreators.AuthorCreator.CreateModel(reader);
                list.Add(author);
            }
            dbContext.CloseConnection();
            foreach (Author author in list)
            {
                Console.WriteLine($"{author.AuthorFirstName} {author.AuthorLastName}");

            }
        }


    }
}

       
   

     

