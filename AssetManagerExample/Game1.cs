﻿using AudioPlayer;
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

        // Badge Texture Dictionary.
        Dictionary<string, Texture2D> badges = new Dictionary<string, Texture2D>();

        // Badge Object Dictionary.
        Dictionary<string, Badge> badgeObjects = new Dictionary<string, Badge>();

        // Queue Method.
        Queue<Texture2D> qbadges = new Queue<Texture2D>();

        //KeyValuePair<string,Texture2D> _current;

        Texture2D _dequeued, background;
        TimeSpan time = new TimeSpan();

        SoundEffectInstance player;

        // Set up position variables for the badges.
        Rectangle[] positions = new Rectangle[5];
        int positionCounter = 0;

        // Week 6 Exercise.
        // Create a dictionary of Sprite objects based on the some of the badges and display them on the screen.

        // Queue Method.
        //Queue<Badge> queueBadgeObjects = new Queue<Badge>();
        //Badge dequeuedBadge;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

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

            // Set up badge position.
            //Rectangle position1 = new Rectangle(viewport.Width / 2, viewport.Height / 2, 100, 100);

            IsMouseVisible = true;
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

            // Load badge textures.
            badges = Loader.ContentLoad<Texture2D>(Content, "Badges");

            // Ex 6         
            // Set up viewport object.
            Viewport viewport = GraphicsDevice.Viewport;


            // Set up badge position.
            // Set up menu positions
            Rectangle position1 = new Rectangle(viewport.Width / 2-60, viewport.Height / 2-60, 100, 100);
            Rectangle position2 = new Rectangle(position1.X + 70, position1.Y, 100, 100);
            Rectangle position3 = new Rectangle(position2.X + 70, position2.Y, 100, 100);
            Rectangle position4 = new Rectangle(position3.X + 70, position3.Y, 100, 100);
            Rectangle position5 = new Rectangle(position4.X + 70, position4.Y, 100, 100);

            positions[0] = position1;
            positions[1] = position2;
            positions[2] = position3;
            positions[3] = position4;
            positions[4] = position5;

            // Texture version.
            foreach (var item in badges)
            {
                // Queue Method
                // qbadges.Enqueue(item.Value);

                // Ex 6.
                // Set up Badge Objects.
                badgeObjects.Add(item.Key, new Badge(this, item.Key, item.Value, false, positions[positionCounter]));
                positionCounter++;
            }

            // Texture version.
            // Get the first texture item.

            //_dequeued = qbadges.Dequeue();
            //qbadges.Enqueue(_dequeued);

            // Add badge objects to badge object queue.
            //foreach (var badge in badgeObjects)
            //{
            //    queueBadgeObjects.Enqueue(badge.Value);              
            //}

            // Get the first badge object.
            //dequeuedBadge = queueBadgeObjects.Dequeue();
            //queueBadgeObjects.Enqueue(dequeuedBadge);

            // Load sound effects.
            AudioManager.SoundEffects = Loader.ContentLoad<SoundEffect>(Content, "Sounds");

            player = AudioManager.SoundEffects["Badges_0"].CreateInstance();
            //player.Play();

            //AudioManager.Play(ref player, "Badges_0");

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("gameOverBackground");
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
            // Update current badge.
            //dequeuedBadge.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region Timer and Queue Version - Commented out
            //// Count up.
            //time += gameTime.ElapsedGameTime;                      

            //// What to do each second.
            //if (time.Seconds > 1)
            //{
            //    // Reset time.
            //    time = new TimeSpan();

            //    #region Texture version.
            //    // Move onto the next badge in the circular queue.
            //    //_dequeued = qbadges.Dequeue();
            //    //qbadges.Enqueue(_dequeued);
            //    #endregion

            //    // Ex 6
            //    // Move onto the next badge item in the circular queue.
            //    //dequeuedBadge = queueBadgeObjects.Dequeue();
            //    //queueBadgeObjects.Enqueue(dequeuedBadge);

            //    //SoundEffectInstance sound = null;
            //   // AudioManager.Play(ref player, "Badges_0");
            //}



            //// Check if a badge has been clicked. Currently, clicking a badge with no sound effect assigned will crash the game.
            //if(dequeuedBadge.Clicked == true)
            //{
            //    AudioManager.Play(ref player, dequeuedBadge.Name);
            //    dequeuedBadge.Clicked = false;
            //}

            // TODO: Add your update logic here
            #endregion

            foreach (var item in badgeObjects)
            {
                item.Value.Update(gameTime);

                // Check if a menu item has been clicked.
                if (item.Value.Clicked == true)
                {
                    AudioManager.Play(ref player, item.Value.Name);
                    item.Value.Clicked = false;
                }
            }

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

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            //spriteBatch.Draw(_dequeued, new Vector2(100, 100), Color.White);  

            spriteBatch.End();

            foreach (var item in badgeObjects)
            {
                item.Value.Draw(spriteBatch);
            }

            // Queue version.
            //dequeuedBadge.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

