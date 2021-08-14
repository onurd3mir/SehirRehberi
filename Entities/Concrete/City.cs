using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<Photo> photos { get; set; }
    }
}
