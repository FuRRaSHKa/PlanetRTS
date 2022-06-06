using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float decisionInteval;

    private List<PlanetFacade> planetFacades;

    private void Start()
    {
        EventManager.OnStartGame += () => StartCoroutine(DecisionUpdate());
        EventManager.OnEndGame += (value) => StopAllCoroutines();
    }

    public void SetPlanets(List<PlanetFacade> planetFacades)
    {
        this.planetFacades = planetFacades;
    }

    private IEnumerator DecisionUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(decisionInteval);
            MakeDecision();
        }
    }

    private void MakeDecision()
    {
        PlanetFacade[] planetFacade = planetFacades.Where(f => f.IsHaveEnemy()).ToArray();
        if (planetFacade.Length == 0)
        {
            return;
        }
        int targetId = Random.Range(0, planetFacade.Length);
        PlanetFacade preTarget = planetFacades[Random.Range(0, planetFacades.Count)];
        PlanetFacade target = planetFacades.DefaultIfEmpty(null).FirstOrDefault(f => f.IsEnemyMoreThanPlayer(planetFacade[targetId].EnemyCount) && !f.IsHaveEnemy());
        if (target == null)
        {
            target = preTarget;
        }

        ShipHandler.Instance.SendEnemyShips(planetFacade[targetId].PlanetId, target, Random.Range(0.3f, 0.9f));
    }
}
