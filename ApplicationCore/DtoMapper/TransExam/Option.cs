using ApplicationCore.Models.TransExam;
using ApplicationCore.Views.TransExam;
using AutoMapper;

namespace ApplicationCore.DtoMapper.TransExam;

public class OptionMappingProfile : Profile
{
	public OptionMappingProfile()
	{
		CreateMap<Option, OptionViewModel>();

		CreateMap<OptionViewModel, Option>();
	}
}
