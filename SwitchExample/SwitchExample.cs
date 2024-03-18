using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace SwitchExample;

[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net481)]
// Fazit aus Ergebnis von BenchmarkDotNet:
// .NET 8.0 ist schneller als .NET Framework 4.8.1 (etwa um Faktor 2)
// Switch ist immer konstant schnell
// IfElseIf ist nur schneller, wenn erster Vergleich erfolgreich ist
public class SwitchExample
{
    [Params(RGB.R, RGB.G, RGB.B)]
    public RGB RGB;

    // Auszug aus dem generierten IL Code (sharplab.io)
    // 3 mal mit kleinen Abweichungen
    // IL_000c: ldarg.0
    // IL_000d: ldfld valuetype RGB SwitchExample::RGB
    // IL_0012: ldc.i4.0
    // IL_0013: ceq
    // IL_0015: stloc.2
    [Benchmark]
    public string IfElseIf()
    {
        var result = "";
        for (var i = 0; i < 10.000; i++)
        {
            if (RGB == RGB.R)
            {
                result = "Red";
            }
            else if (RGB == RGB.G)
            {
                result = "Green";
            }
            else if (RGB == RGB.B)
            {
                result = "Blue";
            }
        }
        return result;
    }

    // Auszug aus dem generierten IL Code (sharplab.io)
    // IL_0006: switch (IL_0019, IL_0021, IL_0029)
    [Benchmark]
    public string Switch()
    {
        var result = "";
        for (var i = 0; i < 10.000; i++)
        {
            switch (RGB)
            {
                case RGB.R:
                    result = "Red";
                    break;
                case RGB.G:
                    result = "Green";
                    break;
                case RGB.B:
                    result = "Blue";
                    break;
            }
        }
        return result;
    }
}

public enum RGB
{
    R,
    G,
    B
}
