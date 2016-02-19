using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LemonParticlesSystem.Screen
{
    public class SplashScreen : Screen
    {
        Texture2D background;
        [XmlElement("Path")]
        public string backgroundPath;

        public SplashScreen()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            background = Content.Load<Texture2D>(backgroundPath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputController.Input.Instance.IsKeyPressed(Keys.Enter))
            {
                ScreenManager.instance.AddScreen(new MenuScreen());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0,0, (int)ScreenManager.instance.ScreenSize.X, (int)ScreenManager.instance.ScreenSize.Y), Color.White);
        }
    }
}
