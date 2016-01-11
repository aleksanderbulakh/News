using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "Логін")]
        public string UserName { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Залишитися в системі")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} повинне містити не менше {2} символів.", MinimumLength = 5)]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Логін повинен містити лише цифри та літери латинського алфавіту.")]
        [Display(Name = "Логін")]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} повинне містити не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль та його підтвердження не збігаються.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Виберіть роль")]
        public string Role { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
