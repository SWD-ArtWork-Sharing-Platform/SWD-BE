namespace Management.Util
{
    public class SD
    {
        
        public enum ArtworkStatus
        {
            All,
            NotAvailable,
            Available,
            Expired,
            Deleted
        }
        public const string CUSTOMER = "8df6bc3f93a218ee4cb31427312f62d4498cabd22742e5dea3561e16b3e5b260";
        public const string CREATOR = "fdd5f6745e3ae0acae613e1e69c3042a0bee192e81d812448dec52022cff2aa1";
        public const string ADMIN = "835d6dc88b708bc646d6db82c853ef4182fabbd4a8de59c213f2b5ab3ae7d9be";
        public const string MODERATOR = "778ac5b81fa251b450f827846378739caee510c31b01cfa9d31822b88bed8441";
        public const string ANYNOMOUS = "6baf58a5ce7a1fcb2a9f146292751122d9ab707d4c54db39c301b7b266709283";

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

        public static ArtworkStatus CheckArtworkStatus(string input)
        {
            switch (input)
            {
                case "Available": return ArtworkStatus.Available;
                case "NotAvailable": return ArtworkStatus.NotAvailable;
                case "Expired": return ArtworkStatus.Expired;
                case "Deleted": return ArtworkStatus.Deleted;
                default: return ArtworkStatus.All;
            }

        }
    }
}
