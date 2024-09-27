using ApplicationCore.Models.TransExam;
using ApplicationCore.Views.TransExam;
using AutoMapper;

namespace ApplicationCore.DtoMapper.TransExam;

public class QuestionMappingProfile : Profile
{
	public QuestionMappingProfile()
	{
		CreateMap<Question, QuestionViewModel>();

		CreateMap<QuestionViewModel, Question>();
	}
}
