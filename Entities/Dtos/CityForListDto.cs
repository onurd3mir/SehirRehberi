using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CityForListDto:IDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
