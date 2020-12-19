namespace FS.Api.Application.Queries.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using DataObject.User.Responses;
    using Domain.Core.Interfaces;
    using MediatR;
    using Query;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.Get(request.Id);
            
            return new GetUserResponse(user.Id, user.Name, user.Email, user.Password, user.Gender, user.BirthDate);
        }
    }
}