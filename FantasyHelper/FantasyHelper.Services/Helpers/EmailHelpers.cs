using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

namespace FantasyHelper.Services.Helpers
{
    public static class EmailHelpers
    {
        public static async Task<SendResponse?> SendEmail(SmtpMessageSettings settings)
        {
            Email.DefaultSender = new SmtpSender(() => new SmtpClient(settings.Host)
            {
                Port = settings.Port,
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = true
            });

            return await Email
                .From(settings.SenderEmail, settings.SenderName)
                .To(settings.ReceiverEmail, settings.ReceiverName)
                .Subject(settings.Subject)
                .Body(settings.Body, settings.BodyIsHTML)
                .PlaintextAlternativeBody(settings.PlainTextBody)
                .SendAsync();
        }

        public static string ConstructEmailBaseContent(string header, string content)
            => "<!DOCTYPE html>" +
                $"<html lang=\"en\" xmlns=\"https://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">" +
                $"<head>" +
                $"<meta charset=\"utf-8\">" +
                $"<meta name=\"viewport\" content=\"width=device-width,initial-scale=1\">" +
                $"<meta name=\"x-apple-disable-message-reformatting\">" +
                $"<!--[if !mso]><!-->" +
                $"<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />" +
                $"<!--<![endif]-->" +
                $"<title></title>" +
                $"<!--[if mso]>" +
                $"<style type=\"text/css\">" +
                $"table {{border - collapse:collapse;border:0;border-spacing:0;margin:0;}}" +
                $"div, td {{padding:0;}}" +
                $"div {{margin:0 !important;}}" +
                $"a {{text-decoration:none !important;padding:25px !important;}}" +
                $"</style>" +
                $"<noscript>" +
                $"<xml>" +
                $"<o:OfficeDocumentSettings>" +
                $"<o:PixelsPerInch>96</o:PixelsPerInch>" +
                $"</o:OfficeDocumentSettings>" +
                $"</xml>" +
                $"</noscript>" +
                $"<![endif]-->" +
                $"</head>" +
                $"<body style=\"margin:0;padding:0;word-spacing:normal;background-color:#ffffff;font-family:sans-serif;\">" +
                $"<div role=\"article\" aria-roledescription=\"email\" lang=\"en\" style=\"-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;background-color:#ffffff;\">" +
                $"<table role=\"presentation\" style=\"width:100%;border:0;border-spacing:0;\">" +
                    // Header
                    $"<tr height=\"80px\" style=\"background-color:#30323D;height:80px;\">" +
                        $"<td align=\"center\" style=\"color:white;font-size:24px;\">{header}</td>" +
                    $"</tr>" +
                    // Body
                    $"<tr style=\"margin-top:50px;margin-bottom:200px;\">" +
                        $"<td align=\"center\" style=\"line-height:30px;text-align:center;font-size:16px;\">" +
                            "<br/><br/>" + content +
                        $"</td>" +
                    $"</tr>" +
                    // Footer
                    $"<tr height=\"80px\" style=\"height:80px;margin-top:30px;margin-bottom:30px;\">" +
                        $"<td align=\"center\" style=\"border-top:2px solid #30323D;color:#30323D;font-size:14px;line-height:30px;\">Detta är ett automatiserat mail från Fantasy Helpers." +
                    $"</tr>" +
                $"</table>" +
                $"</div>" +
                $"</body>" +
                $"</html>";
    }

    public class SmtpMessageSettings
    {
        public string Host { get; set; } = default!;
        public short Port { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string SenderEmail { get; set; } = default!;
        public string SenderName { get; set; } = default!;
        public string ReceiverEmail { get; set; } = default!;
        public string ReceiverName { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;
        public bool BodyIsHTML { get; set; } = true;
        public string PlainTextBody { get; set; } = "";
    }
}
