﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    // new and integrated parent
    public class Occupant
    {
        public string emoji;
        public string species;

        public Point location;

        /* Class Occupant sits above anything that can be on our grid, so any 
         * grid occupant can report its location. This method is not virtual,
         * so it currently can't be overridden. */

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }
    }
}