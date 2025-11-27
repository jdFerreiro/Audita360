using MediatR;

namespace Audit360.Application.Features.FindingTypes.Commands
{
    public record DeleteFindingTypeCommand(int Id) : IRequest;
}