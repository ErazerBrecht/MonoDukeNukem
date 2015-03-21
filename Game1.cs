#region Using Statements
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tao.Sdl;

#endregion

namespace Mono
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameLoop _level;

        public Game1(): base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 800;   // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            _graphics.ApplyChanges();
        }


        protected override void LoadContent()
        {
            /// <summary>
            /// LoadContent will be called once per game and is the place to load
            /// all of your content.
            /// </summary>

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Begin.LoadContent(this.Content);
            Speler.LoadContent(this.Content);
            BulletPlayer.LoadContent(this.Content);
            Robot.LoadContent(this.Content);
            MonsterRight.LoadContent(this.Content);
            MonsterLeft.LoadContent(this.Content);
            Tank.LoadContent(this.Content);
            FireRobot.LoadContent(this.Content);
            BulletEnemy.LoadContent(this.Content);
            Leven.LoadContent(this.Content);
            PlatformGround.LoadContent(this.Content);
            PlatformLeft.LoadContent(this.Content);
            PlatformGroundReverse.LoadContent(this.Content);
            PlatformMiddle.LoadContent(this.Content);
            PlatformRight.LoadContent(this.Content);
            Flamethrower.LoadContent(this.Content);
            BulletFlame.LoadContent(this.Content);
            Box.LoadContent(this.Content);
            Barrel.LoadContent(this.Content);
            SpikesUp.LoadContent(this.Content);
            SpikesDown.LoadContent(this.Content);
            Mine.LoadContent(this.Content);
            Floppy.LoadContent(this.Content);
            Cola.LoadContent(this.Content);
            KeyRed.LoadContent(this.Content);
            KeyBlue.LoadContent(this.Content);
            Door.LoadContent(this.Content);
            Text.LoadContent(this.Content);
            Background.LoadContent(this.Content);
            Score.LoadContent(this.Content);
            PrimitiveDrawing.LoadContent(this.Content);

            //TODO: DENK HIER TERUG OVER NA!
            Screen._height = 1376;
            Screen._width = 3328;
            GameObject.MaxWidth = 3328;

            _level = new Begin();

        }



        protected override void Update(GameTime gameTime)
        {
            /// <summary>
            /// Allows the game to run logic such as updating the world,
            /// checking for collisions, gathering input, and playing audio.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>

            //Set FPS to fixed value, 30 fps
            //this.TargetElapsedTime = TimeSpan.FromMilliseconds(1000/30);

            //The return value is het nieuwe level of hetzelfde level!
            ButtonCheck.Update();
            _level = _level.Update(gameTime);     //Update every object + CheckCollision with every object (tile, enemy, bullet)
            
        }

        protected override void Draw(GameTime gameTime)
        {
            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Screen.ViewMatrix);
            _level.Teken(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
