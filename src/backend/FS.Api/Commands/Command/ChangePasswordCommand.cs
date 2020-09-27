using System;
using MediatR;

namespace FS.Api.Commands.Command
{
    public class ChangePasswordCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public ChangePasswordCommand(Guid id, string oldPassword, string newPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}