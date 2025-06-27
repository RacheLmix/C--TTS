using AutoMapper;
using Days8.Models;
using Days8.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, TransactionReadDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<Transaction, TransactionSummaryDto>();
    }
} 