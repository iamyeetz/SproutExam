using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastructure.Interface
{
    public interface IEmployeeTypeRepository
    {
        Task<EmployeeType> Get(int id);
    }
}
