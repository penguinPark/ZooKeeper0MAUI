using System;
namespace ZooKeeper0MAUI
{
    public class Zone
    {
        private Occupant _occupant = null;
        public Occupant occupant
        {
            get { return _occupant; }
            set {
                _occupant = value;
                if (_occupant != null) {
                    _occupant.location = location;
                }
            }
        }

        public Point location;
        public Button zoneButton;

        public string emoji
        {
            get
            {
                if (occupant == null) return "";
                return occupant.emoji;
            }
        }

        public string rtLabel
        {
            get
            {
                if (occupant as Animal == null) return "";
                return ((Animal)occupant).reactionTime.ToString();
            }
        }

        public void UpdateZoneImage()
        {
            // Above "getter" ensures we always get a String, whether an emoji or blank, so we don't have to write extra conditional logic here.
            zoneButton.Text = $"{emoji + rtLabel}";
            Console.WriteLine("Zone info: " + emoji + rtLabel);
        }

        /* Notice that we have two constructors for Zone. C# determines which one to call based on the signature (the list of parameters) from the caller.
         * 
         * The first creates a new button for the grid and associates it with the new zone.
         * 
         * The second associates the new zone with an already-existing button, which is provided by the fourth parameter.
         * 
         * As the game exists currently, we only use the second Zone constructor for the "holding pen", so we could probably rewrite the code to require fewer parameters... but we might find another use for this version later.
         * 
         * Alternatively, we might write more constructors with different signatures in order to handle different Zone creation scenarios!
         */
        // look back into 
        public Zone(int x, int y, Occupant occupant)
        {
            location.x = x;
            location.y = y;
            this.occupant = occupant;

            zoneButton = Game.mainPage.MakeGridButton(x, y);
            UpdateZoneImage();
        }

        public Zone(int x, int y, Occupant occupant, Button existingButton)
        {
            location.x = x;
            location.y = y;
            this.occupant = occupant;

            zoneButton = existingButton;
            UpdateZoneImage();
        }
    }
}
