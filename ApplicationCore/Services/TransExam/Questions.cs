using ApplicationCore.DataAccess;
using ApplicationCore.Models.TransExam;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.TransExam;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ApplicationCore.Services.TransExam;

public interface IQuestionService
{
   Task<IEnumerable<Question>> FetchAsync();
   Task<Question?> GetByIdAsync(int id);
   Task<Question> CreateAsync(Question entity);
   Task UpdateAsync(Question entity);
   Task RemoveAsync(Question entity);

}

public class QuestionService : IQuestionService
{
   private readonly IDefaultRepository<Question> _repository;
   public QuestionService(IDefaultRepository<Question> repository)
   {
      _repository = repository;
   }
   public async Task<IEnumerable<Question>> FetchAsync()
      => await _repository.ListAsync(new QuestionsSpecification());

   public async Task<Question?> GetByIdAsync(int id)
      => await _repository.GetByIdAsync(id);

   public async Task<Question> CreateAsync(Question entity)
      => await _repository.AddAsync(entity);

   public async Task UpdateAsync(Question entity)
   => await _repository.UpdateAsync(entity);

   public async Task RemoveAsync(Question entity)
   {
      entity.Removed = true;
      await _repository.UpdateAsync(entity);
   }
}
