using ApplicationCore.Views.TransExam;
using ApplicationCore.Models.TransExam;
using Infrastructure.Helpers;
using AutoMapper;
using Infrastructure.Paging;

namespace ApplicationCore.Helpers.TransExam;

public static class OptionsHelpers
{

   public static IEnumerable<Option> FilterByKeyword(this IEnumerable<Option> options, ICollection<string> keywords)
      => options.FilterByTitle(keywords);
   public static IEnumerable<Option> FilterByTitle(this IEnumerable<Option> options, ICollection<string> keywords)
      => options.Where(option => keywords.Any(keyword => option.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))).ToList();

   #region Views
   public static OptionViewModel MapViewModel(this Option option, IMapper mapper)
   {
      var model = mapper.Map<OptionViewModel>(option); 
      return model;
   }
   public static Option MapEntity(this OptionViewModel model, IMapper mapper, string currentOptionId, Option? entity = null)
   {
      if (entity == null) entity = mapper.Map<OptionViewModel, Option>(model);
      else entity = mapper.Map<OptionViewModel, Option>(model, entity);
      
      

      return entity;
   }
   public static Option MapEntity(this OptionViewModel model, IMapper mapper)
      => mapper.Map<OptionViewModel, Option>(model);

   public static List<OptionViewModel> MapViewModelList(this IEnumerable<Option> options, IMapper mapper)
      => options.Select(item => MapViewModel(item, mapper)).ToList();

   public static PagedList<Option, OptionViewModel> GetPagedList(this IEnumerable<Option> options, IMapper mapper, int page = 1, int pageSize = -1)
   {
      var pageList = new PagedList<Option, OptionViewModel>(options, page, pageSize);

      pageList.SetViewList(pageList.List.MapViewModelList(mapper));

      return pageList;
   }
   #endregion

}
