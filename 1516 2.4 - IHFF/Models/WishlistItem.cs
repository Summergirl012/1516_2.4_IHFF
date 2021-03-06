﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IHFF.Models
{
    [Table("WishlistItem")]
    public class WishlistItem
    {
        public WishlistItem() { }

        public WishlistItem(Event e, int amount, Wishlist wishlist)
        {
            Amount = amount;
            PayedFor = false;
            WishlistUID = wishlist.UID;
            Wishlist = wishlist;
            EventId = e.EventId;
            Event = e;
            Date = e.Date;
            LocationId = e.LocationId;
            Location = e.Location;
            Discriminator = e.Discriminator;
        }

        public WishlistItem(RestaurantReservation r, Wishlist wishlist)
        {
            Amount = r.Amount;
            PayedFor = false;
            WishlistUID = wishlist.UID;
            Wishlist = wishlist;
            EventId = r.EventId;
            Reservation = r;
            Date = r.Date;
            Discriminator = "R";
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishlistItemId { get; set; }
        public int Amount { get; set; }
        public bool PayedFor { get; set; }
        
        public string WishlistUID { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual RestaurantReservation Reservation { get; set; }

        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string Discriminator { get; set; }

        [NotMapped]
        public bool Selected { get; set; }

        public ItemType GetItemType()
        {
            switch (this.Discriminator)
            {
                case "M":
                case "S":
                default:
                    return ItemType.Event;
                case "R":
                    return ItemType.Reservation;
            }
        }

        public string GetName()
        {
            switch (GetItemType())
            {
                case ItemType.Event:
                    return Event.GetName();
                case ItemType.Reservation:
                    return Reservation.Restaurant.Name;
                default:
                    return "";
            }
        }
        public string GetLocation()
        {
            switch (GetItemType())
            {
                case ItemType.Event:
                    return Event.LocationString;
                case ItemType.Reservation:
                    return Reservation.Restaurant.Address;
                default:
                    return "";
            }
        }
        public string GetImage()
        {
            switch (GetItemType())
            {
                case ItemType.Event:
                    return Event.GetImage();
                case ItemType.Reservation:
                    return Reservation.Restaurant.Image;
                default:
                    return "";
            }
        }
        public decimal GetPrice()
        {
            switch (GetItemType())
            {
                case ItemType.Event:
                    return Event.GetPrice();
                case ItemType.Reservation:
                    return 10;
                default:
                    return 0;
            }
        }
    }

    public enum ItemType
    {
        Event,
        Reservation
    }
}