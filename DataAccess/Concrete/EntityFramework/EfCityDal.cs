using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCityDal:EfEntityRepositoryBase<City,SehirRehberiContext>,ICityDal
    {
        public List<City> GetCities()
        {
            using (var context= new SehirRehberiContext())
            {
                return context.Cities.Include(p=>p.photos).ToList();
            }
        }

        public City GetCityById(int cityId)
        {
            using (var context = new SehirRehberiContext())
            {
                return context.Cities.Include(p => p.photos).FirstOrDefault(c=>c.Id==cityId);
            }
        }
    }
}
