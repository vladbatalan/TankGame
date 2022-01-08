using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace Tancuri
{
    public static class ObjectHandler
    {
        public static ConcurrentQueue<PaintUpdateObject> AllObjects = new ConcurrentQueue<PaintUpdateObject>();
        public static Tank player1Tank;
        public static Tank player2Tank;

        public static Tank GetEnemyOfTank(Tank tank)
        {
            if (tank == player1Tank) return player2Tank;
            if (tank == player2Tank) return player1Tank;
            return null;
        }

        public static void Paint(Graphics g)
        {
            foreach(PaintUpdateObject obj in AllObjects)
            {
                obj.Paint(g);
            }
        }

        public static void Update()
        {
            foreach(PaintUpdateObject obj in AllObjects)
            {
                obj.Update();
            }
        }

        public static void DestroyObject(PaintUpdateObject obj)
        {
            List<PaintUpdateObject> backupList = new List<PaintUpdateObject>();
            PaintUpdateObject first;

            // Find object
            while (!AllObjects.IsEmpty)
            {
                AllObjects.TryDequeue(out first);
                if(first == obj)
                    break;
                backupList.Add(first);
            }

            // Add missing elements
            foreach (PaintUpdateObject paintObj in backupList)
                AllObjects.Enqueue(paintObj);
        }

        public static void DestroyAll()
        {
            PaintUpdateObject first;
            while (!AllObjects.IsEmpty)
            {
                AllObjects.TryDequeue(out first);
            }

        }
    }
}
