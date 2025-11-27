using AutoMapper;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Dto.Users;
using Audit360.Application.Features.Dto.Audits;
using Audit360.Application.Features.Dto.Responsibles;
using Audit360.Application.Features.Dto.Findings;
using Audit360.Application.Features.Dto.FollowUps;
using Audit360.Application.Features.Dto.Roles;
using Audit360.Application.Features.Dto.Statuses;
using Audit360.Application.Features.Dto.FindingTypes;
using Audit360.Application.Features.Dto.FindingSeverities;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.Application.Mapping
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<UserWriteDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<AuditWriteDto, Audit>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new AuditStatus { Id = src.StatusId, Description = string.Empty }))
                .ForMember(dest => dest.ResponsibleId, opt => opt.MapFrom(src => src.ResponsibleId));

            CreateMap<ResponsibleWriteDto, Responsible>();

            CreateMap<FindingWriteDto, Finding>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new FindingType { Id = src.FindingTypeId, Description = string.Empty }))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => new FindingSeverity { Id = src.SeverityId, Description = string.Empty }));

            CreateMap<FollowUpWriteDto, FollowUp>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new FollowUpStatus { Id = src.FollowUpStatusId, Description = string.Empty }));

            CreateMap<RoleWriteDto, Role>();
            CreateMap<AuditStatusWriteDto, AuditStatus>();
            CreateMap<FindingTypeWriteDto, FindingType>();
            CreateMap<FindingSeverityWriteDto, FindingSeverity>();
            CreateMap<FollowUpStatusWriteDto, FollowUpStatus>();
        }
    }
}
