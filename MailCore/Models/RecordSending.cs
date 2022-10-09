using System;

namespace MailCore.Models
{
    public class RecordSending
    {

        /// <summary>
        /// Id записи об отправке сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время отправки
        /// </summary>
        public DateTime timeSending { get; set; }

        /// <summary>
        /// Результат отправки (OK или Failed)
        /// </summary>
        public SendingStatus Result { get; set; }

        /// <summary>
        /// Сообщение об ошибке отправки
        /// </summary>
        public string FailedMessage { get; set; }

        /// <summary>
        /// Внешний ключ к Id сообщения
        /// </summary>
        public int MessageId { get; set; }
        public Message Message { get; set; }

        public RecordSending() { }

        public RecordSending(Message message, SendingStatus result, string failedMessage)
        {
            this.timeSending = DateTime.Now;
            this.MessageId = message.Id;
            this.Message = message;
            this.Result = result;
            this.FailedMessage = failedMessage;
        }

        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var infoString = $"{timeSending:G}: ( {Result} [{FailedMessage}]) Тема: {Message.Subject} \n Сообщение: {Message.Body}";
            return infoString;
        }
    }
}
