using System.Collections;

namespace Test;

class MainTest
{
    static void Main(string[] args)
    {
        ITest s = new Salam(); 
        s.Test();
        
        
          
    }
}


public class CategoryException : Exception
{
    public CategoryException() { }
    public CategoryException(string message)
        : base(message) { }

    public CategoryException(string message, Exception inner)
        : base(message, inner) { }
}

class Salam: ITest
{
      
}

public interface ITest
{
    virtual void Test()
    {
        Console.WriteLine("test");
    }
}