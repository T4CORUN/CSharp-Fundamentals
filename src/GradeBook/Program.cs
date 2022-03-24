using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
		{
			var book = new InMemoryBook(args[0]);
			book.GradeAdded += OnGradeAdded;

			if (book.Name != null)
			{
				Console.WriteLine($"Welcome to {book.Name}'s gradebook.\n");
			}
			else
			{
				Console.WriteLine("This gradebook does not have a name.\n");
			}

		    EnterGrades(book);

            var stats = book.GetStatistics();


			if (book.Name != null)
			{
				Console.WriteLine($"\n\n{book.Name}'s Grades:");
			}
			else
			{
				Console.WriteLine("\n\nUnknown Student's Grades:");
			}

			Console.WriteLine($"The lowest grade is {stats.MinValue:N1}");
			Console.WriteLine($"The average grade is {stats.AverageValue:N1}");
			Console.WriteLine($"The highest grade is {stats.MaxValue:N1}");
			Console.WriteLine($"The letter grade is {stats.LetterGrade}");
		}

		private static void EnterGrades(IBook book)
		{
			while (true)
			{
				Console.Write("Please enter a grade or Q to quit: ");
				var input = Console.ReadLine();

				if (input.ToUpper() == "Q")
				{
					break;
				}

				try
				{
					var grade = double.Parse(input);
					book.AddGrade(grade);
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (FormatException ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					//Console.WriteLine("**");
				}
			}
		}

		public static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A Grade was Added");
        }
    }
}
