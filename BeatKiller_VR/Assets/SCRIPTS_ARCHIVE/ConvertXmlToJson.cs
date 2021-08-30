using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ConvertXmlToJson : MonoBehaviour
{
    public string nameFile;
    
    public InstantiateQuads IQ;
    private void Start()
    {
        readXML();
        
    }

    public void readXML()
    {
        int indexMark;
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("D://UnityProjects//Work//BeatKiller_VR//Assets//Sounds//" + nameFile + ".xml".ToString());
        // получим корневой элемент
        XmlElement xRoot = xDoc.DocumentElement;
        xmlModel model = new xmlModel { };
        // обход всех узлов в корневом элементе
        foreach (XmlNode xnode in xRoot)
        {
            foreach (XmlNode childnode in xnode.ChildNodes)
            {
                if (childnode.Name == "TRACK")
                {
                    model.Name = addNodeSTR("Name", childnode);
                    model.Artist= addNodeSTR("Artist", childnode);
                    model.totalTime =addNodeINT("TotalTime", childnode);
                }

                foreach (XmlNode underChildNode in childnode.ChildNodes)
                {
                    if (underChildNode.Name == "TEMPO")
                    {
                        model.Inizio = addNodeFLOAT("Inizio", underChildNode);
                        model.Bpm = addNodeFLOAT("Bpm", underChildNode);
                        model.Batito = addNodeINT("Battito", underChildNode);
                    }
                    if (underChildNode.Name == "POSITION_MARK")
                    {
                        position_Mark mark = new position_Mark();
                         mark.Name = addNodeSTR("Name", underChildNode);
                        mark.Type = addNodeINT("Type", underChildNode);
                        mark.Start = addNodeFLOAT("Start",  underChildNode);
                        mark.Num = addNodeINT("Num", underChildNode);
                        mark.Red =  addNodeINT("Red", underChildNode);
                        mark.Green = addNodeINT("Green", underChildNode);
                        mark.Blue = addNodeINT("Blue", underChildNode);
                        indexMark = mark.index;
                        model.marks.Add(mark);
                        indexMark++;

                    }
                }
                
            }
        }
        Debug.Log(model.Bpm + " IQ.CreateLVL(model.Bpm)");
        IQ.CreateLVL(model.Bpm);
        
    }
    private string addNodeSTR(string name, XmlNode node)
    { 
        
        XmlNode myNode = node.Attributes.GetNamedItem(name);
        Debug.Log(myNode.Name + " = " + myNode.InnerText);
        return (myNode.InnerText);
       
    }
    private float addNodeFLOAT(string name, XmlNode node)
    {
        XmlNode myNode = node.Attributes.GetNamedItem(name);
        Debug.Log(myNode.Name + " = " + myNode.InnerText);
        return (float.Parse(myNode.InnerText, CultureInfo.InvariantCulture.NumberFormat));
    }
    private int addNodeINT(string name, XmlNode node)
    {
        XmlNode myNode = node.Attributes.GetNamedItem(name);
        Debug.Log(myNode.Name + " = " + myNode.InnerText);
        return(int.Parse(myNode.InnerText));
    }
}

public class xmlModel
{
    public string Name;
    public string Artist;
    public int totalTime;
    public float Inizio;
    public float Bpm;
    public int Batito;
    public List<position_Mark> marks = new List<position_Mark>();
}

public struct position_Mark
{
    public string Name;
    public int Type;
    public float Start;
    public int Num;
    public int Red;
    public int Green;
    public int Blue;
    public int index;
}
