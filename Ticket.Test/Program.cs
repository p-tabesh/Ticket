using System;
using Ticket.Domain;
using Ticket.Domain.Entity;

namespace Test;

class MainTest
{
    public static void Main(string[] args)
    {
        var t = new Test2();
    }
}

class Test
{
    public Test()
    {
        Console.WriteLine("parent constructor");
    }
}

class Test2: Test
{
    public Test2()
    {
        Console.WriteLine("child constructor");
    }
}