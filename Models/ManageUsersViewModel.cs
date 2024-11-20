using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Models
{
    public record ManageUsersViewModel(IdentityUser[] Administrators, IdentityUser[] Everyone);
}