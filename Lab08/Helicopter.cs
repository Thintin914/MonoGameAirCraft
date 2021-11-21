using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab08
{
    class Helicopter : DrawableGameComponent
    {
        Model model;
        Vector3 rotateCenter, offset = new Vector3(0, 0, -1);
        float angleY, angleZ, rotateSpeed = 0.05f;
        internal Matrix view, project;

        public Helicopter(Game g, Vector3 pivot): base(g)
        {
            rotateCenter = pivot;
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("helicopter");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            angleY = (angleY + rotateSpeed) % MathHelper.TwoPi;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix world = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(offset) * Matrix.CreateRotationY(angleY) * Matrix.CreateRotationZ(angleZ) * Matrix.CreateTranslation(rotateCenter);
            
            model.Draw(world, view, project);
        }
    }
}
