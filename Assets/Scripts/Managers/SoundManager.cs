using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] SpawnMoto;
    public float SpawnMotoVolume;
    public AudioClip[] SpawnVoiture;
    public float SpawnVoitureVolume;
    public AudioClip[] SpawnTank;
    public float SpawnTankVolume;

    public AudioClip[] EnemyHit;
    public float EnemyHitVolume;
    public AudioClip[] Shoot;
    public float ShootVolume;

    public AudioClip BipSemtex;
    public float BipVolume;
    public AudioClip[] Explosion;
    public float ExplosionVolume;
    public AudioClip Homing;
    public float HomingVolume;
    public AudioClip Split;
    public float SplitVolume;
    public AudioClip Pierce;
    public float PierceVolume;

    public AudioClip Accept;
    public float AcceptVolume;
    public AudioClip Cancel;
    public float CancelVolume;

    public AudioClip PickLife;
    public float PickLifeVolume;
    public AudioClip LoseLife;
    public float LoseLifeVolume;

    public AudioClip[] PickMoney;

    public AudioClip Death;
    public float DeathVolume;

    public AudioClip[] PeaceMoto;
    public float PeaceMotoVolume;
    public AudioClip[] PeaceVoiture;
    public float PeaceVoitureVolume;
    public AudioClip[] PeaceTank;
    public float PeaceTankVolume;

    public bool DontDestroy;

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

        if(DontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }

        Source = GetComponent<AudioSource>();
        Source.Play();
    }

    public void StopMusic()
    {
        Source.Stop();
    }

    public void SetPitch()
    {
        Source.pitch = Time.timeScale;
    }

    public void PlayOneShot(AudioClip shot)
    {
        Source.PlayOneShot(shot);
    }

    public void PlaySpawnMoto() {
        Source.PlayOneShot(SpawnMoto[Random.Range(0, SpawnMoto.Length)], SpawnMotoVolume);
    }

    public void PlaySpawnVoiture()
    {
        Source.PlayOneShot(SpawnVoiture[Random.Range(0, SpawnVoiture.Length)], SpawnVoitureVolume);
    }

    public void PlaySpawnTank()
    {
        Source.PlayOneShot(SpawnTank[Random.Range(0, SpawnTank.Length)], SpawnTankVolume);
    }

    public void PlayBip()
    {
        //Source.PlayOneShot(BipSemtex);
    }

    public void PlayHit()
    {
        Source.PlayOneShot(EnemyHit[Random.Range(0, EnemyHit.Length)], EnemyHitVolume);
    }

    public void PlayShoot()
    {
        Source.PlayOneShot(Shoot[Random.Range(0, Shoot.Length)], ShootVolume);
    }

    public void PlayExplosion()
    {
        Source.PlayOneShot(Explosion[Random.Range(0, Explosion.Length)], ExplosionVolume);
    }

    public void PlayHoming()
    {
        Source.PlayOneShot(Homing, HomingVolume);
    }

    public void PlaySplit()
    {
        Source.PlayOneShot(Split, SplitVolume);
    }

    public void PlayPierce()
    {
        Source.PlayOneShot(Pierce, PierceVolume);
    }

    public void PlayAccept()
    {
        Source.PlayOneShot(Accept, AcceptVolume);
    }

    public void PlayCancel()
    {
        Source.PlayOneShot(Cancel, CancelVolume);
    }

    public void PlayPickLife()
    {
        Source.PlayOneShot(PickLife, PickLifeVolume);
    }

    public void PlayLoseLife()
    {
        Source.PlayOneShot(LoseLife, LoseLifeVolume);
    }

    public void PlayDeath()
    {
        Source.PlayOneShot(Death, DeathVolume);
    }

    public void PlayPickMoney()
    {
        //Source.PlayOneShot(PickMoney[Random.Range(0, PickMoney.Length)]);
    }

    public void PlayPeaceMoto()
    {
        Source.PlayOneShot(PeaceMoto[Random.Range(0, PeaceMoto.Length)], PeaceMotoVolume);
    }

    public void PlayPeaceVoiture()
    {
        Source.PlayOneShot(PeaceVoiture[Random.Range(0, PeaceVoiture.Length)], PeaceVoitureVolume);
    }

    public void PlayPeaceTank()
    {
        Source.PlayOneShot(PeaceTank[Random.Range(0, PeaceTank.Length)], PeaceTankVolume);
    }
}
