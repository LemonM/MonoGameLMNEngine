using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LemonParticlesSystem.GUI
{
    public class GUI
    {
        List<Text> Labels;
        public List<Button> Buttons { get; set; }

        public Cursor cursor;

        public SpriteFont Font { get; set; }

        public string pathToFont { get; set; }

        public GUI(string path)
        {
            Labels = new List<Text>();
            Buttons = new List<Button>();
            pathToFont = path;
            cursor = new Cursor();
        }

        public void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>(pathToFont);

            XmlManager<Cursor> manager = new XmlManager<Cursor>();
            manager.type = typeof(Cursor);
            cursor = manager.Load(@"Content\Xml\Cursor.xml");
            cursor.LoadContent(content);
        }

        public void UnloadContent()
        {
                
        }

        public void Update(GameTime gameTime)
        {
            foreach (Button button in Buttons)
            {
                button.Update(gameTime);
            }

            cursor.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Text label in Labels)
            {
                label.Draw(spriteBatch);
            }

            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }

            cursor.Draw(spriteBatch);
        }

        public void CreateButton(Vector2 position, string text)
        {
            Buttons.Add(new Button(position, text, this));
        }
    }
}
