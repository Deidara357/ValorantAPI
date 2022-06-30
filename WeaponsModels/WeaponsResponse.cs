using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValorantAPI.WeaponsModels
{
    public class WeaponsResponse
    {
        public string WeaponName { get; set; }
        public string Photo { get; set; }
        public string FireRate { get; set; }
        public string MagazineSize { get; set; }
        public string EquipTimeSeconds { get; set; }
        public string ReloadTimeSeconds { get; set; }
        public List<DamageRanges> Damage { get; set; }
        public string Cost { get; set; }
        public string WeaponType { get; set; }
    }
}
