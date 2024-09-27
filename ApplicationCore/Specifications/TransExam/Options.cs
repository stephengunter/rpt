using Ardalis.Specification;
using ApplicationCore.Models.TransExam;

namespace ApplicationCore.Specifications.TransExam;
public class OptionsSpecification : Specification<Option>
{
   public OptionsSpecification(Question question)
	{
      Query.Where(x => x.QuestionId == question.Id);
   }
}

