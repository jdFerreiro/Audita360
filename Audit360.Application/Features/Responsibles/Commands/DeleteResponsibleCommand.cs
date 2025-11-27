using MediatR;

namespace Audit360.Application.Features.Responsibles.Commands
{
    public record DeleteResponsibleCommand(int Id) : IRequest;
}