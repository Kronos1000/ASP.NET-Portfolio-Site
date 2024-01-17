using PatrickWare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;
using PatrickWare.Models;

namespace Catlins_Honey_NZ.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(UserMessage model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                SendEmail(model);
                // Store success message in TempData
                TempData["SuccessMessage"] = "Email sent successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors during sending
                _logger.LogError(ex, "Error while sending email.");
                TempData["ErrorMessage"] = "Failed to send email. Please try again later.";
                return RedirectToAction("Index");
            }
        }


        public RedirectToActionResult SendEmail(UserMessage model)
        {
            string fromEmail = "Catlinshoney9874@outlook.com"; // this will be address that the form will use to send email 

            string toEmail = "Catlinshoney9874@outlook.com"; // change the address of the current email 
            string subject = model.Subject;
            ////string body = model.Message;

            string body = $"Customer Name: {model.Name}\nCustomer Email: {model.Email}\n\n{model.Message}";

            // Create the MailMessage object
            MailMessage message = new MailMessage(fromEmail, toEmail, subject, body);

            // Configure the SMTP client
            using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
            {
                // Set the credentials for authentication
                smtpClient.Credentials = new System.Net.NetworkCredential("Catlinshoney9874@outlook.com", "Password1234%^");

                // Enable SSL encryption (required for most email providers)
                smtpClient.EnableSsl = true;

                try
                {
                    // Send the email
                    smtpClient.Send(message);

                    TempData["SuccessMessage"] = "Message Sent Successfully";
                    _logger.LogInformation("Email sent successfully.");


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email.");
                    TempData["ErrorMessage"] = "Failed to send email. Please try again later.";
                    throw; // Rethrow the exception to be caught in the SendMessage() method.
                }
            }
            return RedirectToAction("Index");
        }

        // Other actions as before

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
