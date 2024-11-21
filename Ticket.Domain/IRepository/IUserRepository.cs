using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface IUserRepository
{
    void Add(User user);
    User GetById(int id);
}
