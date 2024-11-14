using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    //Variável de velocidade de movimentação
    public float velocidade; 
    //Uma variável para acessar os codigos da Direita do Player
    private DireitaPlayer direitaPlayer;
    private EsquerdaPlayer esquerdaPlayer;
    private FlipCorpoPlayer flipCorpoPlayer;
    private AnimacaoPlayer animacaoPlayer;
    private PePlayer pePlayer;
    private CabecaPlayer cabecaPlayer;

    private bool estaPulando = false;
    private bool puloDuplo = false;
    public float forcaDoPuloY = 1.5f;

    private Coroutine coroutinePulo;
    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        direitaPlayer = GetComponentInChildren<DireitaPlayer>();
        esquerdaPlayer = GetComponentInChildren<EsquerdaPlayer>();
        flipCorpoPlayer = GetComponentInChildren<FlipCorpoPlayer>();
        animacaoPlayer = GetComponentInChildren<AnimacaoPlayer>();
        pePlayer = GetComponentInChildren<PePlayer>();
        cabecaPlayer = GetComponentInChildren<CabecaPlayer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();
        Pular();
    }

    //Método para o player pular
    private void Pular(){
        //Verificar se o jogador clicou na tecla para pular
        if(Input.GetButtonDown("Jump")){
            //Verificar se o jogador está no chão
            if(pePlayer.EstaNoChao == true){
                //Ativo o Pulo
                animacaoPlayer.PlayJump(); //Ativar a animação do pulo
                estaPulando = true;//Definir um estado de pulo
                puloDuplo = true;//Definir um estado de pulo duplo
                AtivarTempoPulo();//Ativar o tempo de pulo
            }
            else{
                //Verificar se eu posso fazer o pulo duplo
                if(puloDuplo == true){
                    animacaoPlayer.PlayDoubleJump();//Ativar a animação do pulo duplo
                    estaPulando = true;//Definir o estado de pulo
                    puloDuplo = false;//Definir o estado do pulo duplo 
                    AtivarTempoPulo();//Ativar o tempo de pulo
                }

            }
        }

        //Verificar se está habilitado o jogador a pular
        if(estaPulando == true){
            //Verificar se a cabeça do jogador está livre
            if(cabecaPlayer.LimiteDaCabeca == false){
                //Fazer o jogador subir para simular o pulo
                rigidbody2D.velocity = Vector3.zero;
                rigidbody2D.gravityScale = 0;
                Vector3 direcaoPulo = new Vector3(0,forcaDoPuloY,0);
                transform.position += direcaoPulo * Time.deltaTime * velocidade;
            }
        }
        else{
            rigidbody2D.gravityScale = 4;
        }
    }

    public void AtivarTempoPulo(){
        if(coroutinePulo != null){
            StopCoroutine(coroutinePulo); //Parar a execução da rotina
        }
        coroutinePulo = StartCoroutine(TempoPulo());
    }

    //Método para inverter o valor do "estaPulando" através de um tempo
    private IEnumerator TempoPulo(){
        yield return new WaitForSeconds(0.3f);
        estaPulando = false;
    }

    //Método para movimentar o player
    private void Movimentar(){
        //Movimentar o Player
        float eixoX = Input.GetAxis("Horizontal");
        //Verificar se chegou na extremidade da direita ou esquerda
        if(eixoX>0 && direitaPlayer.LimiteDireita == true) { eixoX = 0;}
        else if (eixoX<0 && esquerdaPlayer.LimiteEsquerda == true) {eixoX = 0;}

        //Verificar qual a lado olhar
        if(eixoX > 0){
            flipCorpoPlayer.OlharDireita();
        }
        else if(eixoX < 0){
            flipCorpoPlayer.OlharEsquerda();
        }

        //Verificar se o player está no chao
        if(pePlayer.EstaNoChao == true){
            //Verificar se o player está parado ou movendo
            if(eixoX != 0){
                animacaoPlayer.PlayRun();
            }
            else{
                animacaoPlayer.PlayIdle();
            }
        }
        else{
            animacaoPlayer.PlayFall();
        }

        //Movimentar o player
        Vector3 direcaoMovimento = new Vector3(eixoX,0,0);
        transform.position += direcaoMovimento * velocidade * Time.deltaTime;
    }
}
