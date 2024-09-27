using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Helpers;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using ApplicationCore.Consts;

namespace ApplicationCore.Models.TransExam;
public class Question : EntityBase, IBaseRecord, IRemovable
{
   public string Title { get; set; } = string.Empty;
   public virtual ICollection<Option> Options { get; set; } = new List<Option>();

   [NotMapped]
   public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
   public void LoadAttachments(IEnumerable<Attachment> attachments)
   {
      attachments = attachments.Where(x => x.PostType == PostTypes.Question && x.PostId == Id);
      this.Attachments = attachments.HasItems() ? attachments.ToList() : new List<Attachment>();
   }

   public bool MultiAnswers { get; set; }

   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public string CreatedBy { get; set; } = string.Empty;
   public DateTime? LastUpdated { get; set; }
   public string? UpdatedBy { get; set; }
   public bool Removed { get; set; }
}

