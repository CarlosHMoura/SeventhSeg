using SeventhSeg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SeventhSeg.Application.DTOs;

public class ServerDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [DisplayName("Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "The IP is Required")]
    [MinLength(8)]
    [DisplayName("IP")]
    public string Ip { get; set; } = null!;

    [Required(ErrorMessage = "The Port is Required")]
    [DisplayName("Port")]
    public int Port { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
