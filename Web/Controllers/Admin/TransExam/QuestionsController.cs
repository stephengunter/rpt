using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using ApplicationCore.Services.TransExam;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Views;
using ApplicationCore.Helpers;
using AutoMapper;
using ApplicationCore.Helpers.TransExam;
using Web.Controllers;
using ApplicationCore.Consts;
using ApplicationCore.Views.TransExam;
using Web.Models;
using Web.Models.TransExam;
using Infrastructure.Helpers;
using System.Data;
using Infrastructure.Paging;
using ApplicationCore.Models.TransExam;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using ApplicationCore.Authorization;

namespace Web.Controllers.Admin.TransExam;

[Route("admin/trans-exam/[controller]")]
public class QuestionsController : BaseAdminController
{
   private readonly IQuestionService _questionsService;
   private readonly IAttachmentService _attachmentsService;
   private readonly IMapper _mapper;

   public QuestionsController(IQuestionService questionsService, IAttachmentService attachmentsService,
      IMapper mapper)
   {
      _questionsService = questionsService;
      _attachmentsService = attachmentsService;

      _mapper = mapper;
   }

   [HttpGet("init")]
   public async Task<ActionResult<QuestionsAdminModel>> Init()
   {
      string keyword = "";
      int page = 1;
      int pageSize = 10;

      var request = new QuestionsAdminRequest(keyword, page, pageSize);

      return new QuestionsAdminModel(request);
   }


   [HttpGet]
   public async Task<ActionResult<PagedList<Question, QuestionViewModel>>> Index(string keyword = "", int page = 1, int pageSize = 10)
   {
      var questions = await _questionsService.FetchAsync();

      var keywords = keyword.GetKeywords();
      if (keywords.HasItems()) questions = questions.FilterByKeyword(keywords);

      var model = questions.GetPagedList(_mapper, page, pageSize);

      var ids = model.ViewList.Select(x => x.Id).ToList();
      var attachments = await _attachmentsService.FetchAsync(PostTypes.Question, ids);

      foreach (var item in model.ViewList)
      {
         item.Options = item.Options.OrderByDescending(o => o.Correct).ToList();
         var item_attachments = attachments.Where(x => x.PostId == item.Id).ToList();

         item.Attachments = item_attachments.MapViewModelList(_mapper);
      }


      return model;
   }

   [HttpGet("create")]
   public ActionResult<QuestionAddRequest> Create(int option_count = 4)
   { 
      var model = new QuestionAddRequest();
      for (int i = 0; i < option_count; i++)
      {
         model.Options.Add(new OptionAddRequest());
      }
      return model;
   }
   

   [HttpPost]
   public async Task<ActionResult<QuestionViewModel>> Store([FromBody] QuestionAddRequest model)
   {
      bool multiAnswers = false;
      string title = CheckQuestionTitle(model.Title);

      //ValidateOptions(model.Options, multiAnswers);

      if (!ModelState.IsValid) return BadRequest(ModelState);

      model.Title = title;
      var question = new Question();
      var except = new List<string>() { "Attachments", "Options" };
      model.SetValuesTo(question, except);
      question.SetCreated(User.Id());

      question = await _questionsService.CreateAsync(question);

      //foreach (var media in question.Attachments)
      //{
      //   media.PostType = PostType.Question;
      //   media.PostId = question.Id;
      //   media.SetCreated(CurrentUserId);
      //   await _attachmentsService.CreateAsync(media);
      //}

      //foreach (var option in question.Options)
      //{
      //   foreach (var attachment in option.Attachments)
      //   {
      //      attachment.PostType = PostType.Option;
      //      attachment.PostId = option.Id;
      //      attachment.SetCreated(CurrentUserId);
      //      await _attachmentsService.CreateAsync(attachment);
      //   }
      //}


      return Ok(question.MapViewModel(_mapper));
   }
   [HttpGet("{id}")]
   public async Task<ActionResult<QuestionViewModel>> Details(int id)
   {
      var question = await _questionsService.GetByIdAsync(id);
      if (question == null) return NotFound();

     
      var attachments = await _attachmentsService.FetchAsync(PostTypes.Question, id);

      var model = question.MapViewModel(_mapper);
      model.Options = model.Options.OrderByDescending(o => o.Correct).ToList();
      model.Attachments = attachments.MapViewModelList(_mapper);

      return model;
   }
   string CheckQuestionTitle(string input)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("title", "必須填寫標題");
         return "";
      }

      string title = input.Trim();
      return title;
   }

   [HttpGet("edit/{id}")]
   public async Task<ActionResult> Edit(int id)
   {
      var question = await _questionsService.GetByIdAsync(id);
      if (question == null) return NotFound();

      //var optionIds = question.Options.Select(x => x.Id).ToList();

      //var questionAttachments = await _attachmentsService.FetchAsync(PostType.Question, id);
      //var optionAttachments = await _attachmentsService.FetchAsync(PostType.Option, optionIds);

      //var attachments = (questionAttachments ?? new List<UploadFile>()).Concat(optionAttachments ?? new List<UploadFile>());

      //var model = question.MapViewModel(_mapper, allRecruits.ToList(), attachments.ToList());
      var model = question.MapViewModel(_mapper);
      return Ok(model);
   }

   [HttpPut("{id}")]
   public async Task<ActionResult> Update(int id, [FromBody] QuestionEditRequest model)
   {
      bool multiAnswers = false;

      var question = await _questionsService.GetByIdAsync(id);
      if (question == null) return NotFound();

      string title = CheckQuestionTitle(model.Title);

      ValidateOptions(model.Options, multiAnswers);

      if (!ModelState.IsValid) return BadRequest(ModelState);

      model.Title = title;
      var except = new List<string>() { "Options" };
      model.SetValuesTo(question, except);

      await _questionsService.UpdateAsync(question);

      //foreach (var media in question.Attachments)
      //{
      //   media.PostType = PostType.Question;
      //   media.PostId = question.Id;
      //   if (media.Id > 0) media.SetUpdated(CurrentUserId);
      //   else media.SetCreated(CurrentUserId);
      //}
      //await _attachmentsService.SyncAttachmentsAsync(question, question.Attachments);

      //foreach (var option in question.Options)
      //{
      //   foreach (var attachment in option.Attachments)
      //   {
      //      attachment.PostType = PostType.Option;
      //      attachment.PostId = option.Id;

      //      if (attachment.Id > 0) attachment.SetUpdated(CurrentUserId);
      //      else attachment.SetCreated(CurrentUserId);
      //   }

      //   await _attachmentsService.SyncAttachmentsAsync(option, option.Attachments);
      //}


      return Ok(question.MapViewModel(_mapper));
   }

   [HttpDelete("{id}")]
   public async Task<ActionResult> Delete(int id)
   {
      var question = await _questionsService.GetByIdAsync(id);
      if (question == null) return NotFound();

      question.SetUpdated(User.Id());
      await _questionsService.RemoveAsync(question);

      return Ok();
   }

   void ValidateOptions(ICollection<OptionAddRequest> options, bool multiAnswers = false)
   {
      if (options.HasItems())
      {
         var correctOptions = options.Where(item => item.Correct).ToList();
         if (correctOptions.IsNullOrEmpty()) ModelState.AddModelError("options", "必須要有正確的選項");
         else if (correctOptions.Count > 1)
         {
            if (!multiAnswers) ModelState.AddModelError("options", "單選題只能有一個正確選項");

         }
      }

   }


}