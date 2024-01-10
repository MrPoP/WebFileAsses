using WebFileAsses.Models;

namespace WebFileAsses
{
    public static class Constants
    {
        static Dictionary<byte, Priority> AvaliablePriorities = new Dictionary<byte, Priority>()
        {
            { 0, new Priority(){ID = 0, Name = "Low", FilePath = @"\Low\"} },
            { 1,  new Priority(){ID = 1, Name = "High", FilePath = @"\High\"} },
            { 2,  new Priority(){ID = 2, Name = "Critical", FilePath = @"\Critical\"} },
        };
        public static string GetSavePath(byte Id)
        {
            string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads\";
            if (AvaliablePriorities.TryGetValue(Id, out Priority? pro))
            {
                str = Path.Combine(str, pro.FilePath);
            }
            return str;
        }
        public static string GenericStorageName()
        {
            return Path.GetTempFileName();
        }
    }
}
