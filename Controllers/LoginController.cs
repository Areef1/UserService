using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using SDSI.Access.DBconnections;
using SDSI.Access.EncriptionAndDecription;
using SDSI.Access.Interface;
using SDSI.Access.JWTauth;
using SDSI.Access.Email;
using System.Net;
using System;

namespace SDSI.Access.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUser _user;
        private readonly Email.EmailService _emailService;

        public LoginController(IUser user, Email.EmailService _emailService)
        {
            _user = user;
            this._emailService = _emailService;
        }

        [Route("/signup")]
        //[EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        public IActionResult signup(string firstName, string lastName, string email, string userName, string password, string phoneNumber = "")
        {
            // Insert user into database
            //Encritpion encritpion = new Encritpion();
            //string enpassword = encritpion.Encrypt(password);
             var result = _user.insertUser(firstName, lastName, email, userName, password, phoneNumber);
            if (result == 2627)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
            if (result != 200)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (result == 200)
            {
                TokenGeneration tokenGeneration = new TokenGeneration();
                string token = tokenGeneration.GenerateToken(userName);
                return Ok(new { StatusCode= 200 ,
                    AuthorizationToken = token
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       
        [HttpPost("generate-token")]
        [AllowAnonymous]
        public IActionResult login(string userName, string password)
        {
            // Check if user exists in database
           
            var result = _user.checkUser(userName, password);
            if (result == 200)
            {
                TokenGeneration tokenGeneration = new TokenGeneration();
                string token = tokenGeneration.GenerateToken(userName);
                return Ok(new
                {
                    StatusCode = 200,
                    AuthorizationToken = token
                });
            }
            if (result == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (result == 401)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Route("/ForgetPasswrd")]
        [HttpGet]
        public IActionResult ForgetPasswrd(string userName)
        {
            // Check if user exists in database

            string result = _user.checkUser(userName);
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            if (result == null)
            {
                return NotFound($"User '{userName}' not found.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            // Generate a password reset token (this is just an example, implement your own logic)
            var resetToken = "example-reset-token";

            // Create the reset link
            var resetLink = Url.Action("ResetPassword", "ForgotPassword", new { token = resetToken }, Request.Scheme);

            // Send the email
            var message = $"<p>Please reset your password by clicking <a href='{resetLink}'>here</a>.</p>";
            await _emailService.SendEmailAsync("msareef232@gmail.com", "Password Reset", message);

            return Ok(new { Message = "Password reset link has been sent to your email." });
        }
        [HttpPost("generate-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateOtp([FromBody] ForgotPasswordRequest request)
        {

            // Generate a password reset token (this is just an example, implement your own logic)
            Random _random = new Random();
            String resetToken = _random.Next(100000, 999999).ToString();

            // Create the reset link
            var resetLink = Url.Action("ResetPassword", "ForgotPassword", new { token = resetToken }, Request.Scheme);

            // Send the email
            var message = $"<p>OTP to reset your password{resetToken} <a href='{resetLink}'>here</a>.</p>";
            await _emailService.SendEmailAsync("msareef232@gmail.com", "Password Reset", message);

            return Ok(new { Message = "Enter the generated OTP within 2 Minutes." });
        }
        
    }
}
    