using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {

    #region Public Properties

    public float Cronometro = 120f; //fase de no maximo 2 minutos a principio

    [Range (0, 100)]
    public float Pipibar = 50f; //quando alcança o valor máximo perde

    public float ModificadorPipi = 5; //quanto o valor da barra de xixi diminui se estiver na metade da barra

    public float PipiPorCerveja = 5; // quanto uma cerveja aumenta a Pipibar 

    public int CervejaAcumulada = 0; //quantidade de cervejas seguidas

    public int MaximoCerveja = 3; //Máximo de Cerveja antes de ficar bebado e tomar slow

    public float TempoDuracaoCerveja = 2f; //Tempo de duração do efeito por cerveja  antes de começar a decair - embreagado usa o dobro do valor

    public float TemporizadorCerveja; //Tempo restante do efeito da cerveja
    public bool IsDead; //Indica se o jogador perdeu
    #endregion

    #region Private Members

    private HUDManager hud;

    #endregion

    void Start () {
        hud = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUDManager> ();
        //AddCerveja (2);
    }

    // Update is called once per frame
    void Update () {

        if (Cronometro <= 0 || Pipibar >= 100)
            IsDead = true;

        if (!IsDead) {
            //decrementa conômetro
            Cronometro -= Time.deltaTime;

            //decrementa pipibar
            //quanto mais cheia mais rápido diminui
            Pipibar -= (ModificadorPipi / 50 * Pipibar * Time.deltaTime);

            //decrementa efeito cerveja
            TemporizadorCerveja = (TemporizadorCerveja - Time.deltaTime) < 0 ? 0 : TemporizadorCerveja - Time.deltaTime;

            if (TemporizadorCerveja == 0) {
                CervejaAcumulada = 0;
            }
        }
        Cronometro = Cronometro < 0 ? 0 : Cronometro;
        Pipibar = Pipibar < 0 ? 0 : Pipibar;

        UpdateHud ();
        if (IsDead)
        {
            Time.timeScale = 0;
            print("Game Over");
        }
    }

    private void UpdateHud () {
        if (hud == null)
            return;
        hud.SetTimer (Cronometro);
        hud.SetPipiBar (Pipibar);
        hud.SetBeerCount (CervejaAcumulada);
        hud.SetBeerTimer (TemporizadorCerveja);
    }

    public void AddCerveja (int cerveja) {
        CervejaAcumulada += cerveja;

        if (CervejaAcumulada <= MaximoCerveja)
            TemporizadorCerveja = TempoDuracaoCerveja;
        else
            TemporizadorCerveja = TempoDuracaoCerveja * 2;

        //quanto mais cervejas acumuladas maior é o dano à PipiBar
        Pipibar += (PipiPorCerveja * CervejaAcumulada);
    }

    public bool EstaBebado () {
        return CervejaAcumulada > MaximoCerveja;
    }

    public void DiminuiCronometro (float tempo) {
        Cronometro -= tempo;
    }
}