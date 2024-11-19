namespace SAPPortal.Models
{
    public class CommonSessionClass
    {
        public static string UId { get; set; }
        public static string UName { get; set; } 
        public static string CompanyId { get; set; }
        public static string CompanyName { get; set; }
        public static string LogInTime { get; set; }
        public static string? SAPId { get; set; }
        public static string GroupId { get; set; }
     
        public static  List<M_MENU> allowedMenus = new List<M_MENU>();

        public static List<MenuStruct> menuStructData;
        public static List<string> columnNames = new List<string>();

        public static bool IsAuthenticated { get; set; } = false;
    }
}
