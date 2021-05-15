using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;


namespace Cynthia.Card
{
    [CardEffectId("70093")]//布拉维坎的音乐家 MusiciansofBlaviken
    public class MusiciansofBlaviken : CardEffect, IHandlesEvent<BeforeUnitPlay>
    {//坚韧。 若位于手牌，己方打出战力高于自身的单位时，获得3点增益。一共可生效4次。
        public MusiciansofBlaviken(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            await Card.Effect.Resilience(Card);
            return 0;
        }
        public async Task HandleEvent(BeforeUnitPlay @event)
        {
            if (@event.PlayedCard.PlayerIndex == Card.PlayerIndex && @event.PlayedCard.CardPoint() > Card.CardPoint() && Card.Status.CardRow.IsInHand() && Card.Status.Countdown >= 1)
            {
                await Card.Effect.SetCountdown(offset: -1);
                await Card.Effect.Boost(3, Card);
            }
        }
    }
}