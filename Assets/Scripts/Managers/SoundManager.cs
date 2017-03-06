using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] SpawnMoto;
    public AudioClip[] SpawnVoiture;
    public AudioClip[] SpawnTank;

    public AudioClip[] EnemyHit;
    public AudioClip[] Shoot;

    public AudioClip TirMoto;
    public AudioClip TirVoiture;
    public AudioClip TirTank;

    public AudioClip BipSemtex;
    public AudioClip[] Explosion;
    public AudioClip Homing;
    public AudioClip Split;
    public AudioClip Pierce;

    public AudioClip Accept;
    public AudioClip Cancel;

    public AudioClip PickLife;
    public AudioClip LoseLife;

    public AudioClip[] PickMoney;

    public AudioClip Death;

    public AudioClip[] PeaceMoto;
    public AudioClip[] PeaceVoiture;
    public AudioClip[] PeaceTank;

    private AudioSource Source;

    public static SoundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Source = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip shot)
    {
        Source.PlayOneShot(shot);
    }

    public void PlaySpawnMoto() {
        Source.PlayOneShot(SpawnMoto[Random.Range(0, SpawnMoto.Length)]);
    }

    public void PlaySpawnVoiture()
    {
        Source.PlayOneShot(SpawnVoiture[Random.Range(0, SpawnVoiture.Length)]);
    }

    public void PlaySpawnTank()
    {
        Source.PlayOneShot(SpawnTank[Random.Range(0, SpawnTank.Length)]);
    }

    public void PlayTirMoto()
    {
        Source.PlayOneShot(TirMoto);
    }

    public void PlayTirVoiture()
    {
        Source.PlayOneShot(TirVoiture);
    }

    public void PlayTirTank()
    {
        Source.PlayOneShot(TirTank);
    }

    public void PlayBip()
    {
        //Source.PlayOneShot(BipSemtex);
    }

    public void PlayHit()
    {
        Source.PlayOneShot(EnemyHit[Random.Range(0, EnemyHit.Length)]);
    }

    public void PlayShoot()
    {
        Source.PlayOneShot(Shoot[Random.Range(0, Shoot.Length)]);
    }

    public void PlayExplosion()
    {
        Source.PlayOneShot(Explosion[Random.Range(0, Explosion.Length)]);
    }

    public void PlayHoming()
    {
        Source.PlayOneShot(Homing);
    }

    public void PlaySplit()
    {
        Source.PlayOneShot(Split);
    }

    public void PlayPierce()
    {
        Source.PlayOneShot(Pierce);
    }

    public void PlayAccept()
    {
        Source.PlayOneShot(Accept);
    }

    public void PlayCancel()
    {
        Source.PlayOneShot(Cancel);
    }

    public void PlayPickLife()
    {
        Source.PlayOneShot(PickLife);
    }

    public void PlayLoseLife()
    {
        Source.PlayOneShot(LoseLife);
    }

    public void PlayDeath()
    {
        Source.PlayOneShot(Death);
    }

    public void PlayPickMoney()
    {
        Source.PlayOneShot(PickMoney[Random.Range(0, PickMoney.Length)]);
    }

    public void PlayPeaceMoto()
    {
        Source.PlayOneShot(PeaceMoto[Random.Range(0, PeaceMoto.Length)]);
    }

    public void PlayPeaceVoiture()
    {
        Source.PlayOneShot(PeaceVoiture[Random.Range(0, PeaceVoiture.Length)]);
    }

    public void PlayPeaceTank()
    {
        Source.PlayOneShot(PeaceTank[Random.Range(0, PeaceTank.Length)]);
    }
}
