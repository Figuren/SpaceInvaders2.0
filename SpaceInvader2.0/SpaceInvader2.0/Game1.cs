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
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<GameState> gameStates;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 400;        //Changes resolution
            graphics.PreferredBackBufferHeight = 300;
            graphics.ApplyChanges();
            gameStates = new List<GameState>();
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameStates.Add(new Menu(this));         //makes the game start in the menu
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
            gameStates[gameStates.Count - 1].Update(gameTime);  //Calls the current gamestates update code.
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black); //makes background black
            spriteBatch.Begin();
            gameStates[gameStates.Count - 1].Draw(spriteBatch);     //Calls the current gamestates draw code.
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void ChangeGS(GameState newGS)
        {
            gameStates.Add(newGS);          //Funktion that adds new gamestate and removes old one
            gameStates.RemoveAt(0);
        }
    }
}
