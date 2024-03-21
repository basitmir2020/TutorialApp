using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Common.Lookup.CountryLookup;

public class CountryService : ICountryService
{
    private readonly TutorialAppContext _tutorialAppContext;

    public CountryService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }

    public async Task<ResponseViewModelGeneric<List<CountryDto>>> GetCountriesAsync()
    {
        /*var countries = await _tutorialAppContext.LkpCountries
            .Where(x => x.IsActive)
            .OrderBy(x => x.Sequence)
            .ToListAsync();*/

        /*if (countries.Count > 0)
        {
            var countryDto = TinyMapper.Map<List<LkpCountry>, List<CountryDto>>(countries);
            return new ResponseViewModelGeneric<List<CountryDto>>(countryDto)
            {
                StatusCode = 200,
                Success = true,
                Message = "List Of Countries"
            };
        }*/

        return new ResponseViewModelGeneric<List<CountryDto>>
        {
            StatusCode = 400,
            Success = false,
            Message = "No Countries Found"
        };
    }
}