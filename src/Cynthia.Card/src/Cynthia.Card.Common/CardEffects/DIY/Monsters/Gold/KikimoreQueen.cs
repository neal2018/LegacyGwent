using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70036")]//齐齐摩女王
    public class KikimoreQueen : CardEffect
    {
        //吞噬己方牌组中所有战力小于5的铜色单位牌。每吞噬1张牌，便获得2点增益。
        public KikimoreQueen(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            //var cardlist = Game.PlayersCemetery[PlayerIndex].Where(x => x.IsAnyGroup(Group.Copper, Group.Silver)).ToList();
            var cardlist = Game.PlayersDeck[PlayerIndex].Where(x => (x.CardPoint() < 5) && x.Is(Group.Copper, CardType.Unit)).ToList();
            var count = cardlist.Count() * 2;
            foreach (var target in cardlist)
            {
                await target.Effect.ToCemetery();
            }
            await Boost(count, Card);
            await Game.SendEvent(new AfterCardConsume(null, Card));
            return 0;
        }
    }
}