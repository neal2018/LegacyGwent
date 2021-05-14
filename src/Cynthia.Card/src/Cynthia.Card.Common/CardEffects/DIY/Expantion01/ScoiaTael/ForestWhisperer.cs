using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;


namespace Cynthia.Card
{
    [CardEffectId("70082")]//林语者 ForestWhisperer
    public class ForestWhisperer : CardEffect
    {//对1个敌军单位造成等同于同排“树精”单位数量的伤害，并将其上移一排。
        public ForestWhisperer(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            //计算己方同排的单位数量
            var count = Game.RowToList(Card.PlayerIndex, Card.Status.CardRow).IgnoreConcealAndDead().Where(x => x.Status.HasAllCategorie(Categorie.Dryad)).Count();
            var selectList = await Game.GetSelectPlaceCards(Card, selectMode: SelectModeType.AllRow);
            if (!selectList.TrySingle(out var target))
            {
                return 0;
            }
            var row = (target.Status.CardRow.MyRowToIndex() + 1).IndexToMyRow();
            await target.Effect.Damage(count, Card);
            if (!row.IsOnPlace())
            {
                return 0;
            }
            await target.Effect.Move(new CardLocation(row, int.MaxValue), Card);
            return 0;
        }
    }
}