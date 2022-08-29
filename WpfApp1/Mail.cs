using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace WpfApp1
{
    class Mail
    {
        string _login;
        string _pass;
        string _server;
        int _port;
        public Mail(string login, string pass, string server, int port)
        {
            _login = login;
            _pass = pass;
            _server = server;
            _port = port;
        }

        public void SendMail( string to, string header, string body, string attach =null)
        {
            MailMessage message = new MailMessage(_login, to, header, body);
            if (attach != null)
                message.Attachments.Add(new Attachment(attach));
            SmtpClient smtpClient = new SmtpClient(_server, _port);
            smtpClient.Credentials = new NetworkCredential(_login, _pass);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

    }
}
