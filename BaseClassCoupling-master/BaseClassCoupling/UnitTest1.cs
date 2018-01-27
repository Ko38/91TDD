using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BaseClassCoupling
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void calculate_LessThanOneYearEmployee_Bonus()
        {
            //if my monthly salary is 1200, working year is 0.5, my bonus should be 600
            var lessThanOneYearEmployee = new MockLessThanOneYearEmployee()
            {
                Id = 91,
                //Console.WriteLine("your StartDate should be :{0}", DateTime.Today.AddDays(365/2*-1));
                StartWorkingDate = new DateTime(2017, 7, 29)
            };

			var actual = lessThanOneYearEmployee.GetYearlyBonus();
            Assert.AreEqual(600, (int)actual);
        }
    }

	public class MockLessThanOneYearEmployee : LessThanOneYearEmployee
	{
		protected override decimal GetMonthlySalary()
		{
			return 1200;
		}
	}

    public abstract class Employee
    {
        public DateTime StartWorkingDate { get; set; }

        protected virtual decimal GetMonthlySalary()
        {
            DebugHelper.Info($"query monthly salary id:{Id}");
            return SalaryRepo.Get(this.Id);
        }

        public abstract decimal GetYearlyBonus();

        public int Id { get; set; }
    }

    public class LessThanOneYearEmployee : Employee
    {
        public override decimal GetYearlyBonus()
        {
            var salary = this.GetMonthlySalary();
            DebugHelper.Info($"id:{Id}, his monthly salary is:{salary}");
            return Convert.ToDecimal(this.WorkingYear()) * salary;
        }

		protected decimal YearlyBonus()
		{
			return Convert.ToDecimal(this.WorkingYear()) * this.GetMonthlySalary();
		}

		protected decimal ConvertToDecimal(double workingYear, decimal salary)
		{
			return Convert.ToDecimal(workingYear) * salary;
		}

        protected double WorkingYear()
        {
            var year = (DateTime.Now - StartWorkingDate).TotalDays / 365;
            return year > 1 ? 1 : year;
        }
    }

    public class DebugHelper
    {
        public static void Info(string message)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    public class SalaryRepo
    {
        internal static decimal Get(int id)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }
}