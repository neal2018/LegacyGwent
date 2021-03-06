using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70053")]//弗妮希尔
    public class Vernossiel2 : CardEffect
    {//间谍。将3张“弗妮希尔的突击队”加入牌组。触发1次牌组中所有“弗妮希尔的突击队”的交换效果。
        public Vernossiel2(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            for (var i = 0; i < 3; i++)
            {
                await Game.CreateCard(CardId.VernossielsCommando, Game.AnotherPlayer(Card.PlayerIndex), new CardLocation(RowPosition.MyDeck, RNG.Next(0, Game.PlayersDeck[Game.AnotherPlayer(Card.PlayerIndex)].Count)));
            }

            var cards = Game.PlayersDeck[Game.AnotherPlayer(Card.PlayerIndex)].Where(x => x.Status.CardId == CardId.VernossielsCommando).ToList();
            foreach (var card in cards)
            {
                await card.Effect.SetCountdown(offset: -1);
            }
            return 0;
        }
    }
}