using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;

namespace Cynthia.Card
{
    [CardEffectId("70099")]//夜之女王 QueenoftheNight
    public class QueenoftheNight : CardEffect, IHandlesEvent<AfterCardDrain>, IHandlesEvent<AfterTurnOver>
    {//若位于手牌或牌组：回合结束时保持自身增益与己方两回合内造成的汲食战力相同。
        public QueenoftheNight(GameCard card) : base(card) { }
        private int point0 = 0;
        private int point1 = 0;
        private int point2 = 0;
        private int gap;
        public async Task HandleEvent(AfterTurnOver @event)
        {
            if (!(Card.Status.CardRow.IsInHand() || Card.Status.CardRow.IsInDeck())|| PlayerIndex != @event.PlayerIndex) return;

            point2 = point1;
            point1 = point0;
            point0 = 0;
            gap = Card.Status.HealthStatus - (point1 + point2);
            if(gap < 0)
            {
                await Card.Effect.Boost(-gap, Card );
            }
            else if(gap > 0)
            {
                await Card.Effect.Damage(gap, Card, BulletType.RedLight, true);//造成穿透伤害
            }
        }
        public async Task HandleEvent(AfterCardDrain @event)
        {
            if (!(Card.Status.CardRow.IsInDeck() || Card.Status.CardRow.IsInHand())|| @event.Source.PlayerIndex != Card.PlayerIndex) return;
            await Task.Run(() => { point0 += @event.Num;});//异步运行
        }
    }
}
