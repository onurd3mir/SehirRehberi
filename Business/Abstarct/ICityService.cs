using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstarct
{
    public interface ICityService
    {
        IDataResult<City> GetCityById(int cityId);
        IDataResult<List<City>> GetCities();
        IDataResult<City> Add(City city);
        IResult Delete(City city);

    }
}
