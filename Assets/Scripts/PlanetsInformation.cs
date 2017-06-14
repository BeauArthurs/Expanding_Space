using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Planets {
    MERCURY,
    VENUS,
    EARTH,
    MARS,
    JUPITER,
    SATURN,
    URANUS,
    NEPTUNE
}

public class PlanetsInformation : MonoBehaviour
{
    public string currentInfo;
    public Sprite currentSprite;
    public Sprite[] sprites;
    public Image panelImage;

    private string mercuryInfo = "Naam: Mercurius \n" +
            "Type: Planeet, Rotsplaneet \n" +
            "Zwaartekracht: 0.37x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: 260°C \n" +
            "Beschrijving: Mercurius is de dichtst bij de zon staande en tevens kleinste planeet in ons zonnestelsel. De planeet is vernoemd naar de Romeinse god Mercurius vanwege de snelle draai om de zon. Net als de Aarde is het een terrestrische planeet met een vast oppervlak dat veel overeenkomsten vertoont met dat van de Maan. Opmerkelijk is dat deze kleine planeet een vrij sterk magnetisch veld vertoont. Mercurius heeft geen manen.";
    private string venusInfo = "Naam: Venus \n" +
            "Type: Planeet, Rotsplaneet \n" +
            "Zwaartekracht: 0.90x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: 480°C \n" +
            "Beschrijving: Venus is vanaf de zon gezien de tweede planeet van ons zonnestelsel. De planeet is vernoemd naar Venus, de Romeinse godin van de liefde. Vanaf Aarde gezien is Venus op de zon en de maan na het helderste object aan de hemel. Vanwege het feit dat Venus net als Mercurius een binnenplaneet is en daarom vanaf de aarde gezien altijd betrekkelijk dicht bij de zon staat, is Venus alleen zichtbaar gedurende een ½ à 4 uur na zonsondergang of vóór zonsopkomst. Daarom wordt Venus ook wel de avondster of morgenster genoemd.";
    private string earthInfo = "Naam: Aarde \n" +
            "Type: Planeet, Rotsplaneet \n" +
            "Zwaartekracht: 1.00x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: 15°C \n" +
            "Beschrijving: De Aarde (soms de wereld of Terra genoemd) is vanaf de Zon gerekend de derde planeet van ons zonnestelsel. Hierin behoort ze tot de naar haar genoemde 'aardse planeten', waarvan ze zowel qua massa als qua volume de grootste is. Op de Aarde komt leven voor: ze is de woonplaats van miljoenen soorten organismen. Of ze daarin alleen staat is onduidelijk, maar in de rest van het heelal zijn tot nog toe nergens sporen van leven, nu of in het verleden, gevonden.";
    private string marsInfo = "Naam: Mars \n" +
            "Type: Planeet, Rotsplaneet \n" +
            "Zwaartekracht: 0.38x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: -60°C \n" +
            "Beschrijving: Mars is vanaf de zon geteld de vierde planeet van ons zonnestelsel, om de zon draaiend in een baan tussen die van de Aarde en die van Jupiter. De planeet is kleiner dan de Aarde en met een (maximale) magnitude van -2,9 minder helder dan Venus en meestal minder helder dan Jupiter. Mars wordt wel de rode planeet genoemd maar is in werkelijkheid eerder okerkleurig. De planeet is vernoemd naar de Romeinse god van de oorlog. Mars is gemakkelijk met het blote oog te bespeuren, vooral in de maanden rond een oppositie. 's Nachts is Mars dan te zien als een heldere roodachtige 'ster' die evenwel door haar relatieve nabijheid geen puntbron is maar een schijfje.";
    private string jupiterInfo = "Naam: Jupiter \n" +
            "Type: Planeet, Gasreus \n" +
            "Zwaartekracht: 2.65x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: -150°C \n" +
            "Beschrijving: Jupiter is vanaf de zon gezien de vijfde en tevens grootste planeet van ons zonnestelsel. Net als Saturnus is Jupiter een gasreus, hij beschikt dus niet over een vast oppervlak. Zoals de aardse planeten terrestrische planeten genoemd worden, worden gasreuzen ook wel Joviaanse planeten genoemd. Deze naam is afkomstig van het Latijnse Iovis, de genitief van het woord Jupiter. Joviaans betekent vrij vertaald Jupiterachtig of van Jupiter. De planeet is vernoemd naar de Romeinse oppergod Jupiter.";
    private string saturnInfo = "Naam: Saturnus \n" +
            "Type: Planeet, Gasreus \n" +
            "Zwaartekracht: 1.13x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: -170°C \n" +
            "Beschrijving: Saturnus is van de zon af gerekend de zesde planeet in ons zonnestelsel en op Jupiter na de grootste. Beide zijn gasreuzen en zogenaamde 'buitenplaneten'. Saturnus is vernoemd naar de Romeinse god van de landbouw, Saturnus. Saturnus is al sinds de prehistorie bekend.";
    private string uranusInfo = "Naam: Uranus \n" +
            "Type: Planeet, Ijsreus \n" +
            "Zwaartekracht: 1.09x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: -200°C \n" +
            "Beschrijving: Uranus is de op twee na grootste en vanaf de Zon gezien de zevende planeet van ons zonnestelsel. Deze ijsreus is vernoemd naar de god Uranus, ook wel Ouranos, de personificatie van de hemel, uit de Griekse mythologie.";
    private string neptuneInfo = "Naam: Neptunus \n" +
            "Type: Planeet, Ijsreus \n" +
            "Zwaartekracht: 1.43x aarde \n" +
            "Diameter: 143km \n" +
            "Oppervlaktetemperatuur: -210°C \n" +
            "Beschrijving: Neptunus is vanaf de Zon gezien de achtste en verst van de Zon verwijderde planeet van ons zonnestelsel. De planeet is vernoemd naar de Romeinse god van de zee.";

    private void Start()
    {
        SetInfo(2);
    }

    public void SetInfo(int planet)
    {
        switch (planet)
        {
            case 0:
                currentSprite = sprites[0];
                currentInfo = mercuryInfo;
                break;
            case 1:
                currentSprite = sprites[1];
                currentInfo = venusInfo;
                break;
            case 2:
                currentSprite = sprites[2];
                currentInfo = earthInfo;
                break;
            case 3:
                currentSprite = sprites[3];
                currentInfo = marsInfo;
                break;
            case 4:
                currentSprite = sprites[4];
                currentInfo = jupiterInfo;
                break;
            case 5:
                currentSprite = sprites[5];
                currentInfo = saturnInfo;
                break;
            case 6:
                currentSprite = sprites[6];
                currentInfo = uranusInfo;
                break;
            case 7:
                currentSprite = sprites[7];
                currentInfo = neptuneInfo;
                break;
            default:
                currentInfo = "No planet selected.";
                break;
        }
        panelImage.sprite = currentSprite;
    }
}