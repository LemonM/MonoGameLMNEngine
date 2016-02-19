using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using LemonParticlesSystem.GUI;

namespace LemonParticlesSystem.Screen
{
    class MenuScreen : Screen
    {
        GUI.GUI gui;
        Texture2D Background;       

         public MenuScreen()
         {
             
         }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            Background = Content.Load<Texture2D>(@"Texture\Menu\BG");

            gui = new GUI.GUI(@"Fonts\Menu");
            gui.LoadContent(Content);
            gui.CreateButton(new Vector2(ScreenManager.instance.ScreenSize.X / 2 - gui.Font.MeasureString("Start").X / 2,
                               ScreenManager.instance.ScreenSize.Y / 2.5f), "Start");
            gui.CreateButton(new Vector2(ScreenManager.instance.ScreenSize.X / 2 - gui.Font.MeasureString("Exit").X / 2,
                               ScreenManager.instance.ScreenSize.Y / 2.5f + 50), "Exit");

            gui.Buttons[0].OnClick += (object a, EventArgs b) =>
            {
                LoadSceneForm form = new LoadSceneForm();
                (a as Button).color = Color.DarkRed;
                //Task.Factory.StartNew(() => form.Show());
                //gui.Buttons[0].Hide();
                form.ShowDialog();
            };

            gui.Buttons[1].OnClick += (object a, EventArgs b) =>
            {
                ScreenManager.instance.game.Content.Unload();
                ScreenManager.instance.game.Exit();
            };
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(gui.Font, "Particle system by Shadrin Dmitriy. Alpha version 4. Tree fix. 2015.", new Vector2(ScreenManager.instance.ScreenSize.X - 750,
                                                                                    ScreenManager.instance.ScreenSize.Y - 50), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            spriteBatch.Draw(Background, new Rectangle(0, 0, (int)ScreenManager.instance.ScreenSize.X, (int)ScreenManager.instance.ScreenSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.End();

            gui.Draw(spriteBatch);
        }
    }
}
