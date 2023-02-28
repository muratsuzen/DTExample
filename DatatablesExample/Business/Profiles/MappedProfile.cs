using AutoMapper;
using Business.Models;
using Core.Paging;
using Dtos;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class MappedProfile : Profile
    {
        public MappedProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<IPaginate<Product>,ProductListModel>().ReverseMap();
        }
    }
}
