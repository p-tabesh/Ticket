using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class UserMapper
{
    public static UserViewModel MapToDto(User user)
    {

        var userViewModel = new UserViewModel()
        {
            Id = user.Id,
            UserName = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            Team = user.Team.Title
        };
        return userViewModel;
    }
}
