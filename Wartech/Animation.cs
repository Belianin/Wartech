using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wartech
{
    public class Animation
    {
        private readonly int width;
        private readonly int count;
        private readonly int speed;
        private int counter;

        public bool IsFinished => count >= counter;

        public Animation(int width, int count, int speed)
        {
            this.width = width;
            this.count = count;
            this.speed = speed;
        }

        public Rectangle GetFrame()
        {
            var result = new Rectangle(width * GetCurrentFrame(), 0, width, width);
            counter++;
            if (counter >= count * speed)
                counter = 0;

            return result;
        }

        private int GetCurrentFrame()
        {
            return counter / speed;
        }
    }
}