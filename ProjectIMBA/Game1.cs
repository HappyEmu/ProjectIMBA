using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;
using System.IO;
using ProjectIMBA.Input;

namespace ProjectIMBA
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		World world;
		Polygon p;
		BasicEffect effect;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);		

			p = new Polygon();
			this.world = new World(new Vector2(0, 9.81f));

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
			effect = new BasicEffect(GraphicsDevice);
			effect.VertexColorEnabled = true;
			effect.World = Matrix.Identity;
			effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);
			effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 16.0f / 9.0f, 1.0f, 100.0f);

			p.Color = Color.Green;

			p.AddEdge(0f, 0f);
			p.AddEdge(0f, 2f);
			p.AddEdge(2f, 2f);
			p.AddEdge(6f, -5f);
			//p.AddEdge(6f, 2f);
			p.AddEdge(2f, 0f);
			p.Finish();

			p.Triangulate();

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
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			float gt = (float)gameTime.TotalGameTime.TotalSeconds;

			InputState.Update();

			this.world.Step(dt);

			p.Rotate(1.0f * dt);

			// Allows the game to exit
			if (InputState.isPressed(Keys.Escape))
				this.Exit();
			if (InputState.isPressed(Input.Action.Hit))
				this.Exit();
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);			
			
			effect.CurrentTechnique.Passes[0].Apply();

			p.Draw(GraphicsDevice);

			base.Draw(gameTime);
		}
	}
}
