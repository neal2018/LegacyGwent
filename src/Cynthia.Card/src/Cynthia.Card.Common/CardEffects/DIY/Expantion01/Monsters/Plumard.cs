using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;

namespace Cynthia.Card
{
    [CardEffectId("70098")]//渴血鸟怪 Plumard
    public class Plumard : CardEffect, IHandlesEvent<AfterCardDrain>
    {//生成1张佚亡原始同名牌。每当己方非同名牌造成汲食，汲食目标单位1点战力。
        public Plumard(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying,bool isReveal)
        {
            var position = Card.GetLocation();
            await Game.CreateCard(Card.Status.CardId, PlayerIndex, position, setting: ToDoomed2);
            return 0;
        }
        private void ToDoomed2(CardStatus status)
        {
            status.IsDoomed = true;
        }
        public async Task HandleEvent(AfterCardDrain @event)
        {
            if (!Card.Status.CardRow.IsOnPlace() || @event.Source.PlayerIndex != Card.PlayerIndex || @event.Source.Status.CardId==Card.Status.CardId) return;
            //if (!Card.Status.CardRow.IsOnPlace() || @event.Source.Status.CardId==Card.Status.CardId) return;
            await Card.Effect.Drain(1, @event.Target);
        }
    }
}
