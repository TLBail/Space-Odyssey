namespace Script
{
    public class AnimItem
    {
        public Item Item;
        public bool isAdded;
        public NotificationType notificationType;
        
        
        public AnimItem(Item newItem, bool newIsAdded)
        {
            Item = newItem;
            isAdded = newIsAdded;
        }

    }
    
    
}