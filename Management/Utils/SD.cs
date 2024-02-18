namespace Management.Util
{
    public class SD
    {
        public const string MPVI_SUPER_ADMIN = "D54C9BFA74772AA903F2B2E75B6F2E904CD78E211E84F0F5DC7CFDAF1EC8425E";
        public const string MPVI_WAREHOUSE_MANAGER = "035EFFEC9B01FEA585CE58965864F4BDA5A3B1F53875490BAD28B03FDA3F1A6B";
        public const string MPVI_ECONOMIC_LEAD = "DD3AAC2160E629D89A195477696BCD3CB6E7B6B4462A65C321783E9DBBB35DFA";
        public const string MPVI_MEMBER = "5FCDC8D522F265D0E4556A688F98270AA5B50C0D52ECCEBE17B371EA396A9424";
        public const string MPVI_CUSTOMER = "DE180E98F86B1D361913D97CF14966448CD57BEBc886710FC5A2CF1F6DA65222";
        public const string MPVI_ANYNOMOUS = "89A6CF1565A680AAC3F698C0D56DEBE9DF977E89DA38357651A648073AF825A4";
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
                case "MPVI_SUPER_ADMIN": return MPVI_SUPER_ADMIN;
                case "MPVI_WAREHOUSE_MANAGER": return MPVI_WAREHOUSE_MANAGER;
                case "MPVI_ECONOMIC_LEAD": return MPVI_ECONOMIC_LEAD;
                case "MPVI_MEMBER": return MPVI_MEMBER;
                case "MPVI_CUSTOMER": return MPVI_CUSTOMER;
                default: return MPVI_ANYNOMOUS;
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
