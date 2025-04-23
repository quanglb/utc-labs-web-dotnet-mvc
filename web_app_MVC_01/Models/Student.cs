using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace web_app_MVC_01.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
    [Display(Name = "Họ và tên")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có định dạng @gmail.com")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", 
        ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Ngành học là bắt buộc")]
    [Display(Name = "Ngành học")]
    public Branch? Branch { get; set; }

    [Required(ErrorMessage = "Giới tính là bắt buộc")]
    [Display(Name = "Giới tính")]
    public Gender? Gender { get; set; }

    [Display(Name = "Hệ chính quy")]
    public bool IsRegular { get; set; }

    [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Range(typeof(DateTime), "1/1/1963", "12/31/2025", ErrorMessage = "Ngày sinh phải trong khoảng từ 1963 đến 2025")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
    [Display(Name = "Ngày sinh")]
    public DateTime DateOfBorth { get; set; }

    [Required(ErrorMessage = "Điểm là bắt buộc")]
    [Range(0.0, 10.0, ErrorMessage = "Điểm phải từ 0.0 đến 10.0")]
    [Display(Name = "Điểm")]
    public double Score { get; set; }

    public string? AvatarUrl { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public IFormFile? Avatar { get; set; }
}
