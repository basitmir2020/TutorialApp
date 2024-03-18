using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Lookup.CountryLookup;

public interface ICountryService
{
    Task<ResponseViewModelGeneric<List<CountryDto>>> GetCountriesAsync();
}