using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OthersName { get; set; }
        public string FullName { get => $"{FirstName} {LastName} {OthersName}"; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime LastModefied { get; set; }
        public string? LastModefiedby { get; set; }
        public Persona(DateTime createdat, bool isdeleted)
        {
            CreatedAt = createdat;
            IsDeleted = isdeleted;
        }

        public Persona() : this(DateTime.UtcNow, false) { }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
