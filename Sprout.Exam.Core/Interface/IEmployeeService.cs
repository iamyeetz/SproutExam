using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Core.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(int id);
        Task<int> Create(CreateEmployeeDto employee);
        Task<EmployeeDto> Update(EditEmployeeDto employee);
        Task<int> Delete(int id);
    }
}
