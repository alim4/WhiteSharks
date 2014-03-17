using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

public class FileManager : MonoBehaviour {

	private string _filePath = "Episodes/";
	private string _fileName = "FileManagerMasterObject";
	private TextAsset _XMLFile;
	private XmlDocument _XMLDoc;

	// Use this for initialization
	void Start () {
		_XMLFile = (TextAsset)Resources.Load (_filePath + _fileName);
		Debug.Log (_XMLFile);
		_XMLDoc = new XmlDocument ();
		_XMLDoc.LoadXml (_XMLFile.text);

		if (_XMLFile != null){
			WriteNPC ("Jane", 2, "3.0", "7.0", "5.0", "Belly", 1);
			WriteNPC ("Frank", 1, "3.0", "7.0", "5.0", "Fin", 2);

			WriteStartScene ("Yo mamma", 2);
			WriteDialoguer("Dialoguerrrrr", 1);
			WriteBellyAccess ("True", 1);

			getNPCPosition("Frank", 1);

			SaveXML();
		}
	}

	public XmlNodeList ReadXMLFile(string fileName, string tagName) {
		_XMLFile = (TextAsset)Resources.Load (_filePath + fileName);
		_XMLDoc = new XmlDocument ();
		_XMLDoc.LoadXml (_XMLFile.text);

		XmlNodeList nodeList = _XMLDoc.GetElementsByTagName (tagName);
		return nodeList;
	}

	/// <summary>
	/// Write NPC elements into the XML file. Save after. 
	/// </summary>
	/// <param name="NPCname">NP cname.</param>
	/// <param name="episodeNum">Episode number.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="sceneName">Scene name.</param>
	/// <param name="dialoguerID">Dialoguer I.</param>
	public void WriteNPC(string NPCname, int episodeNum, string x, string y, string z, string sceneName, int dialoguerID){
		XmlElement elem;
		XmlNode root = _XMLDoc.GetElementById("_" + episodeNum.ToString());

		// Structure:::
		// <NPC name="Jane">
		//    <position>
		//       <x>1.0</x>
		//       <y>1.0</y>
		//       <z>1.0></y>
		//   </position>
		//   <scene>sceneName</scene>
		//   <dialoguerID>dID</dialoguerID>
		// </NPC>

		// NPC
		elem = _XMLDoc.CreateElement("NPC");
		elem.SetAttribute("name", NPCname);

		// Position
		XmlElement node = _XMLDoc.CreateElement ("position");
		XmlElement xpos = _XMLDoc.CreateElement ("x");
		xpos.InnerText = x;
		XmlElement ypos = _XMLDoc.CreateElement ("y");
		ypos.InnerText = y;
		XmlElement zpos = _XMLDoc.CreateElement ("z");
		zpos.InnerText = z;

		node.AppendChild (xpos);
		node.AppendChild (ypos);
		node.AppendChild (zpos);
		elem.AppendChild (node);

		// Scene Name
		XmlElement scene = _XMLDoc.CreateElement ("scene");
		scene.InnerText = sceneName;
		elem.AppendChild (scene);

		// DialoguerID
		XmlElement dID = _XMLDoc.CreateElement ("dialoguerID");
		dID.InnerText = dialoguerID.ToString ();
		elem.AppendChild (dID);

		root.AppendChild (elem);
	}

	/// <summary>
	/// Writes the start scene for the episode in the XML.
	/// </summary>
	/// <param name="startScene">Start scene.</param>
	/// <param name="episodeNum">Episode number.</param>
	public void WriteStartScene(string startScene, int episodeNum){
		XmlElement elem;
		XmlNode root = _XMLDoc.GetElementById("_" + episodeNum.ToString());

		elem = _XMLDoc.CreateElement ("StartScene");
		elem.InnerText = startScene;

		root.AppendChild (elem);
	}

	/// <summary>
	/// Writes the Dialoguer filename in XML.
	/// </summary>
	/// <param name="dialoguerName">Dialoguer name.</param>
	/// <param name="episodeNum">Episode number.</param>
	public void WriteDialoguer(string dialoguerName, int episodeNum){
		XmlElement elem;
		XmlNode root = _XMLDoc.GetElementById("_" + episodeNum.ToString());
		
		elem = _XMLDoc.CreateElement ("DialoguerName");
		elem.InnerText = dialoguerName;
		
		root.AppendChild (elem);
	}

	/// <summary>
	/// Writes the belly access in XML. BellyAccess should be TRUE or FALSE.
	/// </summary>
	/// <param name="bellyAccess">Belly access.</param>
	/// <param name="episodeNum">Episode number.</param>
	public void WriteBellyAccess(string bellyAccess, int episodeNum){
		XmlElement elem;
		XmlNode root = _XMLDoc.GetElementById("_" + episodeNum.ToString());
		
		elem = _XMLDoc.CreateElement ("BellyAccess");
		elem.InnerText = bellyAccess;
		
		root.AppendChild (elem);
	}

	public void SaveXML(){
		_XMLDoc.Save("Assets/Resources/Episodes/" + _fileName + ".xml");
	}

	private void FormatXML(string xml){
		// Write code here to make XML formatted
	}

	#region Serialization
	public static void Serialize<T> (T obj, string path){
		var writer = new XmlSerializer (typeof(T));
		using (var file = new StreamWriter(path)){
			writer.Serialize(file, obj);
		}
	}

	public static T Deserialize<T>(string path){
		var reader = new XmlSerializer (typeof(T));
		using (var stream = new StreamReader(path)){
			return (T)reader.Deserialize(stream);
		}
	}

	public static void SerializeAppend<T>(T obj, string path){
		var writer = new XmlSerializer (typeof(T));
		FileStream file = File.Open (path, FileMode.Append, FileAccess.Write);
		writer.Serialize (file, obj);
		file.Close ();
	}
	#endregion

	#region Getters and Setters
	public string getFilePath(){
		return _filePath;
	}

	public void setFilePath(string filePath){
		if (filePath != null){
			_filePath = filePath;
		} else {
			Debug.LogError ("filePath is null");
		}
	}

	public string getFileName(){
		return _fileName;
	}

	public void setFileName(string fileName){
		if (fileName != null){
			_fileName = fileName;
		} else {
			Debug.LogError ("fileName is null");
		}
	}

	public TextAsset getXMLFile(){
		return _XMLFile;
	}

	public XmlDocument getXMLDoc(){
		return _XMLDoc;
	}

	public Vector3 getNPCPosition(string NPCname, int episodeNum){
		XmlElement elem;
		XmlNode root = _XMLDoc.GetElementById("_" + episodeNum.ToString());

		XmlNodeList eles = root.ChildNodes;
		foreach (XmlNode node in eles){
			//Debug.Log ("NODE::: " + node.InnerXml + " || " + node.InnerText + " || " + node.LocalName + "\n");
			Debug.Log ("attr: " + node.OuterXml);
			if (node.Attributes.ToString() == NPCname){
				Debug.Log ("Is this what I want? " + node.Name + " --- " + node);
			}
		}

		return Vector3.zero;
	}
	#endregion

	// Update is called once per frame
	void Update () {
	
	}
}
