using System;
using Ticket.Domain;
using Ticket.Domain.Entity;

namespace Test;

class MainTest
{
    public static void Main(string[] args)
    {
        var salam = new Category("test", null, 1, new List<Field>());
    }
}