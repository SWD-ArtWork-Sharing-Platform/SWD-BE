using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class ID_F_ResetPass
    {
        [Key]
        public string Gmail { get; set; } = "";
        public string Code { get; set; } = "";
        public DateTime exprired_time {  get; set; } = DateTime.Now.AddMinutes(15);
    }
}
