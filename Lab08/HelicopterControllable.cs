using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab08
{
    class HelicopterControllable : DrawableGameComponent
    {
        internal Vector3 direction, position, right, up;
        Vector3 initPosition;
        Model model;
        internal ChaseCamera cam;
        float yaw, pitch, speed;
        const float yawRate = 0.05f;
        const float pitchRate = 0.01f;
        const float forwardSpeed = 0.025f;

        public HelicopterControllable(Game g, Vector3 pos) : base(g)
        {
            position = initPosition = pos; ;
            up = Vector3.Up;
            direction = -Vector3.Forward;
            right = Vector3.Cross(direction, up);
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("helicopter");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
                pitch = pitchRate;
            else if (ks.IsKeyDown(Keys.Down))
                pitch = -pitchRate;
            if (ks.IsKeyDown(Keys.Left))
                yaw = yawRate;
            else if (ks.IsKeyDown(Keys.Right))
                yaw = -yawRate;
            else yaw = 0;
            if (ks.IsKeyDown(Keys.Space))
                speed = 4 * forwardSpeed;
            else speed = forwardSpeed;

            Matrix transform = Matrix.CreateFromAxisAngle(right, pitch) * Matrix.CreateRotationY(yaw);
            up = Vector3.TransformNormal(up, transform);
            direction = Vector3.TransformNormal(direction, transform);
            right = Vector3.Normalize(Vector3.Cross(direction, up));
            up = Vector3.Normalize(Vector3.Cross(right, direction));

            position += direction * speed;

            if (ks.IsKeyDown(Keys.R))
            {
                position = initPosition;
                up = Vector3.Up;
                direction = -Vector3.Forward;
                right = Vector3.Cross(direction, up);
            }
            if (position.Y < 0) position.Y = 0;
            base.Update(gameTime);
        }

        private Matrix YDirection()
        {
            Matrix rotationY = Matrix.Identity;
            rotationY.Forward = direction;
            rotationY.Up = up;
            rotationY.Right = right;
            return rotationY;
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix world = Matrix.CreateScale(0.05f) * YDirection() * Matrix.CreateTranslation(position);
            model.Draw(world, cam.view, cam.project);
            base.Draw(gameTime);
        }
    }
}
