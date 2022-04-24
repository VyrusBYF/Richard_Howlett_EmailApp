using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.Extensions.Configuration;

namespace EmailApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        private readonly EmailConfig _emailConfig;
        private readonly ApplicationDbContext _context;


        private readonly EmailMessage testEmail = new EmailMessage(new string[] { "richard.python14@gmail.com" }, "Test Email Async Version", "This is a test body");

        public SendEmailController(IEmailSender emailSender, ApplicationDbContext context, EmailConfig config)
        {
            _emailSender = emailSender;
            _context = context;
            _emailConfig = config;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<EmailMessage>>> Get()
        {
            await _emailSender.SendEmailAsync(testEmail);
            return Ok(await _context.Emails.ToListAsync());
        }

        [HttpPost]
        public async Task SendMailAsync()
        {
            await _emailSender.SendEmailAsync(testEmail);

           
            /*Email email = new Email(
                _emailConfig.From,
                testEmail.To[0],
                testEmail.Subject,
                testEmail.Content,
                Email.DeliveryStatus.Delivered
            );

            _context.Emails.Add(email);
            _context.SaveChanges();*/


            /*Email email = new Email(
                message.To[0].Name,
                _emailConfig.From,
                message.Subject,
                message.Content
            );*/


        }
    }
}
