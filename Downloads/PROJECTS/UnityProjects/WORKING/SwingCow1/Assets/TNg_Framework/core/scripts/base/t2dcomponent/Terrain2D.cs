using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TNg_Framework
{
		[ExecuteInEditMode()]
		public class Terrain2D : MonoBehaviour
		{
				//polygonal chain representing the surface of the terrain
				public List<T2DCurveNode> terrainCurve = new List<T2DCurveNode> ();
				//rectangle defining the edges (boundary) of the terrain
				public Rect TerrainBoundary = new Rect (0, 0, 0, 0);
				//Texture used to fill the terrain from the curve to the boundary
				public Texture FillTexture;
				//width of one tile of FillTexture (world units)
				public float FillTextureTileWidth = EditorConstantStore.INIT_FILL_TEXTURE_WIDTH;
				//height of one tile of FillTexture (world units)
				public float FillTextureTileHeight = EditorConstantStore.INIT_FILL_TEXTURE_HEIGHT;
				//x offset of FillTexture(world units)
				public float FillTextureOffsetX = EditorConstantStore.INIT_FILL_TEXTURE_OFFSEX_X;
				//y offset of FillTexture(world units)
				public float FillTextureOffsetY = EditorConstantStore.INIT_FILL_TEXTURE_OFFSET_Y;
				//check wheter the surface curve is closed or not
				public bool CurveClosed = EditorConstantStore.IS_CLOSED_CURVE;
				//textures used for the stripe near the terrain surface.
				public List<T2DCurveTexture> curveTextures = new List<T2DCurveTexture> ();
				//textures used for grass
				public List<T2DGrassTexture> grassTextures = new List<T2DGrassTexture> ();
				//influences the amount of random scattering
				public float GrassWaveSpeed = EditorConstantStore.INIT_GRASS_WAVE_SPEED;
				//influences the amount of random scattering
				public float GrassScatterRatio = EditorConstantStore.INIT_GRASS_SCATTER_RATIO;
				//
				public bool PlasticEdges = true;

				//private vars
				private T2DTerrainFillMesh mFillMesh;
				private T2DTerrainCurveMesh mCurveMesh;
				private T2DTerrainGrassMesh mGrassMesh;
				private T2DTerrainColliderMesh mColliderMesh;
				private T2DTerrainBoundary mBoundary;
				private bool mCurveIntercrossing;

	#region Properties

				//can terrain be edited by the tools
				public bool IsEditable { get { return terrainCurve != null && terrainCurve.Count >= 2; } }
				//
				public T2DTerrainBoundary boundary { get { return mBoundary; } }
				//
				public T2DTerrainCurveMesh curveMesh { get { return mCurveMesh; } }
				//
				public T2DTerrainGrassMesh grassMesh { get { return mGrassMesh; } }
				//
				public T2DTerrainFillMesh fillMesh { get { return mFillMesh; } }
				//
				public T2DTerrainColliderMesh colliderMesh { get { return mColliderMesh; } }
				//
				public bool CurveIntercrossing { get { return mCurveIntercrossing; } }
				//
				[System.NonSerialized]
				public Object
						EditorReference;
	#endregion

	#region Events

				void OnEnable ()
				{

						EditorReference = null;

						//create t2d meshes 
						mBoundary = new T2DTerrainBoundary (this);
						mFillMesh = new T2DTerrainFillMesh (this);
						mCurveMesh = new T2DTerrainCurveMesh (this);
						mGrassMesh = new T2DTerrainGrassMesh (this);
						mColliderMesh = new T2DTerrainColliderMesh (this);

						if (!mFillMesh.IsMeshValid () || (T2DUtils.DEBUG_REBUILD_ON_ENABLE && !Application.isPlaying)) {

								FixCurve ();
								FixBoundary ();
								RebuildAllMaterials ();
								RebuildAllMeshes ();

						}//end if
		else {
								//make sure the curve control textures area ready
						}//end else

				}//end method

				//fixes the curve if needed and detects any problems with it, so that they can be reported to the user
				public void FixCurve ()
				{

						//fix NaNs
						for (int i = 0; i < terrainCurve.Count; i++) {
								if (float.IsNaN (terrainCurve [i].position.x))
										terrainCurve [i].position.x = 0;
								if (float.IsNaN (terrainCurve [i].position.y))
										terrainCurve [i].position.y = 0;
						}//end for

						//fix the endings
						if (terrainCurve.Count >= 3 && !CurveClosed && terrainCurve [terrainCurve.Count - 1] == terrainCurve [0]) {
								Vector2 delta = terrainCurve [terrainCurve.Count - 1].position - terrainCurve [terrainCurve.Count - 2].position;
								terrainCurve [terrainCurve.Count - 1].position -= 0.5f * delta;
						}//end if

						if (terrainCurve.Count > 3 && CurveClosed) {
								terrainCurve [terrainCurve.Count - 1].Copy (terrainCurve [0]);
						}//end if

						if (EditorConstantStore.CHECK_CURVE_INTERCROSSING) {
								mCurveIntercrossing = false;
								int count = terrainCurve.Count;
								if (CurveClosed)
										count--;
								for (int i = 3; i < count; i++) {

								}//end for

						}//end if

				}//end method

				public void FixBoundary ()
				{
						boundary.FixBoundary ();
				}//end method

				public void RebuildAllMaterials ()
				{
						mFillMesh.RebuildMaterial ();
				}//end method

				public void RebuildAllMeshes ()
				{
				}//end method

				//clear all meshes
				public void Reset ()
				{
						//clear all nodes
						terrainCurve.Clear ();
						TerrainBoundary = new Rect (0, 0, 0, 0);
						mFillMesh.DestroyMesh ();
						mCurveMesh.DestroyMesh ();
						mGrassMesh.DestroyMesh ();
						mColliderMesh.DestroyMesh ();
				}//end method

				//true if the segment (a, b) intersects the curve anywhere from startIndex to endIndex
				public bool IntersectsCurve (int startIndex, int endIndex, Vector2 a, Vector2 b)
				{
						for (int i = startIndex; i < endIndex; i++) {
						}//end for
						return true;
				}//end method

	#endregion

		}//end class

}//end namespace