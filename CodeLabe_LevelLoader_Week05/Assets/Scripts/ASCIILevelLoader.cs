using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class ASCIILevelLoader : MonoBehaviour
{
    //you can make it a singleton
    public int currentLevel = 0;
    //look at matt's script to finalize

    public int CurrnetLevel
    {
        get
        {
            return currentLevel;
            //anytime someone changes the level, we load the next level
            LoadLevel();
        }
        set
        {
            currentLevel = value;
        }
    }

    string FILE_PATH;

    public static ASCIILevelLoader instance;

    void Start()
    {
        instance = this;
        FILE_PATH = Application.dataPath + "/Levels/LevelNum.txt";
        LoadLevel();
    }
    
    void LoadLevel()
    {
        GameObject level = new GameObject("Level Objects");
        //reading the first line in the file as an array, 0 is line 1
        string[] lines = File.ReadAllLines(FILE_PATH.Replace("Num", currentLevel + ""));

        for (int y = 0; y < lines.Length; y++)
        {
            Debug.Log(lines[y]);

            //line is equal to the first line in the array 0
            string line = lines[y].ToUpper();

            //turns one line into many individual characters as ab array
            char[] characters = line.ToCharArray();

            for (int x = 0; x < characters.Length; x++)
            {
                //defining the first character on the array
                char c = characters[x];

                Debug.Log(c);

                GameObject newObject = null;

                switch (c)
                {
                    case 'W': //if the character is a 'w'
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Wall"));
                        break;
                    case 'P'://if the character is a 'p'
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
                        Camera.main.transform.parent = newObject.transform;
                        Camera.main.transform.position = new Vector3(0, 0, -10);
                        break;
                    case 'S':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("PreFabs/Spike"));
                        break;
                    case 'E':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("PreFabs/Goal"));
                        break;
                    default:
                        break;
                        
                }
                

                /*if (c == 'W')
                {
                    //add a wall to our scene
                    //resources.Load loads objects from the resources folder. This is the reference, but it hasn't been established until you add instantiate
                    //Instantiate alone allows you to make primitive types. Resources. Load can be anything in that folder
                    newObject =
                        Instantiate(Resources.Load<GameObject>("Prefabs/Wall"));
                        //newObject.transform.position = new Vector3(x, -y, 0);
                }
                
                if (c == 'P')
                {
                    //add a wall to our scene
                    //resources.Load loads objects from the resources folder. This is the reference, but it hasn't been established until you add instantiate
                    //Instantiate alone allows you to make primitive types. Resources. Load can be anything in that folder
                    newObject =
                        Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
                    Camera.main.transform.parent = newObject.transform;
                    Camera.main.transform.position = new Vector3(0, 0, -10);
                }*/
                
                if (newObject != null)
                {
                    newObject.transform.parent = level.transform;
                    //give it a position based on where it was in the ASCII file
                    newObject.transform.position = new Vector2(x, -y);
                }
            }
        }
    }
}
