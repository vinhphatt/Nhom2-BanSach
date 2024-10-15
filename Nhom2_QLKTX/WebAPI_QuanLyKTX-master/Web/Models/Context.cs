using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Web.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }


 
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        
    }
}
