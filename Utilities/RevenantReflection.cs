using HarmonyLib;
using RimWorld;

public static class RevenantReflection
{
    public static readonly AccessTools.FieldRef<CompRevenant, int> BecomeInvisibleTick =
        AccessTools.FieldRefAccess<CompRevenant, int>("becomeInvisibleTick");
}