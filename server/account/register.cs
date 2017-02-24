#region

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using db;
using MySql.Data.MySqlClient;
using System.Net.Mail;

#endregion

namespace server.account
{
    internal class register : RequestHandler
    {
        protected override void HandleRequest()
        {
            if (Query["isAgeVerified"] != "1")
            {
                using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                    wtr.Write("<Error>WebRegister.ineligible_age</Error>");
                return;
            }

            if(!IsValidEmail(Query["newGUID"]))
            {
                using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                    wtr.Write("<Error>WebRegister.invalid_email_address</Error>");
                return;
            }

            using (Database db = new Database())
            {
                byte[] status;
                if (!IsValidEmail(Query["newGUID"]))
                    status = Encoding.UTF8.GetBytes("<Error>WebForgotPasswordDialog.emailError</Error>");
                if (db.HasUuid(Query["guid"]) &&
                    !db.Verify(Query["guid"], "", Program.GameData).IsGuestAccount)
                {
                    if (db.HasUuid(Query["newGUID"]))
                        status = Encoding.UTF8.GetBytes("<Error>Error.emailAlreadyUsed</Error>");
                    else
                    {
                        MySqlCommand cmd = db.CreateQuery();
                        cmd.CommandText =
                            "UPDATE accounts SET uuid=@newUuid, name=@newUuid, password=SHA1(@password), guest=FALSE WHERE uuid=@uuid, name=@name;";
                        cmd.Parameters.AddWithValue("@uuid", Query["guid"]);
                        cmd.Parameters.AddWithValue("@newUuid", Query["newGUID"]);
                        cmd.Parameters.AddWithValue("@password", Query["newPassword"]);

                        if (cmd.ExecuteNonQuery() > 0)
                            status = Encoding.UTF8.GetBytes("<Success />");
                        else
                            status = Encoding.UTF8.GetBytes("<Error>Error.emailAlreadyUsed</Error>");
                    }
                }
                else
                {
                    var verifyEmail = Program.Settings.GetValue<bool>("verifyEmail");
                    Account acc = db.Register(Query["newGUID"], Query["newPassword"], false, Program.GameData, !verifyEmail);
                    if (acc != null)
                    {
                        if (verifyEmail)
                        {
                            MailMessage message = new MailMessage();
                            message.To.Add(Query["newGUID"]);
                            message.IsBodyHtml = true;
                            message.Subject = "Please verify your account.";
                            message.From = new MailAddress(Program.Settings.GetValue<string>("serverEmail", ""));
                            message.Body = "<center>Please verify your email via this <a href=\"" + Program.Settings.GetValue<string>("serverDomain", "localhost") + "/account/validateEmail?authToken=" + acc.AuthToken + "\" target=\"_blank\">link</a>.</center>";
                            Program.SendEmail(message, true);
                        }
                        status = Encoding.UTF8.GetBytes("<Success/>");
                    }
                    else
                        status = Encoding.UTF8.GetBytes("<Error>Error.emailAlreadyUsed</Error>");
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }

        public bool IsValidEmail(string strIn)
        {
            //https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx

            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            MatchEvaluator DomainMapper = match =>
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            };

            // Use IdnMapping class to convert Unicode domain names. 
            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}