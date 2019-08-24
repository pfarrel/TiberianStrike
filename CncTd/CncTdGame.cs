using TiberianStrike.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities.Explosions;
using TiberianStrike.Entities.Projectiles;

namespace TiberianStrike
{
    public class TiberianStrikeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera camera;
        private GameState gameState;
        private World world;
        private A10 a10;

        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;

        private Point target1 = new Point(300, 50);
        private Point target2 = new Point(300, 400);

        public TiberianStrikeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            camera = new Camera(new Viewport(0, 0, 1920, 1080), 2000, 2000);
            camera.Pos = new Vector2(200, 200);

            world = new World();
            world.AddEntity(new Harvester(world, Player.Two, target1, target2));

            a10 = new A10(world, Player.One, new Point(0, 100));
            world.AddEntity(a10);

            //world.AddEntity(new Refinery(world, Player.Two, new Point(400, 50)));
            world.AddEntity(new HandOfNod(world, Player.Two, new Point(455, 50)));
            world.AddEntity(new Airfield(world, Player.Two, new Point(530, 50)));
            world.AddEntity(new ConstructionYard(world, Player.Two, new Point(620, 50)));
            world.AddEntity(new TempleOfNod(world, Player.Two, new Point(690, 40)));
            world.AddEntity(new Obelisk(world, Player.One, new Point(350, 100)));

            world.AddEntity(new Apc(world, Player.Two, new Point(400, 100)));
            world.AddEntity(new Artillery(world, Player.Two, new Point(424, 100)));
            world.AddEntity(new Bike(world, Player.Two, new Point(448, 100)));
            world.AddEntity(new Buggy(world, Player.Two, new Point(472, 100)));
            world.AddEntity(new FlameTank(world, Player.Two, new Point(496, 100)));
            world.AddEntity(new StealthTank(world, Player.Two, new Point(520, 100)));

            Sam sam = new Sam(world, Player.Two, new Point(400, 300));
            world.AddEntity(sam);

            RocketInfantry rocketInfantry = new RocketInfantry(world, Player.Two, new Point(400, 150));
            world.AddEntity(rocketInfantry);
            rocketInfantry = new RocketInfantry(world, Player.Two, new Point(430, 150));
            world.AddEntity(rocketInfantry);
            rocketInfantry = new RocketInfantry(world, Player.Two, new Point(460, 150));
            world.AddEntity(rocketInfantry);
            rocketInfantry = new RocketInfantry(world, Player.Two, new Point(490, 150));
            world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(520, 150));
            //world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(400, 180));
            //world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(430, 180));
            //world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(460, 180));
            //world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(490, 180));
            //world.AddEntity(rocketInfantry);
            //rocketInfantry = new RocketInfantry(world, Player.Two, new Point(520, 180));
            //world.AddEntity(rocketInfantry);

            gameState = GameState.Paused;

            SoundEffect.MasterVolume = 0.5f;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.Load(Content);
            Sounds.Load(Content);
            Fonts.Load(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameState == GameState.Paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)) {
                    gameState = GameState.Playing;
                }

                previousMouseState = Mouse.GetState();
                previousKeyboardState = Keyboard.GetState();
            }

                if (gameState != GameState.Playing)
                return;

            world.Tick();

            if (IsActive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P))
                {
                    gameState = GameState.Paused;
                }

                Matrix inverse = Matrix.Invert(camera.GetTransformation());
                Vector2 mousePos = Vector2.Transform(new Vector2(previousMouseState.Position.X, previousMouseState.Position.Y), inverse);
                Point mousePositionPoint = new Point((int)mousePos.X, (int)mousePos.Y);
                //if (previousMouseState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                //{
                //    world.AddEntity(new Harvester(world, Player.Two, mousePositionPoint, target1));
                //}

                //if (previousKeyboardState.IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyUp(Keys.R)) {
                //    world.AddEntity(new Refinery(world, Player.One, mousePositionPoint));
                //}

                //if (previousKeyboardState.IsKeyDown(Keys.T) && Keyboard.GetState().IsKeyUp(Keys.T))
                //{
                //    world.AddEntity(new Turret(world, Player.Two, mousePositionPoint));
                //}

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
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    a10.Rocket(gameTime);
                }

                camera.Pos += movement * 20;
            }

            foreach (Harvester harvester in world.GetEntities<Harvester>()) {
                if (harvester.Position == harvester.Target)
                {
                    if (harvester.Target == target1)
                    {
                        harvester.Target = target2;
                    }
                    else
                    {
                        harvester.Target = target1;
                    }
                }
            }

            foreach (IEntity entity in world.Entities)
            {
                entity.Update(gameTime);
            }

            foreach (Projectile bullet in world.Projectiles)
            {
                bullet.Update(gameTime);
            }
            world.Projectiles = world.Projectiles.Where(p => p.IsAlive).ToList();

            foreach (Explosion explosion in world.Explosions)
            {
                explosion.Update(gameTime) ;
            }
            world.Explosions = world.Explosions.Where(e => e.IsAlive).ToList();
            world.Entities = world.Entities.Where(e => e.IsAlive).ToList();

            camera.Pos = new Vector2(a10.Position.X, a10.Position.Y);

            if (!a10.IsAlive)
            {
                gameState = GameState.Lost;
                Sounds.MissionFailed.Play();
            }
            else if (!world.Entities.Any(entity => entity.Player == Player.Two))
            {
                gameState = GameState.Won;
                Sounds.MissionAccomplished.Play();
            }
            else
            {
                gameState = GameState.Playing;
            }

            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformation(), sortMode: SpriteSortMode.BackToFront);

            spriteBatch.Draw(Sprites.Map.Texture, new Rectangle(0, 0, Sprites.Map.Width, Sprites.Map.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, ZOrder.Map);

            foreach (IEntity entity in world.Entities.OrderBy(entity => entity.GetType().Name))
            {
                entity.Draw(spriteBatch);
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

            spriteBatch.Begin();
            spriteBatch.DrawString(Fonts.Font, "SCORE: 100", new Vector2(20, 20), Color.LawnGreen);
            spriteBatch.DrawString(Fonts.Font, "ENEMIES REMAINING: " + world.Entities.Where(entity => entity.Player == Player.Two).Count(), new Vector2(1500, 20), Color.LawnGreen);

            List<string> messages = new List<string>();
            if (gameState == GameState.Won)
            {
                messages.Add("MISSION");
                messages.Add("ACCOMPLISHED");
            }
            else if (gameState == GameState.Lost)
            {
                messages.Add("MISSION");
                messages.Add("FAILED");
            }

            if (messages.Count > 0)
            {
                DrawCenteredString(messages, Fonts.LogoFont, Color.Black, new Vector2(-4, -4));
                DrawCenteredString(messages, Fonts.LogoFont, Color.WhiteSmoke, Vector2.Zero);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCenteredString(List<string> message, SpriteFont font, Color color, Vector2 offset)
        {
            if (message.Count > 0)
            {
                Vector2 size = font.MeasureString(message[0]);
                float centerX = graphics.PreferredBackBufferWidth / 2;
                float centerY = graphics.PreferredBackBufferHeight / 2;
                float totalLines = message.Count * 2 - 1;
                float y = centerY - (totalLines * size.Y / 2);
                foreach (string line in message)
                {
                    size = font.MeasureString(line);
                    float x = centerX - size.X / 2;
                    spriteBatch.DrawString(font, line, new Vector2(x, y) + offset, color);
                    y += size.Y;
                }
            }
        }
    }
}
