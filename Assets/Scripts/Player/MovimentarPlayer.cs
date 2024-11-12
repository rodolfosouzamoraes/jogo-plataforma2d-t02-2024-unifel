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
    // Start is called before the first frame update
    void Start()
    {
        direitaPlayer = GetComponentInChildren<DireitaPlayer>();
        esquerdaPlayer = GetComponentInChildren<EsquerdaPlayer>();
        flipCorpoPlayer = GetComponentInChildren<FlipCorpoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentar o Player
        float eixoX = Input.GetAxis("Horizontal");
        //Verificar se chegou na extremidade da direita
        if(eixoX>0 && direitaPlayer.LimiteDireita == true) { eixoX = 0;}
        else if (eixoX<0 && esquerdaPlayer.LimiteEsquerda == true) {eixoX = 0;}

        //Verificar qual a lado olhar
        if(eixoX > 0){
            flipCorpoPlayer.OlharDireita();
        }
        else if(eixoX < 0){
            flipCorpoPlayer.OlharEsquerda();
        }

        Vector3 direcaoMovimento = new Vector3(eixoX,0,0);
        transform.position += direcaoMovimento * velocidade * Time.deltaTime;
    }
}
