using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    internal interface IPredator
    {
        public void Hunt(Animal predator, int x, int y, string prey) // this was in class week 10
        {
            if (Game.Seek(x, y, Direction.up, prey))
            {
                Game.Attack(predator, Direction.up);
            }
            else if (Game.Seek(x, y, Direction.down, prey))
            {
                Game.Attack(predator, Direction.down);
            }
            else if (Game.Seek(x, y, Direction.left, prey))
            {
                Game.Attack(predator, Direction.left);
            }
            else if (Game.Seek(x, y, Direction.right, prey))
                Game.Attack(predator, Direction.right);
        }
    }
}