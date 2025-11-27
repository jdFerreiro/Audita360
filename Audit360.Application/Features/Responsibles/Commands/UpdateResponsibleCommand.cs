using MediatR;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.Application.Features.Responsibles.Commands
{
    public record UpdateResponsibleCommand(int Id, ResponsibleWriteDto Responsible) : IRequest<MediatR.Unit>;
}