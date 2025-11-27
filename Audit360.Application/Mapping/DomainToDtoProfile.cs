using Audit360.Application.Features.Dto.Audits;
using Audit360.Application.Features.Dto.Findings;
using Audit360.Application.Features.Dto.FindingSeverities;
using Audit360.Application.Features.Dto.FindingTypes;
using Audit360.Application.Features.Dto.FollowUps;
using Audit360.Application.Features.Dto.FollowUpStatuses;
using Audit360.Application.Features.Dto.Responsibles;
using Audit360.Application.Features.Dto.Roles;
using Audit360.Application.Features.Dto.Statuses;
using Audit360.Application.Features.Dto.Users;
using Audit360.Domain.Entities;
using AutoMapper;

namespace Audit360.Application.Mapping
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<Audit, AuditReadDto>().ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status.Id));
            CreateMap<Responsible, ResponsibleReadDto>();
            CreateMap<Finding, FindingReadDto>().ForMember(dest => dest.FindingTypeId, opt => opt.MapFrom(src => src.Type.Id))
                                               .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.Severity.Id));
            CreateMap<FollowUp, FollowUpReadDto>().ForMember(dest => dest.FollowUpStatusId, opt => opt.MapFrom(src => src.Status.Id));
            CreateMap<Role, RoleReadDto>();
            CreateMap<AuditStatus, AuditStatusReadDto>();
            CreateMap<FindingType, FindingTypeReadDto>();
            CreateMap<FindingSeverity, FindingSeverityReadDto>();
            CreateMap<FollowUpStatus, FollowUpStatusReadDto>();
        }
    }
}
