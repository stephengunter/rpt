using ApplicationCore.DataAccess;
using ApplicationCore.Models.TransExam;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.TransExam;
using Infrastructure.Helpers;

namespace ApplicationCore.Services.TransExam;

public interface IOptionService
{
   Task<IEnumerable<Option>> FetchAsync(Question question);
   Task<Option?> GetByIdAsync(int id);
   Task<Option> CreateAsync(Option entity);
   Task UpdateAsync(Option entity);
}

public class OptionService : IOptionService
{
   private readonly IDefaultRepository<Option> _repository;

   public OptionService(IDefaultRepository<Option> repository)
   {
      _repository = repository;
   }
   public async Task<IEnumerable<Option>> FetchAsync(Question question)
      => await _repository.ListAsync(new OptionsSpecification(question));

   public async Task<Option?> GetByIdAsync(int id)
      => await _repository.GetByIdAsync(id);

   public async Task<Option> CreateAsync(Option entity)
      => await _repository.AddAsync(entity);

   public async Task UpdateAsync(Option entity)
   => await _repository.UpdateAsync(entity);
}
