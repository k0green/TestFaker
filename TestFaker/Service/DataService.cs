using Bogus;
using TestFaker.Models;
using TestFaker.Service.IService;

namespace TestFaker.Service;

public class DataService : IDataService
{
    private readonly IAddErrorService _addErrorService;

    public DataService(IAddErrorService addErrorService)
    {
        _addErrorService = addErrorService;
    }
    public List<UserDataDisplayModel> CreatedData(Faker faker, int size, double errorAmount)
    {
        var uddm = new List<UserDataDisplayModel>();
        for(int i=0; i<size; i++)
        {
            var data = new UserDataDisplayModel();
            data.FirstName = faker.Name.FirstName();
            data.LastName = faker.Name.LastName();
            data.Address = $"{faker.Address.City()} {faker.Address.StreetName()} {faker.Address.BuildingNumber()} {faker.Address.ZipCode()}";
            data.Phone = faker.Phone.PhoneNumber();
            if (errorAmount > 0)
            {
                var newData = AddError(data, errorAmount);
                newData.Id = Guid.NewGuid();
                uddm.Add(newData);
            }
            else
            {
                data.Id = Guid.NewGuid();
                uddm.Add(data);
            }
        }

        return uddm;
    }
    
    public List<UserDataDisplayModel> GetData(int size, string locale, double errorAmount, int seed)
    {
        if (locale == null)
        {
            locale = "en";
        }
        locale = locale.Replace("-", "_");
        /*SeedValue += 10;*/
        var faker = new Faker(locale);
        faker.Random = new Randomizer(seed);
        return CreatedData(faker, size, errorAmount);
    }

    public UserDataDisplayModel AddError(UserDataDisplayModel userDataDisplayModel, double errorAmount)
    {
        string valueForEdit = JoinOriginString(userDataDisplayModel);
        string newValue = _addErrorService.SelectsTheErrorType(valueForEdit, errorAmount);
        return SplitChangeString(newValue);
    }

    public string JoinOriginString(UserDataDisplayModel userDataDisplayModel)
    {
        string originValue = userDataDisplayModel.FirstName+"&"+
                             userDataDisplayModel.LastName+"&"+
                             userDataDisplayModel.Address+"&"+
                             userDataDisplayModel.Phone;
        return originValue;
    }

    public UserDataDisplayModel SplitChangeString(string changeString)
    {
        string[] dataParts = changeString.Split(new char[] { '&' });
        var uddm = new UserDataDisplayModel();
        if (dataParts.Length < 1)
        {
            
        }
        uddm.FirstName = dataParts[0];
        uddm.LastName = dataParts[1];
        uddm.Address = dataParts[2];
        uddm.Phone = dataParts[3];
        return uddm;
    }
}