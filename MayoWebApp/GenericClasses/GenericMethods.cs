using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using static MayoWebApp.GenericClasses.Enums;

namespace MayoWebApp.GenericClasses
{
    public class GenericMethods
    {

        public static void SendEmailNotification(List<String> toEmailIds, string subject, string messageBody)
        {
            //MailMessage msg = new MailMessage();
            MimeMessage msg = new MimeMessage();
            try
            {
                //MailboxAddress from = new MailboxAddress("no-reply@mymayo.us");
                msg.From.Add(new MailboxAddress("no-reply@mymayo.us"));
                foreach (var item in toEmailIds)
                    msg.To.Add(new MailboxAddress(item));
                msg.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = messageBody;
                msg.Body = bodyBuilder.ToMessageBody();

                MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
                client.Connect("a2plcpnl0571.prod.iad2.secureserver.net", 465, true);
                client.Authenticate("no-reply@mymayo.us", "1234Abcd@@");

                client.Send(msg);
                client.Disconnect(true);
                client.Dispose();


                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("a2plcpnl0571.prod.iad2.secureserver.net");

                //mail.From = new MailAddress("no-replay@mymayo.us");
                //foreach (var item in toEmailIds)
                //    mail.To.Add(new MailAddress(item));
                //mail.Subject = subject;
                //mail.Body = messageBody;

                //SmtpServer.Port = 465;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("no-replay@mymayo.us", "1234Abcd@@");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Timeout = 5000;
                //SmtpServer.Send(mail);

                Log(LogType.ActivityLog.ToString(), "Email Sent Successfully!");

            }
            catch (Exception ex)
            {
                Log(LogType.ErrorLog.ToString(), ex.ToString());
            }

        }

        public static void Log(string logType, string logMessage)
        {
            try
            {
                if (!Directory.Exists(@"C:\myMayoLogs\" + logType))
                {
                    Directory.CreateDirectory(@"C:\myMayoLogs\" + logType);
                }

                string logPath = @"C:\myMayoLogs\" + logType;
                if (!File.Exists(logPath + "\\log_" + DateTime.Now.ToShortDateString() + ".txt"))
                {
                    File.Create(logPath + "\\log_" + DateTime.Now.ToShortDateString() + ".txt");
                }

                //using (StreamWriter w = File.AppendText(logPath + "\\log_" + DateTime.Now.ToShortDateString() + ".txt"))
                using (StreamWriter w = new StreamWriter(logPath + "\\log_" + DateTime.Now.ToShortDateString() + ".txt", true))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    w.WriteLine("  Error Message :");
                    w.WriteLine($" {logMessage}");
                    w.WriteLine("==============================================================");
                }
            }
            catch (Exception ex)
            {
                // throw;
            }
        }

        public static string GenerateSaltedHash(string password, int size = 64)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            //HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            string hashSalt = hashPassword + "|" + salt;
            return hashSalt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
