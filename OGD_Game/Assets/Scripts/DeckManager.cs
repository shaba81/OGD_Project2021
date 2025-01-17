using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public List<RosterCard> unitsCards;
    public List<RosterCard> buildingsCards;
    public List<UICard> allCards;
    public List<GameObject> unitsCursors;
    public List<GameObject> buildingsCursors;

    public EntitiesDatabaseSO cardsDb;
    public EntitiesDatabaseSO unitsDb;
    public EntitiesDatabaseSO buildingsDb;

    private List<string> unitsNames;
    private List<string> buildingsNames;


    public GameObject unitsObject;
    public GameObject buildingsObject;

    public Color buttonSelectedColor;
    public Color buttonUnselectedColor;

    public Image units_button;
    public Image buildings_button;

    public GameObject unitstext;
    public GameObject buildingstext;
    public GameObject cardseffect;
    private bool effectvisible=false;

    private 

    int index = 0;
    int buildingsIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        buildingsIndex = 0;
        //Prendi i nomi da playerPrefs e salvali nelle rispettive liste
        unitsNames = PlayerPrefsX.GetStringArray("Units").ToList<string>();
        buildingsNames = PlayerPrefsX.GetStringArray("Buildings").ToList<string>();
        LoadCards();
    }

    private void LoadCards()
    {

        for (int i = 0; i < allCards.Count; i++)
        {
            if (!allCards[i].gameObject.activeSelf)
                allCards[i].gameObject.SetActive(true);

            allCards[i].AltSetup(cardsDb.allEntities[i], this);
        }

        
        for (int i = 0; i < unitsCards.Count; i++)
        {
            if (!unitsCards[i].gameObject.activeSelf)
                unitsCards[i].gameObject.SetActive(true);

            if(unitsNames == null)
            {
                unitsCards[i].Setup(unitsDb.allEntities[i]);
            }
            else
            {
                //Prendi PlayerPrefsX.GetArray(i) che � il nome, prendi dal db totale i dati con quel nome e salvalo in data
                foreach (EntitiesDatabaseSO.EntityData _data in cardsDb.allEntities)
                {
                    if (_data.name.Equals(unitsNames[i]))
                    {
                        unitsCards[i].Setup(_data);
                    }
                }
            }

            
        }

        for (int i = 0; i < buildingsCards.Count; i++)
        {
            if (!buildingsCards[i].gameObject.activeSelf)
                buildingsCards[i].gameObject.SetActive(true);

            if (unitsNames == null)
            {
                buildingsCards[i].Setup(buildingsDb.allEntities[i]);
            }
            else
            {
                //Prendi il nome, prendi dal db totale i dati con quel nome e salvalo in data
                foreach (EntitiesDatabaseSO.EntityData _data in cardsDb.allEntities)
                {
                    if (_data.name.Equals(buildingsNames[i]))
                    {
                        buildingsCards[i].Setup(_data);
                    }
                }
            }
           
        }

        unitsNames.Clear();
    }

    public void AddCard(UICard card, EntitiesDatabaseSO.EntityData myData)
    {
        if(!myData.isBuilding)
        {
            if(index < 5)
            {
                unitsCursors[index].SetActive(false);
                unitsDb.allEntities[index] = myData;
                //SAVE HERE
                unitsNames.Add(myData.name);
                unitsCards[index].gameObject.SetActive(true);
                unitsCards[index].Setup(unitsDb.allEntities[index]);

                index++;
                if(index != 5)
                {
                    unitsCursors[index].SetActive(true);
                } 
            }
        } 
        else if (myData.isBuilding)
        {
            Debug.Log("Building clicked");
            if (buildingsIndex < 3)
            {
                buildingsCursors[buildingsIndex].SetActive(false);
                buildingsDb.allEntities[buildingsIndex] = myData;
                //SAVE HERE
                buildingsNames.Add(myData.name);
                buildingsCards[buildingsIndex].gameObject.SetActive(true);
                buildingsCards[buildingsIndex].Setup(buildingsDb.allEntities[buildingsIndex]);

                buildingsIndex++;
                if (buildingsIndex != 3)
                {
                    buildingsCursors[buildingsIndex].SetActive(true);
                }
            }
        }
    }

    public void RemoveCard()
    {

    }

    public void SaveDeck()
    {
        //save data
        PlayerPrefsX.SetStringArray("Units", unitsNames.ToArray());
        PlayerPrefsX.SetStringArray("Buildings", buildingsNames.ToArray());
    }

    public void ResetDeck()
    {
        if (index != 5)
        {
            unitsCursors[index].SetActive(false);  
        }
        index = 0;
        unitsCursors[index].SetActive(true);

        if (buildingsIndex != 4)
        {
            buildingsCursors[index].SetActive(false);
        }
        buildingsIndex = 0;
        buildingsCursors[index].SetActive(true);


        //in deck database initialize the default deck.
    }

    //Used to switch from units and buildings.
    public void SwitchToBuildingsDeck()
    {
        units_button.color = buttonUnselectedColor;
            unitsObject.SetActive(false);
            unitstext.SetActive(false);
        buildings_button.color = buttonSelectedColor;
            buildingsObject.SetActive(true);
            buildingstext.SetActive(true);
    }

    public void SwitchToUnitsDeck()
    {
        buildings_button.color = buttonUnselectedColor;
            buildingsObject.SetActive(false);
            buildingstext.SetActive(false);
        units_button.color = buttonSelectedColor;
            unitsObject.SetActive(true);
            unitstext.SetActive(true);
    }

    public void CardsEffect()
    {
        if(effectvisible)
        {
            effectvisible=false;
            cardseffect.SetActive(false);
        }else{
            effectvisible=true;
            cardseffect.SetActive(true);
        }
    }

}
