using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastructure.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<int> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<int> Delete(Employee employee);
    }
}
