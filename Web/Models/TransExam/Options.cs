using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Views;
using Infrastructure.Paging;
using Infrastructure.Views;
using Microsoft.AspNetCore.Identity;
using System;

namespace Web.Models.TransExam;

public class OptionAddRequest
{
   
   public string Title { get; set; } = String.Empty;
   public bool Correct { get; set; }
}
public class OptionEditRequest : OptionAddRequest
{
   public int QuestionId { get; set; }

}