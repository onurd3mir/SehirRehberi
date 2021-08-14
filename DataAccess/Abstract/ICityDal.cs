using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICityDal:IEntityRepository<City>
    {
        City GetCityById(int cityId);
        List<City> GetCities();
    }
}
