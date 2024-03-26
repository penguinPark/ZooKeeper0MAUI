using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper0MAUI
{
    // nmew a ndn itnegrated
    public interface IScoreCalculator
    {
        int CalculateTotalScore(List<List<Zone>> animalZones);
        int CountSpecies(List<List<Zone>> animalZones);
        int FindTheLeastOne(List<List<Zone>> animalZones);
        int FindTheMostOne(List<List<Zone>> animalZones);
    }
}
