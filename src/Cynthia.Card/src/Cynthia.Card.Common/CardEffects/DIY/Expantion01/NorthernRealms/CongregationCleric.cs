using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System.Collections.Generic;
namespace Cynthia.Card
{
    [CardEffectId("70076")]//集会传教士 CongregationCleric
    public class CongregationCleric : CardEffect, IHandlesEvent<AfterCardLock>
    {//己方回合中，每当铜色单位被锁定，在同排召唤其3战力的原始同名牌。
        public CongregationCleric(GameCard card) : base(card) { }
        public async Task HandleEvent(AfterCardLock @event)
        {
            if ((@event.Source.PlayerIndex == PlayerIndex) && (Card.Status.CardRow.IsOnPlace()))
            {
                await Game.CreateCardAtEnd(@event.Target.CardInfo().CardId, PlayerIndex, Card.Status.CardRow, setting: Lesser);
            }
            return;
        }
        private void Lesser(CardStatus status)
        {
            status.IsDoomed = true;
            status.Strength = 2;
        }
    }
    
}