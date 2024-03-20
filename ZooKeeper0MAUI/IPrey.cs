using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    internal interface IPrey
    {
        public void Flee(Animal prey, int x, int y, string predator)
        {
            if (Game.Seek(x, y, Direction.up, predator))
            {
                if (Game.Retreat(prey, Direction.down)) return;
            }
            if (Game.Seek(x, y, Direction.down, predator))
            {
                if (Game.Retreat(prey, Direction.up)) return;
            }
            if (Game.Seek(x, y, Direction.left, predator))
            {
                if (Game.Retreat(prey, Direction.right)) return;
            }
            if (Game.Seek(x, y, Direction.right, predator))
            {
                if (Game.Retreat(prey, Direction.left)) return;
            }
        }
    }
}
