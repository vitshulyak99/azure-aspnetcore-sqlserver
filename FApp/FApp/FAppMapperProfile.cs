using System;
using AutoMapper;
using AutoMapper.Configuration;
using FApp.DAL;
using FApp.Models;

namespace FApp
{
    public class FAppMapperProfile : Profile
    {
        public FAppMapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(
                    u => u.IsBlocked, 
                    um => um.MapFrom(u => u.IsBlocked))
                .ForMember(
                    u => u.Email, 
                    um => um.MapFrom(u => u.Email))
                .ForMember(
                    u => u.UserName, 
                    um => um.MapFrom(u => u.UserName))
                .ForMember(
                    u => u.CreateDate, 
                    um => um.MapFrom(u => u.CreatedDateTime))
                .ForMember(
                    u => u.LastLogin, 
                    um => um.MapFrom(u => u.LastLoginDateTime))
                ;
        }
    }
}