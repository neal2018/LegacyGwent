using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;

namespace Cynthia.Card
{
    [CardEffectId("70090")]//吸血鬼女 Alp
    public class Alp : CardEffect, IHandlesEvent<AfterCardDrain>
    {//对一个敌军单位造成等同于自身战力的伤害，随后失去所有增益。若位于手牌或牌组，己方每发动1次汲食获得1点增益。
        public Alp(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            var list = await Game.GetSelectPlaceCards(Card, selectMode: SelectModeType.EnemyRow);
            if (list.Count <= 0) return 0;
            var card = list.Single();
            await card.Effect.Damage(Card.CardPoint(), Card);
            await Card.Effect.Damage(Card.Status.HealthStatus, Card, BulletType.RedLight, true);//造成穿透伤害
            return 0;
        }
        public async Task HandleEvent(AfterCardDrain @event)
        {
            if (!(Card.Status.CardRow.IsInDeck() || Card.Status.CardRow.IsInHand())|| @event.Source.PlayerIndex != Card.PlayerIndex) return;
            await Card.Effect.Boost(1, Card);
        }
    }
}
