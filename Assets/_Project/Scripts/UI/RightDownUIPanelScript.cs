using UnityEngine;
using static SimulationController;

public class RightDownUIPanelScript : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject EnabledPauseButton;
    public GameObject DisabledPauseButton;
    public GameObject EnabledSpeed1Button;
    public GameObject DisabledSpeed1Button;
    public GameObject EnabledSpeed2Button;
    public GameObject DisabledSpeed2Button;
    public GameObject EnabledSpeed3Button;
    public GameObject DisabledSpeed3Button;
    public GameObject EnabledSpeed6Button;
    public GameObject DisabledSpeed6Button;

    public void PausedEnable()
    {
        EnabledPauseButton.gameObject.SetActive(true);
        DisabledPauseButton.gameObject.SetActive(false);

        EnabledSpeed1Button.gameObject.SetActive(false);
        EnabledSpeed2Button.gameObject.SetActive(false);
        //EnabledSpeed3Button.gameObject.SetActive(false);
        //EnabledSpeed6Button.gameObject.SetActive(false);

        DisabledSpeed1Button.gameObject.SetActive(true);
        DisabledSpeed2Button.gameObject.SetActive(true);
        //DisabledSpeed3Button.gameObject.SetActive(true);
        //DisabledSpeed6Button.gameObject.SetActive(true);
    }

    public void PausedDisable(TimeSpeed gameSpeed)
    {
        EnabledPauseButton.gameObject.SetActive(false);
        DisabledPauseButton.gameObject.SetActive(true);

        switch (gameSpeed)
        {
            case TimeSpeed.One:
                SwitchSpeed1();
                break;
            case TimeSpeed.Two:
                SwitchSpeed2();
                break;
            case TimeSpeed.Three:
                SwitchSpeed3();
                break;
            case TimeSpeed.Six:
                SwitchSpeed6();
                break;
        }
    }

    public void SwitchSpeed1()
    {
        EnabledSpeed1Button.gameObject.SetActive(!EnabledSpeed1Button.activeSelf);
        DisabledSpeed1Button.gameObject.SetActive(!DisabledSpeed1Button.activeSelf);
    }

    public void SwitchSpeed2()
    {
        EnabledSpeed2Button.gameObject.SetActive(!EnabledSpeed2Button.activeSelf);
        DisabledSpeed2Button.gameObject.SetActive(!DisabledSpeed2Button.activeSelf);
    }
    public void SwitchSpeed3()
    {

    }

    public void SwitchSpeed6()
    {

    }
}
