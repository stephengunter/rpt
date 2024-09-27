using ApplicationCore.Models;
using ApplicationCore.Models.TransExam;
using Ardalis.Specification;

namespace ApplicationCore.Consts;

public class PostTypes
{
   public static string Question = new Question().GetType().Name;
   public static string Option = new Option().GetType().Name;
}