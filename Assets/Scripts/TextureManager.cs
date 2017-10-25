using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour {
	public Texture m_textureFile;
	public TextAsset m_atlasFile;
	private Dictionary<string, TextureData> m_sprites;
	private static TextureManager instance;
	public static TextureManager Instance{
		get{
			return instance ?? (instance = new GameObject("Singleton").AddComponent<TextureManager>()); 
		}
	}
	// Use this for initialization
	void Awake()
	{
		if(instance != null && instance != this){
			Destroy(gameObject);
			return;
		}
		instance = this;
		m_sprites = new Dictionary<string, TextureData>();
		DontDestroyOnLoad(gameObject);
		ParseXML(m_atlasFile.text);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void ParseXML(string xmlData){
		XMLDocument xDoc = new XMLDocument();
		xDoc.Load(new StringReader(xmlData));
		string xmlPathPattern = "//atlas/image";
		XmlNodeList nodes = xDoc.SelectNodes(xmlPathPattern);
		foreach(XmlNode node in nodes){
			TextureData data = ParseNode(node);
			m_sprites[data.name] = data;
		}
	}
	private TextureData ParseNode(XmlNode node){
		TextureData data;
		float textureWidth = m_textureFile.width;
		float textureHeight = m_textureFile.height;
		
		XmlNode nameNode = node.FirstChild;
		XmlNode xNode = nameNode.NextSibling;
		XmlNode yNode = xNode.NextSibling;
		XmlNode widthNode = yNode.NextSibling;
		XmlNode heightNode = widthNode.NextSibling;
		
		data.name = nameNode.InnerXml;
		data.x = float.Parse(xNode.InnerXml)/textureWidth;
		data.y = float.Parse(yNode.InnerXml)/textureHeight;
		data.width = float.Parse(widthNode.InnerXml)/textureWidth;
		data.height = float.Parse(heightNode.InnerXml)/textureHeight;
		return data;
	}
	public TextureData GetTexture(string imageName){
		TextureData retVal;
		retVal.name = "default";
		retVal.x = 0;
		retVal.y = 0;
		retVal.width = 1;
		retVal.height = 1;
		if(m_sprites.ContainsKey(imageName)){
			retVal = m_sprites[imageName];
		}
		return retVal;
	}
}
