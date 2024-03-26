using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    //int4egreated
    public class Bird : Animal
    {
        public bool Fly(Bird bird, int x, int y, int distance)
        {
            if (Game.Seek(x, y, Direction.up, "null", distance))
            {
                if (Game.Retreat(bird, Direction.up, distance)) return true;
            }
            else if (Game.Seek(x, y, Direction.down, "null", distance))
            {
                if (Game.Retreat(bird, Direction.down, distance)) return true;
            }
            else if (Game.Seek(x, y, Direction.left, "null", distance))
            {
                if (Game.Retreat(bird, Direction.left, distance)) return true;
            }
            else if (Game.Seek(x, y, Direction.right, "null", distance))
            {
                if (Game.Retreat(bird, Direction.right, distance)) return true;
            }
            return false; // nowhere to fly
        }
    }
}
