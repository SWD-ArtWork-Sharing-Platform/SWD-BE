namespace Auth.Models.DTO
{
    public class MailCheckDTO
    {
        public string email { get; set; }
        public string autocorrect { get; set; }
        public string deliverability { get; set; }
        public string quality_score { get; set; }
        public Value_Text is_valid_format { get; set; }
        public Value_Text is_free_email { get; set; }
        public Value_Text is_disposable_email { get; set; }
        public Value_Text is_role_email { get; set; }
        public Value_Text is_catchall_email { get; set; }
        public Value_Text is_mx_found { get; set; }
        public Value_Text is_smtp_valid { get; set; }


    }


    public class Value_Text
    {
        public bool value { get; set; } = false;
        public string text { get; set; }
    }
}
