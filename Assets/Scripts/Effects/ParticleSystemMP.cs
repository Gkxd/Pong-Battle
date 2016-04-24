using UnityEngine;
using System.Collections;

public class ParticleSystemMP : MonoBehaviour {

    public ParticleSystem particleSystemA;
    public ParticleSystem particleSystemB;
    public int player;

    private ParticleSystem.EmissionModule emissionA;
    private ParticleSystem.EmissionModule emissionB;

    void Start() {
        emissionA = particleSystemA.emission;
        emissionB = particleSystemB.emission;
    }

    void Update() {
        if (player == 0) {
            emissionA.enabled = GameState.Player1Mp >= 20;
            emissionB.enabled = GameState.Player1Mp >= 200;
        }
        else if (player == 1) {
            emissionA.enabled = GameState.Player2Mp >= 20;
            emissionB.enabled = GameState.Player2Mp >= 200;
        }
    }
}
