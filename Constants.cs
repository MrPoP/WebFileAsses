using System;
using WebFileAsses.Models;

namespace WebFileAsses
{
    public static class Constants
    {
        static Random random = new Random();
        static Dictionary<byte, Priority> AvaliablePriorities = new Dictionary<byte, Priority>()
        {
            { 0, new Priority(){ID = 0, Name = "Low", FilePath = @"\Low\"} },
            { 1,  new Priority(){ID = 1, Name = "High", FilePath = @"\High\"} },
            { 2,  new Priority(){ID = 2, Name = "Critical", FilePath = @"\Critical\"} },
        };
        public static string PriorityName(byte priority)
        {
            string str = "";
            if (AvaliablePriorities.TryGetValue(priority, out Priority? pro))
            {
                str = pro.Name;
            }
            return str;
        }
        public static string GetSavePath(byte Id)
        {
            string str = "";
            if (AvaliablePriorities.TryGetValue(Id, out Priority? pro))
            {
                str = Path.Combine(str, pro.FilePath);
            }
            return str;
        }
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GenericStorageName()
        {
            return new string(Enumerable.Repeat(chars, 10)
        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
