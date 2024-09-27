using ApplicationCore.Consts;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Views;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Views;

public class ProfilesViewModel
{
	public string UserId { get; set; } = String.Empty;
   public string Name { get; set; } = String.Empty;
   public DateTime? DateOfBirth { get; set; }
   public string? Ps { get; set; }

   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public DateTime? LastUpdated { get; set; }
   public string? UpdatedBy { get; set; }

   public string DobNum => DateOfBirth.HasValue ? Convert.ToDateTime(DateOfBirth.Value).GetDateString(roc: true) : "";
   public string DateOfBirthText => DateOfBirth.ToDateString();
   public string CreatedAtText => CreatedAt.ToDateString();
   public string LastUpdatedText => LastUpdated.ToDateString();

}
