using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace practiceA.Models
{
    public class BasicInf
    {
        [DisplayName("客戶編號")]
        [Required(ErrorMessage = "請輸入客戶編號")]
        [StringLength(10, ErrorMessage = "長度上限10")]
        public string id { get; set; }
        [DisplayName("客戶密碼")]
        [Required(ErrorMessage = "請輸入客戶密碼")]
        [StringLength(10, ErrorMessage = "長度上限10")]
        [DataType(DataType.Password)]
        public string psd { get; set; }
    }
}