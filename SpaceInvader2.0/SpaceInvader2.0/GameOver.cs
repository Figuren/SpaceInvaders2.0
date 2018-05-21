using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvader2
{
    class GameOver : GameState
    {   //used when the player is dead and it's gameover time.
        private Game1 myGame;
        private int myHighScore;
        private int myScore;
        private double myTimePassed;
        private SpriteFont mySpriteFont;
        public GameOver(Game1 game,int score,int hscore)
            : base(game)
        {
            myGame = game;
            myScore = score;
            myHighScore = hscore;
            mySpriteFont = myGame.Content.Load<SpriteFont>("Text");
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            myTimePassed += gt.ElapsedGameTime.TotalSeconds;        //mytimepassed is used so you have to wait 3 seconds before exiting gameover screen.
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && myTimePassed > 3)
                myGame.ChangeGS(new Menu(myGame));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(mySpriteFont, "Your score : " + myScore, new Microsoft.Xna.Framework.Vector2(40, 40), Microsoft.Xna.Framework.Color.White,0f,Microsoft.Xna.Framework.Vector2.Zero,0.15f,SpriteEffects.None,0f);
            spriteBatch.DrawString(mySpriteFont, "High score : " + myHighScore, new Microsoft.Xna.Framework.Vector2(80, 80), Microsoft.Xna.Framework.Color.White, 0f, Microsoft.Xna.Framework.Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
            if (myHighScore < myScore)
                spriteBatch.DrawString(mySpriteFont, "NEW HIGHSCORE!", new Microsoft.Xna.Framework.Vector2(100, 100), Microsoft.Xna.Framework.Color.White, 0f, Microsoft.Xna.Framework.Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
        }
    }
}
