using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Infrastructure.Interface;
using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SproutExamDbContext _context;
        public EmployeeRepository(SproutExamDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> Create(Employee employee)
        {
            var id = _context.Add(employee).Entity.Id;
            _context.SaveChanges();
            return id;
        }

        public async Task<int> Delete(Employee employee)
        {
            var id = _context.Employees.Update(employee).Entity.Id;
            _context.SaveChanges();
            return id;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var record = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
            return record;
        }

        public async Task<Employee> GetById(int id)
        {
            var record = await _context.Employees.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            return record;
        }

        public async Task<Employee> Update(Employee employee)
        {
            var record = _context.Employees.Update(employee).Entity;
            _context.SaveChanges();
            return record;
        }
    }
}
