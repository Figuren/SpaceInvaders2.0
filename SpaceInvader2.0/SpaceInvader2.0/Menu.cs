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
    public class Menu : GameState
    {
        private string myGameName;
        private string myPressKey;

        private Game1 myGame;
        private SpriteFont mySpriteFont;

        private Vector2 myGameNameOrg;
        private Vector2 myPKOrg;

        private List<Invader> myInvaderList;

        private float timer;
        private float timer2;
        private double timerClick;

        private Random myRand;

        public Menu(Game1 game)
            : base(game)
        {
            myInvaderList = new List<Invader>();
            myPressKey = "Press __ To Play";
            myGameName = "Invaders must die";
            mySpriteFont = game.Content.Load<SpriteFont>("Text");
            myGameNameOrg = mySpriteFont.MeasureString(myGameName) / 2;     //sets origin of the text so it's easier to center it on screen.
            myPKOrg = mySpriteFont.MeasureString(myPressKey) / 2;
            myGame = game;
            myRand = new Random();
        }
        public override void Update(GameTime gt)
        {
            timerClick += gt.ElapsedGameTime.TotalSeconds;
            timer += gt.ElapsedGameTime.Milliseconds;       //timers for the tanks going past the screen.
            timer2 += gt.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && timerClick > 3)
                myGame.ChangeGS(new GamePlay(myGame));      //press q to change gamestate and start the game
            if (timer > 2000)
            {
                myInvaderList.Add(new Invader(myGame, new Vector2(450, myRand.Next(0, 300))));  //add new tank every 2 second
                timer = 0;
            }
            if (timer2 > 50)
            {
                Invader tempInvader = null;                 //tempinvader is used to delete out of bound tanks
                foreach (Invader invader in myInvaderList)
                {
                    invader.Update();
                    if (invader.Pos.X < -10)
                    {
                        tempInvader = invader;
                    }
                }
                if (tempInvader != null)
                    myInvaderList.Remove(tempInvader);
                timer2 = 0;
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Invader invader in myInvaderList)
                invader.Draw(spriteBatch);
            spriteBatch.DrawString(mySpriteFont, myGameName, new Vector2(200, 50), Color.White, 0f, myGameNameOrg, 0.2f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(mySpriteFont, myPressKey, new Vector2(200, 100), Color.White, 0f, myPKOrg, 0.1f, SpriteEffects.None, 1f);
        }
    }
}
