namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;

    public class CreateAccountCommand : IRequest<Guid>
    {
        public CreateAccountCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
        
    }
}