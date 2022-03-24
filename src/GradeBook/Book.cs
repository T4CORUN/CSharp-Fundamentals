using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
	public delegate void GradeAddedDelegate(object sender, EventArgs args);

	public class NamedObject
	{
		public NamedObject(string name)
		{
			Name = name;
		}

		public string Name
		{
			get;
			set;
		}
	}

	public interface IBook
	{
		void AddGrade(double grade);
		string Name { get; }
		event GradeAddedDelegate GradeAdded;
		Statistics GetStatistics();
	}

	public abstract class Book : NamedObject, IBook
	{
		public Book(string name) : base(name)
		{
		}

		public abstract event GradeAddedDelegate GradeAdded;

		public abstract void AddGrade(double Grade);
		public virtual Statistics GetStatistics()
		{
			throw new NotImplementedException();
		}
	}

	public class DiskBook : Book
	{
		public DiskBook(string name) : base(name)
		{
		}

		public override event GradeAddedDelegate GradeAdded;

		public override void AddGrade(double Grade)
		{
			using(var writer = File.AppendText($"{Name}.txt"))
			{
				writer.WriteLine(Grade);
				if(GradeAdded != null)
				{
					GradeAdded(this, new EventArgs());
				}
			}
		}

		public override Statistics GetStatistics()
		{
			var result = new Statistics();

			using(var reader = File.OpenText($"{Name}.txt"))
			{
				var line = reader.ReadLine();
			
				while( line != null)
				{
					var number = double.Parse(line);
					result.Add(number);
					line = reader.ReadLine();
				}
			}

			result.PostCleanup();

			return result;
		}
	}

	public class InMemoryBook : Book
	{
		public InMemoryBook(string name) : base(name)
		{
			grades = new List<double>();
		}

		public void AddGrade(char letter)
		{
			//Basic switch statement
			switch(letter)
			{
				case 'A':
					AddGrade(90);
					break;
				case 'B':
					AddGrade(80);
					break;
				case 'C':
					AddGrade(70);
					break;
				case 'D':
					AddGrade(60);
					break;
				case 'F':
					AddGrade(50);
					break;
				default:
					AddGrade(0);
					break;
			}
		}

		public override void AddGrade(double Grade)
		{
			if(Grade >= 0.0 && Grade <= 100.0)
			{
				grades.Add(Grade);
				if(GradeAdded != null)
				{
					GradeAdded(this, new EventArgs());
				}
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(Grade)}: {Grade}");
			}
		}

		public override Statistics GetStatistics()
		{
			var result = new Statistics();

			foreach(var number in grades)
			{
				result.Add(number);

			}

			/*
			//do while example
			var index = 0;

			if ( index  < grades.Count )
			{
				do
				{
					result.Add(grades[index]);
					index += 1;
				} while ( index < grades.Count);
			}
			*/

			/*
			//for loop		
			for(var index = 0; index < grades.Count; index++)
			{
				result.Add(grades[Index]);
			}
			*/

			result.PostCleanup();

			return result;
		}

		public override event GradeAddedDelegate GradeAdded;

		private List<double> grades;
		//private string name;

		public const string CATEGORY = "Science";
	}
}