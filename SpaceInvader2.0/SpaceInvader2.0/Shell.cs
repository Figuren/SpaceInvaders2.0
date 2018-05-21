using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvader2
{
    public class Shell
    {
        private Texture2D myTexture;

        private Vector2 myPos;
        private Vector2 myOrg;

        private int myVelocity;

        private SpriteEffects myDown;

        private Color myColor;

        public Shell(Game1 game, int velocity, Vector2 pos)
        {
            myTexture = game.Content.Load<Texture2D>("Shell");
            myVelocity = velocity;
            myPos = pos;
            if (myVelocity < 0)
            {
                myDown = SpriteEffects.None;
                myColor = Color.Red;            //checks if it's a player shot shell or an invader shot shell
            }                                   //then makes it diffrent colour and direction.
            else
            {
                myDown = SpriteEffects.FlipVertically;
                myColor = Color.Green;
            }
            myOrg = new Vector2(myTexture.Width / 2, myTexture.Height / 2);
        }
        public void Update()
        {
            myPos.Y += myVelocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture,myPos,null,myColor,0f,myOrg,1f,myDown,1f);
        }
        public Vector2 Pos
        {
            get { return myPos; }
            set { myPos = value; }
        }
        public int velocity
        {
            get { return myVelocity; }
        }
    }
}
