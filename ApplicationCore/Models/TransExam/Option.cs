using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Helpers;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using ApplicationCore.Consts;

namespace ApplicationCore.Models.TransExam;
public class Option : EntityBase
{
   public string Title { get; set; } = string.Empty;
   public bool Correct { get; set; }
   public int QuestionId { get; set; }

   [Required]
   public virtual Question? Question { get; set; }

   [NotMapped]
   public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
   public void LoadAttachments(IEnumerable<Attachment> attachments)
   {
      attachments = attachments.Where(x => x.PostType == PostTypes.Option && x.PostId == Id);
      this.Attachments = attachments.HasItems() ? attachments.ToList() : new List<Attachment>();
   }

   public bool MultiAnswers { get; set; }
}

