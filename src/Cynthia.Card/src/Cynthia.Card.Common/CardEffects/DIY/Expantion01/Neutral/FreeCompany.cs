using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;


namespace Cynthia.Card
{
    [CardEffectId("70092")]//自由集会 FreeCompany
    public class FreeCompany : CardEffect
    {//检视己方牌组中3张铜色单位牌，随后召唤1张，并使所有与它同类型的友军单位获得1点增益。
        public FreeCompany(GameCard card) : base(card) { }
        public override async Task<int> CardUseEffect()
		{
			//打乱己方卡组,并且取3张铜色卡
            var list = Game.PlayersDeck[Card.PlayerIndex]
            .Where(x => x.Status.Group == Group.Copper && x.CardInfo().CardType == CardType.Unit).Mess(RNG).Take(3);
            //让玩家选择一张卡
            var result = await Game.GetSelectMenuCards
            (Card.PlayerIndex, list.ToList(), 1, "选择打出一张牌");
            //如果玩家一张卡都没选择,没有效果
            if (result.Count() == 0) return 0;
            var card = result.First();
            var result2 = await Game.GetSelectRow(Card.PlayerIndex, Card, TurnType.My.GetRow());
            var row = Game.RowToList(Card.PlayerIndex, result2);
            var categories = card.Status.Categories;
            await card.Effect.Summon(new CardLocation(result2, row.Count), Card);

            //await Game.Debug("标签开始筛选:" + categories.Join(","));
            var targetCards = Game.GetAllCard(PlayerIndex).Where(x => x.PlayerIndex == PlayerIndex && x.Status.CardRow.IsOnPlace() && x.Status.Categories.Intersect(categories).Any());
            //await Game.Debug($"筛选出了{targetCards.Count()}个");
            foreach (var target in targetCards)
            {
                await target.Effect.Boost(1, Card);
            }
            return 0;
		}
    }
}