/*
Copyright 2019 , Sergio Bromberg
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

// To be placed in Assets/Editor folder

using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Callbacks;
using System.IO;

public class CopyOpenVR {
	[PostProcessBuildAttribute(1)]
	
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
		
		string buildDirectory = Path.GetDirectoryName(pathToBuiltProject);
		
		string dataDirectory = Path.Combine(buildDirectory, Path.GetFileNameWithoutExtension(pathToBuiltProject) + "_Data");
		
        string pluginDirectory = Path.Combine(dataDirectory, "Plugins");

        if (!Directory.Exists(pluginDirectory))
        {
            Directory.CreateDirectory(pluginDirectory);
            Debug.Log("Created plugins directory " + pluginDirectory);
        }


		//Change this to the location of the file in your file system
		//File.Copy(@"C:\Program Files\Unity2018.2.11\Editor\Data\UnityExtensions\Unity\VR\Win64\openvr_api.dll", Path.Combine(dataDirectory, "Plugins/openvr_api.dll"));
        //File.Copy(@"D:\Program Files\Unity2018.2\Editor\Data\UnityExtensions\Unity\VR\Win64\openvr_api.dll", Path.Combine(dataDirectory, "Plugins/openvr_api.dll"));
        File.Copy(@"D:\Program Files\Unity2018.2\Editor\Data\UnityExtensions\Unity\VR\Win64\openvr_api.dll", Path.Combine(pluginDirectory,"openvr_api.dll"));

        //TODO: If "Plugins" folder does  not exist, create it. 
	}
}