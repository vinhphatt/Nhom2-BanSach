using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BAOCAO_HOTRO
    {
        [Key]
        public int mabao_cao { get; set; }

        [Required]
        public int mahs { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Nội dung")]
        public string noidung { get; set; }

        [Required]
        public DateTime ngay_tao { get; set; }

        public bool da_xu_ly { get; set; }

        [Required]
        [Display(Name = "Loại báo cáo")]
        public string loai_bao_cao { get; set; }

        [Display(Name = "Phản hồi")]
        public string phan_hoi { get; set; }

        // Navigation property
        public virtual HOCSINH HOCSINH { get; set; }
    }
}
