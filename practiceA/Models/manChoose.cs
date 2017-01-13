using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace practiceA.Models
{
    public class ManChoose
    {
        [Key]
        public int no { get; set; }

        [DisplayName("暱稱")]
        [StringLength(20, ErrorMessage = "長度上限20")]
        public string nickName { get; set; }

        [DisplayName("活動名稱")]
        [StringLength(10, ErrorMessage = "長度上限10")]
        public string partyName { get; set; }

        [DisplayName("時間")]
        public string time { get; set; }
    }
    public class ManChooseDBContext : DbContext
    {
        public DbSet<Party> Party { get; set; }
    }
}