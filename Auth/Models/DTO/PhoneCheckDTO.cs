namespace Auth.Models.DTO
{
    public class PhoneCheckDTO
    {
        public string phone { get; set; }
        public bool valid { get; set; } = false;
        public Format format { get; set; }

        public Country country { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string carrier { get; set; }



    }



    public class Country
    {
        public string code { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }


    public class Format
    {
        public string international { get; set; }
        public string local { get; set; }

    }
    public class PhoneInputDto
    {
        public string api_key { get;} = "0b4a7b91b44e40648af2b59fb2e8190c";
        public string phone { get; set; } = "";
    }
}
