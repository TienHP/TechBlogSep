using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TNg_Framework
{
//hold all vertex mesh of terrain
		public abstract class T2DTerrainMesh
		{
				private Terrain2D mTerrain;

	#region Properties
		
				//terrain object 
				protected Terrain2D Terrain { get { return mTerrain; } }

				//current transform of terrain object
				protected Transform transform { get { return mTerrain.transform; } }

				//Curves of the terrain
				protected List<T2DCurveNode> TerrainCurve { get { return mTerrain.terrainCurve; } }

				//Textures used for the curve mesh
				protected List<T2DCurveTexture> curveTextures { get { return mTerrain.curveTextures; } }

				//textures used for grass
				protected List<T2DGrassTexture> grassTextures { get { return mTerrain.grassTextures; } }

				//protected List<Texture2D> curveControlTextures {get {return mTerrain.c}};

	#endregion

				public T2DTerrainMesh (Terrain2D terrain)
				{
						mTerrain = terrain;	
				}//end ctor

				protected void ResetMeshObjectsTransform ()
				{

						//reset fill mesh
						transform.FindChild (EditorConstantStore.FILL_MESH_NAME).localPosition = Vector3.zero;
						transform.FindChild (EditorConstantStore.FILL_MESH_NAME).localRotation = Quaternion.identity;      
						transform.FindChild (EditorConstantStore.FILL_MESH_NAME).localScale = Vector3.one;

						//reset curve mesh
						transform.FindChild (EditorConstantStore.CURVE_MESH_NAME).localPosition = Vector3.zero;
						transform.FindChild (EditorConstantStore.CURVE_MESH_NAME).localRotation = Quaternion.identity;
						transform.FindChild (EditorConstantStore.CURVE_MESH_NAME).localScale = Vector3.one;

						//reset grass mesh
						transform.FindChild (EditorConstantStore.GRASS_MESH_NAME).localPosition = Vector3.zero;
						transform.FindChild (EditorConstantStore.GRASS_MESH_NAME).localRotation = Quaternion.identity;
						transform.FindChild (EditorConstantStore.GRASS_MESH_NAME).localScale = Vector3.one;

						//reset collider mesh
						transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME).localPosition = Vector3.zero;
						transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME).localRotation = Quaternion.identity;
						transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME).localScale = Vector3.one;

				}//end method
		

				//make sure the sub-objects of the main object exist
				protected void EnsureMeshObjectsExist ()
				{
						if (transform.FindChild (EditorConstantStore.FILL_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.FILL_MESH_NAME);
								go.transform.parent = transform;
						}//end if

						if (transform.FindChild (EditorConstantStore.CURVE_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.FILL_MESH_NAME);
								go.transform.parent = transform;
						}///end if

						if (transform.FindChild (EditorConstantStore.GRASS_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.GRASS_MESH_NAME);
								go.transform.parent = transform;
						}//end if

						if (transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.COLLIDER_MESH_NAME);
								go.transform.parent = transform;
						}//end if
				}//end method

				protected void EnsureScriptsAttached (GameObject meshObject)
				{
				}//end method

				//makes sure the components carrying the mesh and material data in the sub-objects exist
				protected void EnsureMeshComponentsExist ()
				{
				}//end method

				protected void EnsureMeshFilterExist (GameObject meshObject)
				{
				}//end method
		
		}//end class
}//end namespace
