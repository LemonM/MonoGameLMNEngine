using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using LemonParticlesSystem.ParticlesSystem;

namespace LemonParticlesSystem
{
    class SceneManager
    {
        Scene CurrentScene;
        XmlManager<Scene> sceneXmlManager;

        ContentManager Content;

        public SceneManager()
        {
            sceneXmlManager = new XmlManager<Scene>();
            //CreateNewScene();
        }

        public void CreateNewScene()
        {
            CurrentScene = new Scene();
            CurrentScene.Emitters = new List<ParticleEmitter>();
        }

        public void LoadScene(string ScenePath)
        {
            sceneXmlManager.type = typeof(Scene);
            CurrentScene = sceneXmlManager.Load(ScenePath);
        }

        public void SaveScene(string ScenePath)
        {
            sceneXmlManager.Save(ScenePath, CurrentScene);
        }

        public void LoadContent(ContentManager content)
        {
            CurrentScene.LoadContent(content);
        }

        public void UnloadContent()
        {
            CurrentScene.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.DepthRead, RasterizerState.CullNone, null);
            CurrentScene.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
