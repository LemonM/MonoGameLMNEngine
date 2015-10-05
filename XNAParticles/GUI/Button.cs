using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LemonParticlesSystem.GUI.Interfaces;

namespace LemonParticlesSystem.GUI 
{
    public class Button : IInteractiveGUIObject
    {
        public Color color;
        Rectangle rect;
        SpriteFont font;

        
        GUI ParentGUI { get; set; }

        public string ButtonString { get; set; }
        public Vector2 Position { get; set; }

        public event EventHandler OnClick;
        public event EventHandler OnMouseOver;
        public event EventHandler OnHide;
        public event EventHandler OnShow;

        public Button(Vector2 position, string text, GUI parentGui) 
        {
            ParentGUI = parentGui;
            Position = position;
            ButtonString = text;
            rect = new Rectangle((int)Position.X, (int)Position.Y, (int)parentGui.Font.MeasureString(ButtonString).X, (int)parentGui.Font.MeasureString(ButtonString).Y);
            color = Color.White;
            OnMouseOver += OnMouseOverHandler;
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(ParentGUI.pathToFont);
        }

        public void Update(GameTime gameTime)
        {
            if (!rect.Intersects(ParentGUI.cursor.rectangle))
                color = Color.White;
            else if (OnMouseOver != null)
                    OnMouseOver(this, null);

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ParentGUI.Font, ButtonString, Position, color);
        }

        void OnMouseOverHandler(object sender, EventArgs e)
        {
            (sender as Button).color.G = (byte)MathHelper.SmoothStep((sender as Button).color.G, 90, 0.1f);
            (sender as Button).color.B = (byte)MathHelper.SmoothStep((sender as Button).color.B, 90, 0.1f);
            (sender as Button).color.R = (byte)MathHelper.SmoothStep((sender as Button).color.R, 90, 0.1f);
            if (ParentGUI.cursor.state.LeftButton == ButtonState.Pressed)
            {

                if (OnClick != null)
                    OnClick(this, null);
            }
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
