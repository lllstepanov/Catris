using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;

namespace Catris 
{
	/// <summary>
	/// Handles operation on Cat.xml 
	/// </summary>
	internal class CatParser
	{
		/// <summary>
		/// Collection of cat information
		/// </summary>
		List<Cat> cats = new List<Cat>();
		
		/// <summary>
		/// Xml variable
		/// </summary>
		XmlDocument xml;

		/// <summary>
		/// Gets information about the cats from xml
		/// </summary>
		internal List<Cat> ParseCats()
		{
			/// Folder and name of the file
			string path = "Data/Cats";

			/// Text asset with loaded from resources file
			TextAsset catXML = (TextAsset)Resources.Load(path, typeof(TextAsset));

			/// New xml object creation 
			xml = new XmlDocument();

			/// Load text asset to xml object
			xml.LoadXml(catXML.text);

			/// Start first mode loop (cats)
			foreach (XmlNode xmlCats in xml.ChildNodes)
			{
				/// Check if node name of equel to "cats"
				if (xmlCats.Name == "cats")
				{
					/// Start second mode loop (cat)
					foreach (XmlNode xmlItem in xmlCats.ChildNodes)
					{
						/// Check if node name of equel to "cat"
						if (xmlItem.Name == "cat")
						{
							/// Assign number variable with variable from xmk
							byte number = byte.Parse ( xmlItem.Attributes["number"].Value );

							/// New null color varialbe
							Color color;

							/// Assign color variable with variable from xmk
							ColorUtility.TryParseHtmlString(xmlItem.Attributes["color"].Value, out color);

							/// Construct the cat
							Cat receivedItem = new Cat(number, color);

							/// Add new cat into the collection
							cats.Add(receivedItem);
						}
					}
				}
			}

			/// Return collection of the cats
			return cats;
		}
	}

	/// <summary>
	/// Cat class contains information about cat
	/// </summary>
	[Serializable]
	internal class Cat
	{
		/// <summary>
		/// Number of the cat
		/// </summary>
		internal byte number { get; set; }

		/// <summary>
		/// Color of the cat
		/// </summary>
		internal Color color { get; set; }

		/// <summary>
		/// Constuctor
		/// </summary>
		internal Cat(byte number, Color color)
		{
			/// Assign number variable
			this.number = number;

			/// Assign color variable
			this.color = color;
		}
	}
}
