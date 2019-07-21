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

        private Texture2D a10Sprite;
        private Texture2D harvesterSprite;
        private Texture2D mapSprite;
        private Texture2D refinerySprite;
        private Texture2D turretConstructingSprite;
        private Texture2D turretSprite;
        private Texture2D turretSpriteNod;
        private Texture2D bulletSprite;
        private Texture2D whitePixelSprite;
        private Sprites sprites;

        private SoundEffect turretShot;
        private SoundEffect machineGun;

        private A10 a10;
        private List<Harvester> harvesters;
        private List<Refinery> refineries;
        private List<Turret> turrets;
        private List<Projectile> bullets;
        private List<Explosion> explosions;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;

        private Point target1 = new Point(300, 50);
        private Point target2 = new Point(300, 500);

        public CncTdGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

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
            bullets = new List<Projectile>();
            explosions = new List<Explosion>();
            IsMouseVisible = true;
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            camera = new Camera(new Viewport(0, 0, 1280, 720), 2000, 2000);
            camera.Pos = new Vector2(200, 200);

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

            a10Sprite = Content.Load<Texture2D>("a10");
            harvesterSprite = Content.Load<Texture2D>("harvester");
            mapSprite = Content.Load<Texture2D>("map");
            refinerySprite = Content.Load<Texture2D>("refinery");
            turretConstructingSprite = Content.Load<Texture2D>("gun-turret-build");
            turretSprite = Content.Load<Texture2D>("gun-turret");
            bulletSprite = Content.Load<Texture2D>("120mm");
            whitePixelSprite = Content.Load<Texture2D>("whitepixel");
            sprites = Sprites.Load(Content);

            turretShot = Content.Load<SoundEffect>("Sounds/tnkfire4");
            machineGun = Content.Load<SoundEffect>("Sounds/gun8");

            a10 = new A10(this, Player.One, new Point(100, 100));

            turretSpriteNod = turretSprite = Content.Load<Texture2D>("gun-turret");
            Color[] data = new Color[turretSprite.Width * turretSprite.Height];
            turretSpriteNod.GetData(data);
            Color source = new Color(246, 214, 121);
            Color source2 = new Color(222, 190, 105);
            Color source3 = new Color(178, 149, 80);
            Color source4 = new Color(170, 153, 85);
            Color source5 = new Color(194, 174, 97);
            Color source6 = new Color(198, 170, 93);
            Color source7 = new Color(145, 137, 76);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == source || data[i] == source2 || data[i] == source3 || data[i] == source4 || data[i] == source5 || data[i] == source6 || data[i] == source7)
                {
                    data[i] = Color.Red;
                }
            }
            turretSpriteNod.SetData(data);
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
                    harvesters.Add(new Harvester(this, Player.One, mousePositionPoint, target1));
                }

                if (previousMouseState.RightButton == ButtonState.Pressed && Mouse.GetState().RightButton == ButtonState.Released)
                {
                    foreach (Harvester harvester in harvesters)
                    {
                        harvester.Target = mousePositionPoint;
                    }
                    foreach (Turret turret in turrets)
                    {
                        turret.Target = mousePositionPoint;
                    }
                }

                if (previousKeyboardState.IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyUp(Keys.R)) {
                    refineries.Add(new Refinery(this, Player.One, mousePositionPoint, gameTime.TotalGameTime));
                }

                if (previousKeyboardState.IsKeyDown(Keys.T) && Keyboard.GetState().IsKeyUp(Keys.T))
                {
                    turrets.Add(new Turret(this, Player.Two, mousePositionPoint, gameTime.TotalGameTime, turretShot));
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
                    if (!a10.IsFiring)
                    {
                        a10.Shoot(gameTime, explosions, sprites);
                        machineGun.Play();
                    }
                }

                camera.Pos += movement * 20;
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
            List<Projectile> survivingBullets = new List<Projectile>();
            foreach (Projectile bullet in bullets)
            {
                bullet.Update(gameTime, allEntities);
                if (bullet.Alive)
                {
                    survivingBullets.Add(bullet);
                }
                else
                {
                    explosions.Add(new ShellExplosion(bullet.Position, sprites));
                }
            }
            bullets = survivingBullets;

            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime, allEntities);
            }
            explosions = explosions.Where(e => e.IsAlive).ToList();
            a10.Update(gameTime, allEntities);
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
                turret.Draw(gameTime, spriteBatch, turretConstructingSprite, turretSprite, turretSpriteNod);
            }
            foreach (Projectile bullet in bullets)
            {
                bullet.Draw(gameTime, spriteBatch, bulletSprite);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(gameTime, spriteBatch);
            }
            a10.Draw(gameTime, spriteBatch, a10Sprite, whitePixelSprite);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
