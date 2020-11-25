using TiberianStrike.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities.Explosions;
using TiberianStrike.Entities.Projectiles;
using TiberianStrike.Input;

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

        private InputManager inputManager;

        private Point target1 = new Point(300, 50);
        private Point target2 = new Point(300, 400);

        private Rectangle worldBounds = new Rectangle(0, 0, World.FixedWidth, World.FixedHeight);

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
            inputManager = new InputManager();

            world = new World(World.FixedWidth, World.FixedHeight);

            a10 = new A10(world, Player.One, new Point(600, 700), MathHelper.ToRadians(-90));
            world.AddEntity(a10);
            world.Explore(a10.PositionVector, 100f);

            camera = new Camera(new Viewport(0, 0, 1920, 1080), World.FixedWidth, World.FixedHeight);
            camera.Pos = a10.PositionVector;

            world.AddEntity(new HandOfNod(world, Player.Two, new Point(455, 50)));
            world.AddEntity(new Airfield(world, Player.Two, new Point(530, 50)));
            world.AddEntity(new ConstructionYard(world, Player.Two, new Point(620, 50)));
            world.AddEntity(new TempleOfNod(world, Player.Two, new Point(690, 40)));
            world.AddEntity(new Obelisk(world, Player.Two, new Point(350, 100)));
            world.AddEntity(new SamSite(world, Player.Two, new Point(400, 300)));

            world.AddEntity(new Harvester(world, Player.Two, target1, target2));
            world.AddEntity(new Apc(world, Player.Two, new Point(400, 100)));
            world.AddEntity(new Artillery(world, Player.Two, new Point(424, 100)));
            world.AddEntity(new Bike(world, Player.Two, new Point(448, 100)));
            world.AddEntity(new Buggy(world, Player.Two, new Point(472, 100)));
            world.AddEntity(new FlameTank(world, Player.Two, new Point(496, 100)));
            world.AddEntity(new StealthTank(world, Player.Two, new Point(520, 100)));
            world.AddEntity(new RocketInfantry(world, Player.Two, new Point(400, 150)));
            world.AddEntity(new RocketInfantry(world, Player.Two, new Point(430, 150)));
            world.AddEntity(new RocketInfantry(world, Player.Two, new Point(460, 150)));
            world.AddEntity(new RocketInfantry(world, Player.Two, new Point(490, 150)));

            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(608, 496))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(608, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(624, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(624, 560))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(624, 576))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(640, 496))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(640, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(640, 528))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(640, 560))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(656, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(656, 528))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(656, 544))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(656, 560))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(672, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(672, 560))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(688, 512))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(688, 496))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(688, 480))));
            world.AddEntity(new Wall(world, Player.Two, HackWallCoordinates(new Point(704, 480))));

            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(624, 448))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(624, 432))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(640, 448))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(640, 432))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(656, 448))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(672, 448))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(672, 432))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(688, 448))));
            world.AddEntity(new Sandbags(world, Player.Two, HackWallCoordinates(new Point(688, 432))));

            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(624, 412))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(640, 412))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(656, 412))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(672, 412))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(688, 412))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(624, 396))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(640, 396))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(672, 396))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(688, 396))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(624, 380))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(688, 380))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(624, 364))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(640, 364))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(672, 364))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(688, 364))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(624, 348))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(640, 348))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(656, 348))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(672, 348))));
            world.AddEntity(new ChainlinkFence(world, Player.Two, HackWallCoordinates(new Point(688, 348))));

            gameState = GameState.Paused;

            SoundEffect.MasterVolume = 0.5f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.Load(Content);
            Sounds.Load(Content);
            Fonts.Load(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            PlayerInput command = inputManager.Process();
            if (command.Quit)
            {
                Exit();
            }

            if (command.Pause)
            {
                if (gameState == GameState.Paused)
                {
                    gameState = GameState.Playing;
                }
                else if (gameState == GameState.Playing)
                {
                    gameState = GameState.Paused;
                }
            }


            if (gameState != GameState.Playing)
            {
                return;
            }

            if (IsActive)
            {
                ProcessInput(command);
            }

            world.Tick();

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
                entity.Update();
            }

            foreach (Projectile bullet in world.Projectiles)
            {
                bullet.Update();
            }
            world.Projectiles = world.Projectiles.Where(p => p.IsAlive).ToList();

            foreach (Explosion explosion in world.Explosions)
            {
                explosion.Update();
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

            if (a10.Position.X < 0 || a10.Position.X > worldBounds.Width || a10.Position.Y < 0 || a10.Position.Y > worldBounds.Height)
            {
                a10.RotateInstantlyToPointAt(new Vector2(worldBounds.Width / 2, worldBounds.Height / 2));
            }

            world.Explore(a10.PositionVector, 100f);

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
                bullet.Draw(spriteBatch);
            }
            foreach (Explosion explosion in world.Explosions)
            {
                explosion.Draw(spriteBatch);
            }
            world.DrawFog(spriteBatch);

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
            } else if (gameState == GameState.Paused)
            {
                messages.Add("PAUSED");
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
                    size = font.MeasureString(line) * 3;
                    float x = centerX - size.X / 2;
                    spriteBatch.DrawString(font, line, new Vector2(x, y) + offset, color, 0, Vector2.Zero, 3.0f, SpriteEffects.None, 1);
                    y += size.Y;
                }
            }
        }


        private void ProcessInput(PlayerInput command)
        {
            // Matrix inverse = Matrix.Invert(camera.GetTransformation());
            // Vector2 mousePos = Vector2.Transform(new Vector2(previousMouseState.Position.X, previousMouseState.Position.Y), inverse);
            // Point mousePositionPoint = new Point((int)mousePos.X, (int)mousePos.Y);

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
                a10.Shoot();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                a10.Bomb();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                a10.Rocket();
            }
        }

        private static Point HackWallCoordinates(Point point)
        {
            const int minX = 608;
            const int minY = 348;
            const int newMinX = 504;
            const int newMinY = 288;

            int x = newMinX + (((point.X - minX) / 16) * World.CellSize);
            int y = newMinY + (((point.Y - minY) / 16) * World.CellSize);
            return new Point(x, y);
        }
    }
}
