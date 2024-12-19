namespace Test;

class MainTest
{
    static void Main(string[] args)
    {
       
    }
}

class Test
{
    public int Salam { get; set; }
    
}

class MyClass:Test
{
    public int Adad { get; set; }
    public string Reshte { get; set; }
}


public class IntList
{
    private int[] _intArray = new int[10];
    public void Add(int Adad)
    {
        _intArray.Append(Adad);
    }
}

public class StringList
{
    private string[] _stringArray = new string[10];
    public void Add(string Reshte)
    {
        _stringArray.Append(Reshte);
    }
}