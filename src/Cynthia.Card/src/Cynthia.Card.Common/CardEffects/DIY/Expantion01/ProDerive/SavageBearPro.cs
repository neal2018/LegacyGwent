using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("640080")]//恶熊：晋升
    public class SavageBearPro : CardEffect, IHandlesEvent<AfterUnitDown>
    {//对后续打出至对方半场的单位造成2点伤害。
        public SavageBearPro(GameCard card) : base(card) { }

        public async Task HandleEvent(AfterUnitDown @event)
        {
            if (Card.PlayerIndex != @event.Target.PlayerIndex && Card.Status.CardRow.IsOnPlace() && @event.IsFromHand)
            {
                await @event.Target.Effect.Damage(2, Card);
            }
        }
    }
}