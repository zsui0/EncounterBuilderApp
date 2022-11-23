using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncounterBuilderApp
{
    class Monster
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string ChallengeRating { get; set; }
        //public string SpeedJson { get; set; }

        public Monster(string name, string size, string type, string challengerating)
        {
            this.Name = name;
            this.Size = size;
            this.Type = type;
            this.ChallengeRating = challengerating;
            //this.SpeedJson = speedjson;
        }
    }
}
