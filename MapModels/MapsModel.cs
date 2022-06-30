using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValorantAPI
{
    public class MapsModel
    {
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        public string DisplayName { get; set; }
        public string Splash { get; set; }
    }
}
