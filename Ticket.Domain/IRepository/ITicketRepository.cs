using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ITicketRepository
{
    void Add(Tickets ticket);
}
