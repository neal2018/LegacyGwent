using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
	[CardEffectId("130090")]//赛浦利安·威利：晋升
	public class CyprianWileyPro : CardEffect
	{//对双方一个单位的所有同名牌造成4点削弱。
		public CyprianWileyPro(GameCard card) : base(card){}
		public override async Task<int> CardPlayEffect(bool isSpying,bool isReveal)
		{
			var selected = await Game.GetSelectPlaceCards(Card);
            if (!selected.TrySingle(out var target))
            {
                return 0;
            }
            var id = target.Status.CardId;
            var targetCards = Game.GetPlaceCards(AnotherPlayer).Where(x => x.Status.CardId == id);
            foreach (var card in targetCards)
            {
                await card.Effect.Weaken(4, Card);
            }
			var targetCards2 = Game.GetPlaceCards(PlayerIndex).Where(x => x.Status.CardId == id);
            foreach (var card in targetCards2)
            {
                await card.Effect.Weaken(4, Card);
            }
			return 0;
		}
	}
}