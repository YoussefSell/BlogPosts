using ResultNet;
using System;

namespace ToThrowOrToReturn
{
    public static class EmailService
    {
        public static void SendNotificationEmail_WithExceptions(string userEmail, string templateId)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentException("user email is null or empty", nameof(userEmail));

            if (string.IsNullOrEmpty(templateId))
                throw new ArgumentException("templateId is null or empty", nameof(templateId));

            var template = LoadTemplate(templateId);
            if (template is null)
                throw new EmailTemplateNotFoundException(templateId);

            sendEmail(new SendTemplateEmailOptions
            {
                To = userEmail,
                template = template,
                From = "some value",
                SmtpOptions = "some value",
            });
        }

        public static Result SendNotificationEmail_WithResultObject(string userEmail, string templateId)
        {
            if (string.IsNullOrEmpty(userEmail))
                return Result.Failure()
                    .WithMessage("user email is null or empty")
                    .WithCode("user_email_required");

            if (string.IsNullOrEmpty(templateId))
                return Result.Failure()
                    .WithMessage("templateId is null or empty")
                    .WithCode("template_id_required");

            var template = LoadTemplate(templateId);
            if (template is null)
                return Result.Failure()
                    .WithMessage("couldn't locate any email template with the given id")
                    .WithCode("email_template_not_found")
                    .WithMataData("templateId", templateId);

            sendEmail(new SendTemplateEmailOptions
            {
                To = userEmail,
                template = template,
                From = "some value",
                SmtpOptions = "some value",
            });

            return Result.Success();
        }

        private static void sendEmail(SendTemplateEmailOptions sendTemplateEmailOptions)
        {
            
        }

        private static object LoadTemplate(string key)
        {
            return new { };
        }
    }

    internal class SendTemplateEmailOptions
    {
        public string To { get; set; }
        public object template { get; set; }
        public object From { get; set; }
        public object SmtpOptions { get; set; }
    }

    public class EmailTemplateNotFoundException : Exception
    {
        public EmailTemplateNotFoundException(string templateId)
            : base(message: $"template with id [{templateId}] not exist")
        {

        }
    }
}
