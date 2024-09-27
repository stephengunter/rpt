using ApplicationCore.Views.TransExam;
using ApplicationCore.Models.TransExam;
using Infrastructure.Helpers;
using AutoMapper;
using Infrastructure.Paging;

namespace ApplicationCore.Helpers.TransExam;

public static class QuestionsHelpers
{
   public static IEnumerable<Question> GetOrdered(this IEnumerable<Question> questions)
      => questions.OrderByDescending(x => x.CreatedAt);

   public static IEnumerable<Question> FilterByKeyword(this IEnumerable<Question> questions, ICollection<string> keywords)
      => questions.FilterByTitle(keywords);
   public static IEnumerable<Question> FilterByTitle(this IEnumerable<Question> questions, ICollection<string> keywords)
      => questions.Where(question => keywords.Any(keyword => question.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))).ToList();

   #region Views
   public static QuestionViewModel MapViewModel(this Question question, IMapper mapper)
   {
      var model = mapper.Map<QuestionViewModel>(question); 
      return model;
   }
   public static Question MapEntity(this QuestionViewModel model, IMapper mapper, string currentQuestionId, Question? entity = null)
   {
      if (entity == null) entity = mapper.Map<QuestionViewModel, Question>(model);
      else entity = mapper.Map<QuestionViewModel, Question>(model, entity);
      
      entity.LastUpdated = DateTime.Now;

      return entity;
   }
   public static Question MapEntity(this QuestionViewModel model, IMapper mapper)
      => mapper.Map<QuestionViewModel, Question>(model);

   public static List<QuestionViewModel> MapViewModelList(this IEnumerable<Question> questions, IMapper mapper)
      => questions.Select(item => MapViewModel(item, mapper)).ToList();

   public static PagedList<Question, QuestionViewModel> GetPagedList(this IEnumerable<Question> questions, IMapper mapper, int page = 1, int pageSize = -1)
   {
      var pageList = new PagedList<Question, QuestionViewModel>(questions, page, pageSize);

      pageList.SetViewList(pageList.List.MapViewModelList(mapper));

      return pageList;
   }
   #endregion

}
