namespace TestFaker.Service.IService;

public interface IAddErrorService
{
    public string DeleteSymbol(string value);

    public string AddSymbol(string value);

    public string ReverseSymbol(string value);

    public string SelectsTheErrorType(string value, double errorAmount);
}