using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MacUndertale___JJK
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D titleImg;
        SpriteFont titleFont;
        Song titleSong;
        Rectangle window;
        float titleO, titleTime, screenTime;
        Vector2 openingText;
        
        enum Screen
        {
            Title, Opening, Main, Win, Lose
        }
        Screen screen = Screen.Title;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 1500, 900);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            openingText = new Vector2(275, 375);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            titleImg = Content.Load<Texture2D>("titleScreen");
            titleSong = Content.Load<Song>("titleTheme");
            titleFont = Content.Load<SpriteFont>("titleFont");
            MediaPlayer.Volume = 0.6f;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Title)
            {
                screenTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                titleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                titleO = 0f;
                if (titleTime <= 0.5f)
                    titleO = titleTime / 0.5f;
                else if (titleTime <= 1.5f)
                    titleO = 1;
                else if (titleTime <= 2f)
                    titleO = 1f - ((titleTime - 1.5f) / 0.5f);
                else
                {
                    titleO = 0;
                    titleTime = 0;
                }
                
                if (MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(titleSong);
                }
                if (screenTime >= 5)
                    screen = Screen.Opening;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Title)
                _spriteBatch.Draw(titleImg, window, Color.White * titleO);
            if (screen == Screen.Opening)
            {
                _spriteBatch.DrawString(titleFont, "Welcome to Shibuya!", openingText, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
