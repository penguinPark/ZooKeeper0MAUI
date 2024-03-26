using Microsoft.Maui;
using Microsoft.Maui.Platform;
//using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ZooKeeper0MAUI
{
    // move flee retreat fly hunt attack mouse:totalflee directioanl flee
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        static public int maxCellsX = 10;
        static public int maxCellsY = 10;

        static public MainPage mainPage;

        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen;

        static public int totalScore = 0; 
        //New attributes, which will be used by ZoneManager INTEGRATED
        static public int directionIndex;
        static public string direction = "";


        static public void SetUpGame(MainPage p)
        {
            mainPage = p;
            ZoneManager zoneManager = new ZoneManager(); //INTEGRATED
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
            Button hpButton = (Button)mainPage.FindByName("HoldingPen");
            holdingPen = new Zone(-1, -1, null, hpButton);
            //At the beginning of the game create a random direction INTEGRATED
            direction = zoneManager.CreateRandomDirection();
        }

        /*
        static public bool AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return false; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                for (var x = 0; x < numCellsX; x++)
                {
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                numCellsY++;
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList);
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return false; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null));
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
            return true;
        }*/

        static public void ZoneClick(Zone clickedZone)
        {
            ScoreCalculator scoreCalculator = new ScoreCalculator();// INTEGRATED
            ZoneManager zoneManager = new ZoneManager();
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones); // INT

                zoneManager.AddZoneWhenFull();//Adding new zone should be executed after all animals finish their actions INT
                if (zoneManager.IsWin())
                {
                    return;
                }
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones); // INT

            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
            holdingPen.UpdateZoneImage(); // deletes the image on the holding pen
        }

        static public void AddAnimalToHolding(string occupantType)
        {
            ZoneManager zoneManager = new ZoneManager(); //INT
            if (holdingPen.occupant != null)
            {
                return;
            }
            if (occupantType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (occupantType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (occupantType == "raptor") holdingPen.occupant = new Raptor("RAAAAAA");
            if (occupantType == "chick") holdingPen.occupant = new Chick("baby");
            // INTEGRATED
            if (occupantType == "rooster") holdingPen.occupant = new Rooster("Earl Wings");
            if (occupantType == "vulture") holdingPen.occupant = new Vulture("Van Helswing");
            if (occupantType == "grass") holdingPen.occupant = new Grass();
            if (occupantType == "corpse") holdingPen.occupant = new Corpse();
            holdingPen.UpdateZoneImage();
            //ActivateAnimals();
            zoneManager.AddZoneWhenFull();//Keeping watching whether current is full and then adding new zone INTEGRATED
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant as Animal != null && ((Animal)zone.occupant).reactionTime == r && ((Animal)zone.occupant).TurnCheck == false)
                        {
                            ((Animal)zone.occupant).Activate();
                            zone.UpdateZoneImage(); // updating zone image here!
                        }
                    }
                }
            }
            //Going through deaths INT
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    Animal animal = zone.occupant as Animal;
                    if (animal != null && animal.turnsSinceLastHunt > 5)
                    {
                        zone.occupant = new Corpse();
                    }
                }
            }

            //Going through chicks maturing into other birds INT
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    Chick chick = zone.occupant as Chick;

                    if (chick != null && chick.totalTurns > 3) //grow up!!!
                    {
                        Random random = new Random();
                        int choice = random.Next(10);
                        if (choice < 2)
                        {
                            zone.occupant = new Raptor("raptor");
                        }
                        else if (choice < 7) // The probability of a rooster is 1 in 2
                        {
                            zone.occupant = new Rooster("rooster");
                        }
                        else // The remaining 1/3 probability is allocated to Vultures
                        {
                            zone.occupant = new Vulture("vulture");
                        }
                    }
                }
            }

            //Going through resetting turnchecks INT
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant as Animal != null)
                    {
                        ((Animal)zone.occupant).TurnCheck = false;
                    }
                }
            }
        }

        // UPDATED AND INTEGRATED
        static public bool Seek(int x, int y, Direction d, string target, int distance)
        {
            if (target == "null") // Searching for an empty spot
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return true;
            }
            else
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return false;
                if (animalZones[y][x].occupant.species == target)
                {
                    return true;
                }
            }
            return false;
        }
        // INTEGRATED + ADD UPDATE IMAGES

        static public int Move(Animal animal, Direction d, int distance)
        {
            int movedDistance = 0;
            int x = animal.location.x;
            int y = animal.location.y;

            for (int i = 0; i < distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) break;
                if (animalZones[y][x].occupant == null)
                {
                    animalZones[animal.location.y][animal.location.x].occupant = null;
                    animalZones[y][x].occupant = animal;
                    movedDistance++;
                }
                else
                {
                    break;
                }
            }
            return movedDistance;
        }

        // NEW AND INTEPGRATED 
        static public bool Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    if (animalZones[y - 1][x].occupant != null)
                    {
                        animalZones[y - 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.down:
                    if (animalZones[y + 1][x].occupant != null)
                    {
                        animalZones[y + 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.left:
                    if (animalZones[y][x - 1].occupant != null)
                    {
                        animalZones[y][x - 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.right:
                    if (animalZones[y][x + 1].occupant != null)
                    {
                        animalZones[y][x + 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
            }
            return false; // nothing to hunt
        }

        // new and integrated
        static public bool Retreat(Animal runner, Direction d, int distance)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    if (y > 0 && animalZones[y - distance][x].occupant == null)
                    {
                        animalZones[y - distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                case Direction.down:
                    if (y < numCellsY && animalZones[y + distance][x].occupant == null)
                    {
                        animalZones[y + distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - distance].occupant == null)
                    {
                        animalZones[y][x - distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX && animalZones[y][x + distance].occupant == null)
                    {
                        animalZones[y][x + distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
            }
            return false; // cannot retreat
        }

        // new and integrated
        static public int SeekForMouse(int x, int y, Direction d, string target, int distance)
        {
            int squaresToNearest = 0;
            for (int i = 1; i <= distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }

                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return 0;
                if (animalZones[y][x].occupant == null) return 0;
                if (animalZones[y][x].occupant.species == target)
                {
                    squaresToNearest = i;
                    return squaresToNearest;
                }
            }
            return 0;
        }
    }
}

