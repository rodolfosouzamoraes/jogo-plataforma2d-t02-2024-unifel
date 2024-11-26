using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public static PlayerMng Instance;
    public static FlipCorpoPlayer flipCorpoPlayer;
    public static AnimacaoPlayer animacaoPlayer;
    public static PePlayer pePlayer;
    public static CabecaPlayer cabecaPlayer;
    public static DireitaPlayer direitaPlayer;
    public static EsquerdaPlayer esquerdaPlayer;
    public static MovimentarPlayer movimentarPlayer;
    public static PlayerDano playerDano;
    public static Rigidbody2D rigidBody2D;

    void Awake(){
        if(Instance == null){
            flipCorpoPlayer = GetComponentInChildren<FlipCorpoPlayer>();
            animacaoPlayer = GetComponentInChildren<AnimacaoPlayer>();
            pePlayer = GetComponentInChildren<PePlayer>();
            cabecaPlayer = GetComponentInChildren<CabecaPlayer>();
            direitaPlayer = GetComponentInChildren<DireitaPlayer>();
            esquerdaPlayer = GetComponentInChildren<EsquerdaPlayer>();
            movimentarPlayer = GetComponent<MovimentarPlayer>();
            rigidBody2D = GetComponent<Rigidbody2D>();
            playerDano = GetComponent<PlayerDano>();
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public bool movimentacaoHabilitada;

    void Start(){
        movimentacaoHabilitada = false;
    }

    public void ResetarVelocidadeDaFisica(){
        rigidBody2D.velocity = Vector3.zero;
    }
    public void ArremessarPlayer(int x, int y){
        rigidBody2D.AddForce(new Vector2(x,y));
    }
    public void HabilitarMovimentacao(){
        movimentacaoHabilitada = true;
    }
    public void DesabilitarMovimentacao(){
        movimentacaoHabilitada = false;
    }
    public void RemoverSimulacaoDaFisica(){
        ResetarVelocidadeDaFisica();
        rigidBody2D.simulated = false;
    }

    public void CongelarPlayer(){
        DesabilitarMovimentacao();
        ResetarVelocidadeDaFisica();
        animacaoPlayer.PlayIdle();
    }
}
