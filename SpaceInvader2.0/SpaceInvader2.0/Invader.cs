using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvader2
{
    class Invader
    {
        private Texture2D myTexture;
        private Rectangle mySpritePos;

        private Vector2 myOrigin;
        private Vector2 myPos;

        private bool myRight;       //if the tanks moves left or right.
        private bool myIsDead;

        private int myVelocity;     //the speed of the tank, should increase when there is fewer tanks in a row.

        public Invader(Game1 game, Vector2 pos)
        {
            myTexture = game.Content.Load<Texture2D>("Tank");
            mySpritePos = new Rectangle(0, 0, 25, 20);
            myOrigin = new Vector2(25 / 2, 20 / 2);         //makes the origin the center of the tank.
            myPos = pos;
            myVelocity = 1;
            myRight = false;            //starts with the tank moving left.
            myIsDead = false;
        }
        public void Update()
        {
            if (myRight == true)            //moves the tank in the right direction.
                myPos.X += myVelocity;
            else
                myPos.X -= myVelocity;
            if (mySpritePos.X < 75)     //changes the rectangle in the sprite sheet so it becomes an animation.
                mySpritePos.X += 25;
            else
                mySpritePos.X = 0;
        }
        public void ChangeDirection()       //function that changes moving direction of the tank and makes it go downward
        {
            if (myRight == true)        //changes the hull direction of tank
                mySpritePos.Y = 0;
            else
                mySpritePos.Y += 20;
            myRight = !myRight;
            myPos.Y += 5;                   //moves tank down.
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, myPos, mySpritePos, Color.White, 0f, myOrigin, 1f, SpriteEffects.None, 0f);
        }
        public Vector2 Pos
        {
            get { return myPos; }
        }
        public int Velocity
        {
            get { return myVelocity; }
            set { myVelocity = value; }
        }
        public bool isDead
        {
            get { return myIsDead; }
            set { myIsDead = value; }
        }
    }
}
