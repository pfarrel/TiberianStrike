using CncTd.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        private Camera camera;
        private World world;
        private A10 a10;

        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;

        private Point target1 = new Point(300, 50);
        private Point target2 = new Point(300, 400);

        public CncTdGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

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
            IsMouseVisible = true;
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            camera = new Camera(new Viewport(0, 0, 1920, 1080), 2000, 2000);
            camera.Pos = new Vector2(200, 200);

            world = new World();
            world.AddEntity(new Harvester(world, Player.One, target1, target2));

            a10 = new A10(world, Player.One, new Point(100, 100));
            world.AddEntity(a10);

            SoundEffect.MasterVolume = 0.5f;

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

            Sprites.Load(Content);
            Sounds.Load(Content);

            


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
                Matrix inverse = Matrix.Invert(camera.GetTransformation());
                Vector2 mousePos = Vector2.Transform(new Vector2(previousMouseState.Position.X, previousMouseState.Position.Y), inverse);
                Point mousePositionPoint = new Point((int)mousePos.X, (int)mousePos.Y);
                if (previousMouseState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    world.AddEntity(new Harvester(world, Player.One, mousePositionPoint, target1));
                }

                if (previousKeyboardState.IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyUp(Keys.R)) {
                    world.AddEntity(new Refinery(world, Player.One, mousePositionPoint, gameTime.TotalGameTime));
                }

                if (previousKeyboardState.IsKeyDown(Keys.T) && Keyboard.GetState().IsKeyUp(Keys.T))
                {
                    world.AddEntity(new Turret(world, Player.Two, mousePositionPoint, gameTime.TotalGameTime));
                }

                Vector2 movement = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    movement.X--;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    movement.X++;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    movement.Y--;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    movement.Y++;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    a10.TurnLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    a10.TurnRight();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    a10.Shoot(gameTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.B))
                {
                    a10.Bomb(gameTime);
                }

                camera.Pos += movement * 20;
            }

            foreach (Harvester harvester in world.GetEntities<Harvester>()) {
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
            }

            foreach (IPlayerEntity entity in world.Entities)
            {
                entity.Update(gameTime);
            }

            List<Projectile> survivingBullets = new List<Projectile>();
            foreach (Projectile bullet in world.Projectiles)
            {
                bullet.Update(gameTime);
                if (bullet.Alive)
                {
                    survivingBullets.Add(bullet);
                }
                else
                {
                    if (bullet is CannonShot)
                    {
                        world.AddExplosion(new ShellExplosion(bullet.Position));
                    }
                    else if (bullet is Bomblet)
                    {
                        world.AddExplosion(new NapalmExplosion(bullet.Position));
                        Sounds.FireExplosion.Play();
                    }
                    else if (bullet is Bullet)
                    {
                        world.AddExplosion(new BulletImpact(bullet.Position));
                    }
                }
            }
            world.Projectiles = survivingBullets;

            foreach (Explosion explosion in world.Explosions)
            {
                explosion.Update(gameTime, world.Entities) ;
            }
            world.Explosions = world.Explosions.Where(e => e.IsAlive).ToList();
            world.Entities = world.Entities.Where(e => e.IsAlive).ToList();

            camera.Pos = new Vector2(a10.Position.X, a10.Position.Y);

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
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformation());

            spriteBatch.Draw(Sprites.Map.SpriteSheet, new Rectangle(0, 0, Sprites.Map.Width, Sprites.Map.Height), Color.White);

            foreach (IPlayerEntity entity in world.Entities.OrderBy(entity => entity.GetType().Name))
            {
                entity.Draw(gameTime, spriteBatch);
            }
            foreach (Projectile bullet in world.Projectiles)
            {
                bullet.Draw(gameTime, spriteBatch);
            }
            foreach (Explosion explosion in world.Explosions)
            {
                explosion.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
