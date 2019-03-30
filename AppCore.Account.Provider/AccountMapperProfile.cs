using CoreApp.Account.Model;
using CoreApp.Account.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Account.Provider
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<UserInfo, User>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.FirtName + " " + s.MiddleName + " " + s.LastName))
                .ForMember(d => d.ID, m => m.MapFrom(s => s.UserId))
                .ReverseMap();


            CreateMap<User, UserInfo>()
                .ForMember(d => d.FirtName, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.UserId, m => m.MapFrom(s => s.ID))
                .ReverseMap();


        }
    }
}
