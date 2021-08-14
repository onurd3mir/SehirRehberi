using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstarct
{
    public interface IUserService
    { 
        void Add(User user);
        User GetByUsername(string username);
    }
}
