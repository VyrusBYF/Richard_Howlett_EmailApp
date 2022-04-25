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
    public class SendEmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailConfig _emailConfig;
        private readonly ApplicationDbContext _context;


        //private readonly EmailMessage testEmail = new EmailMessage("richard.python14@gmail.com", "Test Email Async Version", "This is a test body");

        public SendEmailController(IEmailSender emailSender, ApplicationDbContext context, EmailConfig config)
        {
            _emailSender = emailSender;
            _context = context;
            _emailConfig = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmailMessage>>> Get()
        {
            return Ok(await _context.Emails.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<EmailMessage>>> SendMailAsync(EmailMessage message)
        {
            Email email = new(
                _emailConfig.From,
                message.To,
                message.Subject,
                message.Content,
                Email.DeliveryStatus.Sending
            );
            if (Response.StatusCode == 200)
            {
                await _emailSender.SendEmailAsync(message);
                email.Status = Email.DeliveryStatus.Delivered;
                _context.Emails.Add(email);
                _context.SaveChanges();
                return Ok(await _context.Emails.ToListAsync());
            }else
            {
                email.Status = Email.DeliveryStatus.Failed;
                _context.Emails.Add(email);
                _context.SaveChanges();
                return BadRequest();
            }
        }
    }
}
