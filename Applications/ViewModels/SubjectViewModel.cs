using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ClassId { get; set; }
        public ClassViewModel Classes { get; set; }
    }
}
