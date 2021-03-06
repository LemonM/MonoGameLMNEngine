﻿using System;
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
    class MainScreen : GameScreen
    {
        SceneManager sceneManager;
        SpriteFont font;

        string Path;

        public SceneManager SceneMngr
        {
            get { return sceneManager; }
        }

        public MainScreen(string path)
        {
            
            sceneManager = new SceneManager();
            Path = path;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            sceneManager.LoadScene(Path);
            sceneManager.LoadContent(content);
            font = Content.Load<SpriteFont>(@"Fonts\Font");
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
                Screen.ScreenManager.instance.AddScreen(new MenuScreen());   
            }
            sceneManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sceneManager.Draw(spriteBatch);
        }
    }
}
