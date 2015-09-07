using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LemonParticlesSystem.Screen
{
    public class ScreenManager
    {
        ContentManager Content;

        Game _game;

        protected static ScreenManager Instance;

        GameScreen CurrentScreen;
        GameScreen NewScreen;

        Vector2 screenSize;

        XmlManager<GameScreen> xmlGameScreenManager;

        Stack<GameScreen> ScreenStack = new Stack<GameScreen>();

        public event EventHandler<ScreenChangeEventArgs> OnScreenChangeStart;
        public event EventHandler OnScreenChangeEnd;

        public Game game
        {
            get { return _game; }
            set { _game = value; }
        }

        public static ScreenManager instance
        {
            get
            {
                if (Instance == null)
                    Instance = new ScreenManager();
                return Instance;
            }
        }

        public Vector2 ScreenSize
        {
            get { return screenSize; }
            set
            {
                screenSize.X = Math.Max(0, (int)value.X);
                screenSize.Y = Math.Max(0, (int)value.Y);
            }
        }

        
        public ScreenManager()
        {
            OnScreenChangeStart += ScreenChange;
            CurrentScreen = new MenuScreen();
        }
         
        public void AddScreen(GameScreen screen)
        {
            if (OnScreenChangeStart != null)
                OnScreenChangeStart(this, new ScreenChangeEventArgs(CurrentScreen, screen));
        }

        void ScreenChange(object sender, ScreenChangeEventArgs scrChangeEvtArgs)
        {
            NewScreen = scrChangeEvtArgs.newScreen;
            ScreenStack.Push(NewScreen);
            if (scrChangeEvtArgs.currentScreen != null)
                scrChangeEvtArgs.currentScreen.UnloadContent();
            CurrentScreen = NewScreen;
            CurrentScreen.LoadContent(Content); 
        }
        #region MainMethods
		
        public void Initialize() 
        {
            
        }

        public void LoadContent(ContentManager content) 
        {
            Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            CurrentScreen.LoadContent(content);
        }
        public void UnloadContent() { }
        public void Update(GameTime gameTime) 
        {
            CurrentScreen.Update(gameTime);
            
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            CurrentScreen.Draw(spriteBatch);
        }

	    #endregion
    }
}
