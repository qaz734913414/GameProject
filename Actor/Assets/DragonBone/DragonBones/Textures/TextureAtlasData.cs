// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace DragonBones
{
	public class TextureAtlasData
	{

		public bool autoSearch;

		public string name;
		public string imagePath;
		
		public List<TextureData> textureDataList = new List<TextureData>();

		public TextureAtlasData ()
		{
			autoSearch = false;
		}


		public void dispose()
		{
			for (int i = 0; i < textureDataList.Count; ++i)
			{
				textureDataList[i].dispose();
				//delete textureDataList[i];
			}
			
			textureDataList.Clear();
		}
		
		public TextureData getTextureData(string textureName) 
		{
			for (int i = 0; i < textureDataList.Count; ++i)
			{
				if (textureDataList[i].name == textureName)
				{
					return textureDataList[i];
				}
			}
			
			return null;
		}

	}
}

