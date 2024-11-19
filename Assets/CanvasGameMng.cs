using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public Image imgBarraDeVida;
    public TextMeshProUGUI txtTotalItensColetados;
    public TextMeshProUGUI txtTempoDeJogo;
    public Sprite[] sptsBarraDeVida;
    private int vidas;
    public bool jogadorMorreu = false;

    // Start is called before the first frame update
    void Start()
    {
        vidas = sptsBarraDeVida.Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementarVidaJogador(){
        vidas--;
        if(vidas < 0){
            MatarJogador();
        }
        else{
            imgBarraDeVida.sprite = sptsBarraDeVida[vidas];
        }
    }

    public void MatarJogador(){
        jogadorMorreu = true;//Ter uma variavel que diz que jogador morreu
        vidas = 0;
        imgBarraDeVida.sprite = sptsBarraDeVida[vidas];
        PlayerMng.animacaoPlayer.PlayerDeath();//Ativar a animação de morte
        //Desabilitar a Movimentação do jogador
        //Remover a simulacão da física
        StartCoroutine(ReiniciarLevel());//Contar um tempo para reiniciar a cena
    }

    IEnumerator ReiniciarLevel(){
        yield return new WaitForSeconds(3f);
        ReiniciarLevelAtual();
    }

    public void ReiniciarLevelAtual(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
