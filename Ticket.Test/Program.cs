using Ticket.Application.Utilities;


namespace Ticket.Test;

class MainTest
{
    static void Main(string[] args)
    {
        var list1 = new List<string>
        {
            "salam",
            "test"
        };

        var list2 = new List<string>
        {
            "salam2",
            "test2"
        };

        list1.AddRange(list2);

        foreach (var item in list1)
        {
            Console.WriteLine(item);
        }
    }
}

