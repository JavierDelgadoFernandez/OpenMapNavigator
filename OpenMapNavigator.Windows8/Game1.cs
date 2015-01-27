using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenMapNavigator.GraphicsEngine;
using OpenMapNavigator.GraphicsEngine.Graphics.Util;
using Windows.Storage;

namespace OpenMapNavigator.Windows8
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect basicEffect;

        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        //Matrix view = Matrix.CreateLookAt(new Vector3(-4.213676f, 43.3178444f, 0.01f), new Vector3(-4.213676f, 43.3178444f, 0), Vector3.Up);
        Matrix view = Matrix.CreateLookAt(new Vector3(-421334.6f, 4331684.5f, 1000f), new Vector3(-421334.6f, 4331684.5f, 0), Vector3.Up);
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.001f, float.MaxValue);

        GraphicNavigator graphicNavigator;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice);

            graphicNavigator = new GraphicNavigator(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            vertexBuffer = graphicNavigator.GetVertex();

            var fwd = 0.0001;
            fwd = 10;

            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Up))
                view.Translation = new Vector3((float)(view.Translation.X), (float)(view.Translation.Y - fwd), (float)(view.Translation.Z));
            if (ks.IsKeyDown(Keys.Down))
                view.Translation = new Vector3((float)(view.Translation.X), (float)(view.Translation.Y + fwd), (float)(view.Translation.Z));
            if (ks.IsKeyDown(Keys.Left))
                view.Translation = new Vector3((float)(view.Translation.X + fwd), (float)(view.Translation.Y), (float)(view.Translation.Z));
            if (ks.IsKeyDown(Keys.Right))
                view.Translation = new Vector3((float)(view.Translation.X - fwd), (float)(view.Translation.Y), (float)(view.Translation.Z));


            var zoom = 0.0005;
            zoom = 50;
            if(ks.IsKeyDown(Keys.PageDown))
                view.Translation = new Vector3((float)(view.Translation.X), (float)(view.Translation.Y), (float)(view.Translation.Z + zoom));

            if (ks.IsKeyDown(Keys.PageUp))
                view.Translation = new Vector3((float)(view.Translation.X), (float)(view.Translation.Y), (float)(view.Translation.Z - zoom));

            base.Update(gameTime);
        }

        VertexBuffer vertexBuffer;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.LightGray);

            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = true;

            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount / 3); 
            }

            base.Draw(gameTime);
        }
    }
}
