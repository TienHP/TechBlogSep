using UnityEngine;
using System.Collections;

namespace TNg_Framework
{
		public class T2DTerrainFillMesh : T2DTerrainMesh
		{
				public T2DTerrainFillMesh (Terrain2D terrain) : base (terrain)
				{
				}//end ctor
		
				//mesh renderer
				public MeshRenderer renderer {
						get {
								EnsureMeshComponentsExist ();
								return transform.FindChild (EditorConstantStore.FILL_MESH_NAME).GetComponent<MeshRenderer> ();
						}
				}//end method

				//return true if the mesh is valid
				public bool IsMeshValid ()
				{
						return true;
				}//end method

	#region Mesh

				void RebuildMesh ()
				{
				}//end method

				//destroy mesh data
				public void DestroyMesh ()
				{
				}//end method

	#endregion

	#region Materials

				//rebuilds the material from scratch deleting the old one if needed
				public void RebuildMaterial ()
				{
				}//end method

	#endregion

		}//end class
}//end namespace
