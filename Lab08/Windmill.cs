using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Lab08
{
    class Windmill : DrawableGameComponent
    {
        Model baseModel, fanModel;
        internal Matrix view, project;
        Vector3 position;
        float rotateAngle, rotateSpeed = 0.02f;

        public Windmill (Game g, Vector3 pos) : base(g)
        {
            position = pos;
        }

        protected override void LoadContent()
        {
            baseModel = Game.Content.Load<Model>("base");
            fanModel = Game.Content.Load<Model>("fan");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            rotateAngle = (rotateAngle + rotateSpeed) % MathHelper.TwoPi;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix world = Matrix.CreateScale(10) * Matrix.CreateTranslation(position);
            baseModel.Draw(world, view, project);

            world = Matrix.CreateScale(10) * Matrix.CreateRotationZ(rotateAngle) * Matrix.CreateTranslation(position);
            fanModel.Draw(world, view, project);

            base.Draw(gameTime);
        }
    }
}
