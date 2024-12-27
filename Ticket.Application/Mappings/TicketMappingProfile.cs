using Ticket.Domain.Entity;
using Ticket.Application.Models;
using AutoMapper;

namespace Ticket.Application.Mappings;

public class TicketMappingProfile: Profile
{
    public TicketMappingProfile()
    {
        CreateMap<TicketModel, Tickets>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            
    }
    
}