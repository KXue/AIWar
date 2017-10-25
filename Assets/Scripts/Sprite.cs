using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct TextureData{
	public string name;
	public float x;
	public float y;
	public float width;
	public float height;
}
public class Sprite : MonoBehaviour {
	public string m_imageName;
	private TextureData m_data;
	// Use this for initialization
	void Start () {
		m_data = TextureManager.Instance.GetTexture(m_imageName);
		Vector2[] uvs = new Vector2[4];
		uvs[0].x = m_data.x;
		uvs[0].y = 1 - (m_data.y + m_data.height);
		uvs[1].x = (m_data.x + m_data.width);     
		uvs[1].y = 1 - m_data.y;     
		uvs[2].x = (m_data.x + m_data.width);     
		uvs[2].y = 1 - (m_data.y + m_data.height);     
		uvs[3].x = m_data.x;     
		uvs[3].y = 1 - m_data.y;     //get the mesh and change it's uv coordinates.     
		Mesh mesh = GetComponent<MeshFilter>().mesh;     
		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
