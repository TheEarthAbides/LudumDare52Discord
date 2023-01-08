

using UnityEngine;

using UnityEditor;

using System.IO;

public class HandleTextFile

{

    [MenuItem("Tools/Write file")]

    static void WriteString()

    {

        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        
        for(int i = 0; i < SpawnManager.instance.bassKeyFrames.Count; i++)
        {
            writer.WriteLine(SpawnManager.instance.bassKeyFrames[i].ToString());
        }

        writer.Close();

        //Re-import the file to update the reference in the editor

        AssetDatabase.ImportAsset(path);

        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file

        Debug.Log(asset.text);

    }

    [MenuItem("Tools/Read file")]

    static void ReadString()

    {

        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file

        for(int i = 0; i < SpawnManager.instance.Level1.Length; i++)
        {
            SpawnManager.instance.Level1[i].Pig = false;
            SpawnManager.instance.Level1[i].Rat = false;
            SpawnManager.instance.Level1[i].Raccoon = false;
            SpawnManager.instance.Level1[i].Worm = false;
        }

        StreamReader reader = new StreamReader(path);

        while(!reader.EndOfStream)
        {
            
            SpawnManager.instance.Level1[int.Parse(reader.ReadLine())].Rat = true;
            
        }
        reader.Close();

    }

}

