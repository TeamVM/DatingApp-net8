﻿using API.DTOs;
using API.Extensions;
using AutoMapper;
using Entities;

namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
            .ForMember(d => d.PhotoUlr, o 
            => o.MapFrom(s => s.Photos.FirstOrDefault(x =>x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
    }
}
