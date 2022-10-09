namespace MailCore.Infrastructure
{
    using MailCore.Models;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public interface IAction
    { Task<RecordSending> Result(Message message); }

    public class Sending : IAction
    {

        private readonly string _smtpHost, _smtpEmail, _smtpPass, _emailFrom;
        private readonly int _smtpPort;

        public Sending(string smtpHost, int smtpPort, string smtpEmail, string smtpPass, string emailFrom)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpEmail = smtpEmail;
            _smtpPass = smtpPass;
            _emailFrom = emailFrom;
        }

        /// <summary>
        /// Выполнение отправки сообщения
        /// </summary>
        /// <param Параметры сообщения="message"></param>
        /// <returns>Запись о результат отправки</returns>
        public async Task<RecordSending> Result(Message message)
        {
            var _sendingStatus = SendingStatus.OK;
            var _failedMessage = string.Empty;
            using (MailMessage _message = new MailMessage())
            {

                foreach (string recipient in message.Recipients)
                    if (isValidEmail(recipient))
                        _message.To.Add(recipient);
                _message.From = new MailAddress(_emailFrom);
                _message.Subject = message.Subject;
                _message.Body = message.Body;

                using (SmtpClient _client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    _client.Credentials = new NetworkCredential(_smtpEmail, _smtpPass);
                    _client.EnableSsl = true;
                    try
                    {
                        await _client.SendMailAsync(_message);
                    }
                    catch (Exception ex)
                    {
                        _sendingStatus = SendingStatus.Failed;
                        _failedMessage = ex.Message;
                    }
                    return new RecordSending(message, _sendingStatus, _failedMessage);
                }
            }
        }

        private static bool isValidEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9]+.+[a-z]{2,6})";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
