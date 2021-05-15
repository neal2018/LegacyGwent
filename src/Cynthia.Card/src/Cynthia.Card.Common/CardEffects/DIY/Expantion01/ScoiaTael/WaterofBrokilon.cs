using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70085")]//布洛克莱昂之水 WaterofBrokilon
    public class WaterofBrokilon : CardEffect
    {//将单排上所有非“树精”单位变为等战力的“新生树精”。
        public WaterofBrokilon(GameCard card) : base(card) { }
        private int point;
        public override async Task<int> CardUseEffect()
        {
            var result = await Game.GetSelectRow(Card.PlayerIndex, Card, TurnType.All.GetRow());
            var row = Game.RowToList(Card.PlayerIndex, result).IgnoreConcealAndDead().Where(x => !x.HasAllCategorie(Categorie.Dryad));
            foreach (var card in row)
            {
                if (card.Status.CardRow.IsOnPlace())
                {
                    point = card.Status.Strength + card.Status.HealthStatus;
                    await card.Effect.Transform(CardId.DryadFledgling, Card, setting: SamePoint, isForce:true);
                }
            }
			return 0;
        }
        private void SamePoint(GameCard card)
        {
            card.Status.Strength = point;
        }
    }
}