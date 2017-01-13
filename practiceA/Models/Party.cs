using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace practiceA.Models
{
    public class Party
    {
        [Key]
        public int no { get; set; }

        [DisplayName("店家名稱")]
        public string shopName { get; set; }

        [DisplayName("電話")]
        public string phone { get; set; }

        [DisplayName("地址")]
        [Required(ErrorMessage = "請輸入地址")]
        public string address { get; set; }

        [DisplayName("備註")]
        public string note { get; set; }

        [DisplayName("時間")]
        [Required(ErrorMessage = "請選擇時間")]
        public string time { get; set; }

        [DisplayName("暱稱")]
        [Required(ErrorMessage = "請輸入暱稱")]
        [StringLength(20, ErrorMessage = "長度上限20")]
        public string nickName { get; set; }

        [DisplayName("底色")]
        public string color { get; set; }

        [DisplayName("活動名稱")]
        [Required(ErrorMessage = "請輸入活動名稱")]
        [StringLength(10, ErrorMessage = "長度上限10")]
        public string partyName { get; set; }

        [DisplayName("與會人員")]
        public string participants { get; set; }
    }
    public class PartyDBContext : DbContext
    {
        public DbSet<Party> Party { get; set; }
    }
}