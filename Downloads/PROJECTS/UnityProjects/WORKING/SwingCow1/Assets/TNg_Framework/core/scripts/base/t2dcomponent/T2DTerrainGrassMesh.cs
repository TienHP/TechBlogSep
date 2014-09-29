using UnityEngine;
using System.Collections;

namespace TNg_Framework
{
		public class T2DTerrainGrassMesh : T2DTerrainMesh
		{

	#region Properties

				public MeshRenderer renderer {
						get {
								EnsureMeshComponentsExist ();
								return transform.FindChild (EditorConstantStore.GRASS_MESH_NAME).GetComponent<MeshRenderer> ();
						}
				}

	#endregion


				public T2DTerrainGrassMesh (Terrain2D terrain) : base (terrain)
				{
		
				}

				public void DestroyMesh ()
				{

				}

		}//end class
}//end namespace
