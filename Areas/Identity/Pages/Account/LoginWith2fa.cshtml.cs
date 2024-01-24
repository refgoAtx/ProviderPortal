// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Provider.Areas.Identity.Data;
using System.Net;
using System.Net.Mail;

namespace Provider.Areas.Identity.Pages.Account
{
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ProviderUser> _signInManager;
        private readonly UserManager<ProviderUser> _userManager;
        private readonly ILogger<LoginWith2faModel> _logger;
		private readonly Provider.Data.ProviderContext _context;

		public LoginWith2faModel(
            SignInManager<ProviderUser> signInManager,
            UserManager<ProviderUser> userManager,
            ILogger<LoginWith2faModel> logger,
			Provider.Data.ProviderContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
			_context = context;
		}

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

			
			var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);

			if (providers.Any(_ => _ == "Email"))
			{
				var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
				await SendEmailAsync(user.Email, "2FA Code",
					$"<h3 >{token}</h3>.");
			}
			else
			{
				throw new InvalidOperationException($"Unable to load two-factor authentication user.");
			}


			ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }
		private async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
		{
			try
			{
				MailMessage message = new MailMessage();
				SmtpClient smtpClient = new SmtpClient();
				message.From = new MailAddress("tapan.bhatt@eixsys.com");
				message.To.Add(email);
				message.Subject = subject;
				message.IsBodyHtml = true;
				message.Body = confirmLink;
				smtpClient.Host = "smtp.office365.com";
				smtpClient.Port = 587;
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("tapan.bhatt@eixsys.com", "Ehoffice123#");
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Send(message);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}

			returnUrl = returnUrl ?? Url.Content("~/");

			var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
			if (user == null)
			{
				throw new InvalidOperationException($"Unable to load two-factor authentication user.");
			}

			var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

			//var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);
			var result = await _signInManager.TwoFactorSignInAsync("Email", authenticatorCode, rememberMe, Input.RememberMachine);
			var userId = await _userManager.GetUserIdAsync(user);

			var userAgent = HttpContext.Request.Headers["User-Agent"];
			string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
			if (ip == "::1")
			{
				ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[4].ToString();
			}
			ProviderLoginHistory log = new()
			{
				LoginTime = DateTime.Now,
				UserID = userId,
				Browser = userAgent,
				Ip = ip,
				Createdat = DateTime.Now,
				Createdby = "Admin"
			};

			_context.LoginHistory.Add(log);
			_context.SaveChanges();


			if (result.Succeeded)
			{
				_logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
				return LocalRedirect(returnUrl);
			}
			else if (result.IsLockedOut)
			{
				_logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
				return RedirectToPage("./Lockout");
			}
			else
			{
				_logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
				ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
				return Page();
			}
		}
    }
}
