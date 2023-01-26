using System;
using System.Collections.Specialized;
using System.Text;
using CRMProfileShared;
using CSService.Models;

namespace CSService.Providers
{
    public class ElasticService : BaseMailService
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

            if (messageInfo.From == null || string.IsNullOrWhiteSpace(
                messageInfo.From.Key) || string.IsNullOrWhiteSpace(messageInfo
                .From.Alias) || string.IsNullOrWhiteSpace(messageInfo.From
                .Address))
            {
                result.Code = Status.ProviderNotInput;
                return result;
            }

            if (messageInfo.Tos != null && messageInfo.Tos.Count > 0)
            {
                status = true;
                foreach (ToObject itemTo in messageInfo.Tos)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(tos))
                        {
                            tos += Constants.Format.SemiColon;
                        }
                        tos += itemTo.To;

                        // Fill parameter
                        subject = messageInfo.ContentParameter.Subject;
                        content = messageInfo.ContentParameter.Content;
                        if (itemTo.Parameters != null)
                        {
                            foreach (var paramater in itemTo.Parameters)
                            {
                                /*subject = subject.Replace(Constants.Format. +
                                    paramater.Key + AppConstants.Sharp,
                                    paramater.Value != null ? paramater.Value
                                    .ToString() : string.Empty);
                                content = content.Replace(AppConstants.Sharp +
                                    paramater.Key + AppConstants.Sharp,
                                    paramater.Value != null ? paramater.Value
                                    .ToString() : string.Empty);*/
                            }
                        }

                        // Add mail list to by cc
                        if (itemTo.CC != null && itemTo.CC.Length > 0)
                        {
                            foreach (string mailCc in itemTo.CC)
                            {
                                tos += (Constants.Format.SemiColon + mailCc);
                            }
                        }
                        // Add mail list to by bcc
                        if (itemTo.BCC != null && itemTo.BCC.Length > 0)
                        {
                            foreach (string mailBcc in itemTo.BCC)
                            {
                                tos += (Constants.Format.SemiColon + mailBcc);
                            }
                        }

                        NameValueCollection values = new NameValueCollection();
                        values.Add("apikey", messageInfo.From.Key);
                        values.Add("from", messageInfo.From.Address);
                        values.Add("fromName", messageInfo.From.Alias);
                        values.Add("to", tos);
                        values.Add("subject", subject);
                        //values.Add("bodyText", "Text Body");
                        values.Add("bodyHtml", content);
                        //values.Add("isTransactional", true);

                        string errMessage = string.Empty;
                        //status = Send(emailSettings.Address, values,
                        //    ref errMessage);

                        status = status && true;
                        if (status)
                        {
                            message += (itemTo.To + AppConstants.Hyphen +
                                Result.Status.Success.ToString()
                                .Translate(messageInfo.Language));
                        }
                        else
                        {
                            message += (itemTo.To + AppConstants.Hyphen +
                                errMessage);
                        }
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

        private bool Send(string address, NameValueCollection values,
            ref string errMessage)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, values);
                    JObject jsonResult = JObject.Parse(
                        Encoding.UTF8.GetString(apiResponse));
                    if ((bool)jsonResult["success"] == true)
                    {
                        return true;
                    }
                    else
                    {
                        errMessage = jsonResult["error"].ToString();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    errMessage = "Exception caught: " + ex.Message + "\n" +
                        ex.StackTrace;
                    return false;
                }
            }
        }
    }
}
