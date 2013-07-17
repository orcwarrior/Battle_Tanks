using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Battle_Tanks.Visuals;
namespace Battle_Tanks.Objects
{
	public class staticObjectGroup : staticObject
	{
		List <Sprite> objsVisuals;
		protected eSOBQuarters quarters;

		public override bool Destroy(Rectangle destroyerBBox)
        {
            bool sthDestroyed = false;
            if (isColideable && isDestroyable)
            {
                for (int i = 0; i < objsVisuals.Count; i++)
                {
                    Rectangle bbox = new Rectangle(objsVisuals[i].pos, new Size(Engine.gridSize / 2, Engine.gridSize / 2));
                    if (bbox.IntersectsWith(destroyerBBox))
                    {
                        objsVisuals.RemoveAt(i); i--;
                        sthDestroyed = true;
                        if (objsVisuals.Count == 0)
                        { //all group objs destroyed
                            _Destroy();
                        }
                    }
                }
            }
            return sthDestroyed;
		}
        public override bool intersectWith(Rectangle rectangle)
        {
            for (int i = 0; i < objsVisuals.Count; i++)
            {
                Rectangle bbox = new Rectangle(objsVisuals[i].pos, new Size(Engine.gridSize / 2, Engine.gridSize / 2));
                if(bbox.IntersectsWith(rectangle))
                    return true;
            }
            return false;
        }

        public override void Update()
		{
			throw new NotImplementedException();
		}

		public override void Render()
		{
			for (int i = 0; i < objsVisuals.Count; i++)
				objsVisuals[i].Render();
		}

		public staticObjectGroup(Point pos, eStaticObjType objType, eSOBQuarters quarts)
		:	 base(pos,objType)
		{
			objsVisuals = new List<Sprite>();
			quarters = quarts;
            string baseName="";
            if (objType == eStaticObjType.BRICK_HARD)
                baseName = "BRICKHARD";
            else if (objType == eStaticObjType.BRICK)
                baseName = "BRICK";
            else return;
				Point tmp = pos;
				//Ta kolejnoœæ gwarantuje renderowanie kawa³ków obietków bez problemu z nakladaniem sie tektur na siebie:
				tmp.X += Engine.gridSize / 2;
				tmp.Y += Engine.gridSize / 2;
				if (quarters.HasFlag(eSOBQuarters.LOWER_RIGHT))
					objsVisuals.Add(new Sprite(baseName+"_LR.png", tmp));
				tmp.X -= Engine.gridSize / 2;
				if (quarters.HasFlag(eSOBQuarters.LOWER_LEFT))
                    objsVisuals.Add(new Sprite(baseName + "_LL.png", tmp));

				tmp.X += Engine.gridSize / 2;
				tmp.Y -= Engine.gridSize / 2;
				if (quarters.HasFlag(eSOBQuarters.UPPER_RIGHT))
                    objsVisuals.Add(new Sprite(baseName + "_UR.png", tmp));
				tmp.X -= Engine.gridSize / 2;
				if(quarters.HasFlag(eSOBQuarters.UPPER_LEFT))
                    objsVisuals.Add(new Sprite(baseName + "_UL.png", tmp));

		}

        private void _Add(staticObject SOB)
        {
            throw new NotImplementedException();
        }
	}
}
