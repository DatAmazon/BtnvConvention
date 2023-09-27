namespace DB.DbAccess
{
    public class SetConst
    {
        public static string connectionString = "Data Source=DESKTOP-S3UOKJU\\SQLEXPRESS;Initial Catalog=Lession2DB;Integrated Security=True";

        public static string GetListProduct = "ProcGetListPro";
        public static string GetProductById = "ProcGetProByID";
        public static string CreateProduct = "ProcCreateProduct";
        public static string EditProductById = "ProcEditProById";
        public static string DeleteProductById = "ProcDeleteById";
        public static string SearchProByName = "searchProByName";

        public static string GetOrders = "ProcGetListOrder";
        public static string GetOrderById = "ProcGetOrderByID";
        public static string DeleteOrder = "ProcDeleteOrderById";
        public static string CreateOrder = "ProcCreateOrder";
        public static string searchOrderByID = "searchOrderById";
        public static string searchOrderByCustomerName = "ProcGetOrderByCustomerName";
        public static string searchOrderByTime = "ProcSearchOrderByTime";
        public static string DetailAllProductInOrderId = "DetailAllProductInOrderId";

        public static string GetOrderDetails = "ProcGetListOrder_Details";
        public static string GetOrderDetailById = "ProcGetOrder_DetailByID";
        public static string CreateOrderDetail = "ProcCreateOrderDetail";
        public static string searchOrderDetailByID = "searchOrder_DetailById";
    }
}
