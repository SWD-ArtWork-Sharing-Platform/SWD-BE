using Market.Models;

namespace Market.Utils
{
    public class Refresh
    {
        public static DCategory Category(DCategory obj)
        {
            obj.FArtworks = null;
            obj.CategoryId = null;

            return obj;
        }

        public static DOrderDetail DOrderDetail(DOrderDetail obj)
        {
            obj.OrderDetailId = null;
            obj.Order = null;

            return obj;
        }

        public static DPackageOfCreator DPackageOfCreator(DPackageOfCreator obj)
        {
            obj.Id = null;
            obj.Package = null;

            return obj;
        }

        public static FArtwork FArtwork(FArtwork obj)
        {
            obj.ArtworkId = null;
            obj.Category = null;
            obj.DOrderDetails = null;

            return obj;
        }
        public static FOrder FOrder(FOrder obj)
        {
            obj.OrderId = null;
            obj.DOrderDetails = null;

            return obj;
        }

        public static FPackage FPackage(FPackage obj)
        {
            obj.PackageId = null;
            obj.DPackageOfCreators = null;

            return obj;
        }

        public static FPayment FPayment(FPayment obj)
        {
            obj.PaymentId = null;
            obj.Order = null;

            return obj;
        }

        public static FWishlist FWishlist(FWishlist obj)
        {
            obj.WishlistId = null;

            return obj;
        }

        public static FReport FReport(FReport obj)
        {
            obj.ReportId = null;

            return obj;
        }

    }
}
