namespace MailAPI.Settings
{
    public class EmailSettings
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPEmail { get; set; }
        public string SMTPPassword { get; set; }
        public string EmailFrom { get; set; }
    }
}
