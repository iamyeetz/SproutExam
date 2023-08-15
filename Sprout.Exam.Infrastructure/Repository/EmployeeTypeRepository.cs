using Sprout.Exam.Infrastructure.Interface;
using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastructure.Repository
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        SproutExamDbContext _context;
        public EmployeeTypeRepository(SproutExamDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeType> Get(int id)
        {
            var toReturn = _context.EmployeeTypes.Find(id);
            return toReturn;
        }
    }
}
