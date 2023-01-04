using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace inventory.Data
{
    public static class ItemLogService
    {
        private static void SaveAll(Guid userId, List<ItemLog> itemlog)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string LogFilePath = Utils.GetLogFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(itemlog);
            File.WriteAllText(LogFilePath, json);
        }

        public static List<ItemLog> GetAll(Guid userId)
        {
            string LogFilePath = Utils.GetRequestFilePath();
            if (!File.Exists(LogFilePath))
            {
                return new List<ItemLog>();
            }

            var json = File.ReadAllText(LogFilePath);

            return JsonSerializer.Deserialize<List<ItemLog>>(json);
        }

        public static List<ItemLog> Create(Guid userId, Guid takenuserid, Guid itemid, string itemname, int quantity)
        {
            //if (dueDate < DateTime.Today)
            //{
            //    throw new Exception("Due date must be in the future.");
            //}

            List<ItemLog> log = GetAll(userId);
            log.Add(new ItemLog
            {
                ItemName = itemname,
                Quantity = quantity,
                ItemId = itemid,
                Approved_By = userId,
                Request_by = takenuserid
            });
            SaveAll(userId, log);
            return log;
        }

        public static List<ItemLog> Delete(Guid userId, Guid id)
        {
            List<ItemLog> logs = GetAll(userId);
            ItemLog log = logs.FirstOrDefault(x => x.Id == id);

            if (logs == null)
            {
                throw new Exception("request not found.");
            }

            logs.Remove(log);
            SaveAll(userId, logs);
            return logs;
        }

        public static void DeleteByLogId()
        {
            string LogFilePath = Utils.GetLogFilePath();
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }
        }

        //public static List<ItemModel> Update(Guid userId, Guid id, string itemname, decimal price, int quantity)
        //{
        //    List<ItemModel> items = GetAll(userId);
        //    ItemModel itemtoupdate = items.FirstOrDefault(x => x.Id == id);

        //    if (itemtoupdate == null)
        //    {
        //        throw new Exception("item not found.");
        //    }

        //    itemtoupdate.ItemName = itemname;
        //    itemtoupdate.Price = price;
        //    itemtoupdate.Quantity = quantity;
        //    SaveAll(userId, items);
        //    return items;
        //}
    }

}
