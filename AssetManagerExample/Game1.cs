using AudioPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NSLoader;
using System;
using System.Collections.Generic;



namespace AssetManagerExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dictionary<string, Texture2D> badges = new Dictionary<string, Texture2D>();
        Queue<Texture2D> qbadges = new Queue<Texture2D>();
        //KeyValuePair<string,Texture2D> _current;

        Texture2D _dequeued;
        TimeSpan time = new TimeSpan();

        // Week 6 Exercise.
        // Create a dictionary of Sprite objects based on the some of the badges and display them on the screen.


        SoundEffectInstance player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

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
            badges = Loader.ContentLoad<Texture2D>(Content, "Badges");
            foreach (var item in badges)
            {
                qbadges.Enqueue(item.Value);
            }
            // Get the first item
            _dequeued = qbadges.Dequeue();
            qbadges.Enqueue(_dequeued);

            //AudioManager.SoundEffects = Loader.ContentLoad<SoundEffect>(Content, "Sounds");

            //player = AudioManager.SoundEffects["sound1"].CreateInstance();
            //player.Play();

            //AudioManager.Play(ref player, "Badges_0");   

            // TODO: use this.Content to load your game content here
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
            time += gameTime.ElapsedGameTime;
            if (time.Seconds > 1)
            {
                time = new TimeSpan();
                _dequeued = qbadges.Dequeue();
                qbadges.Enqueue(_dequeued);

                //SoundEffectInstance sound = null;
                //AudioManager.Play(ref player, "Badges_0");
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(_dequeued, new Vector2(100, 100), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

