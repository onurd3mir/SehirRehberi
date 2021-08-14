using Business.Abstarct;
using Business.BusinessAspect;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CityManager : ICityService
    {
        private ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public IDataResult<City> Add(City city)
        {
            _cityDal.Add(city);
            return new SuccessDataResult<City>(city,"Şehir Başarıyla Eklendi");
        }

        public IResult Delete(City city)
        {
            _cityDal.Delete(city);
            return new SuccessResult();
        }

        //[SecuredOperation]
        public IDataResult<List<City>> GetCities()
        {
            var result = _cityDal.GetCities().ToList();
            return new SuccessDataResult<List<City>>(result);
        }

        public IDataResult<City> GetCityById(int cityId)
        {
            return new SuccessDataResult<City>(_cityDal.GetCityById(cityId));
        }
    }
}
