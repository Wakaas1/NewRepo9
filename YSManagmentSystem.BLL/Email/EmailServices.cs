using Dapper;
using System.Net.Mail;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.User;


namespace YSManagmentSystem.BLL.Email
{
        public class EmailServices : IEmailServices
        {
            private readonly IDapperRepo _dapper;
            public EmailServices(IDapperRepo dapper)
            {
                _dapper= dapper;
            }
            private List<EmailCreadential> GetEmailCread()
            {
                DynamicParameters parameters = new DynamicParameters();
                return _dapper.ReturnList<EmailCreadential>("GetEmailCreed", parameters).ToList();
            }

            public void SendEmail(string email, string token, string name)
            {
            
                var emailcread = GetEmailCread().ToArray();
                string Link = token;
                var emailTemp = emailcread[0].EmailTemp.ToString();
                string mailBody = string.Format(emailTemp, name, Link);
            
                MailMessage message =
                    new MailMessage(new MailAddress(emailcread[0].UserName, "BitLayer"), new MailAddress(email));
                message.Subject = "otp";
                message.Body = mailBody;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = emailcread[0].UserName;
                credentials.Password = emailcread[0].Password;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.Send(message);
        }
        //public void SendEmail(string email, string token, string name)
        //{


        //    string Link = token;



        //    var emailCredential = _configuration.GetSection("EmailCredentials");

        //    var data = emailCredential.Get<EmailCredential>();

        //    var path = _webHost.ContentRootPath + Path.DirectorySeparatorChar.ToString()
        //        + "Email" + Path.DirectorySeparatorChar.ToString() + "EmailTemp.html";
        //    string pathToDb = @"wwwroot\Email\EmailTemp.html";



        //    string mailHtmlbody = "";

        //    using (StreamReader steamReader = File.OpenText(pathToDb))
        //    {
        //        mailHtmlbody = steamReader.ReadToEnd();
        //    }


        //    mailHtmlbody = mailHtmlbody.Replace("#:HrefLink:#", Link);

        //    mailHtmlbody = mailHtmlbody.Replace("#:Name:#", name);
        //    string mailBody = string.Format(mailHtmlbody, name, Link);


        //    MailMessage message = new MailMessage(new MailAddress(data.UserName, "DapperProcSP"), new MailAddress(email));
        //    message.Subject = "EmailConfirmation";
        //    message.Body = mailHtmlbody;
        //    message.IsBodyHtml = true;


        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp-mail.outlook.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


        //    System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
        //    credential.UserName = data.UserName;
        //    credential.Password = data.Password;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = credential;

        //    smtp.Send(message);



        //}
    }
}
