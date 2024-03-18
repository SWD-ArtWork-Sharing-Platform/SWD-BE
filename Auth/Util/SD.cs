namespace Auth.Util
{
    public class SD
    {
        public const string CUSTOMER = "8DF6BC3F93A218EE4CB31427312F62D4498CABD22742E5DEA3561E16B3E5B260";
        public const string CREATOR = "FDD5F6745E3AE0ACAE613E1E69C3042A0BEE192E81D812448DEC52022CFF2AA1";
        public const string ADMIN = "835D6DC88B708BC646D6DB82C853EF4182FABBD4A8DE59C213F2B5AB3AE7D9BE";
        public const string MODERATOR = "778AC5B81FA251B450F827846378739CAEE510C31B01CFA9D31822B88BED8441";
        public const string ANYNOMOUS = "6BAF58A5CE7A1FCB2A9F146292751122D9AB707D4C54DB39C301B7B266709283";

        public const string ACTIVE = "Active";
        public const string INACTIVE = "Inactive";


        public static string CheckRole(string input)
        {
            switch (input)
            {
                case "CUSTOMER": return CUSTOMER;
                case "CREATOR": return CREATOR;
                case "ADMIN": return ADMIN;
                case "MODERATOR": return MODERATOR;
                default: return ANYNOMOUS;
            }

        }
    }
}
