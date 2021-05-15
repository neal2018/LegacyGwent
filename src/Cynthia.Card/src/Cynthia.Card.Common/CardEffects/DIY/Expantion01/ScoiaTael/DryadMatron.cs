using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;


namespace Cynthia.Card
{
    [CardEffectId("70083")]//树精族母 DryadMatron
    public class DryadMatron : CardEffect, IHandlesEvent<AfterUnitDown>, IHandlesEvent<AfterTransform>
    {//后续出现的其他友军“树精”单位获得2点强化。
        public DryadMatron(GameCard card) : base(card) { }
        public async Task HandleEvent(AfterUnitDown @event)
        {
            if (@event.Target == Card || !@event.Target.HasAllCategorie(Categorie.Dryad) || !Card.IsAliveOnPlance() || @event.Target.PlayerIndex != PlayerIndex || (@event.IsMoveInfo.isMove && !@event.IsMoveInfo.isFromeEnemy))
            {
                return;
            }
            await @event.Target.Effect.Strengthen(2, Card);
        }
        public async Task HandleEvent(AfterTransform @event)
        {
            if (@event.Target == Card || !@event.Target.HasAllCategorie(Categorie.Dryad) || !Card.IsAliveOnPlance() || @event.Target.PlayerIndex != PlayerIndex)
            {
                return;
            }
            await @event.Target.Effect.Strengthen(2, Card);
        }
    }
}