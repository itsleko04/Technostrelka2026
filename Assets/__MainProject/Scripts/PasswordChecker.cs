using DG.Tweening;
using TMPro;
using UnityEngine;
using static PasswdUtils;

public class PasswordChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _passwordRateTxt;
    [SerializeField] private TMP_InputField _passwordInput;

    public void UpdatePasswordRate()
    {
        GetPasswordStrength(out string rate, out Color color);
        _passwordRateTxt.text = $"Статус: {rate}";
        _passwordRateTxt.DOColor(color, .3f);
    }

    private void GetPasswordStrength(out string rate, out Color color)
    {
        int strength = CheckPasswordStrength(_passwordInput.text);
        PasswordStrength PRate = GetPasswordStrengthRate(strength);
        rate = PRate.ToString();
        color = Color.white;

        switch (PRate)
        {
            case PasswordStrength.Weak:
                rate = "Слабый";
                color = Color.red;
                break;

            case PasswordStrength.Normal:
                rate = "Средний";
                color = new Color32(255, 153, 28, 255);
                break;

            case PasswordStrength.Hard:
                rate = "Сильный";
                color = Color.green;
                break;

            case PasswordStrength.Paranoic:
                rate = "Слишком сложный";
                color = Color.black;
                break;
        }
    }
}
