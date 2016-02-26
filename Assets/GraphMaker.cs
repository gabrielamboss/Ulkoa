using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GraphMaker : MonoBehaviour {

    private static bool hasInitialized = false;
    private Deck currDeck;
    private List<Match> matchList;

    public GameObject contentParent;
    public GameObject textPrefab;
    public GameObject indexPrefab;
    public Text deckName;

    private Color lightBlue = new Color(0.2431f, 0.6f, 0.898f);

    // Use this for initialization
    void Start () {
        StartCoroutine(InitializeScene());
        //deckName.text = currDeck.DeckName;
        DrawAxis();
        DrawMarks();
        DrawArrows();
        DrawGrid();
        DrawGraphic();
    }

    private void DrawGrid()
    {
        //Horizontal ones
        for(float x = 1; x > -3f; x -= 0.8f)
        {
            makeGrid(new Vector3(-5f, x, 0), new Vector3(5.5f, x, 0));
        }

        //Vertical ones
        for(float x = -5f; x <= 5f; x += 0.5f)
        {
            makeGrid(new Vector3(x, -3f, 0), new Vector3(x, 1.5f, 0));
        }
    }

    private void makeGrid(Vector3 begin, Vector3 end)
    {
        LineRenderer line = new GameObject().AddComponent<LineRenderer>();
        line.transform.parent = this.transform.parent;
        line.SetWidth(0.01F, 0.01F);
        line.SetColors(lightBlue, lightBlue);
        line.SetPosition(0, begin);
        line.SetPosition(1, end);
        line.SetVertexCount(2);
        line.material = new Material(Shader.Find("Particles/Additive"));
    }

    void DrawGraphic()
    {

        if (matchList.Count <= 20)
        {
            GameObject lastIndex = Instantiate(indexPrefab, new Vector3(-233 + 26 * (matchList.Count - 1), -238, 0), Quaternion.identity) as GameObject;
            lastIndex.transform.SetParent(this.transform, false);
            lastIndex.GetComponent<Text>().text = (matchList[matchList.Count - 1].MatchNumber + 1).ToString();

            for (int x = 0; x < matchList.Count - 1; x++)
            {
                decimal taxadeacertos1 = Decimal.Divide(matchList[x].CorrectPoints, matchList[x].CorrectPoints + matchList[x].WrongPoints);
                float AxisY1 = ((float)taxadeacertos1 * 4f) - 3;
                float AxisX1 = matchList[x].MatchNumber * 0.5f - 4.5f;
                decimal taxadeacertos2 = Decimal.Divide(matchList[x + 1].CorrectPoints, matchList[x + 1].CorrectPoints + matchList[x + 1].WrongPoints);
                float AxisY2 = ((float)taxadeacertos2 * 4f) - 3;
                float AxisX2 = matchList[x + 1].MatchNumber * 0.5f - 4.5f;
                makeLineGraph(new Vector3(AxisX1, AxisY1, 0), new Vector3(AxisX2, AxisY2, 0));
                GameObject index = Instantiate(indexPrefab, new Vector3(-233 + 26 * x, -238, 0), Quaternion.identity) as GameObject;
                index.transform.SetParent(this.transform, false);
                index.GetComponent<Text>().text = (matchList[x].MatchNumber + 1).ToString();

            }
        }

        else
        {
            int inicio = matchList.Count - 20;
            GameObject lastIndex = Instantiate(indexPrefab, new Vector3(-233 + 26 * (matchList.Count - 1 - inicio), -238, 0), Quaternion.identity) as GameObject;
            lastIndex.transform.SetParent(this.transform, false);
            lastIndex.GetComponent<Text>().text = (matchList[matchList.Count - 1].MatchNumber + 1).ToString();

            for (int x = inicio; x < matchList.Count - 1; x++)
            {
                decimal taxadeacertos1 = Decimal.Divide(matchList[x].CorrectPoints, matchList[x].CorrectPoints + matchList[x].WrongPoints);
                float AxisY1 = ((float)taxadeacertos1 * 4f) - 3;
                float AxisX1 = (matchList[x].MatchNumber - inicio) * 0.5f - 4.5f;
                decimal taxadeacertos2 = Decimal.Divide(matchList[x + 1].CorrectPoints, matchList[x + 1].CorrectPoints + matchList[x + 1].WrongPoints);
                float AxisY2 = ((float)taxadeacertos2 * 4f) - 3;
                float AxisX2 = (matchList[x + 1].MatchNumber - inicio) * 0.5f - 4.5f;
                makeLineGraph(new Vector3(AxisX1, AxisY1, 0), new Vector3(AxisX2, AxisY2, 0));
                GameObject index = Instantiate(indexPrefab, new Vector3(-233 + 26 * (x - inicio), -238, 0), Quaternion.identity) as GameObject;
                index.transform.SetParent(this.transform, false);
                index.GetComponent<Text>().text = (matchList[x].MatchNumber + 1).ToString();

            }
        }
    }

    private void makeLineGraph(Vector3 begin, Vector3 end)
    {
        LineRenderer line = new GameObject().AddComponent<LineRenderer>();
        line.transform.parent = this.transform.parent;
        line.SetWidth(0.05F, 0.05F);
        line.SetColors(Color.green, Color.green);
        line.SetPosition(0, begin);
        line.SetPosition(1, end);
        line.SetVertexCount(2);
        line.material = new Material(Shader.Find("Particles/Additive"));
    }

    void DrawAxis()
    {
        makeLine(new Vector3(-5, -3, 0), new Vector3(-5, 1.8f, 0));
        makeLine(new Vector3(-5, -3, 0), new Vector3(6, -3, 0));
    }

    void DrawMarks()
    {
        //Y marks
        for(float x = -2.2f; x <= 1f; x += 0.8f)
        {
            makeLine(new Vector3(-4.9f, x, 0), new Vector3(-5.1f, x, 0));
        }

        //X marks
        for (float x = -4.5f; x <= 5f; x += 0.5f)
        {
            makeLine(new Vector3(x, -3.1f, 0), new Vector3(x, -2.9f, 0));
        }
    }

    void DrawArrows()
    {
        makeLine(new Vector3(-5, 1.8f, 0), new Vector3(-4.9f, 1.6f,0));
        makeLine(new Vector3(-5, 1.8f, 0), new Vector3(-5.1f, 1.6f, 0));
        makeLine(new Vector3(6, -3, 0), new Vector3(5.8f, -3.1f, 0));
        makeLine(new Vector3(6, -3, 0), new Vector3(5.8f, -2.9f, 0));
    }
    void makeLine(Vector3 begin, Vector3 end)
    {
        LineRenderer line = new GameObject().AddComponent<LineRenderer>();
        line.transform.parent = this.transform.parent;
        line.SetWidth(0.025F, 0.025F);
        line.SetColors(lightBlue, lightBlue);
        line.SetPosition(0, begin);
        line.SetPosition(1, end);
        line.SetVertexCount(2);
        line.material = new Material(Shader.Find("Particles/Additive"));
    }
    

    public IEnumerator Initialize()
    {
        currDeck = GlobalVariables.GetSelectedDeck();
        MatchDao matchDao = new MatchDao();
        matchList = matchDao.getMatchsByDeck(currDeck);



        foreach (Match match in matchList)
        {
            GameObject newMatchNumber = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newMatchNumber.transform.SetParent(contentParent.transform, false);
            GameObject newMatchCorrect = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newMatchCorrect.transform.SetParent(contentParent.transform, false);
            GameObject newMatchWrong = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newMatchWrong.transform.SetParent(contentParent.transform, false);
            //newDeckID.text = currDeck.ObjectId; Deck nao possui mais campo Id
            newMatchNumber.GetComponent<Text>().text = (match.MatchNumber + 1).ToString();
            newMatchCorrect.GetComponent<Text>().text = match.CorrectPoints.ToString();
            newMatchWrong.GetComponent<Text>().text = match.WrongPoints.ToString();
        }

        hasInitialized = true;

        return null;
    }

    public static bool HasInitialized()
    {
        return hasInitialized;
    }

    private IEnumerator InitializeScene()
    {
        yield return Initialize();

        while (!HasInitialized())
        { yield return null; }

    }


// Update is called once per frame
void Update () {
	
	}
}
