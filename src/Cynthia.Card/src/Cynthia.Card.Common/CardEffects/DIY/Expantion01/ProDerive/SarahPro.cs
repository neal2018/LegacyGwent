using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("130030")]//莎拉：晋升
    public class SarahPro : CardEffect
    {//交换2张颜色相同的牌。
        public SarahPro(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            var deckGroup2 = Game.PlayersDeck[PlayerIndex].Select(x => x.Status.Group).Distinct().ToArray();
            var selectList2 = Game.PlayersHandCard[PlayerIndex].Where(x => x.IsAnyGroup(deckGroup2)).ToList();
            if (!(await Game.GetSelectMenuCards(PlayerIndex, selectList2)).TrySingle(out var swapHandCard2))
            {
                return 0;
            }
            if (!Game.PlayersDeck[PlayerIndex].Where(x => x.Is(swapHandCard2.Status.Group)).TryMessOne(out var swapDeckCard2, Game.RNG))
            {
                return 0;
            }
            await swapHandCard2.Effect.Swap(swapDeckCard2);
            
            var deckGroup = Game.PlayersDeck[PlayerIndex].Select(x => x.Status.Group).Distinct().ToArray();
            var selectList = Game.PlayersHandCard[PlayerIndex].Where(x => x.IsAnyGroup(deckGroup)).ToList();
            if (!(await Game.GetSelectMenuCards(PlayerIndex, selectList)).TrySingle(out var swapHandCard))
            {
                return 0;
            }
            if (!Game.PlayersDeck[PlayerIndex].Where(x => x.Is(swapHandCard.Status.Group)).TryMessOne(out var swapDeckCard, Game.RNG))
            {
                return 0;
            }
            await swapHandCard.Effect.Swap(swapDeckCard);
            
            return 0;
        }
    }
}