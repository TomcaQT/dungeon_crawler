using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    
    
    
    public abstract class ItemData : ScriptableObject
    {
        public string Name;

        public string Description; // ????

        public ItemQuality ItemQuality;

        public Sprite ItemSprite;

        public abstract void OnPickup(PlayerStats player);

    }
}
