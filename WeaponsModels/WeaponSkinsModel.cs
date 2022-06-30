using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValorantAPI.WeaponsModels
{
    public class WeaponSkinsModel
    {
        public string WeaponName { get; set; }
        public List<Skins> WeaponSkins { get; set; }
    }
}
