using System;

namespace ZooKeeper0MAUI
{
    public class Cat : Animal, IPredator
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)Cat
        }

        public override void Activate()
        {
            base.Activate();
            //Meow();
            (this as IPredator).Hunt(this, location.x, location.y, "mouse");
            (this as IPredator).Hunt(this, location.x, location.y, "chick");
        }

        // cat hunts mouse and chick and flees from raptors ********************************************************

        /* Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */


        /* Cats run from raptors! 
        public void Flee() // this was in class week 10
        {
            if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
        }

        public void Meow() // this was in class week 10
        {
            if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.up, "mouse") || Game.Seek(location.x, location.y, Direction.up, "chick"))
            {
                Game.Attack(this, Direction.up);
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse") || Game.Seek(location.x, location.y, Direction.down, "chick"))
            {
                Game.Attack(this, Direction.down);
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse") || Game.Seek(location.x, location.y, Direction.left, "chick"))
            {
                Game.Attack(this, Direction.left);
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse") || Game.Seek(location.x, location.y, Direction.right, "chick"))
            {
                Game.Attack(this, Direction.right);
            }
        } */
    }
}

