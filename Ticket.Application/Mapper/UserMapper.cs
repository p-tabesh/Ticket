using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class UserMapper
{
    public static UserViewModel MapToDto(User user)
    {

        var userViewModel = new UserViewModel(user.Id,
            user.Username,
            user.Email,
            user.IsActive,
            user.IsAdmin,
            user.Team.Title);

        return userViewModel;
    }
}
