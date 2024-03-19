using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
        public class Chick : Bird
        {
            public Chick(string name)
            {
                emoji = "🐥";
                species = "chick";
                this.name = name; // "this" to clarify instance vs. method parameter
                reactionTime = new Random().Next(6, 10);
            }

            public override void Activate()
            {
                base.Activate();
                Console.WriteLine("pipip");
                Flee();
            }

            public void Flee()
            {
                if (Game.Seek(location.x, location.y, Direction.up, "cat"))
                {
                    if (Game.Retreat(this, Direction.down)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.down, "cat"))
                {
                    if (Game.Retreat(this, Direction.up)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.left, "cat"))
                {
                    if (Game.Retreat(this, Direction.right)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.right, "cat"))
                {
                    if (Game.Retreat(this, Direction.left)) return;
                }
            }
        }
}
