using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstarct
{
    public interface IPhotoService
    {
        IDataResult<Photo> GetPhotoById(int id);
        IDataResult<List<Photo>> GetPhotosByCity(int cityId);
        IResult Add(Photo photo);
        IResult Delete(Photo photo);
    }
}
