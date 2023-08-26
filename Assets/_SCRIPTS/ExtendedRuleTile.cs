using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class ExtendedRuleTile : RuleTile
{
    public enum SiblingGroup
    {
        Terrain,
    }
    public SiblingGroup siblingGroup;

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        switch (neighbor)
        {
            case TilingRule.Neighbor.This:
                {
                    return other is ExtendedRuleTile
                        && (other as ExtendedRuleTile).siblingGroup == this.siblingGroup;
                }
            case TilingRule.Neighbor.NotThis:
                {
                    return !(other is ExtendedRuleTile
                        && (other as ExtendedRuleTile).siblingGroup == this.siblingGroup);
                }
        }

        return base.RuleMatch(neighbor, other);
    }
}
