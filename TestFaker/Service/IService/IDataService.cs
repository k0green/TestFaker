using Bogus;
using TestFaker.Models;

namespace TestFaker.Service.IService;

public interface IDataService
{
    public List<UserDataDisplayModel> GetData(int size, string locale, double errorAmount, int seed);
    public List<UserDataDisplayModel> CreatedData(Faker faker, int size, double errorAmount);
    public UserDataDisplayModel AddError(UserDataDisplayModel userDataDisplayModel, double errorAmount);
    public string JoinOriginString(UserDataDisplayModel userDataDisplayModel);
    public UserDataDisplayModel SplitChangeString(string changeString);
}