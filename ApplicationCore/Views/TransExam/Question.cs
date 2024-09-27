using ApplicationCore.Helpers;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Views.TransExam;

public class QuestionViewModel : EntityBaseView, IBaseRecordView
{
   public int Id { get; set; }
   public string Title { get; set; } = String.Empty;

   public bool MultiAnswers { get; set; }

   public string OptionsText { get; set; } = String.Empty;

   public DateTime CreatedAt { get; set; }
   public string CreatedBy { get; set; } = String.Empty;
   public DateTime? LastUpdated { get; set; }
   public string? UpdatedBy { get; set; }

   public string CreatedAtText => CreatedAt.ToDateString();
   public string LastUpdatedText => LastUpdated.ToDateString();

   public ICollection<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();


   public ICollection<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();

   public int Index { get; set; }


}
