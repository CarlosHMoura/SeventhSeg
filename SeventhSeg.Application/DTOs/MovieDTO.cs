﻿using SeventhSeg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeventhSeg.Application.DTOs;

public class MovieDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(3)]
    [DisplayName("Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The FileName is Required")]
    [MinLength(3)]
    [DisplayName("FileName")]
    public string FileName { get; set; }

    [Required(ErrorMessage = "The PathFile is Required")]
    [MinLength(3)]
    [DisplayName("PathFile")]
    public string PathFile { get; set; }

    [Required(ErrorMessage = "The SizeInBytes is Required")]
    [DisplayName("SizeInBytes")]
    public int SizeInBytes { get; set; }

    [Required(ErrorMessage = "The ServerId is Required")]
    [DisplayName("ServerId")]
    public Guid ServerId { get; set; }

    [JsonIgnore]
    public Server? Server { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    [DisplayName("CreatedDate")]
    public DateTime? UpdatedDate { get; set; }
}