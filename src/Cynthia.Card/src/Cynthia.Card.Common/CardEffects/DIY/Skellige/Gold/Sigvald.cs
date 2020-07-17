using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70038")]//龙龟
    public class Sigvald : CardEffect, IHandlesEvent<AfterTurnOver>
    {//回合结束时，复活至随机排，获得1点强化。
        public Sigvald(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            await Task.CompletedTask;
            return 0;
        }
        public async Task HandleEvent(AfterTurnOver @event)
        {
            if (@event.PlayerIndex != Card.PlayerIndex || !Card.Status.CardRow.IsInCemetery())
            {
                return;
            }
            await Card.Effect.Strengthen(1, Card);
            await Card.Effect.Resurrect(Game.GetRandomCanPlayLocation(Card.PlayerIndex, false), Card);
            return;
        }
    }
}