using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Item_and_inventory
{
    public class Inventaire : MonoBehaviour
    {

        
        public delegate void OnInventoryChange();
        public OnInventoryChange OnInventoryChangeCallBack;

        [SerializeField] private List<Item> items;
        [SerializeField] private int maxItemSpace;

        private NotificationManager notificationManager;

        public void setMaxItemSpace(int maxItemSpace)
        {
            this.maxItemSpace = maxItemSpace;
        }
        
        
        private void Awake()
        {
            items = new List<Item>();
            notificationManager = gameObject.GetComponent<NotificationManager>();
        }


        public List<Item> getItemList()
        {
            return items;
        }
            

        public bool isOwningFollowingItems(Item[] itemsToTest)
        {
            return !itemsToTest.Except(items).Any();
        }


        public bool addItem(Item itemToAdd)
        {
            if (isEnoughSpaceAvalaibleToAddOneItem)
            {
                addItemWithAnimation(itemToAdd);
                return true;
            }
            
            notificationManager.newNotification(NotificationType.INVENTAIREPLEIN);
            return false;
            
        }

        private bool isEnoughSpaceAvalaibleToAddOneItem => items.Count < maxItemSpace;

        
        public bool addItems(Item[] itemsToAdd)
        {
            if (notEnoughSpaceForFollowingItems(itemsToAdd)) return false;
            List<Item[]> sortedList = sortItem(itemsToAdd);
            foreach (Item[] items in sortedList) realyAddItems(items);
            return true;
        }

        private void realyAddItems(Item[] itemsToAdd)
        {
            if (isAllItemEquals(itemsToAdd))
            {
                addItemsWithGroupedAnimation(itemsToAdd);
            }
            else
            {
                foreach (Item itemToAdd in itemsToAdd)
                {
                    addItemWithAnimation(itemToAdd);
                }
            }
            
        }

        private List<Item[]> sortItem(Item[] itemsToSort)
        {
            List<Item[]> lists = new List<Item[]>();
            lists.Add(itemsToSort);
            return lists;
        }

        
        private void addItemWithAnimation(Item itemToAdd)
        {
            items.Add(itemToAdd);
            notificationManager.newNotification(new AnimItem(itemToAdd, true));
            OnInventoryChangeCallBack?.Invoke();

        }

        private void addItemsWithGroupedAnimation(Item[] itemsToAdd)
        {
            foreach(Item item in itemsToAdd) items.Add(item);
            notificationManager.newNotification(
                new AnimItems(itemsToAdd.First(), true, itemsToAdd.Length));
            OnInventoryChangeCallBack?.Invoke();
        }
        

        private bool isAllItemEquals(Item[] itemsToTest)
        {
            String nameOfFirstItem = itemsToTest[0].name;
            foreach(Item item in itemsToTest) 
                if(!item.name.Equals(nameOfFirstItem)) return false;
            return true;


        }

        private bool notEnoughSpaceForFollowingItems(Item[] itemsToTest)
        {
            int finalInventorySpace = itemsToTest.Length + items.Count;
            bool result = finalInventorySpace > maxItemSpace;
            if(result) notificationManager.newNotification(NotificationType.INVENTAIREPLEIN);
            return result;
        }
        
        public bool removeItem(Item itemToRemove)
        {
            if (items.Find((itema) => itema.name == itemToRemove.name))
            {
                Item oldItem = ScriptableObject.CreateInstance<Item>();
                oldItem.name = itemToRemove.name;
                oldItem.icon = itemToRemove.icon;
            
                notificationManager.newNotification(new AnimItem(oldItem, false));

                items.Remove(itemToRemove);
                OnInventoryChangeCallBack?.Invoke();
                return true;
            }
            notificationManager.newNotification(NotificationType.INFORMATION, "Impossible de suprimer l'objet");
            return false;

        }

        public int spaceRemaining()
        {
            return maxItemSpace - items.Count;
        }
        
        
        
    }
}