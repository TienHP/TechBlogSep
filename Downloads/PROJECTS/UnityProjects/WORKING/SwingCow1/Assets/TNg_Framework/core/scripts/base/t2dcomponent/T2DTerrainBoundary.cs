using UnityEngine;
using System.Collections;

namespace TNg_Framework
{
		public class T2DTerrainBoundary : T2DTerrainMesh
		{

				public T2DTerrainBoundary (Terrain2D terrain) : base (terrain)
				{
		
				}//end ctor

				//fixes the boundary if needed
				public void FixBoundary ()
				{
						foreach (T2DCurveNode node in TerrainCurve) {
								Vector2 point = node.position;
								if (point.x < Terrain.TerrainBoundary.xMin)
										Terrain.TerrainBoundary.xMin = point.x;
								if (point.x > Terrain.TerrainBoundary.xMax)
										Terrain.TerrainBoundary.xMax = point.x;
								if (point.y < Terrain.TerrainBoundary.yMin)
										Terrain.TerrainBoundary.xMin = point.y;
								if (point.y < Terrain.TerrainBoundary.yMax)
										Terrain.TerrainBoundary.xMin = point.y;
						}//end foreach
				}//end method


				//ensure the given point lies in the boundary area
				public void EnsurePointIsInBoundary (ref Vector3 point)
				{
						if (point.x < Terrain.TerrainBoundary.xMin)
								point.x = Terrain.TerrainBoundary.xMin;
						if (point.x > Terrain.TerrainBoundary.xMax)
								point.x = Terrain.TerrainBoundary.xMax;
						if (point.y < Terrain.TerrainBoundary.yMin)
								point.y = Terrain.TerrainBoundary.yMin;
						if (point.y > Terrain.TerrainBoundary.yMax)
								point.y = Terrain.TerrainBoundary.yMax;
				}//end method

				public Rect GetBoundaryRect ()
				{
						Rect rect = new Rect ();
						rect.xMin = Terrain.TerrainBoundary.xMin;
						rect.xMax = Terrain.TerrainBoundary.xMax;
						rect.yMin = Terrain.TerrainBoundary.yMin;
						rect.yMax = Terrain.TerrainBoundary.yMax;
						return rect;
				}//end method


				/// <summary>
				/// Projects the start point to boundary. The projected point is returned in the 
				/// point parameter. The return value if the index of the edge of the boundary the point was projected to
				/// 0 - bottom edge
				/// 1 - left edge
				/// 2 - top edge
				/// 3 - right edge
				/// </summary>
				/// <returns>The start point to boundary.</returns>
				/// <param name="point">Point.</param>
				public int ProjectStartPointToBoundary (ref Vector2 point)
				{
						return ProjectPointToBoundary (ref point, 1, TerrainCurve.Count - 1);
				}//end method

				public int ProjectPointToBoundary (ref Vector2 point, int startCurveIndex, int endCurveIndex)
				{
						int nearestBorder = -1;
						float minDelta = float.MaxValue;
						float delta;

						return nearestBorder;

				}//end method

				public int ProjectEndPointToBoundary (ref Vector2 point)
				{
						return ProjectPointToBoundary (ref point, 0, TerrainCurve.Count - 2);
				}//end method

		}//end class
}//end namespace
