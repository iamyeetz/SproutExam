using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Infrastructure.Models;
using System;

namespace Sprout.Exam.WebApp.Models
{
    public abstract class BaseEmployee 
    {
        public abstract decimal ComputeSalary();
    }

    public class RegularEmployee : BaseEmployee
    {
        public decimal MonthlySalary  { get; set; }
        public decimal AbsentCount { get; set; }
        public decimal TaxRate { get; set; }

        public override decimal ComputeSalary()
        {
            decimal dailyRate = MonthlySalary / 22;
            decimal salary = MonthlySalary - (MonthlySalary * (TaxRate / 100));
            salary = salary - (AbsentCount * dailyRate);
            return Math.Round(salary,2);
        }
    }
    public class ContractualEmployee : BaseEmployee
    {
        public decimal DailyRate { get; set; }
        public decimal WorkDayCount { get; set; }
        public override decimal ComputeSalary()
        {
            decimal salary = DailyRate * WorkDayCount;
            return Math.Round(salary,2);
        }
    }

    public class ProbationaryEmployee : BaseEmployee
    {
        public override decimal ComputeSalary()
        {
            throw new NotImplementedException();
        }
    }



}
