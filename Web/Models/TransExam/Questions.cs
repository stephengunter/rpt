using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Views;
using Infrastructure.Paging;
using Infrastructure.Views;
using Microsoft.AspNetCore.Identity;
using System;

namespace Web.Models.TransExam;

public class QuestionsAdminRequest
{
   public QuestionsAdminRequest(string? keyword, int page = 1, int pageSize = 10)
   {
      Keyword = keyword;
      Page = page < 1 ? 1 : page;
      PageSize = pageSize;
   }
   public string? Keyword { get; set; }
   public int Page { get; set; } 
   public int PageSize { get; set; }
}
public class QuestionsAdminModel
{
   public QuestionsAdminModel(QuestionsAdminRequest request)
   {
      Request = request;
   }
   public int OptionCount => 4;
   public QuestionsAdminRequest Request { get; set; }
   public ICollection<string> Recruits = new List<string>();

}

public class QuestionAddRequest
{
   public string Title { get; set; } = String.Empty;


   public string Item { get; set; } = String.Empty;

   public ICollection<OptionAddRequest> Options { get; set; } = new List<OptionAddRequest>();

}
public class QuestionEditRequest : QuestionAddRequest
{
   

}