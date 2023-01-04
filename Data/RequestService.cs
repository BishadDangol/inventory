using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace inventory.Data
{
    public static class RequestService
    {
        private static void SaveAll(Guid userId, List<Request> requests)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string RequestFilePath = Utils.GetRequestFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(requests);
            File.WriteAllText(RequestFilePath, json);
        }

        public static List<Request> GetAll(Guid userId)
        {
            string RequestFilePath = Utils.GetRequestFilePath();
            if (!File.Exists(RequestFilePath))
            {
                return new List<Request>();
            }

            var json = File.ReadAllText(RequestFilePath);

            return JsonSerializer.Deserialize<List<Request>>(json);
        }

        public static List<Request> Create(Guid userId, Guid itemid, string itemname,  int quantity)
        {
            //if (dueDate < DateTime.Today)
            //{
            //    throw new Exception("Due date must be in the future.");
            //}

            List<Request> request = GetAll(userId);
            request.Add(new Request
            {
                ItemName = itemname,
                Quantity = quantity,
                ItemId = itemid,
                Request_by = userId
            });
            SaveAll(userId, request);
            return request;
        }

        public static List<Request> Delete(Guid userId, Guid id)
        {
            List<Request> requests = GetAll(userId);
            Request request = requests.FirstOrDefault(x => x.Id == id);

            if (requests == null)
            {
                throw new Exception("request not found.");
            }

            requests.Remove(request);
            SaveAll(userId, requests);
            return requests;
        }

        public static void DeleteByRequestId()
        {
            string requestFilePath = Utils.GetRequestFilePath();
            if (File.Exists(requestFilePath))
            {
                File.Delete(requestFilePath);
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
