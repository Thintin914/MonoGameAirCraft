using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab08
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ChaseCamera cam;
        private Model groundModel;
        Windmill windmill;
        Helicopter helicopter;
        HelicopterControllable player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cam = new ChaseCamera(this);
            cam.up = Vector3.Up;
            cam.fieldOfView = MathHelper.PiOver4;
            cam.aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            cam.nearClip = 0.1f;
            cam.farClip = 1000;
            cam.positionOffset = new Vector3(0, 0.25f, 1);
            cam.lookAtOffset = new Vector3(0, 0.1f, -1);
            cam.targetUp = Vector3.Up;
            cam.targetPosition = new Vector3(0.0f, 0.9f, -4.0f); // windmill position
            cam.targetDirection = -Vector3.Forward;
            Components.Add(cam);

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            windmill = new Windmill(this, new Vector3(0, 0.9f, -4));
            windmill.view = cam.view;
            windmill.project = cam.project;
            Components.Add(windmill);

            helicopter = new Helicopter(this, new Vector3(0, 0.9f, -4));
            helicopter.view = cam.view;
            helicopter.project = cam.project;
            Components.Add(helicopter);

            player = new HelicopterControllable(this, new Vector3(0, 1, -6));
            player.cam = cam;
            Components.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            groundModel = Content.Load<Model>("ground");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            windmill.view = cam.view;
            windmill.project = cam.project;
            helicopter.view = cam.view;
            helicopter.project = cam.project;

            cam.targetDirection = player.direction;
            cam.targetPosition = player.position;
            cam.targetUp = player.up;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            groundModel.Draw(Matrix.CreateScale(0.001f), cam.view, cam.project);
            base.Draw(gameTime);
        }
    }
}
