namespace MailCore.Models
{
    public class Message
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Массив получателей
        /// </summary>
        public string[] Recipients { get; set; }
    }
}
