using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Users.Command.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private IUserStore<User> _userStore;
        public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> handler, IUserContext userContext
            , IUserStore<User> userstore)
        {
            _logger = handler;
            _userContext = userContext;
            _userStore = userstore;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var userContext = _userContext.GetCurrentUser();
            _logger.LogInformation("Updating user : {UserId} , with {@Request}", userContext!.Id, request);
            var dbUser = await _userStore.FindByIdAsync(userContext!.Id, cancellationToken);
            if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), userContext!.Id);
            }
            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateBirth;
            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
