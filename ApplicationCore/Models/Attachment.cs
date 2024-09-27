using Infrastructure.Entities;
using Infrastructure.Helpers;

namespace ApplicationCore.Models;

public class Attachment : EntityBase, IBaseUploadFile, IBaseRecord, IRemovable, ISortable
{
	public string PostType { get; set; } = String.Empty;
   public int PostId { get; set; }
   public string? Title { get; set; }
   public string? Description { get; set; }
   public string OriFileName { get; set; } = String.Empty;

   public string FileName { get; set; } = String.Empty;
   public string Ext { get; set; } = String.Empty;
   public long FileSize { get; set; }
   public string Host { get; set; } = String.Empty;
   public string DirectoryPath { get; set; } = String.Empty;


   public bool Removed { get; set; }
   public int Order { get; set; }
   public bool Active => ISortableHelpers.IsActive(this);

   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public string CreatedBy { get; set; } = string.Empty;
   public DateTime? LastUpdated { get; set; }
   public string? UpdatedBy { get; set; }


   public string GetContentType()
   {
      return Ext switch
      {
         ".jpg" => "image/jpeg",
         ".jpeg" => "image/jpeg",
         ".png" => "image/png",
         ".gif" => "image/gif",
         ".pdf" => "application/pdf",
         ".txt" => "text/plain",
         ".doc" => "application/msword",
         ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
         ".xls" => "application/vnd.ms-excel",
         ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
         ".mp3" => "audio/mpeg",
         ".mp4" => "video/mp4",
         ".zip" => "application/zip",
         _ => "application/octet-stream" // Default to binary if the type is unknown
      };
   }

}
