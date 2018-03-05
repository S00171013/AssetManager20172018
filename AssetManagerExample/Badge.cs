using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AssetManagerExample
{
    class Badge
    {
        protected Game myGame;

        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public bool Clicked { get; set; }
        public Rectangle BoundingRectangle { get; set; }

        public Badge(Game1 gameIn, string nameIn, Texture2D textureIn, bool clickedStatusIn, Rectangle rectIn)
        {
            myGame = gameIn;
            Name = nameIn;
            Texture = textureIn;
            Clicked = clickedStatusIn;
            BoundingRectangle = rectIn;
        }

        public void Update(GameTime gameTime)
        {
            // Declare variable to keep track of mouse state.
            var mouseState = Mouse.GetState();

            var mousePosition = new Point(mouseState.X, mouseState.Y);

            // If the user clicks on the badge...
            if (mouseState.LeftButton == ButtonState.Pressed && BoundingRectangle.Contains(mousePosition))
            {
                // ...This badge has been selected by the player.
                Clicked = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //spriteBatch.Draw(Texture, BoundingRectangle, Color.White);
            spriteBatch.Draw(Texture, new Vector2(BoundingRectangle.X, BoundingRectangle.Y), Color.White);
            

            spriteBatch.End();
        }

    }
}
