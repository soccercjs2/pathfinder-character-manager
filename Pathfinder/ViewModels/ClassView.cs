using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class ClassView
    {
        public int CharacterId { get; set; }
        public List<Class> Classes { get; set; }
        public List<KeyValuePair<string, int>> ClassHealths { get; set;}

        private PathfinderContext db = new PathfinderContext();

        public ClassView() { }
        public ClassView(int characterId)
        {
            this.CharacterId = characterId;
            this.Classes = db.Classes.Where(m => m.CharacterId == characterId).ToList<Class>();
            this.ClassHealths = LoadClassHealths(characterId);
        }

        private List<KeyValuePair<string, int>> LoadClassHealths(int characterId)
        {
            List<ClassHealth> classHealths = db.ClasseHealths.Where(m => m.CharacterId == characterId).ToList<ClassHealth>();
            List<KeyValuePair<string, int>> classHealthViews = new List<KeyValuePair<string, int>>();

            foreach (ClassHealth health in classHealths)
            {
                classHealthViews.Add(new KeyValuePair<string, int>(
                    db.Classes.Find(health.ClassId).Name,
                    health.Health
                ));
            }

            return classHealthViews;
        }
    }
}