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
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    [DisplayName("Status")]
    public RecyclerStatusEnum Status { get; set; }

    [JsonIgnore]
    [DisplayName("Days")]
    public int Days { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
