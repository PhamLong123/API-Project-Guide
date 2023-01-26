using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using RedExpress.Helper.Utility;
using MSA.CS.Interface;
using MSA.CS.Util;

namespace MSA.SMS.Mail.SMTP
{
    /// <summary>
    /// SMTP Service
    /// </summary>
    public class SMTPService : BaseMailService
    {
        /// <summary>
        /// Send mail to an email address
        /// (Override)
        /// </summary>
        /// <param name="messageInfo">Message Info</param>
        /// <returns>Result Status</returns>
        public override Result Send(MessageInfo messageInfo)
        {
            // Create result
            Result result = new Result();
            string tos = string.Empty;
            string subject = string.Empty;
            string content = string.Empty;
            bool status = false;
            string message = string.Empty;

            if (messageInfo.Tos != null && messageInfo.Tos.Count > 0)
            {
                status = true;
                var emailSettings = AppSettings.ApiSettings.EmailSettings
                    .Intance(messageInfo.Provider);
                if (messageInfo.From == null)
                {
                    messageInfo.From = new FromObject();
                }
                if (string.IsNullOrWhiteSpace(messageInfo.From.UserName))
                {
                    messageInfo.From.UserName = emailSettings.User;
                }
                if (string.IsNullOrWhiteSpace(messageInfo.From.Pwd))
                {
                    messageInfo.From.Pwd = emailSettings.Password;
                }
                if (string.IsNullOrWhiteSpace(messageInfo.From.Alias))
                {
                    messageInfo.From.Alias = emailSettings.Alias;
                }
                if (string.IsNullOrWhiteSpace(messageInfo.From.Address))
                {
                    messageInfo.From.Address = emailSettings.From;
                }
                foreach (ToObject itemTo in messageInfo.Tos)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(tos))
                        {
                            tos += AppConstants.Semicolon;
                        }
                        tos += itemTo.To;

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(
                            messageInfo.From.Address, messageInfo.From.Alias);

                        // Fill parameter
                        subject = messageInfo.ContentParameter.Subject;
                        content = messageInfo.ContentParameter.Content;
                        if (itemTo.Parameters != null)
                        {
                            foreach (var paramater in itemTo.Parameters)
                            {
                                subject = subject.Replace(AppConstants.Sharp +
                                    paramater.Key + AppConstants.Sharp,
                                    paramater.Value != null ? paramater.Value
                                    .ToString() : string.Empty);
                                content = content.Replace(AppConstants.Sharp +
                                    paramater.Key + AppConstants.Sharp,
                                    paramater.Value != null ? paramater.Value
                                    .ToString() : string.Empty);
                            }
                        }

                        // Add mail list to
                        mailMessage.To.Add(itemTo.To);
                        // Add mail list to by cc
                        if (itemTo.CC != null && itemTo.CC.Length > 0)
                        {
                            foreach (string mailCc in itemTo.CC)
                            {
                                tos += (AppConstants.Semicolon + mailCc);
                                mailMessage.CC.Add(mailCc);
                            }
                        }
                        // Add mail list to by bcc
                        if (itemTo.BCC != null && itemTo.BCC.Length > 0)
                        {
                            foreach (string mailBcc in itemTo.BCC)
                            {
                                tos += (AppConstants.Semicolon + mailBcc);
                                mailMessage.Bcc.Add(mailBcc);
                            }
                        }
                        mailMessage.Subject = subject;
                        mailMessage.Body = content;
                        mailMessage.IsBodyHtml = true;
                        if (itemTo.Attachments != null)
                        {
                            foreach (Attachment attachment in itemTo
                                .Attachments.Select(n => new Attachment(n)))
                            {
                                mailMessage.Attachments.Add(attachment);
                            }
                        }

                        using (SmtpClient smtpClient = new SmtpClient(
                            emailSettings.Address, emailSettings.Port))
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod
                                .Network;
                            smtpClient.Credentials = new NetworkCredential(
                                messageInfo.From.UserName, 
                                messageInfo.From.Pwd);
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(mailMessage);
                        }

                        status = status && true;
                        message += (itemTo.To + AppConstants.Hyphen +
                            Result.Status.Success.ToString()
                            .Translate(messageInfo.Language));
                    }
                    catch (Exception ex)
                    {
                        // Set result status
                        status = false;
                        message += (itemTo.To + AppConstants.Hyphen + 
                            ex.Message);
                    }
                }
            }

            // Set result status
            if (status)
            {
                result.Code = Result.Status.Success;
            }
            else
            {
                result.Code = Result.Status.SendFail;
            }
            result.Message = message;

            return result;
        }
    }
}
