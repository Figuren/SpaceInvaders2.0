using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvader2
{
    /* gamestate is an template how the main classes of the game should be structured and it makes dealing with
     * switching between parts of the game easier */
    public abstract class GameState
    {
        private Game1 myGame;
        public GameState(Game1 game)
        {
            myGame = game;
        }
        public abstract void Update(GameTime gt);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
