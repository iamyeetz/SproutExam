using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
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
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;
        private IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository,
                               IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public Task<int> Create(CreateEmployeeDto employee)
        {
            return _repository.Create(_mapper.Map<Employee>(employee));
        }

        public async Task<int> Delete(int id)
        {
            var record = await _repository.GetById(id).ConfigureAwait(false);
            record.IsDeleted = true;
            var deletedRecordId = await _repository.Delete(_mapper.Map<Employee>(record)).ConfigureAwait(false);
            return deletedRecordId;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var employeeList = await _repository.GetAll().ConfigureAwait(false);
            return employeeList.Select(x => _mapper.Map<EmployeeDto>(x));

        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var record = await _repository.GetById(id).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(record);
        }

        public async Task<EmployeeDto> Update(EditEmployeeDto employee)
        {
            var record = await _repository.Update(_mapper.Map<Employee>(employee)).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(record);
        }
    }
}
