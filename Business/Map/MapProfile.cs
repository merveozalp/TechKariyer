using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Map
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
;        }
    }
}
