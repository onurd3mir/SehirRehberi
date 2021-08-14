using Business.Abstarct;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private IPhotoDal _photoDal;

        public PhotoManager(IPhotoDal photoDal)
        {
            _photoDal = photoDal;
        }

        public IResult Add(Photo photo)
        {
            _photoDal.Add(photo);
            return new SuccessResult(); 
        }

        public IResult Delete(Photo photo)
        {
            _photoDal.Delete(photo);
            return new SuccessResult();
        }

        public IDataResult<Photo> GetPhotoById(int id)
        {
            return new SuccessDataResult<Photo>(_photoDal.Get(p=>p.Id==id));
        }

        public IDataResult<List<Photo>> GetPhotosByCity(int cityId)
        {
            return new SuccessDataResult<List<Photo>>(_photoDal.GetList(p => p.CityId == cityId).ToList());
        }
    }
}
