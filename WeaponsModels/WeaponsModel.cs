using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValorantAPI.WeaponsModels
{
    public class WeaponsModel
    {
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        public string DisplayName { get; set; }
        public string DisplayIcon { get; set; }
        public WeaponStats WeaponStats { get; set; }
        public ShopData ShopData { get; set; }
        public List<Skins> Skins { get; set; }
    }

    public class WeaponStats
    {
        public float FireRate { get; set; }
        public int MagazineSize { get; set; }
        public float EquipTimeSeconds { get; set; }
        public float ReloadTimeSeconds { get; set; }
        public List<DamageRanges> DamageRanges { get; set; }
    }

    public class DamageRanges
    {
        public string RangeStartMeters { get; set; }
        public string RangeEndMeters { get; set; }
        public string HeadDamage { get; set; }
        public string BodyDamage { get; set; }
        public string LegDamage { get; set; }
    }

    public class ShopData
    {
        public int Cost { get; set; }
        public string CategoryText { get; set; }
    }

    public class Skins
    {
        public string DisplayName { get; set; }
        public string DisplayIcon { get; set; }
    }
}
