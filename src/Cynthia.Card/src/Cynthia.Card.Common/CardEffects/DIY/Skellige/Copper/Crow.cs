using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70056")]//邪鸦 Crow
    public class Crow : CardEffect,IHandlesEvent<AfterTurnOver>
    {//每当场上有3个与自身战力相同的单位时，在回合结束时召唤1张同名牌，随后使牌库中的同名牌获得1点增益。
        public Crow(GameCard card) : base(card) { }
        public async Task HandleEvent(AfterTurnOver @event)
        {
            if (Game.GameRound.ToPlayerIndex(Game) != PlayerIndex || !Card.Status.CardRow.IsInDeck())
            {
                return;
            }
            var cards = Game.GetAllCard(Game.AnotherPlayer(Card.PlayerIndex)).Where(x=>x.Status.CardRow.IsOnPlace() && x.CardPoint() == Card.CardPoint()).ToList();
            if (cards.Count() >= 3)
            {
                //列出所有可以打出卡
                var list = Game.PlayersDeck[Card.PlayerIndex].Where(x => x.Status.CardId == Card.Status.CardId);
                if (list.Count() == 0)
                {
                    return;
                }
                //只召唤最后一个，召唤到同排最右侧
                if (Card == list.Last())
                {
                    await Card.Effect.Summon(Game.GetRandomCanPlayLocation(Card.PlayerIndex, true), Card);
                    var deck = Game.PlayersDeck[Card.PlayerIndex].Where(x => x.Status.CardId == Card.Status.CardId).ToList();
                    if (deck.Count() == 0)
                    {
                        return;
                    }
                    foreach (var card in deck)
                    {
                        await card.Effect.Boost(1, Card);
                    }    
                }
            }
            return;
        }
    }
}