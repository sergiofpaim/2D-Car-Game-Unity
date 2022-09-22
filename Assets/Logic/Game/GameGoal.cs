using System.Collections.Generic;
using System.Linq;

namespace Assets.Logic.Game
{
    public static class GameGoal
    {
        private static List<(string id, Collisions collisions)> collidableObjects = new();

        public static string Winner = null;

        public static void RegisterCollidable(string id, Collisions collisions)
        {
            collidableObjects.Add((id, collisions));
        }

        internal static void EvaluateCollisions(Collisions collided, string name)
        {
            if (collided.OntoMoving && !collided.By)
                Winner = $"The {name} car won!";
            else if (collided.OntoMoving && collided.By)
                Winner = "The game tied!";
            else if (collided.OntoStatic)
                Winner = $"The {collidableObjects.Where(p => name != p.id).FirstOrDefault().id} car won!";
        }
    }
}
