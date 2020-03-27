using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
namespace Cynthia.Card
{
    [CardEffectId("70002")]//考德威尔伯爵
    public class CountCaldWell : CardEffect, IHandlesEvent<AfterTurnOver>
    {//择一：从牌库中打出一张战力不高于自身的铜色单位，在回合结束把它送进墓地；或吞噬牌库中一张战力高于自身的铜色单位牌，将它的战力作为自身的增益。
        public CountCaldWell(GameCard card) : base(card) { }

        private bool _needKill = false;
        private GameCard _target = null;
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            //选择
            await Game.Debug("play");
            var switchCard = await Card.GetMenuSwitch
            (
                ("借刀", "从牌库中打出一张战力不高于自身的铜色单位，在回合结束把它送进墓地。"),
                ("弃子", "吞噬牌库中一张战力高于自身的铜色单位牌，将它的战力作为自身的增益。")
            );
            await Game.Debug("select");
            if (switchCard == 0)
            {
                _target = null;
                var list = Game.PlayersDeck[Card.PlayerIndex].Where(x => (x.CardPoint() <= Card.CardPoint()) && x.Is(Group.Copper)).Mess(Game.RNG).ToList();
                if (list.Count() == 0) { return 0; }
                //选一张
                var cards = await Game.GetSelectMenuCards(Card.PlayerIndex, list, 1);

                if (cards.Count() == 0) { return 0; }

                //打出
                _needKill = true;
                _target = cards.Single();
                await _target.MoveToCardStayFirst();
                return 1;
            }
            else if (switchCard == 1)
            {
                Card.CardPoint();
                var list = Game.PlayersDeck[Card.PlayerIndex].Where(x => (x.CardPoint() > Card.CardPoint()) && x.Is(Group.Copper)).Mess(Game.RNG).ToList();
                if (list.Count() == 0) { return 0; }

                //让玩家选择一张卡
                var result = await Game.GetSelectMenuCards(Card.PlayerIndex, list, 1);
                //如果玩家一张卡都没选择,没有效果
                if (result.Count() == 0) { return 0; }

                await Card.Effect.Consume(result.Single());
                return 0;
            }
            await Game.Debug("end");
            return 0;
        }
        public async Task HandleEvent(AfterTurnOver @event)
        {
            await Game.Debug("turn");
            if (_needKill && Card.Status.CardRow.IsOnPlace())
            {
                if (_target.Status.CardRow.IsOnPlace())
                {
                    await _target.Effect.ToCemetery(CardBreakEffectType.Scorch);
                }
                _needKill = false;
            }
        }
    }
}