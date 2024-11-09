using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Exceptions;


public class CategoryException : Exception
{
    public CategoryException(string message)
        : base(message) { }
}
