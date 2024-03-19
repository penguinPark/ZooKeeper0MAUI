using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    public class Raptor : Bird
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast)
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("RAAAAAAAAA ");
            Hunt();
        }

        public void Hunt()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "cat"))
            {
                Game.Attack(this, Direction.up);
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "cat"))
            {
                Game.Attack(this, Direction.down);
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "cat"))
            {
                Game.Attack(this, Direction.left);
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "cat"))
            {
                Game.Attack(this, Direction.right);
            }
            else if (Game.Seek(location.x, location.y, Direction.up, "mouse"))
            {
                Game.Attack(this, Direction.up);
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse"))
            {
                Game.Attack(this, Direction.down);
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse"))
            {
                Game.Attack(this, Direction.left);
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse"))
            {
                Game.Attack(this, Direction.right);
            }
        }
    }
}
