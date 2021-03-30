using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CartsProject.Models
{
    public class User
    {
        [Display(Name = "ID")]
        public string id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)] //字元長度1~256
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string password1 { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)] //字元長度1~256
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string password2 { get; set; }
        [Required]
        [Display(Name = "暱稱")]
        public string userName { get; set; }
        [Required]
        [Display(Name = "連絡電話")]
        public string phoneNumber { get; set; }
    }
}