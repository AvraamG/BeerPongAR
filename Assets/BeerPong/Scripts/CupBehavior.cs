using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CupBehavior : Interactable
{

    AudioSource hitSound;

    GameObject cupSleeve;
    GameObject cupBase;


    MeshRenderer sleeveMeshRenderer;

    float destructionTime;

    #region Distance Indicator
    Color letterDistanceGreen = new Color(0.48f, 0.98f, 0.73f);
    Color letterDistanceYellow = new Color(1f, 0.90f, 0.26f);
    Color letterDistanceOrange = new Color(1f, 0.90f, 0f);

    GameObject distanceIndicatorObject;
    TextMesh distanceTextMesh;
    #endregion

    #region Particles
    ParticleSystem hit;
    ParticleSystem star;
    ParticleSystem halo;

    ParticleSystem.MainModule hitParticleMain;
    ParticleSystem.MainModule startParticleMain;
    ParticleSystem.MainModule haloParticle;
    #endregion

    #region Behavior Specifics

    public enum CupType
    {
        Red,
        Green,
        Gold,
        Silver,
        Black
    }

    public CupType myCupType;


    //TODO instead of the level Manager bring a base points worth on the item, so it can scale on multiple items.

    Color customRed = new Color(1, 0, 0);
    Color customGreen = new Color(0.231f, 0.96f, 0.26f);
    Color customGold = new Color(0.962f, 0.7f, 0.23f);
    Color customSilver = new Color(0.5f, 0.5f, 0.5f);
    Color customBlack = new Color(0, 0, 0);

    #endregion

    private void OnEnable()
    {

        cupSleeve = this.transform.Find("CupSleeve").gameObject;
        sleeveMeshRenderer = cupSleeve.GetComponent<MeshRenderer>();

        hitSound = this.GetComponent<AudioSource>();

        hit = this.transform.Find("BalloonParticlePlasticPop").gameObject.GetComponent<ParticleSystem>();
        hitParticleMain = hit.main;

        star = this.transform.Find("Stars").gameObject.GetComponent<ParticleSystem>();
        startParticleMain = star.main;

        halo = this.transform.Find("Halo").gameObject.GetComponent<ParticleSystem>();
        haloParticle = halo.main;

        //TODO Replace this with proper instantiation
        int randomIndex = UnityEngine.Random.Range(0, 4);

        InitializeCup((CupType)randomIndex);
        CalculateDestructionTime();

    }





    /// <summary>
    /// Initializes different balloon types with different rewards and visualizations.
    /// </summary>
    /// <param name="type">Type of Balloon</param>
    public void InitializeCup(CupType type)
    {
        myCupType = type;
        AssignPoints();

    }

    /// <summary>
    /// Assign the points this item is worth.
    /// </summary>
    void AssignPoints()
    {
        switch (myCupType)
        {
            case CupType.Red:
                pointsWorth = 10;
                sleeveMeshRenderer.material.color = customRed;
                hitParticleMain.startColor = customRed;
                break;
            case CupType.Green:
                pointsWorth = 20;
                sleeveMeshRenderer.material.color = customGreen;
                hitParticleMain.startColor = customGreen;
                break;
            case CupType.Gold:
                pointsWorth = 100;
                sleeveMeshRenderer.material.color = customGold;
                hitParticleMain.startColor = customGold;
                break;
            case CupType.Silver:

                pointsWorth = 50;
                sleeveMeshRenderer.material.color = customSilver;
                hitParticleMain.startColor = customSilver;

                break;
            case CupType.Black:

                pointsWorth = 1;
                sleeveMeshRenderer.material.color = customBlack;
                hitParticleMain.startColor = customBlack;

                break;
            default:
                break;
        }
    }
    /// <summary>
    /// This method takes into account all the time needed for the audiovisuals to happen in order for the item to be destroyed.
    /// In the past it should be used as a reference to the object pool.
    /// </summary>
    void CalculateDestructionTime()
    {

        List<float> alltimes = new List<float> { hitSound.clip.length, hitParticleMain.duration, startParticleMain.duration, haloParticle.duration };
        destructionTime = alltimes.Max();
    }



    private void Update()
    {
        UpdateDistanceFromPlayer();
    }

    /// <summary>
    /// Visually updates the distance from the camera, in this case the main camera.
    /// </summary>
    private void UpdateDistanceFromPlayer()
    {
        float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        distanceTextMesh.text = Math.Abs(distance).ToString("F1");

        if (Math.Abs(distance) < 1)
        {
            distanceTextMesh.color = letterDistanceGreen;

        }
        else if (Math.Abs(distance) > 1 && Math.Abs(distance) < 4)
        {
            distanceTextMesh.color = letterDistanceYellow;

        }
        else
        {
            distanceTextMesh.color = letterDistanceOrange;
        }
    }




    override public void InteractWithItem(InteractionItem interactionItem)
    {
        if (OnItemInteractedWith != null)
        {
            OnItemInteractedWith(this);
        }

        //TODO Check which Item can actually pop the balloon :D 
        PopBalloon();
    }

    /// <summary>
    /// Aside the general Item interaction, this is the specific reaction that happens when the balloon Interacts With something. 
    /// </summary>
    void PopBalloon()
    {
        cupSleeve.SetActive(false);

        //TODO I need this to fall off.
        distanceIndicatorObject.SetActive(false);

        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }

        if (!hit.isPlaying)
        {
            hit.Play();
        }
        if (!halo.isPlaying)
        {
            halo.Play();
        }
        if (!star.isPlaying)
        {
            star.Play();
        }


        Destroy(this.gameObject, destructionTime);
    }
}
