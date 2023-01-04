using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace inventory.Data
{
    public static class ItemModelService
    {
        private static void SaveAll(Guid userId, List<ItemModel> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string ItemFilePath = Utils.GetItemFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(ItemFilePath, json);
        }

        public static List<ItemModel> GetAll(Guid userId)
        {
            string ItemFilePath = Utils.GetItemFilePath();
            if (!File.Exists(ItemFilePath))
            {
                return new List<ItemModel>();
            }

            var json = File.ReadAllText(ItemFilePath);

            return JsonSerializer.Deserialize<List<ItemModel>>(json);
        }

        public static List<ItemModel> Create(Guid userId, string itemname, decimal price, int quantity)
        {
            //if (dueDate < DateTime.Today)
            //{
            //    throw new Exception("Due date must be in the future.");
            //}

            List<ItemModel> item = GetAll(userId);
            item.Add(new ItemModel
            {
                ItemName = itemname,
                Quantity = quantity,
                Price = price
            });
            SaveAll(userId, item);
            return item;
        }

        public static List<ItemModel> Delete(Guid userId, Guid id)
        {
            List<ItemModel> items = GetAll(userId);
            ItemModel item = items.FirstOrDefault(x => x.Id == id);

            if (items == null)
            {
                throw new Exception("item not found.");
            }

            items.Remove(item);
            SaveAll(userId, items);
            return items;
        }

        public static void DeleteByItemId()
        {
            string itemFilePath = Utils.GetItemFilePath();
            if (File.Exists(itemFilePath))
            {
                File.Delete(itemFilePath);
            }
        }

        public static List<ItemModel> Update(Guid userId, Guid id, string itemname, decimal price, int quantity)
        {
            List<ItemModel> items = GetAll(userId);
            ItemModel itemtoupdate = items.FirstOrDefault(x => x.Id == id);

            if (itemtoupdate == null)
            {
                throw new Exception("item not found.");
            }

            itemtoupdate.ItemName = itemname;
            itemtoupdate.Price = price;
            itemtoupdate.Quantity = quantity;
            SaveAll(userId, items);
            return items;
        }
    }

}
    


