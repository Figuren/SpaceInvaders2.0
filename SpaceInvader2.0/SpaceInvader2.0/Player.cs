using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvader2
{
    public class Player
    {
        private Vector2 myPos;
        private Vector2 myOrigin;

        private Texture2D myTexture;

        private Rectangle myTextRect;
        
        public Player(Game1 game)
        {
            myTexture = game.Content.Load<Texture2D>("Player");
            myPos = new Vector2(200, 260);
            myOrigin = new Vector2(18, 7.5f);  //makes the origin in the middle of the texture
            myTextRect = new Rectangle(0, 0, 25, 20);
        }
        public void Update()
        {
            myPos += Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, myPos, myTextRect, Color.White, 0f, myOrigin, 1f, SpriteEffects.None, 1f);
        }
        private Vector2 Move()        // here is where i process the input to make the player move i also change the textures origin
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
            {
                myTextRect.X = 0;
                myOrigin.X = 18;
                return new Vector2(-3, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                myTextRect.X = 25;
                myOrigin.X = 8;
                return new Vector2(3, 0);
            }
            return Vector2.Zero;
        }
        public Vector2 Pos
        {
            get { return myPos; }
        }
    }
}
