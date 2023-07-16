using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    [SerializeField] int NumMenuOptions = 5;
    [SerializeField] TextMeshProUGUI [] MenuOptionsObjects;

    [SerializeField] TMP_FontAsset NormalMenuItemFont;
    [SerializeField] TMP_FontAsset SelectedMenuItemFont;

    [SerializeField] Color NormalOptionColor;
    [SerializeField] Color SelectedOptionColor;
    [SerializeField] Color DisabledOptionColor;
    

    int currentMenuOption = 2;
    public int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitMenuFormat();                                                       // init menu text format
        GamepadUtils.Init();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Controls.MenuKeys();
        NextMenuOption( );
    }

    void InitMenuFormat(){
        foreach (TextMeshProUGUI  option in MenuOptionsObjects){
            option.font = NormalMenuItemFont;
            option.color = NormalOptionColor;
        }
        MenuOptionsObjects[currentMenuOption].font = SelectedMenuItemFont;
        MenuOptionsObjects[currentMenuOption].color = SelectedOptionColor;

        // "Martelanço"
        //MenuOptionsObjects[1].color = DisabledOptionColor;
    }

    void NextMenuOption(){
        if (direction == 0) return;
        if (direction == 2) { ExecuteOption();}

        // reset previous option's text format
        MenuOptionsObjects[currentMenuOption].font = NormalMenuItemFont;
        MenuOptionsObjects[currentMenuOption].color = NormalOptionColor;

        // move current to next option
        currentMenuOption = ( ( currentMenuOption + direction) % NumMenuOptions + NumMenuOptions ) % NumMenuOptions;

        // set new current option's text format
        MenuOptionsObjects[currentMenuOption].font = SelectedMenuItemFont;
        MenuOptionsObjects[currentMenuOption].color = SelectedOptionColor;

        direction = 0;
    }

    void ExecuteOption(){
        switch (currentMenuOption){
            case 0 : MnuCalibration();      break ;
            case 1 : MnuDefAmblyopicEye();  break ;
            case 2 : MnuContinue();         break ;
        }
    }

    public void MnuCalibration(){
        Debug.Log("Calibration");
    }

    public void MnuDefAmblyopicEye(){
        SceneManager.LoadScene("01 - SelectAmbEye - additive", LoadSceneMode.Additive);
    }

    public void MnuContinue(){
        
        Debug.Log("Continuar");
    }

    
}
