using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class ValueConverterStatic
{
    public static string DoubleToString(double arg)
    {
        string result;
        if (arg >= 1_000_000_000_000_000_000_000_000d)
            result = (arg / 1_000_000_000_000_000_000_000_000d).ToString("0.#") + "Y"; // йотта (yotta, 1e24)
        else if (arg >= 1_000_000_000_000_000_000_000d)
            result = (arg / 1_000_000_000_000_000_000_000d).ToString("0.#") + "Z"; // зетта (zetta, 1e21)
        else if (arg >= 1_000_000_000_000_000_000d)
            result = (arg / 1_000_000_000_000_000_000d).ToString("0.#") + "E"; // экса (exa, 1e18)
        else if (arg >= 1_000_000_000_000_000d)
            result = (arg / 1_000_000_000_000_000d).ToString("0.#") + "P"; // пета (peta, 1e15)
        else if (arg >= 1_000_000_000_000d)
            result = (arg / 1_000_000_000_000d).ToString("0.#") + "T"; // тера (tera, 1e12)
        else if (arg >= 1_000_000_000d)
            result = (arg / 1_000_000_000d).ToString("0.#") + "B"; // миллиарды (giga, 1e9)
        else if (arg >= 1_000_000d)
            result = (arg / 1_000_000d).ToString("0.#") + "M"; // миллионы (mega, 1e6)
        else if (arg >= 1_000d)
            result = (arg / 1_000d).ToString("0.#") + "K"; // тыс€чи (kilo, 1e3)
        else
            result = arg.ToString("0");

        return result;
    }
}
