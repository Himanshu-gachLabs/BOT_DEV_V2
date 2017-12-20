using System;
using System.Net.Mail;

namespace Saketa_BOT_V2.Dialogs
{
    public class SendEmail
    {
        public void SendEmailtoAdmin(string content, string email)
        {
            try
            {
                MailMessage mail = new MailMessage("hk.himanshu214@gmail.com", email);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                mail.Subject = "Saketa FAQ Bot: New Content";
                mail.Body = "User tried with new content: <b>" + content + "</b><br/><br/>Please train the BOT accordingly.";
                mail.IsBodyHtml = true;
                client.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "hk.himanshu214@gmail.com";
                NetworkCred.Password = "Pass@word12";
                client.UseDefaultCredentials = true;
                client.Credentials = NetworkCred;
                client.Port = 587;
                client.Send(mail);
                // MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }
    }
}