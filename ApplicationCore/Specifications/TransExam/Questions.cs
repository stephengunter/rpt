using Ardalis.Specification;
using ApplicationCore.Models.TransExam;

namespace ApplicationCore.Specifications.TransExam;
public class QuestionsSpecification : Specification<Question>
{
   public QuestionsSpecification()
	{
      Query.Include(x => x.Options).Where(x => !x.Removed);
   }
   public QuestionsSpecification(int id)
   {
      Query.Include(x => x.Options).Where(x => !x.Removed).Where(x => x.Id == id);
   }
   public QuestionsSpecification(IEnumerable<int> ids)
   {
      Query.Include(x => x.Options).Where(x => ids.Contains(x.Id));
   }
}

