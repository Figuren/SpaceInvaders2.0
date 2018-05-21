using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace SpaceInvader2
{
    public class GamePlay : GameState
    {
        private List<List<Invader>> myInvaders;                     //the first list is the rows and the second list is the individual tanks.
        private List<Shell> myShells;
        
        private float timer;
        private float shootTimer;

        private Game1 myGame;

        private Player myPlayer;

        private SpriteFont myFont;

        private int myScore;
        private int myHighScore;

        public GamePlay(Game1 game)
            : base(game)
        {
            myFont = game.Content.Load<SpriteFont>("Text");
            myGame = game;
            myHighScore = HighScoreINIT();
            Vector2 pos = new Vector2(385, 175);
            myShells = new List<Shell>();
            myInvaders = new List<List<Invader>>();
            for (int i = 0; i < 5; i++)                             //adds 5 row of tanks.
            {
                pos.X = 385;
                myInvaders.Add(new List<Invader>());
                for (int y = 0; y < 8; y++)                         //with 8 tanks in every row. OBS myinvader[0][0] is the most right tank in the highest row.
                {
                    myInvaders[i].Add(new Invader(game, pos));
                    pos.X -= 30;
                }
                pos.Y -= 25;
            }
            myPlayer = new Player(game);
        }
        public override void Update(GameTime gametime)
        {
            shootTimer += (float)gametime.ElapsedGameTime.Milliseconds;
            timer += (float)gametime.ElapsedGameTime.Milliseconds;      //makes a timer that is in milliseconds
            myPlayer.Update();
            foreach (List<Invader> row in myInvaders)
            {
                if (row.Count == 0)
                {
                    myInvaders.Remove(row);             //checks if a row of invaders is empty and if it is remove it.
                    Vector2 pos = new Vector2(385, 50);
                    myInvaders.Add(new List<Invader>());
                    for (int i = 0; i < 8; i++)
                    {
                        myInvaders[myInvaders.Count-1].Add(new Invader(myGame,pos));
                        pos.X -=30;
                    }
                        break;
                }
            }
            foreach (List<Invader> Row in myInvaders)
            {
                if (Row[Row.Count - 1].Pos.X < 11 || Row[0].Pos.X > 388)    //checks if tank is at the boundery and if it is all tank in the row changes direction
                    foreach (Invader invader in Row)
                        invader.ChangeDirection();
                foreach (Invader invader in Row)
                {
                    invader.Update();               //updates all tanks.
                }
            }
            foreach (Shell shell in myShells)
                shell.Update();     //updates all shells
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && shootTimer > 500)
            {
                myShells.Add(new Shell(myGame, -2, new Vector2(myPlayer.Pos.X, myPlayer.Pos.Y - 12)));
                shootTimer = 0;
            }
            if (timer > 500)
            {
                TankShoot();            //makes tanks shoot every 0.5 seconds
                timer = 0;
            }
            CheckCollision();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            myPlayer.Draw(spriteBatch);
            foreach (List<Invader> Row in myInvaders)
            {
                foreach (Invader invader in Row)
                {
                    invader.Draw(spriteBatch);          //Draws all tanks.
                }
            }
            foreach (Shell shell in myShells)
                shell.Draw(spriteBatch);        //draws all shells
            spriteBatch.DrawString(myFont, "Score : " + myScore.ToString(), new Vector2(0, 0), Color.White,0f,Vector2.Zero,0.15f,SpriteEffects.None,1f);
            spriteBatch.DrawString(myFont, "HighScore : " + myHighScore.ToString(), new Vector2(400, 0), Color.White, 0f, new Vector2(myFont.MeasureString("HighScore : " + myHighScore.ToString()).X, 0), 0.15f, SpriteEffects.None, 1f);
        }
        private void CheckCollision()
        {
            List<Shell> tempBullet = new List<Shell>(); //temp bullets are used to hold all bullets that hit to be removed later in this function
            int invaderHit = 0;
            foreach (Shell bullet in myShells)
            {
                #region invader bullet collision
                if (bullet.velocity < 0)        //checks so no friendly can kill invader.
                {
                    foreach (List<Invader> row in myInvaders)
                    {
                        if (bullet.Pos.Y > row[0].Pos.Y + 12)               //checks if the bullet is below the row of tanks, and if it is there is no chance of hitting
                        {
                            break;
                        }
                        else if (bullet.Pos.Y > row[0].Pos.Y - 12 && bullet.Pos.Y < row[0].Pos.Y + 12)  //checks if the bullet is in the right y pos to hit row
                        {
                            foreach (Invader invader in row)
                            {
                                if (bullet.Pos.X < invader.Pos.X + 12 && bullet.Pos.X > invader.Pos.X - 12 && invader.isDead == false)
                                {       //checks if any tanks in that row will be hit on the x axis aswell
                                    invader.isDead = true;
                                    tempBullet.Add(bullet);
                                    invaderHit += 1;
                                    myScore++;
                                }
                            }
                        }
                    }
                }
                #endregion
                #region player bullet collision
                else if (bullet.velocity > 0)
                {
                    if (bullet.Pos.X < myPlayer.Pos.X + 18 && bullet.Pos.X > myPlayer.Pos.X - 18 && bullet.Pos.Y < myPlayer.Pos.Y + 7.5f && bullet.Pos.Y > myPlayer.Pos.Y - 7.5f)
                    {
                        GameOver();
                    }
                }
                #endregion
            }
            for (int i = 0; i < invaderHit; i++)    //removes all hit invaders.
            {
                RemoveHitInvaders();
            }
            foreach (Shell bullet in tempBullet)    //removes all hit bullets.
            {
                myShells.Remove(bullet);
            }
        }
        private void RemoveHitInvaders()
        {
            for (int y = 0; y < myInvaders.Count; y++)
            {
                foreach (Invader invader in myInvaders[y])
                {
                    if (invader.isDead == true)
                    {
                        myInvaders[y].Remove(invader);          //checks for dead invaders then removes them out of the row.
                        return;
                    }
                }
            }
            return;
        }
        private void TankShoot()
        {
            Invader tempTankShoot;
            Random randTank = new Random();
            int row = randTank.Next(0, myInvaders.Count);        //selects a random row
            int tank = randTank.Next(0, myInvaders[row].Count);  //selects a random tank of that row
            tempTankShoot = myInvaders[row][tank];
            for (int i = 0; i < row - 1; i++)
            {
                foreach (Invader invader in myInvaders[i])
                    if (invader.Pos.X < tempTankShoot.Pos.X + 12 && invader.Pos.X > tempTankShoot.Pos.X - 12)
                    {
                        tempTankShoot = invader;            //checks if there is another tank infront and if it makes that one shoot instead.
                        row = 0;
                        break;
                    }
            }
            myShells.Add(new Shell(myGame, 2, new Vector2(tempTankShoot.Pos.X, tempTankShoot.Pos.Y + 15)));     //shoots/adds the shell in game.
        }
        private void GameOver()
        {
            if (myScore > myHighScore)
            {
                StreamWriter writer = new StreamWriter("Highscore");
                writer.WriteLine(myScore);
                writer.Close();
            }
            myGame.ChangeGS(new GameOver(myGame, myScore, myHighScore));
        }
        private int HighScoreINIT()
        {           //reads the highscore of the HighScore file, if no file exists set highscore to 0;
            int score = 0;
            try {               
                StreamReader reader = new StreamReader("Highscore");
                score = int.Parse(reader.ReadLine());
                reader.Close();
                return score;
            }
            catch
            {
                return score;
            }
        }
      /*  private int GetHighScore()
        {

        }
        private void PrintHighScore()
        {

        }*/
    }
}
