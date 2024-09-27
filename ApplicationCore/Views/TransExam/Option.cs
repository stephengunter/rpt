using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Helpers;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using ApplicationCore.Consts;
using Infrastructure.Views;

namespace ApplicationCore.Views.TransExam;
public class OptionViewModel : EntityBaseView
{
   public string Title { get; set; } = String.Empty;
   public bool Correct { get; set; }
   public int QuestionId { get; set; }


   public ICollection<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
}

