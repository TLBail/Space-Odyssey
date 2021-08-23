﻿namespace Script.Item_and_inventory
{
    public class AnimItems : AnimItem
    {
        public int nbItem;

        public AnimItems(Item newItem, bool newIsAdded, int nbItem) : base(newItem, newIsAdded)
        {
            this.nbItem = nbItem;
        }
    }
}