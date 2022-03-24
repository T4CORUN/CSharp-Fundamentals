using System;

namespace GradeBook
{
	public class Statistics
	{

		public double AverageValue
		{
			get
			{
				if ( Count > 0 )
				{
					return Sum / Count;
				}
				else 
				{
					return 0.0;
				}
			}
		}

		private double Sum;
		private int Count;

		public double MinValue;

		public double MaxValue;

		public char LetterGrade
		{
			get
			{
				switch(AverageValue)
				{
					case var d when d >= 90.0:
						return 'A';
					case var d when d >= 80.0:
						return 'B';
					case var d when d >= 70.0:
						return 'C';
					case var d when d >= 60.0:
						return 'D';
					default:
						return 'F';
				}
			}
		}

		public Statistics()
		{

			this.Sum = 0.0;
			this.Count = 0;
			this.MinValue = double.MaxValue;
			this.MaxValue = double.MinValue;
		}

		public void Add(double Number)
		{
			Sum += Number;
			MinValue = Math.Min(Number, MinValue);
			MaxValue = Math.Max(Number, MaxValue);
			Count++;
		}

		public void PostCleanup()
		{

			if(MinValue == double.MaxValue)
			{
				MinValue = 0.0;
			}

			if(MaxValue == double.MinValue)
			{
				MaxValue = 0.0;
			}
		}
	}
}