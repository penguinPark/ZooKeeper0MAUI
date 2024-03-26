using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    //intergrated
    public interface IZoneManager
    {
        void AddZoneWhenFull();
        void AddZones(Direction d);
        string CreateRandomDirection();
        bool IsWin();
    }
}
