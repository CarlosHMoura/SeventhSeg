using SeventhSeg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using SeventhSeg.Domain.Enums;

namespace SeventhSeg.Application.DTOs;

public class RecyclerDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Status is Required")]
    [DisplayName("Status")]
    public RecyclerStatusEnum Status { get; set; }

    [Required(ErrorMessage = "The Days is Required")]
    [DisplayName("Days")]
    public int Days { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
