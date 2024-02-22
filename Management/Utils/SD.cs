namespace Management.Util
{
    public class SD
    {
        public const string ROLE_SUPER_ADMIN = "";
        public const string ROLE_MANAGER = "";
        public const string ROLE_CUSTOMER = "";
        public const string ROLE_ANYNOMOUS = "";
        public const string ROLE_ARTIST = "";
        public enum ArtworkStatus
        {
            All,
            NotAvailable,
            Available,
            Expired,
            Deleted
        }
        public static string CheckRole(string input)
        {
            switch (input)
            {
                case "ROLE_SUPER_ADMIN": return ROLE_SUPER_ADMIN;
                case "ROLE_MANAGER": return ROLE_MANAGER;
                case "ROLE_CUSTOMER": return ROLE_CUSTOMER;
                case "ROLE_ARTIST": return ROLE_ARTIST;
                default: return ROLE_ANYNOMOUS;
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
