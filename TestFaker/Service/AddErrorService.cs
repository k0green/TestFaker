using TestFaker.Service.IService;

namespace TestFaker.Service;

public class AddErrorService : IAddErrorService
{
    private readonly Random _rnd = new Random();
    private readonly char[] _auxiliarySymbols = { '|', '-', ')', '(', ' ', '+', '&' };
    
    public string DeleteSymbol(string value)
    {
        var index = _rnd.Next(1, value.Length - 1);
        var symbol = value[index - 1];
        if (!_auxiliarySymbols.Contains(symbol))
        {
            value = value.Replace(symbol.ToString(), "");
        }
        return value;
    }

    public string AddSymbol(string value)
    {
        var index = _rnd.Next(1, value.Length - 1);
        var symbol = value[index - 1];
        if (_auxiliarySymbols.Contains(symbol))
        {
            index++;
            symbol = value[index - 1];
        }
        value = value.Insert(index, symbol.ToString());
        return value;
    }

    public string ReverseSymbol(string value)
    {
        var index = _rnd.Next(1, value.Length - 2);
        var firstSymbol = value[index].ToString();
        var secondSymbol = value[index + 1].ToString();
        value = value.Replace(firstSymbol, secondSymbol);
        value = value.Replace(secondSymbol, firstSymbol);
        return value;
    }

    public string SelectsTheErrorType(string value, double errorAmount)
    {
        double fO = errorAmount % 1.0;
        int N = (int)(errorAmount - fO);
        if (_rnd.NextDouble() < fO ? true : false)
        {
            N++;
        }
        var startLength = GetMinLength(value);
        var valueForChange = value;
        for (int i = 0; i < N; i++)
        {
            if (GetMinLength(valueForChange) > 3 && GetMinLength(valueForChange) < 3 * GetMinLength(value))
            {
                switch (_rnd.Next(1,3))
                {
                    case 1: 
                        valueForChange = DeleteSymbol(valueForChange);
                        break;
                    case 2:
                        valueForChange = AddSymbol(valueForChange);
                        break;
                    case 3:
                        valueForChange = ReverseSymbol(valueForChange);
                        break;
                }
            }
        }
        return valueForChange;
    }


    private int GetMinLength(string value)
    {
        List<int> lengths = new List<int>(); 
        string[] dataParts = value.Split(new char[] { '&' });
        foreach (var item in dataParts)
        {
            lengths.Add(item.Length);
        }
        var min = lengths.Min();
        return min;
    }
}