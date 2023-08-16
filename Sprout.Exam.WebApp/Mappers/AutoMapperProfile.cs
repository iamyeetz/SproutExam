using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(a => a.Birthdate, b => b.MapFrom(c => c.Birthdate.ToString("yyyy-MM-dd")))
                .ForMember(a => a.TypeId, b => b.MapFrom(c => c.EmployeeTypeId));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(a => a.Birthdate, b => b.MapFrom(c => c.Birthdate.ToString("yyyy-MM-dd")))
                .ForMember(a => a.EmployeeTypeId, b => b.MapFrom(c => c.TypeId))
                .ForMember(a => a.Tin, b => b.MapFrom(c => c.Tin))
                .ForMember(a => a.FullName, b => b.MapFrom(c => c.FullName));

            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(a => a.EmployeeTypeId, b => b.MapFrom(c => c.TypeId))
                .ForMember(a => a.Tin, b => b.MapFrom(c => c.Tin))
                .ForMember(a => a.Birthdate, b => b.MapFrom(c => c.Birthdate.ToString("yyyy-MM-dd")))
                .ForMember(a => a.FullName, b => b.MapFrom(c => c.FullName));

        }
    }
}
