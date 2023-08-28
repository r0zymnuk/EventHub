using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Application.Dtos.Request.Account;
public record UpdateUserModel(
    [MaxLength(50)]
    string? FirstName,
    [MaxLength(50)]
    string? LastName,
    [MaxLength(50)]
    string? Email,
    [MaxLength(15)]
    string? PhoneNumber,
    string? ImageUrl
);