using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70071")]//史帕拉流亡军 StraysofSpalla
    public class StraysofSpalla : CardEffect
    {//检视己方牌组中3张铜色非同名单位牌，随后打出1张。
        public StraysofSpalla(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            //打乱己方卡组,并且取3张铜色非同名单位牌
            var list = Game.PlayersDeck[Card.PlayerIndex]
            .Where(x => x.Status.Group == Group.Copper && x.CardInfo().CardType == CardType.Unit
                        && x.CardInfo().CardId != Card.CardInfo().CardId).Mess(RNG).Take(3);
            //让玩家选择一张卡
            var result = await Game.GetSelectMenuCards
            (Card.PlayerIndex, list.ToList(), 1, "选择打出一张牌");
            //如果玩家一张卡都没选择,没有效果
            if (result.Count() == 0) return 0;
            await result.First().MoveToCardStayFirst();
            return 1;
        }
    }
}