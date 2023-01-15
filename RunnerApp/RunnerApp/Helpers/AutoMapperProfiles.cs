using AutoMapper;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Extensions;

namespace RunnerApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Friend, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.FriendUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.FriendUser.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(ssrc => ssrc.FriendUser.Username))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(ssrc => ssrc.FriendUser.Interests))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(ssrc => ssrc.FriendUser.CreatedDate))
                .ForMember(dest => dest.City, opt => opt.MapFrom(ssrc => ssrc.FriendUser.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(ssrc => ssrc.FriendUser.Country))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(ssrc => ssrc.FriendUser.Id));
        }
    }
}

