﻿using CncTd.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace CncTd
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CncTdGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D harvesterSprite;
        private Texture2D mapSprite;
        private Texture2D refinerySprite;
        private Texture2D turretConstructingSprite;
        private Texture2D turretSprite;
        private Texture2D bulletSprite;
        private Texture2D whitePixelSprite;

        private List<Harvester> harvesters;
        private List<Refinery> refineries;
        private List<Turret> turrets;
        private List<Bullet> bullets;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;

        private Point target1 = new Point(300, 50);
        private Point target2 = new Point(300, 500);

        public CncTdGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            harvesters = new List<Harvester>() { new Harvester(this, Player.One, target1, target2) };
            refineries = new List<Refinery>();
            turrets = new List<Turret>();
            bullets = new List<Bullet>();
            IsMouseVisible = true;
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            harvesterSprite = Content.Load<Texture2D>("harvester");
            mapSprite = Content.Load<Texture2D>("map");
            refinerySprite = Content.Load<Texture2D>("refinery");
            turretConstructingSprite = Content.Load<Texture2D>("gun-turret-build");
            turretSprite = Content.Load<Texture2D>("gun-turret");
            bulletSprite = Content.Load<Texture2D>("120mm");
            whitePixelSprite = Content.Load<Texture2D>("whitepixel");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (IsActive)
            {
                if (previousMouseState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    harvesters.Add(new Harvester(this, Player.One, previousMouseState.Position, target1));
                }

                if (previousMouseState.RightButton == ButtonState.Pressed && Mouse.GetState().RightButton == ButtonState.Released)
                {
                    foreach (Harvester harvester in harvesters)
                    {
                        harvester.Target = previousMouseState.Position;
                    }
                    foreach (Turret turret in turrets)
                    {
                        turret.Target = previousMouseState.Position;
                    }
                }

                if (previousKeyboardState.IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyUp(Keys.R)) {
                    refineries.Add(new Refinery(this, Player.One, previousMouseState.Position, gameTime.TotalGameTime));
                }

                if (previousKeyboardState.IsKeyDown(Keys.T) && Keyboard.GetState().IsKeyUp(Keys.T))
                {
                    turrets.Add(new Turret(this, Player.Two, previousMouseState.Position, gameTime.TotalGameTime));
                }
            }


            List<IPlayerEntity> allEntities = new List<IPlayerEntity>();
            allEntities.AddRange(harvesters);
            allEntities.AddRange(refineries);
            allEntities.AddRange(turrets);

            foreach (Harvester harvester in harvesters) {
                if (harvester.Position == harvester.Target)
                {
                    if (harvester.Target == target1)
                    {
                        harvester.Target = target2;
                    } else
                    {
                        harvester.Target = target1;
                    }
                }
                harvester.Update(gameTime, allEntities);
            }
            foreach (Refinery refinery in refineries)
            {
                refinery.Update(gameTime, allEntities);
            }
            foreach (Turret turret in turrets)
            {
                turret.Update(gameTime, allEntities, bullets);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime, allEntities);
            }
            bullets = bullets.Where(b => b.Alive).ToList();

            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(mapSprite, new Rectangle(0, 0, 744, 744), Color.White);
            foreach (Harvester harvester in harvesters)
            {
                harvester.Draw(gameTime, spriteBatch, harvesterSprite, whitePixelSprite);
            }
            foreach (Refinery refinery in refineries)
            {
                refinery.Draw(gameTime, spriteBatch, refinerySprite);
            }
            foreach (Turret turret in turrets)
            {
                turret.Draw(gameTime, spriteBatch, turretConstructingSprite, turretSprite);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(gameTime, spriteBatch, bulletSprite);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
