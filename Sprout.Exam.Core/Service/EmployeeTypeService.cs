using Sprout.Exam.Core.Interface;
using Sprout.Exam.Infrastructure.Interface;
using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Core.Service
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        IEmployeeTypeRepository _repository;
        public EmployeeTypeService(IEmployeeTypeRepository repository)
        {
            _repository = repository;
        }
        public Task<EmployeeType> Get(int id)
        {
           return _repository.Get(id);
        }
    }
}
