using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace practiceA.Models
{
    public class ManColor
    {
        [Key]
        [DisplayName("暱稱")]
        [StringLength(20, ErrorMessage = "長度上限20")]
        public string nickName { get; set; }

        [DisplayName("底色")]
        public string color { get; set; }
    }
    public class ManColorDBContext : DbContext
    {
        public DbSet<Party> Party { get; set; }
    }
}