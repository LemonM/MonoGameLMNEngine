using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LemonParticlesSystem.InputController;

using LemonParticlesSystem.ParticlesSystem;

namespace LemonParticlesSystem.Screen
{
    class MainScreen : Screen
    {
        SceneManager sceneManager;

        GUI.GUI inGameGUI;

        string Path;

        public SceneManager SceneMngr
        {
            get { return sceneManager; }
        }

        public MainScreen(string path) : base()
        {
            inGameGUI = new GUI.GUI(@"Fonts\Menu");            
            sceneManager = new SceneManager();
            Path = path;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            sceneManager.LoadScene(Path);
            sceneManager.LoadContent(content);
            inGameGUI.LoadContent(content);
            inGameGUI.Buttons.Add(new GUI.Button(new Vector2(1000, 100), "ChangeTexture", inGameGUI));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            sceneManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (Input.Instance.IsKeyPressed(Keys.Escape))
            {
                ScreenManager.instance.AddScreen(new MenuScreen());   
            }
            sceneManager.Update(gameTime);
            inGameGUI.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sceneManager.Draw(spriteBatch);
            inGameGUI.Draw(spriteBatch);
        }
    }
}
