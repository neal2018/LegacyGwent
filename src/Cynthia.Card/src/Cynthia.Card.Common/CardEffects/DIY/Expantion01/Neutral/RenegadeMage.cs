using System.Linq;
using System.Threading.Tasks;

namespace Cynthia.Card
{
    [CardEffectId("70069")]//脱逃的法师 RenegadeMage
    public class RenegadeMage : CardEffect, IHandlesEvent<AfterTurnOver>
    {//己方牌组有至少10张特殊牌时，在回合结束时召唤此单位。
        public RenegadeMage(GameCard card) : base(card) { }
        public async Task HandleEvent(AfterTurnOver @event)
        {
            var list = Game.PlayersDeck[Card.PlayerIndex]
            .Where(x => x.CardInfo().CardType == CardType.Special).Mess(RNG);
            if (list.Count() >= 10)
            {
                //召唤全部
                var cards = Game.PlayersDeck[PlayerIndex].Where(x => x.Status.CardId == Card.Status.CardId).ToList();
                foreach (var card in cards)
                {
                    //召唤到末尾 
                    await card.Effect.Summon(Game.GetRandomCanPlayLocation(Card.PlayerIndex, true), Card);
                }
            }
            return;
        }
    }
}